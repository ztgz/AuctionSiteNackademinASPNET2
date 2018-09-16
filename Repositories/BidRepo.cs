using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Models.DataModels;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace Repositories
{
    public class BidRepo : IBidRepo
    {
        private readonly Uri _baseAddress;

        public BidRepo()
        {
            _baseAddress = new Uri("http://nackowskis.azurewebsites.net");
        }

        public async Task<IList<Bid>> GetBids(int auctionId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //Request the data
                    client.BaseAddress = _baseAddress;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync($"/api/bud/{Auction.GROUP_CODE}/{auctionId}");

                    //Ensure success, otherwise error is thrown
                    response.EnsureSuccessStatusCode();

                    //Make into right format
                    var responseString = await response.Content.ReadAsStringAsync();
                    IList<Bid> bids = JsonConvert.DeserializeObject<IList<Bid>>(responseString);

                    return bids;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateBid(Bid dim)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;
                    var jsonStr = JsonConvert.SerializeObject(dim);
                    var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                    var result  = await client.PostAsync("/api/bud/" + Auction.GROUP_CODE, content);
                    return result.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBid(int bidId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;
                    var result = await client.DeleteAsync($"/api/Bud/{Auction.GROUP_CODE}/{bidId}");
                    return result.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
