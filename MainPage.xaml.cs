using DatabaseHandler.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_DEMO_1.Pages;
using UWP_DEMO_1.Pages.TodoDemo;
using UWP_DEMO_1.Service;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_DEMO_1
{
   
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(HomePage));
        }


        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if(args.IsSettingsSelected)
            {
                // setting page
            } else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch(item.Tag.ToString())
                {
                    case "homePage":
                        ContentFrame.Navigate(typeof(HomePage));
                        break;
                    case "createNewPage":
                        ContentFrame.Navigate(typeof(CreateNewPage));
                        break;
                    case "studentList":
                        ContentFrame.Navigate(typeof(StudentList));
                        break;
                    case "testAPI":
                        ContentFrame.Navigate(typeof(TestAPI));
                        break;
                     case "todoList":
                        ContentFrame.Navigate(typeof(TodoListView));
                        break;
                }
            }

        }


        public void GoToList()
        {
            ContentFrame.Navigate(typeof(StudentList));
        }


    }
}
