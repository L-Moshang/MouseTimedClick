using MouseClickSender.Common.Models;
using MouseClickSender.WinApi;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MouseClickSender.ViewModels
{
    public class SettingInfoViewModel : BindableBase
    {
        public SettingInfoViewModel()
        {
            SettingInfo = FromJsonFile("appsettings.json").GetAwaiter().GetResult();
            MainWindowInfo = new MainWindowInfo();
            BtnRunCommand = new DelegateCommand(BtnRun);
        }

        private SettingInfo settingInfo;
        public SettingInfo SettingInfo { get => settingInfo; set { settingInfo = value; RaisePropertyChanged(); } }


        private MainWindowInfo mainWindowInfo;
        public MainWindowInfo MainWindowInfo { get => mainWindowInfo; set { mainWindowInfo = value; RaisePropertyChanged(); } }



        private DispatcherTimer timer;
        TimeSpan targetSpan;
        TimeSpan timerCounter;

        public DelegateCommand BtnRunCommand { get; set; }
        public DelegateCommand<RoutedEventArgs> BtnPositionCommand { get; set; }

        private async void BtnRun()
        {

            if (MainWindowInfo.StrRun == "开启任务")
            {
                try
                {
                    if (SettingInfo.IsTiming == false)
                    {
                        TryStart();
                    }
                    else
                    {
                        DispatcherTimer timer2 = new DispatcherTimer();
                        timer2.Interval = new TimeSpan(0, 1, 0);
                        timer2.Tick += (s, e) =>
                        {
                            if (SettingInfo.Timing.Split(":")[0] == DateTime.Now.Hour.ToString() && SettingInfo.Timing.Split(":")[1] == DateTime.Now.Minute.ToString())
                            {
                                TryStart();
                            }
                        };
                        timer2.Start();
                        MessageBox.Show(SettingInfo.Timing);
                    }
                    await SaveConfig("appsettings.json");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MainWindowInfo.StrRun = "关闭任务";
            }
            else
            {
                if (timer != null)
                {
                    timer.Stop();
                }

                MainWindowInfo.StrRun = "开启任务";
                MainWindowInfo.StrCountdown = "--:--:--";
            }

        }


        private void TryStart()
        {
            int targetX, targetY;
            if (!int.TryParse(SettingInfo.CoordinateX, out targetX))
            {
                throw new Exception("坐标X 需要为数值类型");
            }

            if (!int.TryParse(SettingInfo.CoordinateY, out targetY))
            {
                throw new Exception("坐标Y 需要为数值类型");
            }

            int hh, mm, ss;

            if (!int.TryParse(SettingInfo.CntervalsHH, out hh))
            {
                throw new Exception("时间需要为数值类型");
            }

            if (!int.TryParse(SettingInfo.CntervalsMM, out mm))
            {
                throw new Exception("时间需要为数值类型t");
            }

            if (!int.TryParse(SettingInfo.CntervalsSS, out ss))
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
                MainWindowInfo.StrCountdown = string.Format("{0:d2}:{1:d2}:{2:d2}", diff.Hours, diff.Minutes, diff.Seconds);

                if (timerCounter.CompareTo(targetSpan) == 0)
                {
                    WIN32_API.LeftMouseClick(targetX, targetY);
                    timerCounter = new TimeSpan(0, 0, 0);
                    if (SettingInfo.IsLoop != true)
                    {
                        timer.Stop();
                        MainWindowInfo.StrRun = "开启任务";
                    }
                }
            };

            timer.Start();
        }

        /// <summary>
        /// 从json文件加载配置文件
        /// </summary>
        /// <param name="filename">配置文件名</param>
        /// <returns>配置实例对象</returns>
        public static async Task<SettingInfo> FromJsonFile(string filename)
        {
            try
            {
                var json = await File.ReadAllTextAsync(filename);
                return JsonConvert.DeserializeObject<SettingInfo>(json);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("加载配置文件出错！", e);
            }
        }

        /// <summary>
        /// 保存当前配置实例对象到文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public async Task SaveConfig(string filename)
        {
            try
            {
                var json = JsonConvert.SerializeObject(SettingInfo, Formatting.Indented);
                await File.WriteAllTextAsync(filename, json);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("保存配置文件出错！", e);
            }
        }
    }
}
