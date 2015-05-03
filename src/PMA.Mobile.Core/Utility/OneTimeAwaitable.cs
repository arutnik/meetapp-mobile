using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Utility
{
    public class OneTimeAwaitable<T>
    {
        Func<Task<T>> _awaitable;

        Task<T> _runningTask;

        public OneTimeAwaitable(Func<Task<T>> awaitable)
        {
            _awaitable = awaitable;
        }

        public Task<T> Run()
        {
            lock (_awaitable)
            {
                if (_runningTask == null)
                {
                    _runningTask = _awaitable();
                }
            }

            return _runningTask;
        }
    }

    public class OneTimeAwaitable
    {
        Func<Task> _awaitable;

        Task _runningTask;

        public OneTimeAwaitable(Func<Task> awaitable)
        {
            _awaitable = awaitable;
        }

        public Task Run()
        {
            lock (_awaitable)
            {
                if (_runningTask == null)
                {
                    _runningTask = _awaitable();
                }
            }

            return _runningTask;
        }
    }
}
