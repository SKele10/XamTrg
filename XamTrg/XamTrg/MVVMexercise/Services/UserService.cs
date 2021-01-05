using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamTrg.MVVMexercise.Models;

namespace XamTrg.MVVMexercise.Services
{
    public class UserService
    {
        private string url;
        HttpClient client;
        public UserService()
        {
            url = "https://apiapptrainingnewapp.azurewebsites.net/api/Users";
            client = new HttpClient();
        }
        public async Task<List<User>> GetUserAsync()
        {
            List<User> users = new List<User>();
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonContents = await responseMessage.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(jsonContents);
            }
            return users;
        }
        public async Task<User> PostUserAync(User user)
        {
            string jsonRequest = JsonConvert.SerializeObject(user);
            StringContent requestContents = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, requestContents);
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonContents = await responseMessage.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(jsonContents);
            }
            return user;
        }
    }
}
