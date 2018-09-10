using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class _AuctionManage
    {
        //Hidden
        public int AuktionId { get; set; }

        [Required(ErrorMessage = "Titel är obligatoriskt")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Titel måste minst vara 1 tecken och max 50")]
        [Display(Name = "Titel")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatoriskt")]
        [StringLength(150, ErrorMessage = "Beskrivning kan max var 150 tecken")]
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

        /// <summary>
        /// Validates the model and returns a list of errors
        /// </summary>
        /// <returns>List of errors</returns>
        public IList<string> Validate()
        {
            IList<string> errors = new List<string>(2);
            if (StartDatum >= SlutDatum)
            {
                errors.Add("Starttid måste komma före sluttid");
            }

            return errors;
        }

        public static _AuctionManage Transform(_AuctionRead read)
        {
            _AuctionManage manage = new _AuctionManage
            {
                AuktionId   = read.AuktionId,
                Titel       = read.Titel,  
                Beskrivning = read.Beskrivning,
                StartDatum  = read.StartDatum,
                SlutDatum   = read.SlutDatum,
                Utropspris  = read.Utropspris
            };

            return manage;
        }
    }
}
