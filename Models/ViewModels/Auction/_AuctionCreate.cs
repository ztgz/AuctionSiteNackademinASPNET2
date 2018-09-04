using System;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class _AuctionCreate
    {
        [Required(ErrorMessage = "Titel är obligatoriskt")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Titel måste minst vara 1 tecken och max 50")]
        [Display(Name = "Titel")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatoriskt")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Beskrivning måste minst vara 5 tecken och max 150")]
        [Display(Name = "Beskrivning")]
        public string Beskrivning { get; set; }

        [Required(ErrorMessage = "Startdatum är obligatoriskt")]
        [DataType(DataType.DateTime, ErrorMessage = "Felaktigt datum")]
        [Display(Name = "Startdatum")]
        public DateTime StartDatum { get; set; }

        [Required(ErrorMessage = "Slutdatum är obligatoriskt")]
        [DataType(DataType.DateTime, ErrorMessage = "Felaktigt datum")]
        [Display(Name = "SlutDatum")]
        public DateTime SlutDatum { get; set; }
        
        [Required(ErrorMessage = "Utropspris är obligatoriskt")]
        [Range(0, int.MaxValue, ErrorMessage = "Måste vara ett positivt number")]
        [Display(Name = "Utropspris")]
        public int Utropspris { get; set; }
    }
}
