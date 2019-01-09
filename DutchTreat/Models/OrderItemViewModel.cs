﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutchTreat.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal UnitPrice { get; set; }

        public ICollection<OrderItemViewModel> Items { get; set; }

        [Required]
        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
        [Column(TypeName = "decimal(18, 18)")]
        public string ProductTitle { get; set; }
        public string ProductArtist { get; set; }
        public string ProductArtId { get; set; }
    }
}