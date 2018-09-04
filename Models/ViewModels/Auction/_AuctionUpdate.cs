using System;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class _AuctionUpdate
    {
        [Required(ErrorMessage = "Titel är obligatoriskt")]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatoriskt")]
        [StringLength(150, MinimumLength = 5)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Startdatum är obligatoriskt")]
        [DataType(DataType.DateTime, ErrorMessage = "Felaktigt datum")]
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Slutdatum är obligatoriskt")]
        [DataType(DataType.DateTime, ErrorMessage = "Felaktigt datum")]
        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Utropspris är obligatoriskt")]
        [Range(0, int.MaxValue, ErrorMessage = "Måste vara ett positivt number")]
        [Display(Name = "Utropspris")]
        public int StartingPrice { get; set; }
    }
}
