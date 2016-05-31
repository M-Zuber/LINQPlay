﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQPLay
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
