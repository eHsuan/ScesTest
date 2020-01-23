﻿using System;

namespace SECS
{
    public sealed class SecsException : Exception
    {
        public SecsMessage SecsMsg { get; }

        public SecsException(SecsMessage msg, string description)
            : base(description)
        {
            SecsMsg = msg;
        }

        public SecsException(string msg)
            : this(null, msg)
        {
        }
    }
}