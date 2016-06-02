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
//using mshtml;
using Cliver;
using System.Configuration;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using Cliver.Bot;
using Cliver.BotGui;
using Microsoft.Win32;
using System.Reflection;

namespace Cliver.BotCustomization
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            //Cliver.CrawlerHost.Linker.ResolveAssembly();
            main();
        }

        static void main()
        {
            //Cliver.Bot.Program.Run();//It is the entry when the app runs as a console app.
            Cliver.BotGui.Program.Run();//It is the entry when the app uses the default GUI.
        }      
    }

    public class CustomBotGui : Cliver.BotGui.BotGui
    {
        override public string[] GetConfigControlNames()
        {
            return new string[] { "General", "Input", "Output", "Web", /*"Browser", "Spider",*/ "Proxy", "Log" };
        }

        override public Cliver.BaseForm GetToolsForm()
        {
            return null;
        }

        override public Type GetBotThreadControlType()
        {
            return typeof(IeRoutineBotThreadControl);
            //return typeof(WebRoutineBotThreadControl);
        }
    }

    public class CustomBot : Cliver.Bot.Bot
    {
        new static public string GetAbout()
        {
            return @"WEB CRAWLER
Created: " + Cliver.Bot.Program.GetCustomizationCompiledTime().ToString() + @"
Developed by: www.cliversoft.com";
        }

        new static public void SessionCreating()
        {
            InternetDateTime.CHECK_TEST_PERIOD_VALIDITY(2016, 5, 30);
        }

        override public void CycleBeginning()
        {
            //IR = new IeRoutine(((IeRoutineBotThreadControl)BotThreadControl.GetInstanceForThisThread()).Browser);
            //IR.UseCache = false;
        }
        
        HttpRoutine HR = new HttpRoutine();
        //IeRoutine IR;

        public class FlightSearchItem : InputItem
        {
            readonly public string Url;
            internal string Origin;
            internal string Destination;
            internal string Date;

            public FlightSearchItem(string url)
            {
                Url = url;
            }

            //public FlightSearchItem(string origin, string destination, string date)
            //{
            //    Origin = origin;
            //    Destination = destination;
            //    Date = date;
            //}

            override public void PROCESSOR(BotCycle bc)
            {
                CustomBot cb = (CustomBot)bc.Bot;

                string post_data = @"
{
  request: {
    slice: [
      {
        origin: '" + Origin + @"',
        destination: '" + Destination + @"',
        date: '" + Date + @"'
      }
    ],
    passengers: {
      adultCount: 1,
      infantInLapCount: 0,
      infantInSeatCount: 0,
      childCount: 0,
      seniorCount: 0
    },
    solutions: 20,
    refundable: false
  }
}";
                string key = "AIzaSyDEnhODeLtcJBSWSwQkKrGIgMq1rSmUkIU";
                string url = "https://www.googleapis.com/qpxExpress/v1/trips/search?key=" + key;
                if (!cb.HR.Post(url, Encoding.ASCII.GetBytes(post_data), "application/json"))
                //if (!cb.HR.Post("https://qpx-express-demo.itasoftware.com/xhr", Encoding.ASCII.GetBytes(post_data), "application/json"))
                //if (!cb.IR.GetDoc(Url))
                    throw new ProcessorException(ProcessorExceptionType.RESTORE_AS_NEW, "Could not get: " + url);

                ////DataSifter.Capture gc = cb.flights.Parse(cb.HR.HtmlResult);
                ////DataSifter.Capture gc = cb.flights.Parse(cb.IR.HtmlResult);
                ////IeRoutines.GetHtmlElementsByAttr(
                //SleepRoutines.Wait(1000);
                //IEnumerable<HtmlElement> hes = cb.IR.GetHtmlElementsByAttr("class", "MHNSJI-wb-h");
                //foreach(HtmlElement he in hes)
                //{

                //}
                //List<string> ss = new List<string>();
                //foreach (HtmlElement he in cb.IR.HtmlDoc.All)
                //{
                //    if (he.OuterHtml.Contains("MHNSJI-wb-h"))
                //        ss.Add(he.OuterHtml);
                //}
                //string f = cb.IR.HtmlDoc.GetElementsByTagName("HTML")[0].OuterHtml;
                
                //SleepRoutines.Wait(1000000);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                object g = serializer.Deserialize<dynamic>(cb.HR.HtmlResult);
            }
        }
    }
}