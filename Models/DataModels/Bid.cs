namespace Models.DataModels
{
    public class Bid
    {
        public int    BudID     { get; set; }
        public int    Summa     { get; set; }
        public int    AuktionId { get; set; }
        public string Budgivare { get; set; }
    }
}
