namespace LinqToXml
{
    class Laptop
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public string CPU { get; set; }
        public int RAM { get; set; }
        public double Price { get; set; }
        public int StoreId { get; set; }
        public override string ToString()
        {
            return string.Format("Код: {0}\n" +
                "Производитель: {1}\n" +
                "Модель: {2}\n" +
                "CPU: {3}\n" +
                "RAM: {4}\n" +
                "Цена: {5}\n" +
                "Код магазина: {6}",
                Id, Company, Model, CPU, RAM, Price, StoreId);
        }
    }
}
