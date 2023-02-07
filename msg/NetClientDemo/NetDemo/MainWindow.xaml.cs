using CommonLib.Network;
using NetDemo.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;


namespace NetDemo
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, INetworkEventListener
    {
        public MainWindow()
        {
            InitializeComponent();

            NetworkProxy.Get.RegisterEventListener(this);
            Unloaded += (s, e) =>
            {
                NetworkProxy.Get.UnregisterEventListener(this);
            };

            // ondestroy windows
        }

        public void AddLogMessage(string msg)
        {
            
        }


        #region button event
        
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            
            NetworkProxy.Get.ConnectToServer("127.0.0.1", 6767);
            // ConnectButton을 누르면 NetworkProxy에 있는 ConnectToServer 메소드를 이용해서
            // 127.0.0.1 ip의 6767포트를 접속
            
            // success
        }
        #endregion

        #region system event
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            
        }

        #endregion

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.RowDefinitions.Clear();
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new SignUp());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.RowDefinitions.Clear();
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new Login());
        }

        public void OnEventReceived(string msg)
        {
            AddLogMessage(msg);
        }
    }
}
