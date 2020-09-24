using LightForms.Commands;
using Plugin.Tick;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TickTest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        #region Notified Property BtnStart
        /// <summary>
        /// BtnStart
        /// </summary>
        private ICommand btnstart;
        public ICommand BtnStart
        {
            get { return btnstart; }
            set { btnstart = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property Chronometer
        /// <summary>
        /// Chronometer
        /// </summary>
        private TimeSpan chronometer = new TimeSpan(0);
        public TimeSpan Chronometer
        {
            get { return chronometer; }
            set { chronometer = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route, object data)
        {
            base.Appearing(route, data);
            CrossTick.Current.Tick += Current_Tick;
            BtnStart = new Command(BtnStart_Command);

            Task.Run(async () => 
            {
                await Task.Delay(5000);
                CrossTick.Current.Stop();
            });
        }

        private void BtnStart_Command(object obj)
        {
            CrossTick.Current.Start();
        }

        private void Current_Tick(object sender, System.EventArgs e)
        {
            Chronometer = Chronometer.Add(TimeSpan.FromSeconds(1));
        }
    }
}