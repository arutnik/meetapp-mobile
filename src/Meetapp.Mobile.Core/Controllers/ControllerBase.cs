using Cirrious.MvvmCross.Plugins.Controllers;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Meetapp.Mobile.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetapp.Mobile.Core.Controllers
{
    public abstract class ControllerBase<TViewModel> : MvxController<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        private class Subscription
        {
            public MvxSubscriptionToken Token;
            public Type Type;
            public Action AutoUnsubscribeAction;
        }

        private List<Subscription> _subscriptionTokens = null;

        /// <summary>
        /// Subscribes to an event. The subscription will expire upon the controller
        /// being GCed, or through one of the Unsubscribe methods on this base class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messenger"></param>
        /// <param name="handler"></param>
        protected void SubscribeToEvent<T>(IMvxMessenger messenger, Action<T> deliveryAction) where T : EventBase
        {
            SubscribeInternal<T>(messenger, () => messenger.Subscribe<T>(deliveryAction));
        }

        /// <summary>
        /// Subscribes to an event on main thread. The subscription will expire upon the controller
        /// being GCed, or through one of the Unsubscribe methods on this base class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messenger"></param>
        /// <param name="handler"></param>
        protected void SubscribeToEventOnMainThread<T>(IMvxMessenger messenger, Action<T> deliveryAction) where T : EventBase
        {
            SubscribeInternal<T>(messenger, () => messenger.SubscribeOnMainThread<T>(deliveryAction));
        }

        /// <summary>
        /// Subscribes to an event on background thread. The subscription will expire upon the controller
        /// being GCed, or through one of the Unsubscribe methods on this base class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messenger"></param>
        /// <param name="handler"></param>
        protected void SubscribeToEventOnThreadPoolMainThread<T>(IMvxMessenger messenger, Action<T> deliveryAction) where T : EventBase
        {
            SubscribeInternal<T>(messenger, () => messenger.SubscribeOnThreadPoolThread<T>(deliveryAction));
        }

        private void SubscribeInternal<T>(IMvxMessenger messenger, Func<MvxSubscriptionToken> subscriptionFunc) where T : EventBase
        {
            var subscription = new Subscription()
            {
                Token = subscriptionFunc(),
            };
            subscription.AutoUnsubscribeAction = () => UnsubscribeInternal<T>(messenger, subscription);
            subscription.Type = typeof(T);

            MessageSubscriptions.Add(subscription);
        }

        void UnsubscribeInternal<T>(IMvxMessenger messenger, Subscription subscription) where T : EventBase
        {
            messenger.Unsubscribe<T>(subscription.Token);
            subscription.Token.Dispose();
            MessageSubscriptions.Remove(subscription);
        }

        /// <summary>
        /// Unsubscribe any handlers for this event type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void UnsubscribeFromEvent<T>() where T : EventBase
        {
            foreach (var subscription in MessageSubscriptions.Where(s => s.Type == typeof(T)).ToArray())
            {
                subscription.AutoUnsubscribeAction();
            }
        }

        /// <summary>
        /// Unsubscribe all event handlers for all event types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void UnsubscribeFromAllEvents()
        {
            foreach (var subscription in MessageSubscriptions.ToArray())
            {
                subscription.AutoUnsubscribeAction();
            }
        }

        private IList<Subscription> MessageSubscriptions
        {
            get
            {
                if (_subscriptionTokens == null) _subscriptionTokens = new List<Subscription>();

                return _subscriptionTokens;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            UnsubscribeFromAllEvents();
        }
    }
}
