namespace MassPayouts.Net.Requisites
{
    public class BankAccount : IRequisites
    {
        public string Number { get; set; }
        public string TIN { get; set; }
        public string BIC { get; set; }
        public string GetRequisites()
        {
            return $"N{Number} T{TIN} B{BIC}";
        }
    }
}
