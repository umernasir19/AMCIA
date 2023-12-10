using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class ResponseClass
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object ResponseObject { get; set; }
    }
}