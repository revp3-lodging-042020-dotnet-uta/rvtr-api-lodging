using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
  public class RentalController : ControllerBase
  {
    private readonly ILogger<RentalController> _logger;
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public RentalController(ILogger<RentalController> logger, UnitOfWork unitOfWork)
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
        var obj = await _unitOfWork.Rental.DeleteAsync(id);
        if (obj == null) return NotFound();

        await _unitOfWork.CommitAsync();

        return Ok(obj);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var queryParams = new QueryModel(this.HttpContext.Request.Query);

      return Ok(await _unitOfWork.Rental.GetAsync());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var obj = await _unitOfWork.Rental.GetAsync(id);
      if (obj == null) return NotFound();
      return Ok(obj);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="rental"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(RentalModel rental)
    {
      if(rental == null) return BadRequest();

      var ExistingEntry = await _unitOfWork.Rental.GetAsync(rental.Id);

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
