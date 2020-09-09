﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.Errors
{
    public class RestException : Exception
    {
        public HttpStatusCode Code { get; }
        public object Error { get; }
        public RestException(HttpStatusCode code, object error = null)
        {
            Code = code;
            Error = error;
        }

  
    }
}
