using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Utils.Options
{
       internal class SwaggerOptions
        {
            public string SwaggerDocName { get; set; }
            public string Title { get; set; }
            public string Version { get; set; }
            public string Description { get; set; }
            public Contact Contact { get; set; }
            public string SwaggerEndPoint { get; set; }
            public string EndPointName { get; set; }
        }

        internal class Contact
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }
    
}
