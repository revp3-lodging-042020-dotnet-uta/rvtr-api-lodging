using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
    /// <summary>
    /// Controller handling the lodging HTTP methods
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
        /// Injects the logging service and unit of work repository
        /// for the controller to utilize the different lodging queries
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="unitOfWork"></param>
        public LodgingController(ILogger<LodgingController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delete lodging information by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] LodgingQueryParamsModel queryParams, int id)
        {
            var obj = await _unitOfWork.Lodging.DeleteAsync(id, queryParams);

            if (obj == null) return NotFound();

            await _unitOfWork.CommitAsync();

            return Ok(obj);
        }

        /// <summary>
        /// Get all lodging information.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] LodgingQueryParamsModel queryParams)
        {
              return Ok(await _unitOfWork.Lodging.GetAsync(queryParams));
        }

        /// <summary>
        /// Get lodging information based on ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromQuery] LodgingQueryParamsModel queryParams, int id)
        {
            var obj = await _unitOfWork.Lodging.GetAsync(id, queryParams);
            if (obj == null) return NotFound();
            return Ok(obj);
        }

        /// <summary>
        /// Updates or inserts lodging information.
        /// If there is no existing model with the given ID, then insertion is called.
        /// If there is an existing model with the given ID, then update is called.
        /// </summary>
        /// <param name="lodging"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] LodgingQueryParamsModel queryParams, LodgingModel lodging)
        {
            if (lodging == null) return BadRequest();

            var ExistingEntry = await _unitOfWork.Lodging.GetAsync(lodging.Id, queryParams);

            if (ExistingEntry == null)
            {
              var obj = await _unitOfWork.Lodging.InsertAsync(lodging);

              await _unitOfWork.CommitAsync();

              return Ok(obj);
            }
            else
            {
              _unitOfWork.Lodging.Update(lodging);

              await _unitOfWork.CommitAsync();

              return Ok(lodging);
            }
        }
    }
}
