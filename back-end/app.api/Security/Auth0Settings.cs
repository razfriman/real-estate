using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.api.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class Auth0Settings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Domain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ClientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ClientSecret { get; set; }
    }
}