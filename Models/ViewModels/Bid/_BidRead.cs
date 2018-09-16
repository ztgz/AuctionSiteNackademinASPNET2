
namespace Models.ViewModels
{
    public class _BidRead
    {
        public int    BudID     { get; set; }
        public int    Summa     { get; set; }
        public int    AuktionId { get; set; }
        public string Budgivare { get; set; }

        public static bool AreEqual(_BidRead obj1, _BidRead obj2)
        {
            return obj1.BudID == obj2.BudID &&
                   obj1.Summa == obj2.Summa &&
                   obj1.AuktionId == obj2.AuktionId &&
                   obj1.Budgivare.Equals(obj2.Budgivare);
        }
    }
}
