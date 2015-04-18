using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMA.Mobile.Core.Framework.Commands
{
    /// <summary>
    /// A commad interface that exposes
    /// an awaitable execution pattern.
    /// </summary>
    public interface IAsyncCommand : IAsyncCommand<object>
    {
    }

    /// <summary>
    /// A commad interface that exposes
    /// an awaitable execution pattern.
    /// </summary>
    public interface IAsyncCommand<in T>
    {
        Task ExecuteAsync(T obj);
        bool CanExecute(object obj);
        ICommand Command { get; }

        void RaiseCanExecuteChanged();
    }
}
