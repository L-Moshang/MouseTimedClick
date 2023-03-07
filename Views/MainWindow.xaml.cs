using MouseClickSender.Common.Models;
using MouseClickSender.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static MouseClickSender.WinApi.WIN32_API;

namespace MouseClickSender.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
           InitializeComponent();
        }

        private DispatcherTimer timer;

        private void btnPosition_Click(object sender, RoutedEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
            }
            else
            {
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 5);
                timer.Tick += (s1, e1) =>
                {
                    POINT p = new POINT();
                    Point pp = Mouse.GetPosition(e.Source as FrameworkElement);//WPF方法
                    if (GetCursorPos(out p))//API方法
                    {
                        MessageBox.Show(string.Format("GetCursorPos {0},{1}", p.X, p.Y));
                    }
                };

                timer.Start();
            }
        }
    }
}
