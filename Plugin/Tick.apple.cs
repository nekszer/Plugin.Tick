using System;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
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
		private static LongRunningTaskService LongRunningTaskService { get; set; }

		public event EventHandler Tick;

		public static void Init()
		{
			MessagingCenter.Instance.Subscribe<StartLongRunningTaskMessage>(StartLongRunningTask, "StartLongRunningTaskMessage", async message => {
				if(LongRunningTaskService == null) LongRunningTaskService = new LongRunningTaskService();
				LongRunningTaskService?.Stop();
				await LongRunningTaskService.Start();
			});
			MessagingCenter.Instance.Subscribe<StopLongRunningTaskMessage>(StartLongRunningTask, "StopLongRunningTaskMessage", message => {
				LongRunningTaskService?.Stop();
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

	internal class LongRunningTaskService
	{
		nint _taskId;
		CancellationTokenSource _cts;

		public async Task Start()
		{
			_cts = new CancellationTokenSource();
			_taskId = UIApplication.SharedApplication.BeginBackgroundTask("LongRunningTask", OnExpiration);
			try
			{
				var counter = new TaskCounter();
				await counter.RunCounter(_cts.Token);

			}
			catch (OperationCanceledException)
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
			UIApplication.SharedApplication.EndBackgroundTask(_taskId);
		}

		public void Stop()
		{
			_cts.Cancel();
		}

		void OnExpiration()
		{
			_cts.Cancel();
		}
	}
}
