using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace YouthLocationBooking.Data.API
{
    public class ApiClient
    {
        public async Task<T> Request<T>(string url)
        {
            HttpResponseMessage response = null;

            try
            {
                Uri uri = new Uri(url);
                using (HttpClient client = new HttpClient())
                    response = await client.GetAsync(uri);
            }
            catch (AggregateException ex)
            {
                return default(T);
            }

            string responseString = await response.Content.ReadAsStringAsync();
            T apiResponseObj = JsonConvert.DeserializeObject<T>(responseString);

            return apiResponseObj;
        }
        
    }
}
