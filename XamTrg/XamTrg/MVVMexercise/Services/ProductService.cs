using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamTrg.MVVM.Models;

namespace XamTrg.MVVM.Services
{
    public class ProductService
    {
        private string url;
        HttpClient client;

        public ProductService()
        {
            url = "https://apiapptrainingnewapp.azurewebsites.net/api/Products";
            client = new HttpClient();
        }

        public async Task<List<ProductInfo>> GetProductInfoDataAsync()
        {
            List<ProductInfo> products = new List<ProductInfo>();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string jsonContents = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ProductInfo>>(jsonContents);
            }
            return products;
        }

        public async Task<ProductInfo> PostProductInfoAsync(ProductInfo product)
        {   
            string jsonRequest = JsonConvert.SerializeObject(product);
            StringContent requestContents = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, requestContents);
            if (response.IsSuccessStatusCode)
            {
                string jsonContents = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<ProductInfo>(jsonContents);
            }
            return product;
        }


    }
}
