namespace MassPayouts.Net.Requisites
{
    public class Card : IRequisites
    {
        public string Number { get; set; }
        public string Owner { get; set; }
        public string GetRequisites()
        {
            return $"N{Number} O{Owner}";
        }
    }
}
