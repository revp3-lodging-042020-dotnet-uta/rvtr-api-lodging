using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
  [ApiController]
  [EnableCors()]
  [Route("api/[controller]")]
  public class RentalController : ControllerBase
  {
    private readonly ILogger<RentalController> _logger;
    private readonly UnitOfWork _unitOfWork;

    public RentalController(ILogger<RentalController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _unitOfWork.Rental.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        return Ok();
      }
      catch
      {
        return NotFound(id);
      }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Rental.SelectAsync());
    }

    [HttpGet("{id")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        return Ok(await _unitOfWork.Rental.SelectAsync(id));
      }
      catch
      {
        return NotFound(id);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(RentalModel rental)
    {
      await _unitOfWork.Rental.InsertAsync(rental);
      await _unitOfWork.CommitAsync();

      return Accepted(rental);
    }

    [HttpPut]
    public async Task<IActionResult> Put(RentalModel rental)
    {
      _unitOfWork.Rental.Update(rental);
      await _unitOfWork.CommitAsync();

      return Accepted(rental);
    }
  }
}
