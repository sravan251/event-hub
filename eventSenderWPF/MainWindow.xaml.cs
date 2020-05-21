using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
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
using eventSenderWPF.VM;
using eventSender;

namespace eventSenderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string eventHubconnectionString=ConfigurationManager.AppSettings["eventHubconnectionString"].ToString();
            DataContext=new MainWindowVM(new EventHubDataSender(eventHubconnectionString));
        }
    }
}
