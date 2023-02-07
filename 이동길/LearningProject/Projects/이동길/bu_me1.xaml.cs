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
    /// bu_me1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class bu_me1 : UserControl
    {
        public bu_me1()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(Object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void lst_MouseEnter(object sender, MouseEventArgs e)
        {
            image1.Source = new BitmapImage(new Uri("/Resources/lip.webp", UriKind.RelativeOrAbsolute));
            image1.Visibility = Visibility.Visible;

            hy1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275483");

            
        }


        private void bwc_MouseEnter(object sender, MouseEventArgs e)
        {
            image1.Source = new BitmapImage(new Uri("/Resources/bwc.jpg", UriKind.RelativeOrAbsolute));
            image1.Visibility = Visibility.Visible;

            hy1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=274892");
        }

        private void bm_MouseEnter(object sender, MouseEventArgs e)
        {
            image1.Source = new BitmapImage(new Uri("/Resources/bm.jpg", UriKind.RelativeOrAbsolute));
            image1.Visibility = Visibility.Visible;

            hy1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275486");
        }

        private void top_MouseEnter(object sender, MouseEventArgs e)
        {
            image1.Source = new BitmapImage(new Uri("/Resources/top.webp", UriKind.RelativeOrAbsolute));
            image1.Visibility = Visibility.Visible;

            hy1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275432");
        }

        private void rs_MouseEnter(object sender, MouseEventArgs e)
        {
            image1.Source = new BitmapImage(new Uri("/Resources/rs.jpg", UriKind.RelativeOrAbsolute));
            image1.Visibility = Visibility.Visible;

            hy1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275427");
        }

        private void eight_MouseEnter(object sender, MouseEventArgs e)
        {
            image1.Source = new BitmapImage(new Uri("/Resources/82.jpg", UriKind.RelativeOrAbsolute));
            image1.Visibility = Visibility.Visible;

            hy1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275370");
        }

        private void tpc_MouseEnter(object sender, MouseEventArgs e)
        {
            image1.Source = new BitmapImage(new Uri("/Resources/tpc.jpg", UriKind.RelativeOrAbsolute));
            image1.Visibility = Visibility.Visible;

            hy1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275313");
        }
    }
}
