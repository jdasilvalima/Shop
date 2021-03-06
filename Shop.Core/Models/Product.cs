﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public class Product : BaseEntity
    {
        //public int Id { get; set; } //BaseEntity

        [StringLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 500)]
        public double Price { get; set; } //decimal
        public string Category { get; set; }
        public string Image { get; set; }
    }
}
