using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class _BidManage
    {
        [Required(ErrorMessage = "Ett belopp måste anges")]
        [Range(1, int.MaxValue, ErrorMessage = "Kan inte ge ett bud som är mindre än 1kr")]
        public int    Summa     { get; set; }
        [Required(ErrorMessage = "En auktion måste anges")]
        public int    AuktionID { get; set; }
    }
}
