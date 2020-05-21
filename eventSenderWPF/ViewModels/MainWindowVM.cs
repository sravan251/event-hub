

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Commands;
using Newtonsoft.Json;
using eventSenderWPF.Models;
using eventSender;
namespace eventSenderWPF.VM
{
    public class MainWindowVM : BindableBase
    {
        private int _counterCappuccino;
        private int _counterEspresso;

        private string _city;
        private string _serialNumber;
        public ICommand MakeCappuccinoCommand { get; }
        public ICommand MakeEspressoCommand { get; }

        private DispatcherTimer _dispatcherTimer;

        public ObservableCollection<string> Logs { get; }

        public IEventSender _eventSender;
        public MainWindowVM(IEventSender eventSender)
        {
            _eventSender = eventSender;
            _serialNumber = Guid.NewGuid().ToString().Substring(0, 8);
            MakeCappuccinoCommand = new DelegateCommand(MakeCappuccion);
            MakeEspressoCommand = new DelegateCommand(MakeEspresson);
            Logs = new ObservableCollection<string>();
            _dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            CoffeeMachineData beanLevelData=CreateCoffeMachineData(nameof(BeanLevel),BeanLevel);
            CoffeeMachineData boilerTempData=CreateCoffeMachineData(nameof(BoilerTemp),BoilerTemp);
            await SendDataAsync(new CoffeeMachineData[] {boilerTempData,beanLevelData});             
        }
        
        private async void MakeCappuccion()
        {
            CounterCappuccino++;
            CoffeeMachineData coffeeMachineData = CreateCoffeMachineData(nameof(CounterCappuccino), CounterCappuccino);
            await SendDataAsync(coffeeMachineData);
        }

        private async void MakeEspresson()
        {
            CounterEspresso++;
            CoffeeMachineData coffeeMachineData = CreateCoffeMachineData(nameof(CounterEspresso), CounterEspresso);
            await SendDataAsync(coffeeMachineData);

        }
        public int CounterCappuccino
        {
            get { return _counterCappuccino; }
            set
            {
                _counterCappuccino = value;
                RaisePropertyChanged();
            }
        }

        public int CounterEspresso
        {
            get { return _counterEspresso; }
            set
            {
                _counterEspresso = value;
                RaisePropertyChanged();
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged();
            }
        }
        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                _serialNumber = value;
                RaisePropertyChanged();
            }
        }

        private int _boilerTemp;
        public int BoilerTemp
        {
            get { return _boilerTemp; }
            set
            {
                _boilerTemp = value;
                RaisePropertyChanged();
            }
        }
        private int _beanLevel;
        public int BeanLevel
        {
            get { return _beanLevel; }
            set
            {
                _beanLevel = value;
                RaisePropertyChanged();
            }
        }
        private bool _isSendingPeriodically;
        public bool IsSendingPeriodically
        {
            get { return _isSendingPeriodically; }
            set
            {
                if (_isSendingPeriodically != value)
                {
                    _isSendingPeriodically = value;
                    if (_isSendingPeriodically)
                    {
                        _dispatcherTimer.Start();
                    }
                    else
                    {
                        _dispatcherTimer.Stop();
                    }

                    RaisePropertyChanged();
                }
            }
        }
        private CoffeeMachineData CreateCoffeMachineData(string sensorType, int sensorValue)
        {
            var coffeeMachineData = new CoffeeMachineData
            {
                City = City,
                SerialNumber = SerialNumber,
                SensorType = sensorType,
                SensorValue = sensorValue,
                RecordingTime = DateTime.Now
            };
            return coffeeMachineData;
        }
        private async Task SendDataAsync(CoffeeMachineData coffeeMachineData)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(coffeeMachineData);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                await _eventSender.SendDataAsync(data);
                LogMessage($"Sent Data{coffeeMachineData.ToString()}");
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }
        }

         private async Task SendDataAsync(IEnumerable<CoffeeMachineData> coffeeMachineDatas)
        {
            try
            {
                var jsonDatas = coffeeMachineDatas.Select(coffeeMachineData=>JsonConvert.SerializeObject(coffeeMachineData));
                var data = jsonDatas.Select(jsonData=>Encoding.UTF8.GetBytes(jsonData));
                await _eventSender.SendDataAsync(data);
                Parallel.ForEach(coffeeMachineDatas,(coffeeMachineData)=>{
                    LogMessage($"Sent Data{coffeeMachineData.ToString()}");
                });
               // coffeeMachineDatas.Select(coffeeMachineData=>LogMessage($"Sent Data{coffeeMachineData.ToString()}"));
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }
        }

        private void LogMessage(string message)
        {            
            Application.Current.Dispatcher.BeginInvoke(new Action(() => Logs.Insert(0, message)));

        }
    }
}