﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelf.Core.Requests.Transactions
{
    public class GetTransactionById : Request
    {
        public long Id { get; set; }
    }
}
