using System;
using FTD2XX_NET; // Подключение библиотеки FTD2XX_NET (файл FTD2XX_NET.dll) 

namespace FTDI_LABs
{
    class Program
    {
        static void Main(string[] args)
        {
            FTDI ftdiDev0 = new FTDI(); // Создание объекта класаа FTDI.
            FTDI.FT_STATUS fT_STATUS = FTDI.FT_STATUS.FT_OK;

            byte[] data = new byte[1] { 0 }; // Создание массива для передачи его данных данных в FT232H
            UInt32 numBytesWritten = 0; // Число записанных байт?

           UInt32 devID = 0; // Переменная для хранения ID подключенной микросхемы

            string devDes; // Дескриптор устройсва (название)

            uint devNum = 0; // Количество подключенных устройсв

            Console.WriteLine("Введите коимбинацию 00, 01, 10 или 11");

            // Консольный ввод и запись полученных данных для отправку в порт FT232H
            switch(Console.ReadLine())
            {
                case "00":
                    data[0] = 0b00000000;
                    break;
                case "01":
                    data[0] = 0b01000000;
                    break;
                case "10":
                    data[0] = 0b10000000;
                    break;
                case "11":
                    data[0] = 0b11000000;
                    break;

                default:
                    data[0] = 0b00000000;
                    break;
            }
            Console.WriteLine(" ");

            ftdiDev0.GetNumberOfDevices(ref devNum); // Получение количества обнаруженных устройств FTDI

            Console.WriteLine($"Количество обнаруженных USB устройств FTDI: {devNum} \n");

            if (devNum > 0)
            {
                FTDI.FT_DEVICE_INFO_NODE[] devList = new FTDI.FT_DEVICE_INFO_NODE[devNum]; // Создание листов для хранения информации о подключенных к ПК FTDI

                fT_STATUS = ftdiDev0.OpenByIndex(0); // Метод открывает одно из подключенных устройств по его названию

                fT_STATUS = ftdiDev0.GetDeviceList(devList); // Заполнение нескольких списков инормацией о устрасвах


                Console.WriteLine("Информация о устройстве: ");

                

                //FTDI.FT232H_EEPROM_STRUCTURE ee = new FTDI.FT232H_EEPROM_STRUCTURE();

                
                
                //ee.SerNumEnable = true;

                //ee.SerialNumber = "FTDI_12345679";

                

                //ftdiDev0.WriteFT232HEEPROM(ee);

                


                //ftdiDev0.WriteFT232HEEPROM()




                ftdiDev0.GetSerialNumber(out devList[0].SerialNumber);
                Console.WriteLine($"Серийный номер устройсва: {devList[0].SerialNumber}");
                

                ftdiDev0.GetDeviceID(ref devList[0].ID); // 67330068
                Console.WriteLine($"ID : {devList[0].ID}");


                ftdiDev0.GetDeviceType(ref devList[0].Type); // FT_DEVICE_232H
                Console.WriteLine($"Тип устройсва : {devList[0].Type}");

                ftdiDev0.GetDescription(out devList[0].Description); // Single RS232-HS
                Console.WriteLine($"Description : {devList[0].Description}");

                Console.WriteLine(" ");

            }


            

            if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
            {
                Console.WriteLine("Устройство с индексом 0 открыта успешно");
            }
            else
            {
                Console.WriteLine($"Не удалось открыть устройство, статус: {fT_STATUS}");
            }
   
            // Установка режима работы микросхемы
            fT_STATUS = ftdiDev0.SetBitMode(0xFF, FTDI.FT_BIT_MODES.FT_BIT_MODE_SYNC_BITBANG); // Маска режима работы порта?, режим работы моста (синхронный bitbang). FT_BIT_MODE_SYNC_FIFO пока не работает

            // Запись данных в микросхему и вывод их в порт
            fT_STATUS = ftdiDev0.Write(data, data.Length, ref numBytesWritten);



        }
    }
}
