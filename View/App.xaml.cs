using DataAcces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public double _cashOnHand { get; set; }
        public int _result { get; set; }
        public bool _staffShift { get; set; }
        public Staff _staffInfo { get; set; }
    }
}
