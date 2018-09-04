using System;

namespace Models.DataModels
{
    public class Auction
    {
        public const int GROUP_CODE = 1360;

        public int      AuktionId  { get; set; }
        public string   Titel       { get; set; }
        public string   Beskrivning { get; set; }
        public DateTime StartDatum  { get; set; }
        public DateTime SlutDatum   { get; set; }
        public int      Utropspris  { get; set; }
        public string   SkapadAv    { get; set; }
        public int      Gruppkod    { get; set; }
    }
}
