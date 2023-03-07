using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MouseClickSender.Common.Models
{
    public class SettingInfo
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


        public string CoordinateX { get => coordinateX; set => coordinateX = value; }
        public string CoordinateY { get => coordinateY; set => coordinateY = value; }
        public string CntervalsHH { get => cntervalsHH; set => cntervalsHH = value; }
        public string CntervalsMM { get => cntervalsMM; set => cntervalsMM = value; }
        public string CntervalsSS { get => cntervalsSS; set => cntervalsSS = value; }
        public string Timing { get => timing; set => timing = value; }
        public bool IsTiming { get => isTiming; set => isTiming = value; }
        public bool IsLoop { get => isLoop; set => isLoop = value; }

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
                var json = JsonConvert.SerializeObject(this, Formatting.Indented);
                await File.WriteAllTextAsync(filename, json);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("保存配置文件出错！", e);
            }
        }
    }
}
