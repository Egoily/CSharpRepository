using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.UserControls
{

    [AddINotifyPropertyChangedInterface]
    public partial class TodoItemControl : UserControl
    {


        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
          "DataSource",
          typeof(ObservableCollection<Schedule>),
          typeof(TodoItemControl),
          new FrameworkPropertyMetadata(
              new ObservableCollection<Schedule>(),
              new PropertyChangedCallback(ItemsSourcePropertyChangedCallback)
              )
          );

        public ObservableCollection<Schedule> DataSource
        {
            get { return (ObservableCollection<Schedule>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        private static void ItemsSourcePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        public static readonly DependencyProperty CurrentObjectProperty = DependencyProperty.Register(
         "CurrentObject",
         typeof(Schedule),
         typeof(TodoItemControl),
         new FrameworkPropertyMetadata(
             new Schedule()
             {
                 Id = Guid.NewGuid().ToString(),
                 CreateTime = DateTime.Now,
                 ExpiredTime = DateTime.Now.AddDays(1),
                 CompletedTime = null,
                 RemindTime = null,
             },
             new PropertyChangedCallback(CurrentObjectPropertyChangedCallback)
             )
         );
        private static void CurrentObjectPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(TodoItemControl))
            {
                TodoItemControl uc = (TodoItemControl)sender;
                uc.SetTitle(e.NewValue == null || string.IsNullOrEmpty((e.NewValue as Schedule).Id));
            }
        }





        private void SetTitle(bool isNew)
        {
            IsNew = isNew;
            groupBox.Header = "待办事项-" + (isNew ? "新增" : "编辑");
            cbbStatus.Visibility = isNew ? Visibility.Collapsed : Visibility.Visible;
        }

        public Schedule CurrentObject
        {
            get { return (Schedule)GetValue(CurrentObjectProperty); }
            set { SetValue(CurrentObjectProperty, value); }
        }

        public string Title { get; set; }
        public bool IsNew { get; set; }


        public static readonly RoutedEvent ClosedRoutedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(TodoItemControl));

        public event RoutedEventHandler Closed
        {
            add { this.AddHandler(ClosedRoutedEvent, value); }
            remove { this.RemoveHandler(ClosedRoutedEvent, value); }
        }













        public TodoItemControl()
        {
            InitializeComponent();

        }


        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (cbbStatus.SelectedItem != null)
            {

                var selectItem = ((KeyValuePair<Object, Object>)cbbStatus.SelectedItem).Key;
                switch (selectItem)
                {
                    case StatusOfTodoItem.Pending:
                        CurrentObject.CompletedTime = null;
                        break;
                    case StatusOfTodoItem.Completed:
                        if (CurrentObject.CompletedTime == null)
                        {
                            CurrentObject.CompletedTime = DateTime.Now;
                        }
                        break;
                    case StatusOfTodoItem.Canceled:
                        CurrentObject.CompletedTime = null;
                        break;
                    default:
                        break;
                }

            }
            if (IsNew)
            {
                CurrentObject.Id = Guid.NewGuid().ToString();
                CurrentObject.CreateTime = DateTime.Now;
                DataSource.Add(CurrentObject.DeepClone() as Schedule);
                CurrentObject = null;
            }
            var args = new RoutedEventArgs(ClosedRoutedEvent, this);

            RaiseEvent(args);
        }
        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            var args = new RoutedEventArgs(ClosedRoutedEvent, this);

            RaiseEvent(args);
        }

        private void CbbStatus_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbbStatus.SelectedItem != null)
            {
                var selectItem = ((KeyValuePair<Object, Object>)cbbStatus.SelectedItem).Key;
                switch (selectItem)
                {
                    case StatusOfTodoItem.Pending:
                        dpCompletedTime.Text = "";
                        break;
                    case StatusOfTodoItem.Completed:
                        if (CurrentObject.CompletedTime == null)
                        {
                            dpCompletedTime.SelectedDate = DateTime.Now;
                        }
                        break;
                    case StatusOfTodoItem.Canceled:
                        dpCompletedTime.Text = "";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

