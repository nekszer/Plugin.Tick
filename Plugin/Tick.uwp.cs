using System;

namespace Plugin.Tick
{
    /// <summary>
    /// Interface for Tick
    /// </summary>
    public class TickImplementation : ITick
    {
        public event EventHandler Tick;

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}