using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TestTask.Server.Repositories.Interfaces;
using TestTask.Shared.Models;
using TestTask.Shared.ViewModels;

namespace TestTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepo<Payment> repo;
        public PaymentsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = this.unitOfWork.GetRepo<Payment>();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var data = await this.repo.GetAllAsync();
            return Ok(data.ToList());
        }
        /// <summary>
        /// Custom
        /// </summary>
        [HttpGet("VM")]        
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentViewModels()
        {
            var data = await this.repo
                .GetAllAsync(x => x.Include(c => c.Order).ThenInclude(o=> o.Customer)
                .Include(x=> x.PaymentMethod));
            return Ok(data.Select(p=>new PaymentViewModel
            {
                PaymentId=p.PaymentId,
                PaymentMethodId=p.PaymentMethodId,
                OrderId=p.OrderId,
                CustomerName= p.Order.Customer.CustomerName,
                PaidThrough=p.PaymentMethod.PaymentMethodName,
                PaymentTime=p.PaymentTime,
                OrderDate=p.Order.OrderDate
            }).ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments(int id)
        {
            var data = await this.repo.GetAsync(x=> x.PaymentId == id);
            return Ok(data);
        }
    }
}
