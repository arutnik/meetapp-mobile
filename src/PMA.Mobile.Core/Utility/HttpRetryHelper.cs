using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Utility
{
    public class HttpRetryHelper<TResult>
    {
        Dictionary<int, Func<FlurlHttpException, bool>> _handlers = new Dictionary<int, Func<FlurlHttpException, bool>>();
        Dictionary<int, Func<FlurlHttpException, TResult>> _resolutions = new Dictionary<int, Func<FlurlHttpException, TResult>>();
        Func<Exception, bool> _catchAllHandler = null;
        Func<Exception, TResult> _catchAllResolution = null;
        Func<Task<TResult>> _method;

        int _retries;

        TimeSpan _sleep = TimeSpan.FromMilliseconds(0);

        public HttpRetryHelper(int maxRetries)
        {
            _retries = maxRetries;
        }

        public HttpRetryHelper(int maxRetries, TimeSpan sleep) : this(maxRetries)
        {
            _sleep = sleep;
        }

        /// <summary>
        /// Sets the method that will be run
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public HttpRetryHelper<TResult> Method(Func<Task<TResult>> method)
        {
            _method = method;
            return this;
        }

        /// <summary>
        /// Sets a handler for a response code
        /// </summary>
        /// <param name="responseCode"></param>
        /// <param name="keepRetrying"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public HttpRetryHelper<TResult> OnCode(int responseCode, Func<FlurlHttpException, bool> keepRetrying, Func<FlurlHttpException, TResult> resolution)
        {
            _handlers[responseCode] = keepRetrying;
            _resolutions[responseCode] = resolution;
            return this;
        }


        public HttpRetryHelper<TResult> OnAnyOtherError(Func<Exception, bool> keepRetrying, Func<Exception, TResult> resolution)
        {
            _catchAllHandler = keepRetrying;
            _catchAllResolution = resolution;
            return this;
        }

        /// <summary>
        /// Runs the method, retrying on exceptions, using
        /// the configured exception handlers.
        /// </summary>
        /// <returns>True if method could complete without excpetions, false otherwise.</returns>
        public async Task<TResult> Execute()
        {
            int triesLeft = _retries + 1;

            while (triesLeft > 0)
            {
                Exception generalException = null;
                try
                {
                    return await _method();
                }
                catch (FlurlHttpException fex)
                {
                    --triesLeft;

                    Func<FlurlHttpException, bool> handler;
                    if (fex.Call.Response != null && _handlers.TryGetValue((int)fex.Call.Response.StatusCode, out handler))
                    {
                        if (!handler(fex))
                        {
                            return _resolutions[(int)fex.Call.Response.StatusCode](fex);
                        }
                    }
                    else
                    {
                        generalException = fex;
                    }
                    
                }
                catch (Exception ex)
                {
                    --triesLeft;

                    generalException = ex;
                }

                if (generalException != null && _catchAllHandler != null 
                    && !_catchAllHandler(generalException))
                {
                    return _catchAllResolution(generalException);
                }

                if (triesLeft > 0)
                {
                    await Task.Delay(_sleep);
                }

                if (triesLeft <= 0 && _catchAllResolution != null)
                {
                    return _catchAllResolution(generalException);
                }

                if (triesLeft <= 0)
                {
                    break;
                }
                
            }
            return default(TResult);
        }
    }
}
