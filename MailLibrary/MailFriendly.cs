﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailLibrary
{
    public class MailFriendly
    {
        /// <summary>
        /// Valid email address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Non-empty string for displaying in an email
        /// </summary>
        public string Display { get; set; }
        /// <summary>
        /// Show Display and address together
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Address: '{Address}' Display: '{Display}'";

    }

}
