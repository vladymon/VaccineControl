using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VC.Web.Data.Entities;

namespace VC.Web.Helpers
{
    public class ApiRest : IApiRest
    {
        HttpClientHandler _httpClientHandler = new HttpClientHandler();

        public ApiRest()
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public async Task<IdentificationDocument> GetDni(string dni)
        {
            var identificationDocument = new IdentificationDocument();
            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (var response = await httpClient.GetAsync("https://api.reniec.cloud/dni/" + dni))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    identificationDocument = JsonConvert.DeserializeObject<IdentificationDocument>(apiResponse);
                }
            }
            return identificationDocument;
        }
    }
}
