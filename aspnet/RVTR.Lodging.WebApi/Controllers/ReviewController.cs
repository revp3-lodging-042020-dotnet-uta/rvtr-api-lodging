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
  public class ReviewController : ControllerBase
  {
    private readonly ILogger<ReviewController> _logger;
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ReviewController(ILogger<ReviewController> logger, UnitOfWork unitOfWork)
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
    public async Task<IActionResult> Delete([FromQuery] ReviewSearchFilterModel filterModel, int id)
    {
      var obj = await _unitOfWork.Review.DeleteAsync(id, filterModel);

      if (obj == null) return NotFound();

      await _unitOfWork.CommitAsync();

      return Ok(obj);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ReviewSearchFilterModel filterModel)
    {
      return Ok(await _unitOfWork.Review.GetAsync(filterModel));
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromQuery] ReviewSearchFilterModel filterModel, int id)
    {
      var obj = await _unitOfWork.Review.GetAsync(id, filterModel);
      if (obj == null) return NotFound();
      return Ok(obj);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="review"></param>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromQuery] ReviewSearchFilterModel filterModel, ReviewModel review)
    {
      if (review == null) return BadRequest();

      var ExistingEntry = await _unitOfWork.Review.GetAsync(review.Id, filterModel);

      if (ExistingEntry == null)
      {
        var obj = await _unitOfWork.Review.InsertAsync(review);

        await _unitOfWork.CommitAsync();

        return Ok(obj);
      }
      else
      {
        _unitOfWork.Review.Update(review);

        await _unitOfWork.CommitAsync();

        return Ok(review);
      }
    }
  }
}
