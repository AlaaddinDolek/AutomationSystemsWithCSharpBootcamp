using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using System.IO;
using System.Windows;
using static OfficeOpenXml.ExcelErrorValue;

namespace GasStation
{

    internal class Program
    {
        const string filePath = "C:\\Deneme\\GasStation.xlsx";
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            do
            {
                Console.WriteLine();
                Console.WriteLine("1-Akaryakıt Satış Ekleme");
                Console.WriteLine("2-Market Satış Ekleme");
                Console.WriteLine("3-Toplam Satış Görüntüleme");
                Console.WriteLine();
                Console.Write("Yukarıdaki işlemlerden birini seçiniz: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        InsertGasSale(1);
                        break;


                    case "2":
                        Console.WriteLine();
                        Console.WriteLine("1 - Coffee");
                        Console.WriteLine("2 - Chocolate");
                        Console.WriteLine("3 - Chips");
                        Console.WriteLine("4 - Biscuit");

                        Console.Write("Satış yapılan ürünü seçiniz: ");
                        string product = Console.ReadLine();
                        switch (product)
                        {
                            case "1": InsertProductSale(1); break;
                            case "2": InsertProductSale(2); break;
                            case "3": InsertProductSale(3); break;
                            case "4": InsertProductSale(4); break;

                            default:
                                Console.WriteLine("Geçerli bir değer giriniz.");
                                break;
                        }


                        break;

                    case "3":
                        Console.WriteLine();
                        Console.WriteLine("1-Akaryakıt Satış Görüntüle");
                        Console.WriteLine("2-Market Satış Görüntüle");
                        Console.WriteLine("3-Toplam Satış Görüntüle");
                        Console.Write("Yukarıdaki işlemlerden birini seçiniz: ");
                        string columnNumber = Console.ReadLine();
                        switch (columnNumber)
                        {
                            case "1":
                                Console.WriteLine($"Akaryakıt Satış Toplamı: {ViewTotalSale(1)}");
                                break;
                            case "2":
                                Console.WriteLine($"Market Satış Toplamı: {ViewTotalSale(4)}");
                                break;
                            case "3":
                                decimal totalSale = ViewTotalSale(1) + ViewTotalSale(4);

                                Console.WriteLine("Akaryakıt Satış Toplamı: }" + totalSale);
                                break;
                            default:
                                Console.WriteLine("Geçerli bir değer giriniz.");
                                break;
                        }

                        break;

                    default:
                        Console.WriteLine("Lütfen geçerli biri değer giriniz.");
                        break;

                }





            } while (true);
        }
        static void InsertSale(int columnNumber, decimal value)
        {
            FileInfo excelFile = new FileInfo(filePath);

            using (var package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                int rowCount = worksheet.Dimension.Rows;

                for (int rowNumber = 1; rowNumber <= rowCount + 1; rowNumber++)
                {
                    var cellValue = worksheet.Cells[rowNumber, columnNumber].Value;

                    if (cellValue == null)
                    {
                        worksheet.Cells[rowNumber, columnNumber].Value = value;
                        break;
                    }
                }

                package.Save();
            }
        }

        static void InsertGasSale(int columnNumber)
        {
            Console.Write("Satış yapılan tutarı giriniz: ");
            string sale = Console.ReadLine();
            try
            {
                decimal decimalSale;
                if (decimal.TryParse(sale, out decimalSale))
                {
                    try
                    {
                        InsertSale(columnNumber, decimalSale);
                        Console.WriteLine("Satış eklendi.");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Bir hata oluştu: " + ex.Message);
                    }

                }
                else
                {
                    Console.WriteLine("İşlem başarısız, geçerli bir satış değeri giriniz;");

                }

            }
            catch (Exception)
            {
                Console.WriteLine("Ekleme yapılamadı, geçerli bir satış değeri giriniz;");
            }
        }

        static void InsertProductSale(int input)
        {
            Console.Write("Satış yapılan adeti giriniz: ");
            string amount = Console.ReadLine();
            int intAmount;
            decimal price;

            try
            {
                if (int.TryParse(amount, out intAmount))
                    if (intAmount < 1)
                    {
                        Console.WriteLine("Geçerli bir adet değeri giriniz.");
                    }
                    else
                    {
                        try
                        {
                            FileInfo excelFile = new FileInfo(filePath);

                            using (var package = new ExcelPackage(excelFile))
                            {
                                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                                int rowCount = worksheet.Dimension.Rows;

                                for (int rowNumber = 1; rowNumber <= rowCount + 1; rowNumber++)
                                {
                                    var cellValue = worksheet.Cells[rowNumber, 3].Value;

                                    if (cellValue == null)
                                    {
                                        switch (input)
                                        {
                                            case 1:
                                                price = 10 * intAmount;
                                                worksheet.Cells[rowNumber, 3].Value = "Coffee";
                                                worksheet.Cells[rowNumber, 4].Value = price;
                                                Console.WriteLine("Satış eklendi.");
                                                break;
                                            case 2:
                                                price = 20 * intAmount;
                                                worksheet.Cells[rowNumber, 3].Value = "Chocolate";
                                                worksheet.Cells[rowNumber, 4].Value = price;
                                                Console.WriteLine("Satış eklendi.");
                                                break;
                                            case 3:
                                                price = 5 * intAmount;
                                                worksheet.Cells[rowNumber, 3].Value = "Chips";
                                                worksheet.Cells[rowNumber, 4].Value = price;
                                                Console.WriteLine("Satış eklendi.");
                                                break;
                                            case 4:
                                                price = 15 * intAmount;
                                                worksheet.Cells[rowNumber, 3].Value = "Biscuit";
                                                worksheet.Cells[rowNumber, 4].Value = price;
                                                Console.WriteLine("Satış eklendi.");
                                                break;
                                            default:
                                                Console.WriteLine("Geçerli bir ürün numarası giriniz");
                                                break;
                                        }

                                    }

                                }


                                package.Save();
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("İşlem başarısız" + ex.Message);
                        }
                    }
                else
                {
                    Console.WriteLine("İşlem başarısız, geçerli bir adet değeri giriniz;");

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ekleme yapılamadı, geçerli bir satış adeti giriniz;");

            }




        }

        static decimal ViewTotalSale(int columnNumber)
        {
            decimal total = 0;
            try
            {
                FileInfo excelFile = new FileInfo(filePath);

                using (var package = new ExcelPackage(excelFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int rowNumber = 1; rowNumber <= rowCount; rowNumber++)
                    {
                        var cellValue = worksheet.Cells[rowNumber, columnNumber].Value;
                        if (cellValue != null)
                        {
                            decimal cellDecimalValue;
                            string cellValueStr = cellValue.ToString();
                            cellValueStr = cellValueStr.Replace(',', '.');

                            if (decimal.TryParse(cellValueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out cellDecimalValue))
                            {
                                total += cellDecimalValue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata oluştu: " + ex.Message);
            }

            return total;
        }
    }
}


