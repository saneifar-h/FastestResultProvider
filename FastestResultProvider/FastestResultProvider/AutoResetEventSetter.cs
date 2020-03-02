using System.Threading;

namespace FastestResultProvider
{
    internal class AutoResetEventSetter
    {
        private readonly AutoResetEvent _autoResetEvent;
        private readonly object _lockObject = new object();
        private bool _isSet;

        public AutoResetEventSetter(AutoResetEvent autoResetEvent)
        {
            _autoResetEvent = autoResetEvent;
        }

        public void Set()
        {
            lock (_lockObject)
            {
                if (_isSet) return;
                _autoResetEvent.Set();
                _isSet = true;
            }
        }
    }
}