﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public class ProductCategory : BaseEntity
    {
        //public int Id { get; set; } //récupère par héritage
        public string Category { get; set; }

    }
}
