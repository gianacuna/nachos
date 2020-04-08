using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace nachos.APIController.core
{
    public abstract class APICore : Controller
    {
        public async Task<TOutput> Get<TOutput>(HttpClient client, String url)
        {
            TOutput result = default(TOutput);

            HttpResponseMessage response = await client.GetAsync(url);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                String raw = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<TOutput>(raw);
            }

            return result;
        }

        public async Task<TOutput> Post<TOutput, TInput>(HttpClient client, String url, TInput inputModel)
        {
            TOutput result = default(TOutput);

            StringContent inputSerialized = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, inputSerialized);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                String raw = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<TOutput>(raw);
            }

            return result;
        }
    }
}
