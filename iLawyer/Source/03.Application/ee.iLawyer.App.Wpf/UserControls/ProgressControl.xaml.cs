using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.UserControls
{
    [AddINotifyPropertyChangedInterface]
    public partial class ProgressControl : UserControl
    {


        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
          "DataSource",
          typeof(ObservableCollection<ProjectProgress>),
          typeof(ProgressControl),
          new FrameworkPropertyMetadata(
              new ObservableCollection<ProjectProgress>(),
              new PropertyChangedCallback(ItemsSourcePropertyChangedCallback)
              )
          );

        public ObservableCollection<ProjectProgress> DataSource
        {
            get { return (ObservableCollection<ProjectProgress>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        private static void ItemsSourcePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        public static readonly DependencyProperty CurrentObjectProperty = DependencyProperty.Register(
         "CurrentObject",
         typeof(ProjectProgress),
         typeof(ProgressControl),
         new FrameworkPropertyMetadata(
             new ProjectProgress()
             {
                 Id = Guid.NewGuid().ToString(),
                 CreateTime = DateTime.Now,
                 HandleTime = DateTime.Now,
             },
             new PropertyChangedCallback(CurrentObjectPropertyChangedCallback)
             )
         );
        private static void CurrentObjectPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(ProgressControl))
            {
                ProgressControl uc = (ProgressControl)sender;
                uc.SetTitle(e.NewValue == null || string.IsNullOrEmpty((e.NewValue as ProjectProgress).Id));
            }
        }





        private void SetTitle(bool isNew)
        {
            IsNew = isNew;
            groupBox.Header = "案件进展-" + (isNew ? "新增" : "编辑");

        }

        public ProjectProgress CurrentObject
        {
            get { return (ProjectProgress)GetValue(CurrentObjectProperty); }
            set { SetValue(CurrentObjectProperty, value); }
        }

        public string Title { get; set; }
        public bool IsNew { get; set; }


        public static readonly RoutedEvent ClosedRoutedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ProgressControl));

        public event RoutedEventHandler Closed
        {
            add { this.AddHandler(ClosedRoutedEvent, value); }
            remove { this.RemoveHandler(ClosedRoutedEvent, value); }
        }





        public ProgressControl()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                CurrentObject.Id = Guid.NewGuid().ToString();
                CurrentObject.CreateTime = DateTime.Now;
                DataSource.Add(CurrentObject.DeepClone() as ProjectProgress);
                CurrentObject = null;
            }
            var args = new RoutedEventArgs(ClosedRoutedEvent, this);
            RaiseEvent(args);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var args = new RoutedEventArgs(ClosedRoutedEvent, this);
            RaiseEvent(args);
        }
    }
}
