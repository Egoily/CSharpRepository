using System.Windows.Controls;
using TaobaoSeckiller.ViewModels;

namespace TaobaoSeckiller.Views
{
    /// <summary>
    /// TaobaoSeckill.xaml 的交互逻辑
    /// </summary>
    public partial class TaobaoSeckill : Page
    {
        public TaobaoSeckill()
        {
            InitializeComponent();
            this.DataContext = new TaobaoSeckillViewModel();
        }
    }
}