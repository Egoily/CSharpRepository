using ee.Core.ComponentModel;
using ee.Core.Framework.Messaging;
using ee.Core.Wpf.Extensions;
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

        public ClientSearchProvider ClientSearchProvider { get { return ClientSearchProvider.Instance; } }

        public ManageProject()
        {
            InitializeComponent();
     
        }

 
    }


    [ValueConversion(typeof(ObservableCollection<Client>), typeof(ObservableCollection<MultiItemSelectorItem>))]
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
                var result = new ObservableCollection<MultiItemSelectorItem>();
                foreach (var item in value as ObservableCollection<Client>)
                {
                    result.Add(new MultiItemSelectorItem()
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
                foreach (var item in value as ObservableCollection<MultiItemSelectorItem>)
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



