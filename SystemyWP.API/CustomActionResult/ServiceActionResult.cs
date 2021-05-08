using System;
using System.Collections.Generic;

namespace SystemyWP.API.CustomActionResult
{
    public class ServiceActionResult
    {
        public bool Success { get; set; }
        public string Comment { get; set; }
        public List<Exception> Exceptions { get; set; }
    }
}