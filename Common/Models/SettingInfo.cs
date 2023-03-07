
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MouseClickSender.Common.Models
{
    public class SettingInfo : BaseDto
    {
        public SettingInfo()
        {
        }

        private string coordinateX;
        private string coordinateY;
        private string cntervalsHH;
        private string cntervalsMM;
        private string cntervalsSS;
        private string timing;
        private bool isTiming;
        private bool isLoop;


        public string CoordinateX { get => coordinateX; set { coordinateX = value; OnPropertyChanged(); } }
        public string CoordinateY
        {
            get => coordinateY; set { coordinateY = value; OnPropertyChanged(); }
        }
        public string CntervalsHH { get => cntervalsHH; set { cntervalsHH = value; OnPropertyChanged(); } }
        public string CntervalsMM { get => cntervalsMM; set { cntervalsMM = value; OnPropertyChanged(); } }
        public string CntervalsSS { get => cntervalsSS; set { cntervalsSS = value; OnPropertyChanged(); } }
        public string Timing { get => timing; set { timing = value; OnPropertyChanged(); } }
        public bool IsTiming { get => isTiming; set { isTiming = value; OnPropertyChanged(); } }
        public bool IsLoop { get => isLoop; set { isLoop = value; OnPropertyChanged(); } }


    }
}
