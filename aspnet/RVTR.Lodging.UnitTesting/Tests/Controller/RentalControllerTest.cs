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
    public class RentalControllerTest
  {

    private class Mocks
    {
      public Mock<LodgingContext> _lodgingContext;
      public Mock<ILogger<RentalController>> _logger;
      public Mock<RentalRepository> _repository;
      public Mock<UnitOfWork> _unitOfWork;

      public Mocks()
      {
        SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
        DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;

        this._lodgingContext = new Mock<LodgingContext>(_options);
        this._logger = new Mock<ILogger<RentalController>>();
        this._repository = new Mock<RentalRepository>(this._lodgingContext.Object);
        this._unitOfWork = new Mock<UnitOfWork>(_lodgingContext.Object);
        this._unitOfWork.Setup(m => m.Rental).Returns(this._repository.Object);
      }

    }

    private RentalController NewRentalController(Mocks mocks)
    {
      return new RentalController(mocks._logger.Object, mocks._unitOfWork.Object);
    }

    [Fact]
    public async void Delete_UsingValidId()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.DeleteAsync(1, null)).Returns(Task.FromResult(new RentalModel()));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Delete(null, 1);
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Delete_UsingInvalidId()
    {
      var mocks = new Mocks();
      RentalModel mockResult = null;
      mocks._repository.Setup(m => m.DeleteAsync(1, null)).Returns(Task.FromResult(mockResult));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Delete(null, 1);
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void GetAll()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync(null)).Returns(
        Task.FromResult<IEnumerable<RentalModel>>(
          new List<RentalModel>() { null, null }
        )
      );

      var _controller = NewRentalController(mocks);

      var result = await _controller.Get(null);
      Assert.IsType<OkObjectResult>(result);

      var items = (result as OkObjectResult).Value as IEnumerable<RentalModel>;
      Assert.Equal(2, items.Count());
    }

    [Fact]
    public async void Get_UsingValidId()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync(1, null)).Returns(
        Task.FromResult<RentalModel>(
          new RentalModel { Id = 1 }
        )
      );

      var _controller = NewRentalController(mocks);

      var result = await _controller.Get(null, 1);
      Assert.IsType<OkObjectResult>(result);

      var value = (result as OkObjectResult).Value as RentalModel;
      Assert.Equal(1, value.Id);
    }

    [Fact]
    public async void Get_UsingInvalidId()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync(1, null)).Returns(Task.FromResult<RentalModel>(null));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Get(null, 1);
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void Post_Insert()
    {
      var mocks = new Mocks();
      var submittedModel = new RentalModel { Id = 1 };

      mocks._repository.Setup(m => m.GetAsync(1, null)).Returns(
        Task.FromResult<RentalModel>(
          null
        )
      );

      mocks._repository.Setup(m => m.InsertAsync(submittedModel)).Returns(Task.FromResult(submittedModel));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Post(null, submittedModel);
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Post_Update()
    {
      var mocks = new Mocks();
      var submittedModel = new RentalModel { Id = 1 };

      mocks._repository.Setup(m => m.GetAsync(1, null)).Returns(
        Task.FromResult<RentalModel>(
          new RentalModel { Id = 1 }
        )
      );

      mocks._repository.Setup(m => m.Update(submittedModel));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Post(null, new RentalModel { Id = 1 });
      Assert.IsType<OkObjectResult>(result);

      var value = (result as OkObjectResult).Value as RentalModel;
      Assert.Equal(1, value.Id);
    }

    [Fact]
    public async void Post_GarbageModelIsRejected()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.InsertAsync(null)).Returns(Task.FromResult<RentalModel>(null));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Post(null, null);
      Assert.IsType<BadRequestResult>(result);
    }
  }
}
