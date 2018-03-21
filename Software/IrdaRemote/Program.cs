using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32; // Класс работы с реестром
using System.Timers;   // Класс работы с таймером
using System.Collections; //Класс работы с ArrayList
using System.Diagnostics; // Класс работы с процессами
using System.Runtime.InteropServices;

namespace IrdaRemote
{
    static class Program
    {
        // struct
        private struct RegKeyName { public string PortName; public string ShowMessage; public string[] CmdName; }
        // private
        private static Form Configure = null;
        private static Form About = null;
        private static NotifyIcon IrdaNotifyIcon = null;
        private static ContextMenuStrip IrdaContextMenu = null;
        private static ToolStripMenuItem IrdaMenuAbout = null;
        private static ToolStripSeparator IrdaSeparator1 = null;
        private static ToolStripMenuItem IrdaMenuConfigure = null;
        private static ToolStripMenuItem IrdaMenuShowMsg = null;
        private static ToolStripSeparator IrdaSeparator2 = null;
        private static ToolStripMenuItem IrdaMenuExit = null;
        private static RegistryKey IrdaRegKey;
        private static RegKeyName RegName;
        private static System.Timers.Timer tmrProgram;
        private static string strPortName = null;
        private static string strTipText = null;
        private static int iShowMessage;
        // public
        public static IrdaPort IrdaComPort;
        public static ArrayList IrdaCommand;
        public static bool isAppFirstStart;
        // constant
        private static string[] strKeyCode = 
        { "{ENTER}", "{ESC}", " ", "{UP}", "{DOWN}", "{LEFT}", "{RIGHT}", "{PGUP}", "{PGDN}" };
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Get all instances of IrdaRemote running on the local computer.
            // Ищем себя и если уже запущена хоть одна копия, то выход,
            Process currentProc = Process.GetCurrentProcess();
            string nameProc = currentProc.ProcessName;
            Process[] localByName = Process.GetProcessesByName(nameProc);
            if (localByName.Length > 1) return;
            
            // Иначе инициализируем компоненты программы
            InitProgram();

            // Если запускаемся первый раз, т.е. записи в реестре не обнаружены
            if (isAppFirstStart)
            {
                ShowBalloonTip(strTipText);
                Configure = new Configure();
                Configure.ShowInTaskbar = false;
                Configure.Show();
                Configure.WindowState = FormWindowState.Normal;
            }
            else
            {
                // если не найден ни один порт, то просто выводим сообщение
                if (IrdaComPort.PortList.Count <= 0)
                {
                    strTipText = IrdaComPort.Status;
                }
                // иначе пытаемся запустит выбранный порт, если он не запущен
                else
                {
                    if (!IrdaComPort.isOpen)
                    {
                        IrdaComPort.Open();
                        if (IrdaComPort.isOpen) tmrProgram.Enabled = true;
                        strTipText = "Port " + IrdaComPort.Name + " " + IrdaComPort.Status;
                    }
                }
                if (iShowMessage != 0) ShowBalloonTip(strTipText);
            }

            // Run Application
            Application.Run();
        }

