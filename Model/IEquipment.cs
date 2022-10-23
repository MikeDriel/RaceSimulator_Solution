using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IEquipment : INotifyPropertyChanged
    {
		int Quality { get; set; }
        
        int Performance { get; set; }

        int Speed { get; set; }

        bool IsBroken { get; set; }
    }
}