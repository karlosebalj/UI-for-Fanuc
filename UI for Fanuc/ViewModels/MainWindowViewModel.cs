using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIforFanuc.PlcService;

namespace UI_for_Fanuc.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public string IpAddress
        {
            get { return _ipAddress; }
            set { Set(ref _ipAddress, value); }
        }
        private string _ipAddress;

        public bool MinusA1Status
        {
            get { return _MinusA1Status; }
            set { Set(ref _MinusA1Status, value); }
        }
        private bool _MinusA1Status;

        public double JogA1
        {
            get { return _JogA1; }
            set { Set(ref _JogA1, value); }
        }
        private double _JogA1;

        public double JogA2
        {
            get { return _JogA2; }
            set { Set(ref _JogA2, value); }
        }
        private double _JogA2;

        public double JogA3
        {
            get { return _JogA3; }
            set { Set(ref _JogA3, value); }
        }
        private double _JogA3;

        public double JogA4
        {
            get { return _JogA4; }
            set { Set(ref _JogA4, value); }
        }
        private double _JogA4;

        public double JogA5
        {
            get { return _JogA5; }
            set { Set(ref _JogA5, value); }
        }
        private double _JogA5;

        public double JogA6
        {
            get { return _JogA6; }
            set { Set(ref _JogA6, value); }
        }
        private double _JogA6;

        public ConnectionStates ConnectionState
        {
            get { return _connectionState; }
            set { Set(ref _connectionState, value); }
        }
        private ConnectionStates _connectionState;

        public TimeSpan ScanTime
        {
            get { return _scanTime; }
            set { Set(ref _scanTime, value); }
        }
        private TimeSpan _scanTime;

        public ICommand ConnectCommand { get; private set; }

        public ICommand DisconnectCommand { get; private set; }

        public ICommand StartCommand { get; private set; }

        public ICommand StopCommand { get; private set; }

        public ICommand MinusA1Command { get; private set; }

        public ICommand PlusA1Command { get; private set; }

        public ICommand MinusA2Command { get; private set; }

        public ICommand PlusA2Command { get; private set; }

        public ICommand MinusA3Command { get; private set; }

        public ICommand PlusA3Command { get; private set; }

        public ICommand MinusA4Command { get; private set; }

        public ICommand PlusA4Command { get; private set; }

        public ICommand MinusA5Command { get; private set; }

        public ICommand PlusA5Command { get; private set; }

        public ICommand MinusA6Command { get; private set; }

        public ICommand PlusA6Command { get; private set; }

        S7PlcService _plcService;

        public MainWindowViewModel()
        {
            _plcService = new S7PlcService();
            ConnectCommand = new RelayCommand(Connect);
            DisconnectCommand = new RelayCommand(Disconnect);
            StartCommand = new RelayCommand(async () => { await Start(); });
            StopCommand = new RelayCommand(async () => { await Stop(); });

            MinusA1Command = new RelayCommand(async () => { await MinusA1(); });
            PlusA1Command = new RelayCommand(async () => { await PlusA1(); });

            MinusA2Command = new RelayCommand(async () => { await MinusA2(); });
            PlusA2Command = new RelayCommand(async () => { await PlusA2(); });

            MinusA3Command = new RelayCommand(async () => { await MinusA3(); });
            PlusA3Command = new RelayCommand(async () => { await PlusA3(); });

            MinusA4Command = new RelayCommand(async () => { await MinusA4(); });
            PlusA4Command = new RelayCommand(async () => { await PlusA4(); });

            MinusA5Command = new RelayCommand(async () => { await MinusA5(); });
            PlusA5Command = new RelayCommand(async () => { await PlusA5(); });

            MinusA6Command = new RelayCommand(async () => { await MinusA6(); });
            PlusA6Command = new RelayCommand(async () => { await PlusA6(); });

            MinusA1Status = _plcService.MinusA1Status;

            IpAddress = "192.168.0.2";

            // uvijek refresha vrijednosti na nulu kada se pokrene sučelje 
            OnPlcServiceValuesRefreshed(null, null);  
            _plcService.ValuesRefreshed += OnPlcServiceValuesRefreshed;
        }

        private void OnPlcServiceValuesRefreshed(object sender, EventArgs e)
        {
            ConnectionState = _plcService.ConnectionState;
            JogA1 = _plcService.JogA1;
            JogA2 = _plcService.JogA2;
            JogA3 = _plcService.JogA3;
            JogA4 = _plcService.JogA4;
            JogA5 = _plcService.JogA5;
            JogA6 = _plcService.JogA6;
            ScanTime = _plcService.ScanTime;
            
        }

        private void Status(object sender, EventArgs e)
        {
            MinusA1Status = _plcService.MinusA1Status;
        }


        private void Connect()
        {
            _plcService.Connect(IpAddress, 0, 1);
        }

        private void Disconnect()
        {
            _plcService.Disconnect();
        }

        private async Task Start()
        {
            await _plcService.WriteStart();
        }

        private async Task Stop()
        {
            await _plcService.WriteStop();
        }

        private async Task MinusA1()
        {
            await _plcService.MinusA1();
        }

        private async Task PlusA1()
        {
            await _plcService.PlusA1();
        }

        private async Task MinusA2()
        {
            await _plcService.MinusA2();
        }
        private async Task PlusA2()
        {
            await _plcService.PlusA2();
        }

        private async Task MinusA3()
        {
            await _plcService.MinusA3();
        }

        private async Task PlusA3()
        {
            await _plcService.PlusA3();
        }

        private async Task MinusA4()
        {
            await _plcService.MinusA4();
        }

        private async Task PlusA4()
        {
            await _plcService.PlusA4();
        }

        private async Task MinusA5()
        {
            await _plcService.MinusA5();
        }
        private async Task PlusA5()
        {
            await _plcService.PlusA5();
        }
        private async Task MinusA6()
        {
            await _plcService.MinusA6();
        }

        private async Task PlusA6()
        {
            await _plcService.PlusA6();
        }



    }


}
