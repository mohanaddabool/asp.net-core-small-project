using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Models
{
    public class OrderModels
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        [MinLength(4)]
        public string OrderNumber { get; set; }

        public ICollection<OrderItemViewModel> Items { get; set; }
    }
}
