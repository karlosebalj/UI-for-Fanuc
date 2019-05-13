using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Sharp7;


namespace UIforFanuc.PlcService
{
    public class S7PlcService
    {
        private bool MinusA1Flag = false;
        private bool PlusA1Flag = false;

        private bool MinusA2Flag = false;
        private bool PlusA2Flag = false;

        private bool MinusA3Flag = false;
        private bool PlusA3Flag = false;

        private bool MinusA4Flag = false;
        private bool PlusA4Flag = false;

        private bool MinusA5Flag = false;
        private bool PlusA5Flag = false;

        private bool MinusA6Flag = false;
        private bool PlusA6Flag = false;

        private readonly S7Client _client;
        private readonly System.Timers.Timer _timer;
        private DateTime _lastScanTime;

        private volatile object _locker = new object();
       

        public S7PlcService()
        {
            _client = new S7Client();
            _timer = new System.Timers.Timer();
            _timer.Interval = 100;
            _timer.Elapsed += OnTimerElapsed;
        }

        public ConnectionStates ConnectionState { get; private set; }

        public double JogA1 { get; private set; }
        public double JogA2 { get; private set; }
        public double JogA3 { get; private set; }
        public double JogA4 { get; private set; }
        public double JogA5 { get; private set; }
        public double JogA6 { get; private set; }

        public bool MinusA1Status { get; private set; }

        public TimeSpan ScanTime { get; private set; }

        public event EventHandler ValuesRefreshed;

        public void Connect(string ipAddress, int rack, int slot)
        {
            try
            {
                ConnectionState = ConnectionStates.Connecting;
                int result = _client.ConnectTo(ipAddress, rack, slot);
                if (result == 0)
                {
                    ConnectionState = ConnectionStates.Online;
                    _timer.Start();
                }
                else
                {
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Connection error: " + _client.ErrorText(result));
                    ConnectionState = ConnectionStates.Offline;
                }
                OnValuesRefreshed();
            }
            catch
            {
                ConnectionState = ConnectionStates.Offline;
                OnValuesRefreshed();
                throw;
            }
        }

        public void Disconnect()
        {
            if (_client.Connected)
            {
                _timer.Stop();
                _client.Disconnect();
                ConnectionState = ConnectionStates.Offline;
                OnValuesRefreshed();
            }
        }

        public async Task WriteStart()
        {
            await Task.Run(() =>
            {
                int writeResult = WriteBit("DB1.DBX0.0", true);
                if (writeResult != 0)
                {
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                }
                Thread.Sleep(30);
                writeResult = WriteBit("DB1.DBX0.0", false);
                if (writeResult != 0)
                {
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                }
            });
        }

        public async Task WriteStop()
        {
            await Task.Run(() =>
            {
                int writeResult = WriteBit("DB1.DBX0.1", true);
                if (writeResult != 0)
            
                {
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                }
                Thread.Sleep(30);
                writeResult = WriteBit("DB1.DBX0.1", false);
                if (writeResult != 0)
                {
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                }
            });
        }

