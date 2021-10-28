using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UWP_DEMO_1.Pages.TodoDemo
{
    public class TodoService
    {
        private static string ApiTodo = "https://jsonplaceholder.typicode.com";
        private static string ApiTodoPostPath = "/todos";
        private static string ApiDataType = "application/json";

        public async Task<bool> Save(Todo todo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiTodo);
                    var dataJson = JsonConvert.SerializeObject(todo);
                    var postData = new StringContent(dataJson, Encoding.UTF8, ApiDataType );
                    var response = await client.PostAsync(ApiTodoPostPath, postData);
                    var result = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(JsonConvert.DeserializeObject<Todo>(result.ToString()));
                    return true;
                }
            }catch(Exception e)
            {
                return false;
            }
        }

        public async Task<List<Todo>> FindAll()
        {
            List<Todo> todoList = new List<Todo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiTodo);
                    var response = await client.GetAsync(ApiTodoPostPath);
                    var result = await response.Content.ReadAsStringAsync();
                    todoList = JsonConvert.DeserializeObject<List<Todo>>(result);
                }
            return todoList;
        }

        public async Task<Todo> FindById(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiTodo);
                var response = client.GetAsync($"{ApiTodoPostPath}/{id}").Result;
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Todo>(result);
            }
        }

        public async Task<bool> Update(int id, Todo todo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiTodo);
                    var todoString = JsonConvert.SerializeObject(todo);
                    var data = new StringContent(todoString, Encoding.UTF8, ApiDataType);
                    var response = await client.PutAsync($"{ApiTodoPostPath}/{id}", data);
                    var result = response.Content.ReadAsStringAsync().Result;
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiTodo);
                    var response = await client.DeleteAsync($"{ApiTodoPostPath}/{id}");
                    var result = response.Content.ReadAsStringAsync().Result;
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
