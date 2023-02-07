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
    /// task5.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class task5 : UserControl
    {
        List<info> infos = null;
        public task5()
        {
            InitializeComponent();
            infos = new List<info>();
            infos.Add(new info() { name = "홍길동", phone = "010-1234-5678", area = "서울" });
            infos.Add(new info() { name = "김철수", phone = "010-5678-9101", area = "경기" });
            infos.Add(new info() { name = "김미영", phone = "010-0000-0000", area = "강원" });
            infolist.ItemsSource = infos;
        }

        public class info
        {
            public string name { get; set; }
            public string phone { get; set; }
            public string area { get; set; }
        }

        private void infolist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            info info = e.AddedItems[0] as info;
            pannel_selected.Children.Clear();
            pannel_selected.Children.Add(new detail(info.name, info.phone, info.area));

        }
    }
}