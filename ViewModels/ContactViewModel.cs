using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }
        
        public string Message { get; set; }
    }
}
