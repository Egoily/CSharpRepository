namespace ee.iLawyer.App.Wpf.Models
{
    public class ShowViewArg
    {
        public virtual string ShownerName { get; set; }
        public virtual object Parameter { get; set; }
        public virtual bool Topmost { get; set; }
        public ShowViewArg(string shownerName, object parameter = null, bool topmost = false)
        {
            ShownerName = shownerName;
            Parameter = parameter;
            Topmost = topmost;
        }

    }
}
