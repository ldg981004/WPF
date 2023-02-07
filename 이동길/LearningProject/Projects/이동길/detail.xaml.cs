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
    /// detail.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class detail : UserControl
    {

        public detail(string name, string phone, string area)
        {
            InitializeComponent();
            lb_name.Content = name;
            lb_phone.Content = phone;
            lb_area.Content = area;



        }
    }
}