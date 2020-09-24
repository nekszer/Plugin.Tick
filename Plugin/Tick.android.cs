using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plugin.Tick
{
    /// <summary>
    /// Interface for Tick
    /// </summary>
    public class TickImplementation : ITick
    {
		private static TickedMessage TickMessage { get; set; } = new TickedMessage();
		private static CancelledMessage CancelMessage { get; set; } = new CancelledMessage();
		private static StartLongRunningTaskMessage StartLongRunningTask { get; set; } = new StartLongRunningTaskMessage();
		private static StopLongRunningTaskMessage StopLongRunningTaskMessage { get; set; } = new StopLongRunningTaskMessage();

		public event EventHandler Tick;

		/// <summary>
		/// 
		/// </summary>
		public static void Init()
		{
			MessagingCenter.Instance.Subscribe<StartLongRunningTaskMessage>(StartLongRunningTask, "StartLongRunningTaskMessage", message => {
				var intent = new Intent(Android.App.Application.Context, typeof(LongRunningTaskService));
				try
				{
					Android.App.Application.Context.StopService(intent);
				}
				catch { }

				try
				{
					Android.App.Application.Context.StartService(intent);
				}
				catch { }
			});
			MessagingCenter.Instance.Subscribe<StopLongRunningTaskMessage>(StartLongRunningTask, "StopLongRunningTaskMessage", message => {
				var intent = new Intent(Android.App.Application.Context, typeof(LongRunningTaskService));
				try
				{
					Android.App.Application.Context.StopService(intent);
				}
				catch { }
			});
		}

		public void Start()
        {
			MessagingCenter.Instance.Send(StartLongRunningTask, "StartLongRunningTaskMessage");
			MessagingCenter.Instance.Unsubscribe<TickedMessage>(TickMessage, "TickedMessage");
			MessagingCenter.Instance.Subscribe<TickedMessage>(TickMessage, "TickedMessage", (tick) => Tick?.Invoke(this, EventArgs.Empty));
			MessagingCenter.Instance.Unsubscribe<CancelledMessage>(CancelMessage, "CancelledMessage");
			MessagingCenter.Instance.Subscribe<CancelledMessage>(CancelMessage, "CancelledMessage", (canceled) =>
			{

			});
		}

		public void Stop()
        {
			MessagingCenter.Instance.Send(StopLongRunningTaskMessage, "StopLongRunningTaskMessage");
        }
    }

	[Service]
	internal class LongRunningTaskService : Service
	{
		CancellationTokenSource _cts;

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			_cts = new CancellationTokenSource();

			Task.Run(() => {
				try
				{
					var counter = new TaskCounter();
					counter.RunCounter(_cts.Token).Wait();
				}
				catch (Android.OS.OperationCanceledException)
				{

				}
				finally
				{
					if (_cts.IsCancellationRequested)
					{
						var message = new CancelledMessage();
						Device.BeginInvokeOnMainThread(() => MessagingCenter.Instance.Send(message, "CancelledMessage"));
					}
				}

			}, _cts.Token);

			return StartCommandResult.Sticky;
		}

		public override void OnDestroy()
		{
			if (_cts != null)
			{
				_cts.Token.ThrowIfCancellationRequested();

				_cts.Cancel();
			}
			base.OnDestroy();
		}
	}
}