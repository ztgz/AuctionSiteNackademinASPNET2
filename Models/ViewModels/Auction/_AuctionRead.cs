using System;

namespace Models.ViewModels
{
    public class _AuctionRead
    {
        public int      AuktionId   { get; set; }
        public string   Titel       { get; set; }
        public string   Beskrivning { get; set; }
        public DateTime StartDatum  { get; set; }
        public DateTime SlutDatum   { get; set; }
        public int      Utropspris  { get; set; }
        public bool     IsOwner     { get; set; }
        public _BidRead MaxBid      { get; set; }

        public static bool AreEqual(_AuctionRead obj1, _AuctionRead obj2)
        {
            return obj1.AuktionId    == obj2.AuktionId   &&
                   obj1.Titel        == obj2.Titel       &&
                   obj1.Beskrivning  == obj2.Beskrivning &&
                   obj1.StartDatum   == obj2.StartDatum  &&
                   obj1.SlutDatum    == obj2.SlutDatum   &&
                   obj1.Utropspris   == obj2.Utropspris  &&
                   obj1.IsOwner      == obj2.IsOwner     &&
                   (obj1.MaxBid == null && obj2.MaxBid == null || obj1.MaxBid.Summa == obj2.MaxBid.Summa);
        }
    }
}
