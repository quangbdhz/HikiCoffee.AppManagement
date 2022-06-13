namespace HikiCoffee.Models
{
    public class ImportProduct
    {
        public int Id { get; set; }

        public DateTime DateImportProduct { get; set; }

        public int Quantity { get; set; }

        public int PriceImportProduct { get; set; }

        public string ProductName { get; set; }

        public string FullNameStaffAddImportProduct { get; set; }

        public string SuplierName { get; set; }
    }
}