        public async Task  MinusA1()
        {
            await Task.Run(() =>
            {
                if (PlusA1Flag == false)

                    {
                    if (MinusA1Flag == false)
                    {
                        MinusA1Flag = true;
                        int writeResult = WriteBit("DB5.DBX0.0", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        MinusA1Flag = false;
                        int writeResult = WriteBit("DB5.DBX0.0", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }

                }

            });
        }

        public async Task PlusA1()
        {
            await Task.Run(() =>
            {

                if (MinusA1Flag == false)
                {
                    if (PlusA1Flag == false)
                    {
                        PlusA1Flag = true;
                        int writeResult = WriteBit("DB5.DBX0.1", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        PlusA1Flag = false;
                        int writeResult = WriteBit("DB5.DBX0.1", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task MinusA2()
        {
            await Task.Run(() =>
            {
                if (PlusA2Flag == false)
                {
                    if (MinusA2Flag == false)
                    {
                        MinusA2Flag = true;
                        int writeResult = WriteBit("DB5.DBX0.2", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        MinusA2Flag = false;
                        int writeResult = WriteBit("DB5.DBX0.2", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task PlusA2()
        {
            await Task.Run(() =>
            {
                if (MinusA2Flag == false)
                {
                    if (PlusA2Flag == false)
                    {
                        PlusA2Flag = true;
                        int writeResult2 = WriteBit("DB5.DBX0.3", true);
                        if (writeResult2 != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult2));
                        }

                    }
                    else
                    {
                        PlusA2Flag = false;
                        int writeResult2 = WriteBit("DB5.DBX0.3", false);
                        if (writeResult2 != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult2));
                        }

                    }
                }
            });
        }

        public async Task MinusA3()
        {
            await Task.Run(() =>
            {
                if (PlusA3Flag == false)
                {
                    if (MinusA3Flag == false)
                    {
                        MinusA3Flag = true;
                        int writeResult = WriteBit("DB5.DBX0.4", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        MinusA3Flag = false;
                        int writeResult = WriteBit("DB5.DBX0.4", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task PlusA3()
        {
            await Task.Run(() =>
            {
                if (MinusA3Flag == false)
                {
                    if (PlusA3Flag == false)
                    {
                        PlusA3Flag = true;
                        int writeResult = WriteBit("DB5.DBX0.5", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        PlusA3Flag = false;
                        int writeResult = WriteBit("DB5.DBX0.5", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task MinusA4()
        {
            await Task.Run(() =>
            {
                if (PlusA4Flag == false)
                {
                    if (MinusA4Flag == false)
                    {
                        MinusA4Flag = true;
                        int writeResult = WriteBit("DB5.DBX0.6", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        MinusA4Flag = false;
                        int writeResult = WriteBit("DB5.DBX0.6", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task PlusA4()
        {
            await Task.Run(() =>
            {
                if (MinusA4Flag == false)
                {
                    if (PlusA4Flag == false)
                    {
                        PlusA4Flag = true;
                        int writeResult = WriteBit("DB5.DBX0.7", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        PlusA4Flag = false;
                        int writeResult = WriteBit("DB5.DBX0.7", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task MinusA5()
        {
            await Task.Run(() =>
            {
                if (PlusA5Flag == false)
                {
                    if (MinusA5Flag == false)
                    {
                        MinusA5Flag = true;
                        int writeResult = WriteBit("DB1.DBX0.3", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        MinusA5Flag = false;
                        int writeResult = WriteBit("DB1.DBX0.3", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task PlusA5()
        {
            await Task.Run(() =>
            {
                if (MinusA5Flag == false)
                {
                    if (PlusA5Flag == false)
                    {
                        PlusA5Flag = true;
                        int writeResult = WriteBit("DB1.DBX0.4", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        PlusA5Flag = false;
                        int writeResult = WriteBit("DB1.DBX0.4", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task MinusA6()
        {
            await Task.Run(() =>
            {
                if (PlusA6Flag == false)
                {
                    if (MinusA6Flag == false)
                    {
                        MinusA6Flag = true;
                        int writeResult = WriteBit("DB1.DBX0.5", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        MinusA6Flag = false;
                        int writeResult = WriteBit("DB1.DBX0.5", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }

        public async Task PlusA6()
        {
            await Task.Run(() =>
            {
                if (MinusA6Flag == false)
                {
                    if (PlusA6Flag == false)
                    {
                        PlusA6Flag = true;
                        int writeResult = WriteBit("DB1.DBX0.6", true);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                    else
                    {
                        PlusA6Flag = false;
                        int writeResult = WriteBit("DB1.DBX0.6", false);
                        if (writeResult != 0)
                        {
                            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Write error: " + _client.ErrorText(writeResult));
                        }

                    }
                }
            });
        }


        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Stop();
                ScanTime = DateTime.Now - _lastScanTime;
                RefreshValues();
                OnValuesRefreshed();
            }
            finally
            {
                _timer.Start();
            }
            _lastScanTime = DateTime.Now;
        }

        private void RefreshValues()
        {
            lock (_locker)
            {

                var buffer = new byte[24];
                int result = _client.DBRead(2, 0, buffer.Length, buffer);
                if (result == 0)
                {
                    JogA1 = S7.GetRealAt(buffer, 0);
                    JogA2 = S7.GetRealAt(buffer, 4);
                    JogA3 = S7.GetRealAt(buffer, 8);
                    JogA4 = S7.GetRealAt(buffer, 12);
                    JogA5 = S7.GetRealAt(buffer, 16);
                    JogA6 = S7.GetRealAt(buffer, 20);
                }
                else
                {
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Read error: " + _client.ErrorText(result));
                }

           
            }
        }

        private void Status()
        {
            lock (_locker)
            {
                var buffer2 = new byte[2];
                int result2 = _client.DBRead(5, 0, buffer2.Length, buffer2);
                if (result2 == 0)
                {
                    MinusA1Status = S7.GetBitAt(buffer2, 0, 0);

                }
                else
                {
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t Read error: " + _client.ErrorText(result2));
                }
            }
        }


                /// <summary>
                /// Writes a bit at the specified address. Es.: DB1.DBX10.2 writes the bit in db 1, word 10, 3rd bit
                /// </summary>
                /// <param name="address">Es.: DB1.DBX10.2 writes the bit in db 1, word 10, 3rd bit</param>
                /// <param name="value">true or false</param>
                /// <returns></returns>
        private int WriteBit(string address, bool value)
        {
            var strings = address.Split('.');
            int db = Convert.ToInt32(strings[0].Replace("DB", ""));
            int pos = Convert.ToInt32(strings[1].Replace("DBX", ""));
            int bit = Convert.ToInt32(strings[2]);
            return WriteBit(db, pos, bit, value);
        }

        private int WriteBit(int db, int pos, int bit, bool value)
        {
            lock (_locker)
            {
                var buffer = new byte[1];
                S7.SetBitAt(ref buffer, 0, bit, value);
                return _client.WriteArea(S7Consts.S7AreaDB, db, pos + bit, buffer.Length, S7Consts.S7WLBit, buffer);
            }
        }

        private void OnValuesRefreshed()
        {
            ValuesRefreshed?.Invoke(this, new EventArgs());
        }
    }
}
