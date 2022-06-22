    namespace HikiCoffee.Models.DataRequest.CoffeeTables
{
    public class ChangeCoffeeTableIdInBillRequest
    {
        public int NewCoffeeTableId { get; set; }

        public int BillId { get; set; }
    }
}
