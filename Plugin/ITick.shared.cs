using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plugin.Tick
{

	internal class StartLongRunningTaskMessage { }
	internal class StopLongRunningTaskMessage { }
	internal class TickedMessage
	{
		public int Seconds { get; set; }
	}

	internal class CancelledMessage
	{

	}

	internal class TaskCounter
	{
		public async Task RunCounter(CancellationToken token)
		{
			await Task.Run(async () => {
				while (true)
				{
					token.ThrowIfCancellationRequested();
					await Task.Delay(1000);
					var message = new TickedMessage
					{
						Seconds = 1
					};
					Device.BeginInvokeOnMainThread(() => MessagingCenter.Instance.Send(message, "TickedMessage"));
				}
			}, token);
		}
	}

	public interface ITick
    {
		event EventHandler Tick;
        void Start();
        void Stop();
    }
}