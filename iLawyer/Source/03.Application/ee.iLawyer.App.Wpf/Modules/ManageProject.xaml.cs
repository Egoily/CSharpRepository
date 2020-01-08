using ee.Core.ComponentModel;
using ee.iLawyer.App.Wpf.UserControls.Pickers;
using ee.iLawyer.App.Wpf.ViewModels;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for ManageProject.xaml
    /// </summary>
    [BizModule(4, "Root", "案件管理", "module.project", "", typeof(ProjectViewModel))]
    public partial class ManageProject : UserControl
    {

        public ManageProject()
        {
            InitializeComponent();

        }

        private void BtnAddTodoItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            popupTodoItemControl.StaysOpen = true;
            popupTodoItemControl.IsOpen = true;
        }

        private void TodoItemControl_Closed(object sender, System.Windows.RoutedEventArgs e)
        {
            popupTodoItemControl.StaysOpen = false;
            popupTodoItemControl.IsOpen = false;
        }
        private void BtnAddProgress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            popupProgressControl.StaysOpen = true;
            popupProgressControl.IsOpen = true;
        }
        private void ProgressControl_Closed(object sender, System.Windows.RoutedEventArgs e)
        {
            popupProgressControl.StaysOpen = false;
            popupProgressControl.IsOpen = false;
        }

        private void DataGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }
    }


    [ValueConversion(typeof(ObservableCollection<Client>), typeof(ObservableCollection<SelectorItem>))]
    public class MultiItemSelectorItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                var result = new ObservableCollection<SelectorItem>();
                foreach (var item in value as ObservableCollection<Client>)
                {
                    result.Add(new SelectorItem()
                    {
                        Id = item.Id,
                        DisplayText = item.Name,
                        Icon = null,
                        Description = item.Impression
                    });
                }

                return result;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                var result = new ObservableCollection<Client>();
                foreach (var item in value as ObservableCollection<SelectorItem>)
                {
                    result.Add(new Client()
                    {
                        Id = item.Id,
                        Name = item.DisplayText,
                    });
                }

                return result;
            }
        }
    }


}



