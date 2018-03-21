using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports; // Класс работы с портом
using System.Collections; //Класс работы с ArrayList

namespace IrdaRemote
{
    class IrdaPort
    {
        //
        const string strEmpty = "NO DATA";
        const string strDeviceOK = "4F 4B 4F 4B ";//"OKOK"
        // Variable
        private static System.IO.Ports.SerialPort ComPort;
        private int CountByte;
        private byte[] BufferPort;
        private long CurrentTime, PastTime;
        private string strDataRecive = null;
        private string strPortStatus = null;
        //
        public ArrayList PortList = null;

        // Constructor
        public IrdaPort()
        {
            ComPort = new SerialPort();
            ComPort.BaudRate = 9600;
            ComPort.DataBits = 8;
            ComPort.Parity = Parity.None;
            ComPort.StopBits = StopBits.One;
            ComPort.Handshake = Handshake.XOnXOff;
            ComPort.RtsEnable = true;
            ComPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
            CountByte = 0;
            //
            BufferPort = new byte[250];
            strDataRecive = strEmpty;
            strPortStatus = "OK";
            //
            PortList = new ArrayList();
            PortList.Clear();
            // Find Ports
            foreach (string s in SerialPort.GetPortNames())
                PortList.Add(s);
            if (PortList.Count <= 0)
                strPortStatus = "Unable to find Com-port!";
        }

        // Properties Name
        public string Name
        {
            set { ComPort.PortName = value; }
            get { return ComPort.PortName; }
        }

        // Properties isOpen
        public bool isOpen
        {
            get { return ComPort.IsOpen; }
        }

        // Properties Data Recive
        public string Data
        {
            get 
            { 
                string str = strDataRecive.ToUpper();
                if (str != strEmpty) strDataRecive = strEmpty;
                return str;
            }
        }

        // Properties Status
        public string Status
        {
            get { return strPortStatus; }
        }

        // Properties Empty
        public string Empty
        {
            get { return strEmpty; }
        }

        public string DeviceOK
        {
            get { return strDeviceOK; }
        }

        // Metode Open
        public void Open()
        {
            // получаем доступ к порту
            try
            {
                if (!ComPort.IsOpen)
                {
                    ComPort.RtsEnable = true;
                    CountByte = 0;
                    ComPort.Open();
                    strPortStatus = "open.";
                }
            }
            catch (Exception)
            {
                strPortStatus = "unable to open!";//Exception exc.Message.ToString();
            }
        }

        public void Close()
        {
            if (ComPort.IsOpen)
            {
                ComPort.RtsEnable = false; 
                ComPort.Close();
                strDataRecive = strEmpty;
                strPortStatus = "close.";
            }
        }

        // Event Data Receive
        private void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            bool isEndReceive = false;
            // Obtain the number of bytes waiting in the port's buffer
            int bytes = ComPort.BytesToRead;
            
            // Create a byte array buffer to hold the incoming data
            byte[] buffer = new byte[bytes];
            
            // Read the data from the port and store it in our buffer
            ComPort.Read(buffer, 0, bytes);
            if (bytes > 250) return;
            
            // Character Timeout 50 ms
            CurrentTime = GetTimeInMilliseconds();
            if (CountByte == 0)
            {
                PastTime = CurrentTime;
            }
            else
            {
                if (CurrentTime - PastTime > 50)
                {
                    CountByte = 0;
                }
            }

            // копируем принятые данные в BufferPort
            for (int i = 0; i < bytes; i++) { BufferPort[CountByte + i] = buffer[i]; }
            CountByte += bytes;
            if (CountByte >= 4) isEndReceive = true;

            // if have finished receive
            if (isEndReceive)
            {
                // создаем строку sbReceive длиной CountByte*3
                StringBuilder sbReceive = new StringBuilder(CountByte * 3);
                // выводим принятые данные в строку sbReceive
                for (int i = 0; i < CountByte; i++) 
                { sbReceive.Append(Convert.ToString(BufferPort[i], 16).PadLeft(2, '0').PadRight(3, ' ')); }
                // сохроняем принятую строку 
                strDataRecive = sbReceive.ToString();                
                CountByte = 0;
            }
        }

        private long GetTimeInMilliseconds()
        {
            DateTime time = DateTime.Now;
            return (((time.Hour * 60 + time.Minute) * 60 + time.Second) * 1000 + time.Millisecond);
        }
    }
}
