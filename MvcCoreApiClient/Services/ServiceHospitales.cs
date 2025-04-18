﻿using System.Net.Http.Headers;
using MvcCoreApiClient.Models;
using Newtonsoft.Json;

namespace MvcCoreApiClient.Services
{
    public class ServiceHospitales
    {
        private string ApiUrl;

        private MediaTypeWithQualityHeaderValue header;

        public ServiceHospitales(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiHospitales");
            this.header = new MediaTypeWithQualityHeaderValue
                ("application/json");
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/hospitales";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string json =  await 
                        response.Content.ReadAsStringAsync();
                    List<Hospital> data =
                        JsonConvert.DeserializeObject<List<Hospital>>(json);
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/hospitales/" + idHospital;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Hospital data = await response.Content.ReadAsAsync<Hospital>();
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
