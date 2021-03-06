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
  /// Controller handling the rental HTTP methods
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class RentalController : ControllerBase
  {
    private readonly ILogger<RentalController> _logger;
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// Injects the logging service and unit of work repository
    /// for the controller to utilize the different rental queries
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public RentalController(ILogger<RentalController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Delete rental information based on ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromQuery] RentalQueryParamsModel queryParams, int id)
    {
        var obj = await _unitOfWork.Rental.DeleteAsync(id, queryParams);
        if (obj == null) return NotFound();

        await _unitOfWork.CommitAsync();

        return Ok(obj);
    }

    /// <summary>
    /// Get all rental information.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] RentalQueryParamsModel queryParams)
    {
      return Ok(await _unitOfWork.Rental.GetAsync(queryParams));
    }

    /// <summary>
    /// Get rental information based on ID.
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromQuery] RentalQueryParamsModel queryParams, int id)
    {
      var obj = await _unitOfWork.Rental.GetAsync(id, queryParams);
      if (obj == null) return NotFound();
      return Ok(obj);
    }

    /// <summary>
    /// Updates or inserts rental information.
    /// If there is no existing model with the given ID, then insertion is called.
    /// If there is an existing model with the given ID, then update is called.
    /// </summary>
    /// <param name="rental"></param>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromQuery] RentalQueryParamsModel queryParams, RentalModel rental)
    {
      if(rental == null) return BadRequest();

      var ExistingEntry = await _unitOfWork.Rental.GetAsync(rental.Id, queryParams);

      if (ExistingEntry == null)
      {
        var obj = await _unitOfWork.Rental.InsertAsync(rental);

        await _unitOfWork.CommitAsync();

        return Ok(obj);
      }
      else
      {
        _unitOfWork.Rental.Update(rental);

        await _unitOfWork.CommitAsync();

        return Ok(rental);
      }
    }
  }
}
