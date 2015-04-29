using PMA.Mobile.Core.Models.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Interfaces.Servers
{
    /// <summary>
    /// Defines all endpoints for communicating with the PMA app server.
    /// </summary>
    public interface IPmaAppServerService
    {

        Task<PmaAppServerResult<ServerLoginResult>> LogInWithFacebook(string facebookId, string facebookAccessToken, string facebookUserSerialized);

    }

    /// <summary>
    /// Models result of a call to the app server.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PmaAppServerResult<T>
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public T Result { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public PmaAppServerCode Status { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }

    public enum PmaAppServerCode
    {
        Ok = 0,
        ServerNotAvailable,
        ConnectionNotAvailable,
        NotAuthenticated,
        NotAuthorized,
        NotFound,

    }
}
