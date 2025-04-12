using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelf.Core.Requests
{
    public abstract class Request
    {
        public string UserId { get; set; } = "ale@hotmail.com";
    }
}
