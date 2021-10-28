using DatabaseHandler.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UWP_DEMO_1.Service
{
    class PostService
    {
        private static string APIUrl = "https://jsonplaceholder.typicode.com";
        private static string APIUrlPath = "/posts";
        private static string APIPostDetail = "/posts/";

        public async Task<Post> Save(Post post)
        {
            using (var client  = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                var postString = JsonConvert.SerializeObject(post);
                var data = new StringContent(postString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(APIUrlPath, data);
                var result =  response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Post>(result);
            }
        } 

        public async Task<List<Post>> FindAll()
        {
            List<Post> list = new List<Post>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                var response = await client.GetAsync(APIUrlPath);
                var result = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Post>>(result);
            }
            return list;
        }

        public async Task<Post> FindById(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                var response = client.GetAsync(APIPostDetail + id).Result;
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Post>(result);
            }
        }

        public async Task<bool> Update(int id, Post post)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                var postString = JsonConvert.SerializeObject(post);
                var data = new StringContent(postString, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(APIPostDetail + id, data);
                var result = response.Content.ReadAsStringAsync().Result;
                return true;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                var response = await client.DeleteAsync(APIPostDetail + id);
                var result = response.Content.ReadAsStringAsync().Result;
                return true;
            }
        }

       
    }
}
