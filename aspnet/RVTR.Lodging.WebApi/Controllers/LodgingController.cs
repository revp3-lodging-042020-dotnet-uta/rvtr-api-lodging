using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [ApiVersion("0.0")]
    [EnableCors("public")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LodgingController : ControllerBase
    {
        private readonly ILogger<LodgingController> _logger;
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="unitOfWork"></param>
        public LodgingController(ILogger<LodgingController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Lodging.DeleteAsync(id);
            if (obj == null) return NotFound();
            return Ok(obj);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Lodging.GetAsync());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _unitOfWork.Lodging.GetAsync(id);
            if (obj == null) return NotFound();
            return Ok(obj);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lodging"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(LodgingModel lodging)
        {
            if (lodging == null) return BadRequest();

            return Ok(await _unitOfWork.Lodging.InsertAsync(lodging));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lodging"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(LodgingModel lodging)
        {
            _unitOfWork.Lodging.Update(lodging);

            return Accepted(lodging);
        }
    }
}
