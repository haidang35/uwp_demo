using DatabaseHandler.Entity;
using DatabaseHandler.Models;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UWP_DEMO_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentList : Page
    {
        public List<Student> students;
        private Student editStudent;
        private List<string> itemsChoosed = new List<string>();
        public StudentList()
        {
            this.InitializeComponent();
            students = StudentModel.findAll();
  
        }

        
        private void dg_Sorting(object sender, DataGridColumnEventArgs e)
        {
            if (e.Column.Tag.ToString() == "rollNumber" || e.Column.Tag.ToString() == "fullName")
            {
                if (e.Column.Tag.ToString() == "rollNumber" || e.Column.Tag.ToString() == "fullName")
                {
                    if (e.Column.SortDirection == null || e.Column.SortDirection == DataGridSortDirection.Descending)
                    {
                        dataGrid1.ItemsSource = new ObservableCollection<Student>(from item in students
                                                                                  orderby item.RollNumber ascending
                                                                                  select item);
                        e.Column.SortDirection = DataGridSortDirection.Ascending;
                    }
                    else
                    {
                        dataGrid1.ItemsSource = new ObservableCollection<Student>(from item in students
                                                                                  orderby item.RollNumber descending
                                                                                  select item);
                        e.Column.SortDirection = DataGridSortDirection.Descending;
                    }
                }
               

                /*foreach (var dgColumn in dataGrid1.Columns)
                {
                    if (dgColumn.Tag.ToString() != e.Column.Tag.ToString())
                    {
                        dgColumn.SortDirection = null;
                    }
                }*/
                 }
         }

        private void cbRowDetailsVis_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            ComboBoxItem cbi = cb.SelectedItem as ComboBoxItem;
            if (this.dataGrid1 != null)
            {
                if (cbi.Content.ToString() == "Selected Row (Default)")
                    dataGrid1.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
                else if (cbi.Content.ToString() == "None")
                    dataGrid1.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
                else if (cbi.Content.ToString() == "All")
                    dataGrid1.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Visible;
            }
        }

        private void OnEditItem(object sender, RoutedEventArgs e)
        {
            var senderChoosed = sender as Button;
            dataGrid1.IsReadOnly = false;
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            editStudent = new Student
            {
                RollNumber = (this.dataGrid1.SelectedItem as Student).RollNumber,
                FullName = (this.dataGrid1.SelectedItem as Student).FullName,
                Dob = (this.dataGrid1.SelectedItem as Student).Dob,
                Gender = (this.dataGrid1.SelectedItem as Student).Gender,
                Email = (this.dataGrid1.SelectedItem as Student).Email,
                Phone = (this.dataGrid1.SelectedItem as Student).Phone,
                Address = (this.dataGrid1.SelectedItem as Student).Address,
                Status = (this.dataGrid1.SelectedItem as Student).Status,
            }; 
        }
        private async void DataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            var studentRow = e.Row.DataContext as Student;
            Debug.WriteLine($"Student {studentRow.ToString()}");
            if(StudentModel.Update(studentRow.RollNumber , studentRow))
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Notification",
                    Content = "You have just updated the student successfull",
                    CloseButtonText = "Continue",
                };
                await dialog.ShowAsync();
            } else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Notification",
                    Content = "You have just updated the student failed",
                    CloseButtonText = "Try again",
                };
                await dialog.ShowAsync();
            }

        }

        private async void Delete_Item(object sender, RoutedEventArgs e)
        {
            Button btnData = sender as Button;
            var stdDelete = btnData.CommandParameter as Student;
            ContentDialog dialogg = new ContentDialog
            {
                Title = "Notification",
                Content = "Are you sure delete the student ?",
                CloseButtonText = "Close",
                PrimaryButtonText = "Sure",
            };
            ContentDialogResult result = await dialogg.ShowAsync();
           
            if (result ==  ContentDialogResult.Primary && StudentModel.Delete(stdDelete.RollNumber))
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Notification",
                    Content = "You have just deleted the student successfull",
                    CloseButtonText = "Continue",
                };
                await dialog.ShowAsync();
                Frame.Navigate(typeof(StudentList));
            } else if(result == ContentDialogResult.Primary && !StudentModel.Delete(stdDelete.RollNumber))
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Notification",
                    Content = "You have just deleted the student failed",
                    CloseButtonText = "Try again",
                };
                await dialog.ShowAsync();
            }
        }

        private ObservableCollection<String> statusList = new ObservableCollection<string> { "Male", "Female" };
        private void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            SearchBox.ItemsSource = statusList.OrderBy(x => x).Select(x => x).Distinct().ToList();
        }
        private void SearchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int genderSelected = 0;
            if(SearchBox.SelectedValue.ToString() == "Male")
            {
                genderSelected = 1;

            }
            dataGrid1.ItemsSource = new ObservableCollection<Student>(from item in students
                                                           where item.Gender == genderSelected
                                                                    select item);
        }

        private void SearchBox_Changing(object sender, TextBoxTextChangingEventArgs e)
        {
            dataGrid1.ItemsSource = new ObservableCollection<Student>(from item in students
                                                                      where item.FullName.IndexOf(searchValue.Text) != -1
                                                                      select item);
            if(searchValue.Text == "")
            {
                dataGrid1.ItemsSource = students;
            }
        }

        private void CheckedStudent(object sender, RoutedEventArgs e)
        {
            var senderChecked = sender as CheckBox;
            var rollNumberChecked = senderChecked.Tag.ToString();
            var checkUnique = true;
            if(itemsChoosed == null)
            {
                itemsChoosed.Add(rollNumberChecked);
            } else
            {
                foreach(var item in itemsChoosed)
                {
                    if(item == senderChecked.Tag.ToString())
                    {
                        checkUnique = false;
                    }
                }
                if (checkUnique)
                {
                    itemsChoosed.Add(senderChecked.Tag.ToString());
                }
            }
            deleteItemsButton.Visibility = Visibility;
        }

        private void UncheckedStudent(object sender, RoutedEventArgs e)
        {
            var senderUnChecked = sender as CheckBox;
            var index = 0;
            Debug.WriteLine(itemsChoosed.Count);
            var indexChecked = 0;
            foreach(var item in itemsChoosed)
            {
                if(item == senderUnChecked.Tag.ToString())
                {
                    indexChecked = index;
                    continue;
                }
                index++;
            }
            itemsChoosed.RemoveAt(indexChecked);
            Debug.WriteLine(itemsChoosed.Count);
            if(itemsChoosed.Count == 0 || itemsChoosed == null)
            {
                deleteItemsButton.Visibility = Visibility.Collapsed;
            }
        }

        private async void Delete_ItemsChoosed(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog { 
                Title = "Warning Notification",
                Content = "Are you sure delete the items",
                CloseButtonText = "Cancel",
                PrimaryButtonText = "Confirm",
            };
            ContentDialogResult result = await dialog.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {
                if(StudentModel.DeleteMulti(itemsChoosed))
                {
                    dialog.Title = "Notification";
                    dialog.Content = "Delete the items successfull";
                    dialog.CloseButtonText = "Close";
                    dialog.PrimaryButtonText = "";
                    Frame.Navigate(typeof(StudentList));
                } else
                {
                    dialog.Title = "Notification";
                    dialog.Content = "Delete the items failed";
                    dialog.CloseButtonText = "Close";
                    dialog.PrimaryButtonText = "";
                }
                await dialog.ShowAsync();
            }
        }
    }
}
