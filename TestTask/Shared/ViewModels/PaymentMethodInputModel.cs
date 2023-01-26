using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Shared.Models;

namespace TestTask.Shared.ViewModels
{
    
    public class PaymentMethodInputModel
    {
        public int PaymentMethodId { get; set; }
        [Required, StringLength(50)]
        public string PaymentMethodName { get; set; } = default!;
        public bool Active { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
