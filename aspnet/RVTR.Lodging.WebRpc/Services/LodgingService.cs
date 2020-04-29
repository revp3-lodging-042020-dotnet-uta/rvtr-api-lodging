using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;

namespace RVTR.Lodging.WebRpc
{
  public class LodgingService : Lodging.LodgingBase
  {
    private readonly ILogger<LodgingService> _logger;
    private readonly UnitOfWork _unitOfWork;

    public LodgingService(ILogger<LodgingService> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    public override Task<LodgingResponse> Book(LodgingRequest request, ServerCallContext context)
    {
      return Task.FromResult(new LodgingResponse()
      {
        Message = "Hello " + request.Name
      });
    }
  }
}
