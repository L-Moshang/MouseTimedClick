using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseClickSender.Common.Models
{
    public class MainWindowInfo : BaseDto
    {
        public MainWindowInfo()
        {
            StrRun = "开启任务";
            StrPosition = "开始定位";
            StrCountdown = "--:--:--";
        }

        string strRun;
        string strPosition;
        string strCountdown;

        public string StrRun { get => strRun; set { strRun = value; OnPropertyChanged(); } }
        public string StrPosition { get => strPosition; set { strPosition = value; OnPropertyChanged(); } }
        public string StrCountdown { get => strCountdown; set { strCountdown = value; OnPropertyChanged(); } }
    }
}
