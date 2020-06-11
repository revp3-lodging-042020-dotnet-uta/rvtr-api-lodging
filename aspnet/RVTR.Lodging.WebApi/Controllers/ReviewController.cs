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
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      await _unitOfWork.Review.DeleteAsync(id);
      return Ok();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Review.GetAsync());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      return Ok(await _unitOfWork.Review.GetAsync(id));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(ReviewModel review)
    {
      await _unitOfWork.Review.InsertAsync(review);

      return Accepted(review);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put(ReviewModel review)
    {
      await _unitOfWork.Review.Update(review);

      return Accepted(review);
    }
  }
}
