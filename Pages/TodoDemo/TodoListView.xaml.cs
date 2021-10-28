using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_DEMO_1.Pages.TodoDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoListView : Page
    {

        public List<Todo> todoList = new List<Todo>();
        public TodoListView()
        {
            this.InitializeComponent();
            this.GetTodoList();
            
        }


        public async void GetTodoList()
        {
            TodoService todoService = new TodoService();
            todoList =  new List<Todo>(await todoService.FindAll());
            todoListGrid.ItemsSource = todoList;
            var newTodo = new Todo
            {
                title = "Hello world",
                completed = "true",
                userId = 10,
            };
            var todo = await todoService.FindById(1);
            await todoService.Update(1, newTodo);
            await todoService .Delete(1);
            Debug.WriteLine($"Result find {todo.title} ");
        }
    }
}
