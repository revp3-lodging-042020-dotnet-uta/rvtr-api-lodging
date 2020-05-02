using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
  [ApiController]
  [EnableCors("public")]
  [Route("api/[controller]")]
  public class LodgingController : ControllerBase
  {
    private readonly ILogger<LodgingController> _logger;
    private readonly UnitOfWork _unitOfWork;

    public LodgingController(ILogger<LodgingController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _unitOfWork.Lodging.DeleteAsync(id);
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
      return Ok(await _unitOfWork.Lodging.SelectAsync());
    }

    [HttpGet("{id")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        return Ok(await _unitOfWork.Lodging.SelectAsync(id));
      }
      catch
      {
        return NotFound(id);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(LodgingModel lodging)
    {
      await _unitOfWork.Lodging.InsertAsync(lodging);
      await _unitOfWork.CommitAsync();

      return Accepted(lodging);
    }

    [HttpPut]
    public async Task<IActionResult> Put(LodgingModel lodging)
    {
      _unitOfWork.Lodging.Update(lodging);
      await _unitOfWork.CommitAsync();

      return Accepted(lodging);
    }
  }
}
