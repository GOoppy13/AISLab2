using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            Laptop[] laptops = new Laptop[]
            {
                new Laptop { Id = 0, Company = "ASUS", Model = "Laptop 14 F415EA-EB736", CPU = "Intel Pentium Gold 7505", RAM = 8, Price = 30999, StoreId = 0},
                new Laptop { Id = 1, Company = "HP", Model = "15s-eq1322ur", CPU = "AMD 3020e", RAM = 8, Price = 32999, StoreId = 1},
                new Laptop { Id = 2, Company = "Acer", Model = "Aspire 3 A315-56-34Q8", CPU = "Intel Core i3-1005G1", RAM = 4, Price = 34999, StoreId = 2},
                new Laptop { Id = 3, Company = "HP", Model = "Laptop 15s-eq1142ur", CPU = "AMD Athlon Silver 3050U", RAM = 8, Price = 36999, StoreId = 0},
                new Laptop { Id = 4, Company = "Acer", Model = "Swift 3 SF314-43", CPU = "AMD Ryzen 3 5300U", RAM = 8, Price = 42999, StoreId = 1},
                new Laptop { Id = 5, Company = "ASUS", Model = "Laptop 14 D415DA-EK614T", CPU = "AMD Ryzen 3 3250U", RAM = 8, Price = 44999, StoreId = 2},
                new Laptop { Id = 6, Company = "HP", Model = "15s-fq2018ur", CPU = "Intel Core i3-1115G4", RAM = 8, Price = 47999, StoreId = 0},
                new Laptop { Id = 7, Company = "ASUS", Model = "VivoBook Flip 14 TM420UA-EC063T", CPU = "AMD Ryzen 3 5300U", RAM = 4, Price = 49999, StoreId = 1},
                new Laptop { Id = 8, Company = "Lenovo", Model = "IdeaPad Flex 5 14ALC05", CPU = "AMD Ryzen 3 5300U", RAM = 8, Price = 51999, StoreId = 2},
                new Laptop { Id = 9, Company = "HP", Model = "Pavilion Aero 13-be0050ur", CPU = "AMD Ryzen 5 5600U", RAM = 8, Price = 55999, StoreId = 0},
                new Laptop { Id = 10, Company = "ASUS", Model = "VivoBook 15 X513EA-BQ2370W", CPU = "Intel Core i3-1115G4", RAM = 8, Price = 58699, StoreId = 1},
                new Laptop { Id = 11, Company = "Acer", Model = "Aspire 3 A315-56-71MM", CPU = "Intel Core i7-1065G7", RAM = 8, Price = 61999, StoreId = 2},
                new Laptop { Id = 12, Company = "Lenovo", Model = "Yoga Slim 7 14ARE05", CPU = "AMD Ryzen 5 4500U", RAM = 8, Price = 64999, StoreId = 0},
                new Laptop { Id = 13, Company = "Dell", Model = "Inspiron 5515-0363", CPU = "AMD Ryzen 7 5700U", RAM = 8, Price = 65999, StoreId = 1},
                new Laptop { Id = 14, Company = "HP", Model = "Pavilion Aero 13-be0005ur", CPU = "AMD Ryzen 5 5600U", RAM = 16, Price = 69999, StoreId = 2},
            };
            GenerXML(laptops);

            CreateXmlDocument();

            Console.Title = "Выполнение запросов LINQ к XML-документу";

            XDocument xmldoc = XDocument.Load("CarDealer.xml");

            Console.WriteLine("........Результаты запросов LINQ к XML-документу........");

            string manufacturer = "Toyota";
            //1. Модели и цены автомобилей фирмы «Фирма».
            GetByManufacturer(xmldoc, manufacturer);

            string bodyType = "купе";
            //2. Средняя цена автомобиля с кузовом «Тип».
            GetAveragePriceByBodyType(xmldoc, bodyType);

            int power = 200;
            //3. Данные по автомобилям мощностью более «», сгрупп по фирмам. 
            GetByPowerGroupByManufacturer(xmldoc, power);

            //4. Список автомобилей с указанием ответств. сотрудника (join).
            GetByResponsibleEmployee(xmldoc);

            int power2 = 200;
            //5. Все автомобили с мощностью двигателя более «...» (XPath).
            GetByPowerXPath(xmldoc, power2);
        }

        private static void GenerXML(Laptop[] laptops)
        {
            var xmlData = from laptop in laptops
                          select
                          new XElement("Laptop",
                            new XAttribute("Id", laptop.Id),
                            new XAttribute("StoreId", laptop.StoreId),
                            new XElement("Company", laptop.Company),
                            new XElement("Model", laptop.Model),
                            new XElement("CPU", laptop.CPU),
                            new XElement("RAM", laptop.RAM),
                            new XElement("Price", laptop.Price)
                          );
            XElement rootElement = new XElement("LaptopStock", xmlData);
            XDocument xmlDoc = new XDocument();
            xmlDoc.Add(rootElement);
            xmlDoc.Save("laptops.xml");
        }

        private static void CreateXmlDocument()
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("автодилер",
                    new XElement("машина", new XAttribute("номер", 1),
                        new XElement("модель", "Mark2"),
                        new XElement("фирма", "Toyota"),
                        new XElement("цена", 800000),
                        new XElement("кузов", "седан"),
                        new XElement("ответственный_работник", "Анатолий"),
                        new XElement("мощность", "200")
                    ),
                    new XElement("машина", new XAttribute("номер", 2),
                        new XElement("модель", "A80"),
                        new XElement("фирма", "Toyota"),
                        new XElement("цена", 2400000),
                        new XElement("кузов", "купе"),
                        new XElement("ответственный_работник", "Дмитрий"),
                        new XElement("мощность", "300")
                    ),
                    new XElement("машина", new XAttribute("номер", 3),
                        new XElement("модель", "GT-R"),
                        new XElement("фирма", "Nissan"),
                        new XElement("цена", 4600000),
                        new XElement("кузов", "купе"),
                        new XElement("ответственный_работник", "Анатолий"),
                        new XElement("мощность", "550")
                    ),
                    new XElement("машина", new XAttribute("номер", 4),
                        new XElement("модель", "Civic"),
                        new XElement("фирма", "Honda"),
                        new XElement("цена", 900000),
                        new XElement("кузов", "хетчбек"),
                        new XElement("ответственный_работник", "Дмитрий"),
                        new XElement("мощность", "150")
                    ),
                    new XElement("машина", new XAttribute("номер", 5),
                        new XElement("модель", "RX-7"),
                        new XElement("фирма", "Mazda"),
                        new XElement("цена", 2400000),
                        new XElement("кузов", "купе"),
                        new XElement("ответственный_работник", "Вячеслав"),
                        new XElement("мощность", "300")
                    )
                )
            );
            doc.Save("CarDealer.xml");
        }

        private static void GetByManufacturer(XDocument xmldoc, string manufacturer)
        {
            var result = from car in xmldoc.Descendants("машина")
                        where (string)car.Element("фирма") == manufacturer
                        select car;

            Console.WriteLine($"........Машины фирмы {manufacturer}........");

            foreach (var car in result)
            {
                Console.WriteLine((string)car.Element("модель") + " - " + (string)car.Element("цена"));
            }
        }

        private static void GetAveragePriceByBodyType(XDocument xmldoc, string bodyType)
        {
            var result = (from car in xmldoc.Descendants("машина")
                        where (string)car.Element("кузов") == bodyType
                        select Convert.ToDouble(car.Element("цена").Value)).Average();

            Console.WriteLine($"........Средняя стоимость машин с кузовом типа {bodyType}........");

            Console.WriteLine(result);
        }

        private static void GetByPowerGroupByManufacturer(XDocument xmldoc, int power)
        {
            var groups = from car in xmldoc.Descendants("машина")
                        where (int)car.Element("мощность") > power
                        group car by car.Element("фирма");

            Console.WriteLine($"........Машины мощности выше {power}........");

            foreach (var group in groups)
            {
                Console.WriteLine($"Машины {(string)group.Key} мощностью выше {power}");

                foreach (var car in group)
                {
                    Console.WriteLine((string)car.Element("модель"));
                }
            }
        }

        private static void GetByResponsibleEmployee(XDocument xmldoc)
        {
            XElement employees = new XElement("работники_салона",
                new XElement("работник",  new XAttribute("код", "а111"),
                    new XAttribute("номер_машины", 1),
                    new XElement("фио", "Буров Анатолий Григорьевич")
                ),
                new XElement("работник",  new XAttribute("код", "а112"),
                    new XAttribute("номер_машины", 2),
                    new XElement("фио", "Петров Дмитрий Александрович")
                ),
                new XElement("работник",  new XAttribute("код", "а113"),
                    new XAttribute("номер_машины", 3),
                    new XElement("фио", "Кудрова Татьяна Михайловна")
                ),
                new XElement("работник",  new XAttribute("код", "а114"),
                    new XAttribute("номер_машины", 4),
                    new XElement("фио", "Самойлов Константин Михайлович")
                ),
                new XElement("работник",  new XAttribute("код", "а115"),
                    new XAttribute("номер_машины", 5),
                    new XElement("фио", "Иванов Михаил Дмитриевич")
                )
            );

            var result = from car in xmldoc.Descendants("машина")
                        from model in car.Elements("модель")
                        join emp in employees.Elements("работник")
                            on car.Attribute("номер").Value equals emp.Attribute("номер_машины").Value
                        select new {
                            name = emp.Element("фио").Value,
                            carMan = car.Element("фирма").Value,
                            carMod = car.Element("модель").Value
                        };

            Console.WriteLine($"........Работники автодилера и машины, за которые они ответственны........");

            foreach (var r in result)
            {
                Console.WriteLine($"{r.name}\t - {r.carMan} {r.carMod}");
            }
        }

        private static void GetByPowerXPath(XDocument xmldoc, int power)
        {
            var result = xmldoc.XPathSelectElements($"//машина[мощность>{power}]");

            Console.WriteLine($"........Машины мощности выше {power}(XPath)........");
            foreach (var car in result)
            {
                Console.WriteLine($"{car.Element("фирма").Value} {car.Element("модель").Value}");
            }
        }
    }
}
