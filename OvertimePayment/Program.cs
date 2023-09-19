using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OvertimePayment
{
    public class Program
    {

        static List<Employee> employees = new List<Employee>()
        {
            new Employee() {TC=1111,Name="John",LastName="Wick",isWorking=false},
            new Employee() {TC=2222,Name="Nikola",LastName="Tesla",isWorking = false}

        };
        static decimal mesaiSaatUcreti = 50;

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("1.Giriş işlemi");
                Console.WriteLine("2.Çıkış işlemi");
                Console.WriteLine("3.Personel Kayıt işlemi");
                string input = Console.ReadLine();

                switch (input)
                {
                    #region Giris
                    case "1":
                        Console.Write("Giriş yapan personel TC: ");
                        string girisTC = Console.ReadLine();

                        try
                        {
                            int tc = Convert.ToInt32(girisTC);
                            if (Convert.ToString(tc).Length != 4)
                            {
                                Console.WriteLine("4 haneli TC giriniz");
                                break;
                            }
                            bool doesExist = false;
                            foreach (Employee item in employees)
                            {

                                if (tc == item.TC)
                                {
                                    doesExist = true;
                                    if (item.isWorking)
                                    {
                                        Console.WriteLine("Personel daha önce giriş yapmıştır");
                                        break;
                                    }
                                    
                                    item.ShiftStart = GetBerlinTime();
                                    item.isWorking = true;

                                    Console.WriteLine($"{item.Name} {item.LastName} giriş yaptı");
                                    Console.WriteLine("Giriş saati: " + item.ShiftStart.ToString("HH:mm"));
                                    break;
                                }
                            }
                            if (doesExist == false) Console.WriteLine("Personel bulunamadı");

                        }
                        catch
                        {
                            Console.WriteLine("Geçerli bir değer giriniz");
                            break;
                        }
                        break;
                    #endregion

                    #region Cikis
                    case "2":
                        Console.Write("Çıkış yapan personel TC: ");
                        string cikisTC = Console.ReadLine();
                        try
                        {
                            int tc = Convert.ToInt32(cikisTC);
                            if (Convert.ToString(tc).Length != 4)
                            {
                                Console.WriteLine("4 haneli TC giriniz");
                                break;
                            }
                            bool doesExist = false;
                            foreach (Employee item in employees)
                            {
                                if (tc == item.TC)
                                {
                                    doesExist = true;
                                    if (item.isWorking == false)
                                    {
                                        Console.WriteLine("Kişi giriş yapmamıştır");
                                        break;
                                    }


                                    item.ShiftEnd = GetBerlinTime();
                                    item.ShiftEnd = item.ShiftEnd.AddHours(9.0);
                                    item.isWorking = false;

                                    Console.WriteLine($"{item.Name} {item.LastName} çıkış yaptı");
                                    Console.WriteLine("Çıkış saati: " + item.ShiftEnd.ToString("HH:mm"));

                                    Console.WriteLine();

                                    TimeSpan mesai = item.ShiftEnd - item.ShiftStart;

                                    if (mesai.TotalHours > 9)
                                    {
                                        Console.WriteLine("Yapılan fazla mesai: " + (mesai.TotalHours - 9).ToString("N4") + " saat");

                                        decimal fazlaMesaiUcreti = Convert.ToDecimal(mesai.TotalHours - 9) * mesaiSaatUcreti;

                                        Console.WriteLine("Ödenecek tutar: " + fazlaMesaiUcreti.ToString("0.##" + " TL"));

                                        break;
                                    }


                                }
                            }
                            if (doesExist == false) Console.WriteLine("Personel bulunamadı");
                        }
                        catch
                        {
                            Console.WriteLine("Geçerli bir değer giriniz");
                            break;
                        }
                        break;
                    #endregion

                    #region Kayit
                    case "3":
                        Console.Write("TC: ");
                        string inputTC = Console.ReadLine();
                        try
                        {
                            int intTC = Convert.ToInt32(inputTC);
                            if (inputTC.Length != 4)
                            {
                                Console.WriteLine("Girilen TC 4 haneli olmalıdır");
                                break;
                            }
                            foreach (Employee item in employees)
                            {
                                if (intTC == item.TC)
                                {
                                    Console.WriteLine("Girilen TC numarasına ait personel mevcuttur.");
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Geçerli bir değer giriniz");
                            break;
                        }


                        Console.Write("İsim: ");
                        string name = Console.ReadLine();

                        Console.Write("Soyisim: ");
                        string lastName = Console.ReadLine();

                        Employee employee = new Employee()
                        {
                            TC = Convert.ToInt32(inputTC),
                            Name = name,
                            LastName = lastName
                        };
                        employees.Add(employee);
                        break;

                    default:

                        Console.WriteLine("Geçerli bir değer giriniz");
                        continue;


                        #endregion

                }
                DateTime GetBerlinTime()
                {
                    TimeZoneInfo berlinTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    DateTime berlinNow = TimeZoneInfo.ConvertTime(DateTime.Now, berlinTime);
                    return berlinNow;
                }
            } while (true);
        }

        public class Employee
        {
            public int TC { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public DateTime ShiftStart { get; set; }
            public DateTime ShiftEnd { get; set; }
            public bool isWorking { get; set; }

        }



    }
}
