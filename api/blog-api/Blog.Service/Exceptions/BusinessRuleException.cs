using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public string Details { get; }

        public BusinessRuleException(string message) : base(message)
        {
        }

        public BusinessRuleException(string message, string details)
        {
            this.Details = details;
        }
    }
}
