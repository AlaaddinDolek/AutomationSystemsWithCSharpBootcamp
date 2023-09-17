using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimePayment
{
    internal class Program
    {
        private static DateTime GetBerlinTime()
        {
            TimeZoneInfo berlinTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTime berlinNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, berlinTime);

            return berlinNow;
        }

        static void Main(string[] args)
        {

            decimal overtimePay = 50;
            int overtimeHours;
            DateTime shiftStartDate, shiftEndDate;

            Console.Write("Mesai başlangıç saati: ");
            int shiftStart = Convert.ToInt32(Console.ReadLine());

            Console.Write("Mesai bitiş saati: ");
            int shiftEnd = Convert.ToInt32(Console.ReadLine());

            DateTime berlinTime = GetBerlinTime();

            shiftStartDate = new DateTime(berlinTime.Year, berlinTime.Month, berlinTime.Day, shiftStart, 0, 0);
            shiftEndDate = new DateTime(berlinTime.Year, berlinTime.Month, berlinTime.Day, shiftEnd, 0, 0);

            TimeSpan shiftHours = shiftEndDate - shiftStartDate;

            if (shiftHours.Hours < 9)
            {
                Console.WriteLine("Mesai Başlangıç: " + shiftStartDate);
                Console.WriteLine("Mesai Bitiş: " + shiftEndDate);
                Console.WriteLine("Çalışılan Saat: " + shiftHours);
                Console.WriteLine("Fazla mesai: Yapılmadı");
                Console.ReadLine();
            }
            else
            {
                overtimeHours = shiftHours.Hours - 9;
                Console.WriteLine("Mesai Başlangıç: " + shiftStartDate);
                Console.WriteLine("Mesai Bitiş: " + shiftEndDate);
                Console.WriteLine("Çalışılan Saat: " + shiftHours);
                Console.WriteLine("Fazla mesai: " + overtimeHours + " saat");
                Console.WriteLine("Ödebecek Fazla mesai ücreti: " + ((shiftHours.Hours - 9) * overtimePay) + " TL");
                Console.ReadLine();
            }











        }



    }
}
