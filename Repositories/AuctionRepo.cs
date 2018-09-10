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
    public class AuctionRepo : IAuctionRepo
    {
        private readonly Uri _baseAddress;

        public AuctionRepo()
        {
            _baseAddress = new Uri("http://nackowskis.azurewebsites.net");
        }

        #region Create
        public async Task<bool> CreateAuction(Auction dim)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;
                    var jsonStr = JsonConvert.SerializeObject(dim);
                    var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                    var result  = await client.PostAsync("/api/Auktion/" + Auction.GROUP_CODE, content);
                    return result.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Read
        public async Task<Auction> GetAuction(int auctionId)
        {
            try
            {
                var response = await GetRequest($"/api/Auktion/{Auction.GROUP_CODE}/{auctionId}");
                Auction auction = JsonConvert.DeserializeObject<Auction>(response);

                return auction;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IList<Auction>> GetAuctions()
        {
            try
            {
                var response = await GetRequest($"/api/Auktion/{Auction.GROUP_CODE}");
                IList<Auction> auctions = JsonConvert.DeserializeObject<IList<Auction>>(response);
                return auctions;
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> GetRequest(string uri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //Request the data
                    client.BaseAddress = _baseAddress;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync(uri);

                    //Ensure success, otherwise error is thrown
                    response.EnsureSuccessStatusCode();

                    //Make into right format
                    var responseString = await response.Content.ReadAsStringAsync();

                    return responseString;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Update
        public async Task<bool> UpdateAuction(Auction dim)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;
                    var jsonStr = JsonConvert.SerializeObject(dim);
                    var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                    var result  = await client.PutAsync($"/api/Auktion/{Auction.GROUP_CODE}" + Auction.GROUP_CODE, content);
                    return result.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteAuction(int auctionId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;
                    var result = await client.DeleteAsync($"/api/Auktion/{Auction.GROUP_CODE}/{auctionId}");
                    return result.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
