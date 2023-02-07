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
    /// inven.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class inven : UserControl
    {
        public inven()
        {
            InitializeComponent();
            
            inven_main1.Children.Clear();
            inven_main1.Children.Add(new bu_me1());

            inven_main2.Children.Clear();
            inven_main2.Children.Add(new bu_me2());
        }

        private void Hyperlink_RequestNavigate(Object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            hyp01.TextDecorations = TextDecorations.Underline;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            hyp01.TextDecorations = null;
        }


    }
}
