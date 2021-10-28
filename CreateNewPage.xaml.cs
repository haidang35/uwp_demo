using DatabaseHandler.Entity;
using DatabaseHandler.Entity;
using DatabaseHandler.Models;
using System;
using System.Collections.Generic;
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

namespace UWP_DEMO_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateNewPage : Page
    {

        private int gender;
        public CreateNewPage()
        {
            this.InitializeComponent();
        }

        private void checkedGender(object sender, RoutedEventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if(rd.Tag.ToString() == "Male")
            {
                gender = 1;
            } else if(rd.Tag.ToString() == "Female")
            {
                gender = 0;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var rn = rollNumber.Text;
            var name = fullName.Text;
            var phoneNumber = phone.Text;
            var emailV = email.Text;
            var dob = birthday.SelectedDate.Value.DateTime;
            var addr = address.Text;
            var stt = 1;
            var newStudent = new Student
            {
                RollNumber = rn,
                FullName = name,
                Phone = phoneNumber,
                Email = emailV,
                Dob = dob,
                Address = addr,
                Status = (StudentStatus)stt,
                Gender = gender,

            };
            if(StudentModel.Save(newStudent))
            {
                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Notification",
                    Content = "You have just created a new student successful",
                    CloseButtonText = "Ok"
                };
                await noWifiDialog.ShowAsync();
                Frame.Navigate(typeof(StudentList));
            }
            else
            {
                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Notification",
                    Content = "Error",
                    CloseButtonText = "Ok"
                };
            }


        }
    }
}
