using LearningProject.Projects.김지원;
using LearningProject.Projects.유동훈;
using LearningProject.Projects.이동길;
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

namespace LearningProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            //WindowState = WindowState.Maximized;
            //ResizeMode = ResizeMode.CanResize;
            
            //Topmost = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            panel_selectList.Visibility = Visibility.Collapsed;
            grid1.Children.Add(new 김지원());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            panel_selectList.Visibility = Visibility.Collapsed;
            grid1.Children.Add(new 유동훈());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            panel_selectList.Visibility = Visibility.Collapsed;
            grid1.Children.Add(new board());
        }
    }
}
