using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;
using RVTR.Lodging.WebApi.Controllers;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class ReviewControllerTest
  {

    private class Mocks
    {
      public Mock<LodgingContext> _lodgingContext;
      public Mock<ILogger<ReviewController>> _logger;
      public Mock<ReviewRepository> _repository;
      public Mock<UnitOfWork> _unitOfWork;

      public Mocks()
      {
        SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
        DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;

        this._lodgingContext = new Mock<LodgingContext>(_options);
        this._logger = new Mock<ILogger<ReviewController>>();
        this._repository = new Mock<ReviewRepository>(this._lodgingContext.Object);
        this._unitOfWork = new Mock<UnitOfWork>(_lodgingContext.Object);
        this._unitOfWork.Setup(m => m.Review).Returns(this._repository.Object);
      }

    }

    private ReviewController NewReviewController(Mocks mocks)
    {
      return new ReviewController(mocks._logger.Object, mocks._unitOfWork.Object);
    }

    [Fact]
    public async void Delete_UsingValidId()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(new ReviewModel()));

      var _controller = NewReviewController(mocks);

      var result = await _controller.Delete(1);
      Assert.IsType(typeof(OkObjectResult), result);
    }

    [Fact]
    public async void Delete_UsingInvalidId()
    {
      var mocks = new Mocks();
      ReviewModel mockResult = null;
      mocks._repository.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(mockResult));

      var _controller = NewReviewController(mocks);

      var result = await _controller.Delete(1);
      Assert.IsType(typeof(NotFoundResult), result);
    }

    [Fact]
    public async void GetAll()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync()).Returns(
        Task.FromResult<IEnumerable<ReviewModel>>(
          new List<ReviewModel>() { null, null }
        )
      );

      var _controller = NewReviewController(mocks);

      var result = await _controller.Get();
      Assert.IsType(typeof(OkObjectResult), result);

      var items = (result as OkObjectResult).Value as IEnumerable<ReviewModel>;
      Assert.Equal(2, items.Count());
    }

    [Fact]
    public async void Get_UsingValidId()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync(1)).Returns(
        Task.FromResult<ReviewModel>(
          new ReviewModel { Id = 1 }
        )
      );

      var _controller = NewReviewController(mocks);

      var result = await _controller.Get(1);
      Assert.IsType(typeof(OkObjectResult), result);

      var value = (result as OkObjectResult).Value as ReviewModel;
      Assert.Equal(1, value.Id);
    }

    [Fact]
    public async void Get_UsingInvalidId()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<ReviewModel>(null));

      var _controller = NewReviewController(mocks);

      var result = await _controller.Get(1);
      Assert.IsType(typeof(NotFoundResult), result);
    }

    [Fact]
    public async void Post()
    {
      var mocks = new Mocks();
      var submittedModel = new ReviewModel();

      mocks._repository.Setup(m => m.InsertAsync(submittedModel)).Returns(Task.FromResult(submittedModel));

      var _controller = NewReviewController(mocks);

      var result = await _controller.Post(submittedModel);
      Assert.IsType(typeof(OkObjectResult), result);
    }

    [Fact]
    public async void Post_GarbageModelIsRejected()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.InsertAsync(null)).Returns(Task.FromResult<ReviewModel>(null));

      var _controller = NewReviewController(mocks);

      var result = await _controller.Post(null);
      Assert.IsType(typeof(BadRequestResult), result);
    }
  }
}