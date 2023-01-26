using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.Server.Repositories.Interfaces;
using TestTask.Shared.Models;

namespace TestTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepo<PizzaSpecial> repo;
        public PizzasController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = this.unitOfWork.GetRepo<PizzaSpecial>();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaSpecial>>> GetPizzaSpecials()
        {
            var data = await this.repo.GetAllAsync();
            return Ok(data.ToList());
        }
    }
}
