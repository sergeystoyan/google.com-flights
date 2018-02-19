//********************************************************************************************
//Author: Sergey Stoyan, CliverSoft.com
//        http://cliversoft.com
//        stoyan@cliversoft.com
//        sergey.stoyan@gmail.com
//        27 February 2007
//Copyright: (C) 2007, Sergey Stoyan
//********************************************************************************************

using System;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Mail;
using Cliver;
using System.Configuration;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using Cliver.Bot;
using Cliver.BotGui;
using Cliver.BotWeb;
using Microsoft.Win32;
using System.Reflection;

namespace Cliver.BotCustomization
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Config.Initialize(@"^Cliver", new string[] { "Engine", "Input", "Output", "Web", "Log", "Browser" });
            
            //Cliver.Bot.Program.Run();//It is the entry when the app runs as a console app.
            Cliver.BotGui.Program.Run();//It is the entry when the app uses the default GUI.
        }
    }

    public class CustomBotThreadManagerForm : BotThreadManagerForm
    {
        override public Type GetBotThreadControlType()
        {
            return typeof(IeRoutineBotThreadControl);
        }
    }

    public class CustomConfigForm : ConfigForm
    {
        override public List<string> GetConfigControlSections()
        {
            return new List<string> { "Custom", "Engine", "Input", "Output", "Web", /*"Browser", "Spider", "Proxy",*/ "Log" };
        }
    }

    public class AboutFormForm : AboutForm
    {
        override public string GetAbout()
        {
            return @"WEB CRAWLER 12
Created: " + Cliver.Bot.Program.GetCustomizationCompiledTime().ToString() + @"
Developed by: www.cliversoft.com";
        }
    }

    public class CustomSession : Session
    {
        public CustomSession()
        {
            //InternetDateTime.CHECK_TEST_PERIOD_VALIDITY(2016, 6, 11);

            vs.Clear();

            if (File.Exists(last_prices_file))
                urls2old_price = Cliver.SerializationRoutines.Json.Deserialize<Dictionary<string, double>>(File.ReadAllText(last_prices_file));

            FileReader fr = new FileReader(Custom.Default.UsersFile, Cliver.Bot.Settings.Input.FileFormat);
            for (FileReader.Row r = fr.ReadLine(); r != null; r = fr.ReadLine())
                users2ui[r["User"]] = new UserInfo() { Mobile = r["Mobile"], SmsGateway = r["SmsGateway"] };
        }

        override public void __Closing()
        {
            //Session.This.IsUnprocessedInputItem
            File.WriteAllText(last_prices_file, Cliver.SerializationRoutines.Json.Serialize(urls2price));
            vs.Insert(0, DateTime.Now.ToShortTimeString());
            vs.Insert(0, DateTime.Now.ToShortDateString());
            FileWriter.This.PrepareAndWriteHtmlLine(vs.ToArray());
        }

        override public void __ErrorClosing(string message)
        {
            MailMessage mm = new MailMessage(Custom.Default.SenderEmail, Custom.Default.AdminEmail)
            {
                Subject = "CliverBot is broken",
                Body = "CliverBot is broken"
            };
            mm.To.Clear();
            mm.To.Add(Custom.Default.AdminMobile + "@" + Custom.Default.AdminSmsGateway);
            email(mm);

            mm = new MailMessage(Custom.Default.SenderEmail, Custom.Default.AdminEmail)
            {
                Subject = "CliverBot: FATAL ERROR",
                Body = "CliverBot stopped due to a fatal error: " + message + ". Details can be found in the logs. Support: sergey.stoyan@gmail.com"
            };
            mm.To.Clear();
            mm.To.Add(Custom.Default.AdminEmail);
            email(mm);
        }

        static readonly string last_prices_file = Log.WorkDir + "\\" + "last_prices.txt";
        static Dictionary<string, double> urls2old_price = null;
        static Dictionary<string, double> urls2price = new Dictionary<string, double>();
        static Dictionary<string, UserInfo> users2ui = new Dictionary<string, UserInfo>();
        public class UserInfo
        {
            public string SmsGateway;
            public string Mobile;
        }

        static List<string> vs = new List<string>();
        static DataSifter.Parser flights = new DataSifter.Parser("Flights.fltr");
        static DataSifter.Parser url = new DataSifter.Parser("Url.fltr");

        static void add_search_results(HtmlDocument hd, string url, out string route, out string price)
        {
            lock (vs)
            {
                route = "";
                price = "";
                try
                {
                    string f = hd != null ? hd.GetElementsByTagName("HTML")[0].OuterHtml : "";
                    DataSifter.Capture c = flights.Parse(f);
                    DataSifter.Capture fc = c.FirstOf("Flight");
                    if (fc == null)
                        throw new ProcessorException(ProcessorExceptionType.ERROR, "Could not get flights.Parse");
                    price = Regex.Replace(FieldPreparation.Html.Normalize(fc.FirstValueOf("Price")), @"[\s,]", "");
                    DataSifter.Capture cu = CustomSession.url.Parse(url);
                    route = FieldPreparation.Html.Normalize(cu.FirstValueOf("Route"));
                    if (string.IsNullOrWhiteSpace(route))
                        route = cu.FirstValueOf("Route1") + "-" + cu.FirstValueOf("Route2");
                    route = FieldPreparation.Html.Normalize(cu.FirstValueOf("A") + "-" + route);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
                vs.Add(route);
                vs.Add(price);
            }
        }

        public class CustomBotCycle : BotCycle
        {
            override public void __Starting()
            {
                irbtc = (IeRoutineBotThreadControl)BotThreadControl.GetInstanceForThisThread();
                IR = new IeRoutine(irbtc.Browser);
                IR.UseCache = false;
            }

            override public void __Exiting()
            {
                irbtc.Invoke(() =>
                {
                    irbtc.Browser.Stop();
                    irbtc.Controls.Remove(irbtc.Browser);
                    irbtc.Browser.Dispose();
                });
            }
            IeRoutineBotThreadControl irbtc;

            IeRoutine IR;

            public class FlightSearchItem : InputItem
            {
                readonly public string Url;
                readonly public string Users;
                readonly public double AlertFactor;

#if DEBUG
                static int c = 0;
#endif
                override public void __Processor(BotCycle bc)
                {
#if DEBUG
                    if (c++ > 2)
                    {
                        Log.Main.Warning("Debug version processes only 3 urls from the input list!");
                        return;
                    }
#endif
                    //Session.FatalErrorClose("");
                    //throw new ProcessorException(ProcessorExceptionType.ERROR, "test");
                    //throw new Session.FatalException("test");
                    //CustomSession session = (CustomSession)bc.Session;
                    CustomBotCycle cbc = (CustomBotCycle)bc;

                    string route;
                    string price;

                    if (!cbc.IR.GetDoc(Url))
                    {
                        add_search_results(null, Url, out route, out price);
                        throw new ProcessorException(ProcessorExceptionType.RESTORE_AS_NEW, "Could not get: " + Url);
                    }

                    SleepRoutines.Wait(20000);
                    add_search_results(cbc.IR.HtmlDoc, Url, out route, out price);

                    string s2 = Regex.Replace(price, @"[^\d\.]", "", RegexOptions.IgnoreCase);
                    double p2 = double.Parse(s2);
                    urls2price[Url] = p2;

                    if (urls2old_price == null)
                        return;

                    double p;
                    if (!urls2old_price.TryGetValue(Url, out p))
                        return;

                    if (AlertFactor * p < p2)
                        return;

                    List<MailMessage> mms = new List<MailMessage>();
                    mms.Add(
                        new MailMessage(Custom.Default.SenderEmail, Custom.Default.SenderEmail)
                        {
                            Subject = "Flight Alert: " + route + " " + price,
                            Body = route + " " + price
                        }
                    );

                    foreach (string u in Users.Split(';'))
                    {
                        UserInfo ui;
                        if (!users2ui.TryGetValue(u, out ui))
                            throw new Session.FatalException("User " + u + " is not defined");
                        mms.Add(
                            new MailMessage(Custom.Default.SenderEmail, ui.Mobile + "@" + ui.SmsGateway)
                            {
                                Subject = route + " " + price,
                                Body = route + " " + price
                            }
                        );
                    }

                    email(mms);
                }

                void email(List<MailMessage> mms)
                {
                    if (mms.Count < 1)
                        return;
                    foreach (MailMessage mm in mms)
                       CustomSession.email(mm);
                }
            }
        }

        static void email(MailMessage mm)
        {
            using (
                SmtpClient sc = new SmtpClient
                {
                    Host = Custom.Default.SmtpHost,
                    Port = Custom.Default.SmtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Custom.Default.SenderEmail, Custom.Default.SmtpPassword)
                }
            )
                try
                {
                    mm.From = new MailAddress(Custom.Default.SenderEmail);
                    Log.Write("Emailing to " + mm.To + ": " + mm.Subject);
#if !DEBUG
                    sc.Send(mm);
#endif
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    Log.Write("Host: " + sc.Host
                        + "\r\nPort: " + sc.Port
                        + "\r\nEnableSsl: " + sc.EnableSsl
                        + "\r\nDeliveryMethod: " + sc.DeliveryMethod
                        + "\r\nUserName: " + Custom.Default.SenderEmail
                        + "\r\nSmtpPassword: " + Custom.Default.SmtpPassword
                        + "\r\nFrom: " + mm.From.Address
                        + "\r\nTo: " + mm.To);
                }
        }
    }
}