using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearningProject.Projects.이동길
{
    /// <summary>
    /// tsak6.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class task6 : UserControl

    {
        List<Item> items = null;

        public task6()
        {
            InitializeComponent();
            items = new List<Item>();
            items.Add(new Item() { name = "상의", level = 40, stat = "힘+40" });
            items.Add(new Item() { name = "하의", level = 50, stat = "힘+50" });
            items.Add(new Item() { name = "무기", level = 70, stat = "힘+80" });
            itemList.ItemsSource = items;
        }
        public class Item
        {
            public string name { get; set; }
            public int level { get; set; }
            public string stat { get; set; }
        }
    }
    
}
