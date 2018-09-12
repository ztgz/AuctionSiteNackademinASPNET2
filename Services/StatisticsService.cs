using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ViewModels.Statistics;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class StatisticsService : IStatisticService
    {
        private readonly IBidRepo     _bidRepo;
        private readonly IAuctionRepo _auctionRepo;

        public StatisticsService (IBidRepo bidRepo, IAuctionRepo auctionRepo)
        {
            _bidRepo     = bidRepo;
            _auctionRepo = auctionRepo;
        }

        public async Task<_BarChart> GetMonthlySummary(DateTime startDate, DateTime endDate, string userId)
        {
            _BarChart result;
            try
            {
                if (startDate > endDate)
                {
                    throw new Exception();
                }

                //Make enddate noninclusive, to get all days of the month
                endDate = endDate.AddMonths(1);
                
                //Dictonaries with all the sums
                SortedDictionary<string, (int startingBidsSum, int maxBidsSum)> dateDictionary = new SortedDictionary<string, (int startingBidsSum, int maxBidsSum)>();
                //Create zero keys for all the months between two periods
                for (DateTime date = startDate.Date; date < endDate; date = date.AddMonths(1))
                {
                    dateDictionary.Add(ConvertToKey(date), (0,0));
                }
                
                //Get the auctions that has the slutdatum between startdate and enddate
                var auctions = (await _auctionRepo.GetAuctions())
                    .Where(a => startDate.Date <= a.SlutDatum.Date && endDate.Date > a.SlutDatum.Date);
                //If userId provided...
                if (!string.IsNullOrEmpty(userId))
                {
                    //...filter auctions on based on user
                    auctions = auctions.Where(a => a.SkapadAv == userId);
                }

                var ahj = auctions.ToList();
                //Append all auction stats to the dictonary
                foreach (var auction in auctions)
                {
                    string key = ConvertToKey(auction.SlutDatum);

                    //Get maxbid for auction, if no bid set maxbid to 0
                    int maxBid = (await _bidRepo.GetBids(auction.AuktionId)).Max(b => b.Summa as int?) ?? 0;

                    //Append sums
                    var values = dateDictionary[key];
                    values.maxBidsSum += maxBid;
                    values.startingBidsSum += auction.Utropspris;

                    //And add to dictonary
                    dateDictionary[key] = values;
                }

                //Set values to the viewmodel
                var labels          = dateDictionary.Keys.ToArray();
                var sumStartingBids = dateDictionary.Values.Select(v => v.startingBidsSum).ToArray();
                var sumMaxBids      = dateDictionary.Values.Select(v => v.maxBidsSum).ToArray();
                var difference      = dateDictionary.Values.Select(v => v.maxBidsSum - v.startingBidsSum).ToArray();
                result = new _BarChart
                {
                    XLabels         = labels,  
                    SumStartingBids = sumStartingBids,
                    SumMaxBids      = sumMaxBids,
                    Difference      = difference
                };
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public async Task<_BarChart> GetMonthlySummary(DateTime startDate, DateTime endDate)
        {
            //Call with empty string to get summary for all users
            return await GetMonthlySummary(startDate, endDate, "");
        }

        private string ConvertToKey(DateTime date)
        {
            return $"{date.Month}/{date.Year}";
        }
    }
}
