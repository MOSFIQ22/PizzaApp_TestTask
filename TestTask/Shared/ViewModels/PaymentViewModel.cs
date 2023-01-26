using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Shared.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }
        [Required, Column(TypeName = "datetime")]
        public DateTime PaymentTime { get; set; }
       
        public int PaymentMethodId { get; set; }

       
        public int OrderId { get; set; }
        public string PaidThrough { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = default!;
    }
}
