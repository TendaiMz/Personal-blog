using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Domain.Entities
{
    public class Entity
    {
        public long Id { get; protected set; }
        public DateTime CreationDate { get; protected set; }

    }
}
