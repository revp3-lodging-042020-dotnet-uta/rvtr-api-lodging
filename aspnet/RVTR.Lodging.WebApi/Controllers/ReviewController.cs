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
  /// Controller handling the review HTTP methods
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
    /// Injects the logging service and unit of work repository
    /// for the controller to utilize the different review queries
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ReviewController(ILogger<ReviewController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// HTTP method for deleting a review entry where the entry
    /// is found through the given ID
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
    /// HTTP method for getting all of the review data
    /// based on the given search parameter filters (inside filtermodel)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ReviewSearchFilterModel filterModel)
    {
      return Ok(await _unitOfWork.Review.GetAsync(filterModel));
    }

    /// <summary>
    /// HTTP method for getting a specific review entry
    /// based on both the given ID and the search parameter filters (inside filtermodel)
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
    /// HTTP method for inserting and updating review entries
    /// based on the given review model's ID.
    /// If there is no existing model with the given ID, then insertion is called.
    /// If there is an existing model with the given ID, then update is called.
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
