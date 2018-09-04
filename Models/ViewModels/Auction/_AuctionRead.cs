using System;

namespace Models.ViewModels
{
    public class _AuctionRead
    {
        public int      AuktionId  { get; set; }
        public string   Titel       { get; set; }
        public string   Beskrivning { get; set; }
        public DateTime StartDatum  { get; set; }
        public DateTime SlutDatum   { get; set; }
        public int      Utropspris  { get; set; }
    }
}
