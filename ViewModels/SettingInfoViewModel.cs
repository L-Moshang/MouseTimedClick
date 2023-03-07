using MouseClickSender.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MouseClickSender.ViewModels
{
    public class SettingInfoViewModel : INotifyDataErrorInfo
    {
        public SettingInfoViewModel() 
        {
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        public bool HasErrors => throw new NotImplementedException();

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