        #region Initialization
        // Initialization programm component
        private static void InitProgram()
        {
            // ComPort
            IrdaComPort = new IrdaPort();
            //RegName
            RegName = new RegKeyName();
            RegName.PortName = "PortName";
            RegName.ShowMessage = "ShowMessage";
            RegName.CmdName = new string[]
            {"CmdEnter","CmdEsc","CmdSpace","CmdUp","CmdDown","CmdLeft","CmdRight","CmdPageUp","CmdPageDown"};
            // IrdaCommand
            IrdaCommand = new ArrayList();
            IrdaCommand.Clear();
            // Read RegKey
            ReadRegKeyValue();
            // NotifyIcon, ContextMenuStript and ToolStripMenuItem
            IrdaNotifyIcon = new NotifyIcon();
            IrdaContextMenu = new ContextMenuStrip();
            IrdaMenuAbout = new ToolStripMenuItem();
            IrdaSeparator1 = new ToolStripSeparator();
            IrdaMenuConfigure = new ToolStripMenuItem();
            IrdaMenuShowMsg = new ToolStripMenuItem();
            IrdaSeparator2 = new ToolStripSeparator();
            IrdaMenuExit = new ToolStripMenuItem();
            // 
            IrdaNotifyIcon.ContextMenuStrip = IrdaContextMenu;
            IrdaNotifyIcon.Icon = Properties.Resources.Irda_Remote_32px;
            IrdaNotifyIcon.Text = "Irda Remote Control";
            //
            IrdaContextMenu.Items.AddRange(new ToolStripItem[] {
            IrdaMenuAbout,
            IrdaSeparator1,
            IrdaMenuConfigure,
            IrdaMenuShowMsg,
            IrdaSeparator2,
            IrdaMenuExit});
            IrdaContextMenu.Name = "IrdaContextMenu";
            IrdaContextMenu.Size = new System.Drawing.Size(153, 98);
            //
            IrdaMenuAbout.Name = "About";
            IrdaMenuAbout.Size = new System.Drawing.Size(152, 22);
            IrdaMenuAbout.Text = "About";
            IrdaMenuAbout.Click += new System.EventHandler(ShowAboutForm);
            //
            IrdaSeparator1.Name = "Separator1";
            IrdaSeparator1.Size = new System.Drawing.Size(149, 6);
            //
            IrdaMenuConfigure.Name = "Configure";
            IrdaMenuConfigure.Size = new System.Drawing.Size(152, 22);
            IrdaMenuConfigure.Text = "Configure...";
            IrdaMenuConfigure.Click += new System.EventHandler(ShowConfigure);
            // 
            IrdaMenuShowMsg.Name = "ShowMsg";
            IrdaMenuShowMsg.Size = new System.Drawing.Size(152, 22);
            IrdaMenuShowMsg.Text = "Show message";
            if (iShowMessage != 0) IrdaMenuShowMsg.ForeColor = System.Drawing.Color.Blue;
            IrdaMenuShowMsg.Click += new System.EventHandler(ShowMessage);
            // 
            IrdaSeparator2.Name = "Separator2";
            IrdaSeparator2.Size = new System.Drawing.Size(149, 6);
            //
            IrdaMenuExit.Name = "Exit";
            IrdaMenuExit.Size = new System.Drawing.Size(152, 22);
            IrdaMenuExit.Text = "Exit";
            IrdaMenuExit.Click += new System.EventHandler(ExitApplication);
            //
            IrdaNotifyIcon.Visible = true;
            // Create a timer with a 100 ms interval.
            tmrProgram = new System.Timers.Timer();
            tmrProgram.Interval = 100;
            tmrProgram.Enabled = false;
            tmrProgram.Elapsed += new ElapsedEventHandler(tmrProgram_Tick);
        }

        // Read Value into RegKey
        private static void ReadRegKeyValue()
        {
            strPortName = "Com1";
            iShowMessage = 0;
            strTipText = "The program is launched for the first time.\rMake a configuration program.";
            IrdaRegKey = Registry.CurrentUser;
            IrdaRegKey = IrdaRegKey.CreateSubKey("Software\\IrdaRemote");
            // Check on first start
            if (Registry.GetValue(IrdaRegKey.ToString(), RegName.PortName, null) == null)
            {
                //RegKey Not Exist - First Start
                IrdaRegKey.SetValue(RegName.PortName, strPortName);
                //
                IrdaRegKey.SetValue(RegName.ShowMessage, iShowMessage);
                //
                foreach (string str in RegName.CmdName)
                {
                    IrdaRegKey.SetValue(str, IrdaComPort.Empty);
                    IrdaCommand.Add(IrdaComPort.Empty);
                }
                isAppFirstStart = true;
            }
            else
            {
                //RegKey Exist
                strPortName = IrdaRegKey.GetValue(RegName.PortName).ToString();
                //
                if (Registry.GetValue(IrdaRegKey.ToString(), RegName.ShowMessage, null) == null)
                    IrdaRegKey.SetValue(RegName.ShowMessage, iShowMessage);
                else
                    iShowMessage = (int)IrdaRegKey.GetValue(RegName.ShowMessage);
                //
                foreach (string str in RegName.CmdName)
                {
                    if (Registry.GetValue(IrdaRegKey.ToString(), str, null) == null)
                    {
                        IrdaRegKey.SetValue(str, IrdaComPort.Empty);
                        IrdaCommand.Add(IrdaComPort.Empty);
                    }
                    else
                        IrdaCommand.Add(IrdaRegKey.GetValue(str).ToString());
                }
                isAppFirstStart = false;
            }
            // ComPort Name
            IrdaComPort.Name = strPortName;
        }

