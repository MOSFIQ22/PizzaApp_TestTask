using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TestTask.Server.Repositories.Interfaces;
using TestTask.Shared.Models;

namespace TestTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepo<PaymentMethod> repo;
        public PaymentMethodsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = this.unitOfWork.GetRepo<PaymentMethod>();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            var data = await this.repo.GetAllAsync();
            return Ok(data.ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(int id)
        {
            var data = await this.repo.GetAsync(x=> x.PaymentMethodId == id);
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            await this.repo.AddAsync(paymentMethod);
            await this.unitOfWork.CompleteAsync();
            return Ok(paymentMethod);
        }
        [HttpPut]
        public async Task<ActionResult> PutPaymentMethod(PaymentMethod paymentMethod)
        {
            await this.repo.UpdateAsync(paymentMethod);
            await this.unitOfWork.CompleteAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentMethod(int id)
        {
            var existing = await this.repo.GetAsync(x => x.PaymentMethodId == id);
            if (existing == null) return NotFound();
            await this.repo.DeleteAsync(existing);
            await this.unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
