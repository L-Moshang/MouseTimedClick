﻿using System;
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
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace MouseClickSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            tbHH2.Text = DateTime.Now.Hour.ToString();
            tbMM2.Text = DateTime.Now.Minute.ToString();
        }

        //This is a replacement for Cursor.Position in WinForms
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        private DispatcherTimer timer;
        TimeSpan targetSpan;
        TimeSpan timerCounter;



        private void TryStart()
        {
            int targetX, targetY;
            if (!int.TryParse(tbX.Text, out targetX))
            {
                throw new Exception("坐标X 需要为数值类型");
            }

            if (!int.TryParse(tbY.Text, out targetY))
            {
                throw new Exception("坐标Y 需要为数值类型");
            }

            int hh, mm, ss;

            if (!int.TryParse(tbHH.Text, out hh))
            {
                throw new Exception("时间需要为数值类型");
            }

            if (!int.TryParse(tbMM.Text, out mm))
            {
                throw new Exception("时间需要为数值类型t");
            }

            if (!int.TryParse(tbSS.Text, out ss))
            {
                throw new Exception("时间需要为数值类型");
            }

            if (hh < 0 || mm < 0 || ss < 0 || hh >= 60 || mm >= 60 || ss >= 60
                || (hh == 0 && mm == 0 && ss == 0))
            {
                throw new Exception("时间范围异常");
            }


            //间隔模式
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);

            targetSpan = new TimeSpan(hh, mm, ss);
            timerCounter = new TimeSpan(0, 0, 0);

            timer.Tick += (s, e) =>
            {
                timerCounter = timerCounter.Add(new TimeSpan(0, 0, 1));

                var diff = targetSpan.Subtract(timerCounter);
                lblNextClickIn.Content = string.Format("{0:d2}:{1:d2}:{2:d2}", diff.Hours, diff.Minutes, diff.Seconds);

                if (timerCounter.CompareTo(targetSpan) == 0)
                {
                    LeftMouseClick(targetX, targetY);
                    timerCounter = new TimeSpan(0, 0, 0);
                    if (chkWhere.IsChecked != true)
                    {
                        timer.Stop();
                        btnRun.Content = "开启";
                    }
                }
            };

            timer.Start();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (btnRun.Content.ToString() == "开启")
            {
                try
                {
                    if (chkType.IsChecked == false)
                    {
                        TryStart();
                    }
                    else
                    {
                        DispatcherTimer timer2 = new DispatcherTimer();
                        timer2.Interval = new TimeSpan(0, 1, 0);//
                        timer2.Tick += (s, e) =>
                        {
                            if (tbHH2.Text == DateTime.Now.Hour.ToString() && tbMM2.Text == DateTime.Now.Minute.ToString())
                            {
                                TryStart();
                            }
                        };
                        timer2.Start();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnRun.Content = "关闭";
            }
            else
            {
                if(timer != null)
                {
                    timer.Stop();
                }
                btnRun.Content = "开启";
                lblNextClickIn.Content = "--:--:--";
            }
        }
    }
}