        // Open ComPort
        private static void ComPortOpen()
        {
            
        }
        #endregion

        #region Public metode
        // Get and Set PortName
        public static string PortName
        {
            set { strPortName = value; IrdaRegKey.SetValue(RegName.PortName, value); }
            get { return strPortName; }
        }

        // Write Value to RegKey and IrdaCommand
        public static void StorePortData(int index, string str)
        {
            IrdaRegKey.SetValue(RegName.CmdName[index], str);
            IrdaCommand[index] = str;
        }

        // Set or Get Enable tmrProgram 
        public static bool isTimerEnable
        {
            set { tmrProgram.Enabled = value; }
            get { return tmrProgram.Enabled; }
        }

        // Set strTipText
        public static string SetMessage
        {
            set 
            { 
                strTipText = value; 
                if (iShowMessage != 0) ShowBalloonTip(strTipText); 
            }
        }

        #endregion

        #region NotyfiIcon and ContextMenu
        //
        private static void ShowAboutForm(object sender, EventArgs e)
        {
            if (About == null || About.IsDisposed) About = new About();
            if (About.Visible == false)
            {
                About.ShowInTaskbar = false;
                About.Show();
                About.WindowState = FormWindowState.Normal;
            }
        }

        // NotyfiIcon BalloonTip
        private static void ShowBalloonTip(string text)
        {
            IrdaNotifyIcon.BalloonTipTitle = "Irda Remote Control";
            IrdaNotifyIcon.BalloonTipText = text;
            IrdaNotifyIcon.ShowBalloonTip(1000);
        }
        
        // Click "Configure" on ContextMenu
        private static void ShowConfigure(object sender, EventArgs e)
        {
            if (Configure == null || Configure.IsDisposed) Configure = new Configure();
            if (Configure.Visible == false)
            {
                Configure.ShowInTaskbar = false;
                Configure.Show();
                Configure.WindowState = FormWindowState.Normal;
            }
        }

        // Click "Show message" on ContextMenu
        private static void ShowMessage(object sender, EventArgs e)
        {
            if (iShowMessage == 1)
            {
                iShowMessage = 0;
                IrdaMenuShowMsg.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                iShowMessage = 1;
                IrdaMenuShowMsg.ForeColor = System.Drawing.Color.Blue;
                ShowBalloonTip(strTipText);
            }
            IrdaRegKey.SetValue(RegName.ShowMessage, iShowMessage);
        }

        // Click "Exit" on ContextMenu
        private static void ExitApplication(object sender, EventArgs e)
        {
            if (Configure != null) Configure.Dispose();
            //
            if (About != null) About.Dispose();
            //
            if (IrdaComPort.isOpen) IrdaComPort.Close();
            //
            tmrProgram.Enabled = false;
            tmrProgram.Dispose();
            //
            IrdaContextMenu.Dispose();
            IrdaNotifyIcon.Dispose();
            //
            Application.Exit();
        }
        #endregion

        #region tmrProgram_Tick event
        private static void tmrProgram_Tick(object source, ElapsedEventArgs e)
        {
            string strData = IrdaComPort.Data;

            if (strData != IrdaComPort.Empty)
            {
                if (strData == IrdaComPort.DeviceOK)
                {
                    strTipText = "The device is connected to the " + IrdaComPort.Name;
                }
                else
                {
                    strTipText = "Receive data = " + strData;
                    // find command to IrdaComand
                    int index = IrdaCommand.IndexOf(strData);
                    if (index >=0 && index < 9)
                    {
                        strTipText += "\r Command = " + RegName.CmdName[index];
                        SendKeys.SendWait(strKeyCode[index]);
                    }
                }
                //
                if (iShowMessage != 0) ShowBalloonTip(strTipText);
            }
        }
        #endregion
    }
}

