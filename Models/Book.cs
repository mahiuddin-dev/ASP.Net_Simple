﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudApplication.Models
{
    public class Book
    {
        public int id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string publish { get; set; }
    }
}