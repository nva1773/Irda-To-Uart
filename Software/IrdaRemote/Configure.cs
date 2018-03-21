using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrdaRemote
{
    public partial class Configure : Form
    {
        private IrdaPort ComPort;
        private const string strDeviceOK = "Device OK";

        // Consturctor
        public Configure()
        {
            InitializeComponent();
            IrdaPortInit();
        }

        // Initialozation
        private void IrdaPortInit()
        {
            // ������� ��������� ����� ������������������� � Program
            ComPort = Program.IrdaComPort;
            // ���� � Program ������� ������, �� ��������� ���
            if (Program.isTimerEnable) Program.isTimerEnable = false;
            // ��������� ����� ������������� �� �����
            foreach (string s in ComPort.PortList)
                cbPortName.Items.Add(s);
            // ���� ����� ���� ����
            if (cbPortName.Items.Count != 0)
            {
                if (Program.isAppFirstStart)
                {
                    // �������������� ������ � ������, ��� ���� ���������� ������� cbPortName_SelectedIndexChanged
                    cbPortName.SelectedIndex = 0;
                    btPortOpenClose.Enabled = true;
                    Program.isAppFirstStart = false;
                }
                else
                {
                    // ���� � �������� ������ ���, ��� ��������������� ��� �������
                    // � ���� �� �����, �������������� ��� � ������ ���
                    string strName = Program.PortName;
                    int i = cbPortName.FindString(strName, 0);
                    if (i != -1)
                        cbPortName.SelectedIndex = i;                    
                    else
                        cbPortName.SelectedIndex = 0;
                    // ���� ���� ������, �� ��������� ������ � ������ �������� ���������
                    if (ComPort.isOpen)
                    {
                        tmrConfigure.Enabled = true;
                        btPortOpenClose.Text = "Close";
                        btPortOpenClose.Enabled = true;
                        gbPortName.Enabled = false;
                    }
                    else btPortOpenClose.Enabled = true;
                }
            }
            else
            {
                // �� ������� �� ���� ����� - ...
                StatusLabel.Text = ComPort.Status;
                StatusLabel.ForeColor = Color.Red;
                gbPortName.Enabled = false;
            }

            // ��������� �������
            int index = 0;
            foreach (string s in Program.IrdaCommand)
            {
                gbCommandList.Controls[index].Text = s;
                index++;
            }
        }

        #region Form Events
        // ComboBox Selected
        private void cbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ComPort.isOpen)
            {
                ComPort.Name = cbPortName.Text.ToString();
                Program.PortName = ComPort.Name;
            }
            StatusLabel.ForeColor = Color.Black;
            StatusLabel.Text = "Select " + ComPort.Name;
        }

        // Button Open/Close Click
        private void btPortOpenClose_MouseClick(object sender, MouseEventArgs e)
        {
            if (ComPort.isOpen)
            { 
                tmrConfigure.Enabled = false;
                tbReceive.Clear();
                ComPort.Close();
                btPortOpenClose.Text = "Open";
                gbPortName.Enabled = true;
            }
            else 
            { 
                ComPort.Open();
                if (ComPort.isOpen)
                {
                    tmrConfigure.Enabled = true;
                    btPortOpenClose.Text = "Close";
                    gbPortName.Enabled = false;
                }
                else
                {
                    StatusLabel.ForeColor = Color.Red;
                }
            }
            StatusLabel.Text = "Port " + ComPort.Name + " " + ComPort.Status;
        }

        // Button GroupBox SetCommand
        private void btSetCommand_MouseClick(object sender, MouseEventArgs e)
        {
            Button bt = (Button)sender;
            int index = bt.TabIndex - 10;
            string str = tbReceive.Text;
            if (ComPort.isOpen)
            {
                if (str != null && str != "" && str != ComPort.Empty && str != strDeviceOK)
                {
                    gbCommandList.Controls[index].Text = str;
                    Program.StorePortData(index, str);
                }
            }
        }

        // Button GroupBox ClearCommand
        private void btClearCommand_MouseClick(object sender, MouseEventArgs e)
        {
            Button bt = (Button)sender;
            int index = bt.TabIndex - 30;
            string str = ComPort.Empty;
            gbCommandList.Controls[index].Text = str;
            Program.StorePortData(index, str);
        }

        // Changed Text into tbReceive
        private void tbReceive_TextChanged(object sender, EventArgs e)
        {
            string str = tbReceive.Text;
            int count = gbCommandList.Controls.Count;
            if (str == ComPort.Empty) return;
            // Find New comand to gbCommandList
            for (int i = 0; i < count; i++)
            {
                if (gbCommandList.Controls[i].Text == str)
                    gbCommandList.Controls[i].BackColor = Color.LightGreen;
                else
                    gbCommandList.Controls[i].BackColor = Color.White;
            }
        }

        // Timer ReadPort Tick
        private void tmrConfigure_Tick(object sender, EventArgs e)
        {
            string strData = ComPort.Data;
            if (strData != ComPort.Empty) LogDataReceive(strData);
        }

        // Event Key Down
        void Configure_KeyDown(object sender, KeyEventArgs e)
        {
            // ���� ������ ������ "Enter"
            //if (e.KeyCode == Keys.Return) this.Close();
            // ���� ������ ������ "Esc"
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        // Form Closed
        private void Configure_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Messate for Program
            if (ComPort.isOpen) Program.isTimerEnable = true;
            Program.SetMessage = StatusLabel.Text;
            // Dispose tmrConfigure
            tmrConfigure.Enabled = false;
            tmrConfigure.Dispose();
        }
        #endregion

        #region Log Data
        //Log receive data to the terminal window.
        private void LogDataReceive(string str)
        {
            if (str == ComPort.DeviceOK) str = strDeviceOK;
            tbReceive.Invoke(new EventHandler(delegate
            {
                tbReceive.Clear();
                tbReceive.AppendText(str);
            }));
        }
        #endregion
        //end
    }
}