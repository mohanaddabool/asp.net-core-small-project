using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Models
{
    public class ContactModels
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required]
        [MaxLength(250)]
        public string Message { get; set; }
    }
}
