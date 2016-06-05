//********************************************************************************************
//Author: Sergey Stoyan
//        sergey.stoyan@gmail.com
//        http://www.cliversoft.com
//        26 November 2014
//Copyright: (C) 2014, Sergey Stoyan
//********************************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Cliver.BotGui
{
    public partial class CustomConfigControl : Cliver.BotGui.ConfigControl
    {
        new public const string NAME = "Custom";

        public CustomConfigControl()
        {
            InitializeComponent();
            Init(NAME);
        }
        
        override protected void set_tool_tip()
        {
            //toolTip1.SetToolTip(this.AlertFactor, "Timeout for waiting IE completed downloading page event.");
            //toolTip1.SetToolTip(this.CloseWebBrowserDialogsAutomatically, "When checked the bot will close IE dialog boxes automatically.");
            //toolTip1.SetToolTip(this.SuppressScriptErrors, "When checked, IE will not throw error messages as of script errors.");
        }

        private void _0_bViewInputFile_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(UsersFile.Text);
            }
            catch (Exception ex)
            {
                LogMessage.Error(ex);
            }
        }

        private void ChooseInputFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.InitialDirectory = PathRoutines.GetDirFromPath(UsersFile.Text);
            d.ShowDialog();
            if (!string.IsNullOrWhiteSpace(d.FileName))
                UsersFile.Text = d.FileName;
        }

        private void UsersFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}

