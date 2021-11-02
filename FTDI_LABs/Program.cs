using System;
using FTD2XX_NET; // Подключение библиотеки FTD2XX_NET (файл FTD2XX_NET.dll) 

namespace FTDI_LABs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            FTDI ftdiDev0 = new FTDI();
            FTDI.FT_STATUS fT_STATUS = FTDI.FT_STATUS.FT_OK;
            byte[] data = new byte[1] { 0 };
            UInt32 numBytesWritten = 0;

            data[0] = 0b11111110;

            fT_STATUS = ftdiDev0.OpenByIndex(0);
            //myFtdiDevice.SetBaudRate(11520);

            fT_STATUS = ftdiDev0.SetBitMode(0xFF, FTDI.FT_BIT_MODES.FT_BIT_MODE_ASYNC_BITBANG);

            fT_STATUS = ftdiDev0.Write(data, data.Length, ref numBytesWritten);


            if (fT_STATUS == 0)
            {
                Console.WriteLine("FTDI Status: Ok!");
            }

        }
    }
}
