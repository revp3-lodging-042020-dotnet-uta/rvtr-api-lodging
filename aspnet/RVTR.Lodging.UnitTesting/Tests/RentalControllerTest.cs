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
      mocks._repository.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(new RentalModel()));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Delete(1);
      Assert.IsType(typeof(OkObjectResult), result);
    }

    [Fact]
    public async void Delete_UsingInvalidId()
    {
      var mocks = new Mocks();
      RentalModel mockResult = null;
      mocks._repository.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(mockResult));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Delete(1);
      Assert.IsType(typeof(NotFoundResult), result);
    }

    [Fact]
    public async void GetAll()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync()).Returns(
        Task.FromResult<IEnumerable<RentalModel>>(
          new List<RentalModel>() { null, null }
        )
      );

      var _controller = NewRentalController(mocks);

      var result = await _controller.Get();
      Assert.IsType(typeof(OkObjectResult), result);

      var items = (result as OkObjectResult).Value as IEnumerable<RentalModel>;
      Assert.Equal(2, items.Count());
    }

    [Fact]
    public async void Get_UsingValidId()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync(1)).Returns(
        Task.FromResult<RentalModel>(
          new RentalModel { Id = 1 }
        )
      );

      var _controller = NewRentalController(mocks);

      var result = await _controller.Get(1);
      Assert.IsType(typeof(OkObjectResult), result);

      var value = (result as OkObjectResult).Value as RentalModel;
      Assert.Equal(1, value.Id);
    }

    [Fact]
    public async void Get_UsingInvalidId()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<RentalModel>(null));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Get(1);
      Assert.IsType(typeof(NotFoundResult), result);
    }

    [Fact]
    public async void Post()
    {
      var mocks = new Mocks();
      var submittedModel = new RentalModel();

      mocks._repository.Setup(m => m.InsertAsync(submittedModel)).Returns(Task.FromResult(submittedModel));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Post(submittedModel);
      Assert.IsType(typeof(AcceptedResult), result);
    }

    [Fact]
    public async void Post_GarbageModelIsRejected()
    {
      var mocks = new Mocks();

      mocks._repository.Setup(m => m.InsertAsync(null)).Returns(Task.FromResult<RentalModel>(null));

      var _controller = NewRentalController(mocks);

      var result = await _controller.Post(null);
      Assert.IsType(typeof(BadRequestObjectResult), result);
    }
  }
}


//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.Data.Sqlite;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Moq;
//using RVTR.Lodging.DataContext;
//using RVTR.Lodging.DataContext.Repositories;
//using RVTR.Lodging.ObjectModel.Models;
//using RVTR.Lodging.WebApi.Controllers;
//using Xunit;
//
//namespace RVTR.Lodging.UnitTesting.Tests
//{
//  public class RentalControllerTest
//  {
//    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
//    private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;
//    private readonly RentalController _controller;
//    private readonly ILogger<RentalController> _logger;
//    private readonly UnitOfWork _unitOfWork;
//
//    public RentalControllerTest()
//    {
//      var contextMock = new Mock<LodgingContext>(_options);
//      var loggerMock = new Mock<ILogger<RentalController>>();
//      var repositoryMock = new Mock<Repository<RentalModel>>(new LodgingContext(_options));
//      var unitOfWorkMock = new Mock<UnitOfWork>(contextMock.Object);
//
//      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
//      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(1));
//      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<RentalModel>())).Returns(Task.FromResult<RentalModel>(null));
//      repositoryMock.Setup(m => m.SelectAsync()).Returns(Task.FromResult<IEnumerable<RentalModel>>(null));
//      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
//      repositoryMock.Setup(m => m.SelectAsync(1)).Returns(Task.FromResult<RentalModel>(null));
//      repositoryMock.Setup(m => m.Update(It.IsAny<RentalModel>()));
//      unitOfWorkMock.Setup(m => m.Rental).Returns(repositoryMock.Object);
//
//      _logger = loggerMock.Object;
//      _unitOfWork = unitOfWorkMock.Object;
//      _controller = new RentalController(_logger, _unitOfWork);
//    }
//
//    [Fact]
//    public async void Test_Controller_Delete()
//    {
//      var resultFail = await _controller.Delete(0);
//      var resultPass = await _controller.Delete(1);
//
//      Assert.NotNull(resultFail);
//      Assert.NotNull(resultPass);
//    }
//
//    [Fact]
//    public async void Test_Controller_Get()
//    {
//      var resultMany = await _controller.Get();
//      var resultFail = await _controller.Get(0);
//      var resultOne = await _controller.Get(1);
//
//      Assert.NotNull(resultMany);
//      Assert.NotNull(resultFail);
//      Assert.NotNull(resultOne);
//    }
//
//    [Fact]
//    public async void Test_Controller_Post()
//    {
//      var resultPass = await _controller.Post(new RentalModel());
//
//      Assert.NotNull(resultPass);
//    }
//
//    [Fact]
//    public async void Test_Controller_Put()
//    {
//      var resultPass = await _controller.Put(new RentalModel());
//
//      Assert.NotNull(resultPass);
//    }
//  }
//}
//
