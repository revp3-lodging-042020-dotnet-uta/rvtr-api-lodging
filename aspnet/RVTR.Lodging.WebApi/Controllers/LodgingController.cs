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
        /// <param name="filterModel"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] LodgingSearchFilterModel filterModel, int id)
        {
            var obj = await _unitOfWork.Lodging.DeleteAsync(id, filterModel);

            if (obj == null) return NotFound();

            await _unitOfWork.CommitAsync();

            return Ok(obj);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] LodgingSearchFilterModel filterModel)
        {
              return Ok(await _unitOfWork.Lodging.GetAsync(filterModel));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromQuery] LodgingSearchFilterModel filterModel, int id)
        {
            var obj = await _unitOfWork.Lodging.GetAsync(id, filterModel);
            if (obj == null) return NotFound();
            return Ok(obj);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lodging"></param>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] LodgingSearchFilterModel filterModel, LodgingModel lodging)
        {
            if (lodging == null) return BadRequest();

            var ExistingEntry = await _unitOfWork.Lodging.GetAsync(lodging.Id, filterModel);

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
