﻿using System;

namespace SystemyWP.API.CustomExceptions
{
    public class PublicException : Exception
    {
        public PublicException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
