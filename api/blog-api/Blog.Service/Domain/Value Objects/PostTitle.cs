using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Domain.Value_Objects
{
    public class PostTitle
    {
        public string Value { get; }

        public PostTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException($"{nameof(title)} cannot be null or empty");
            if (title.Length < 10) throw new ArgumentNullException($"{nameof(title)} is too short");
            Value = title;
        }
    }
}
