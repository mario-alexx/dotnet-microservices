using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpc 
{   
    /// <summary>
    /// gRPC service for interacting with platform data.
    /// </summary>
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrpcPlatformService"/> class.
        /// </summary>
        /// <param name="repository">The platform repository for accessing platform data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
        public GrpcPlatformService(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all platforms.
        /// </summary>
        /// <param name="request">The request for getting all platforms.</param>
        /// <param name="context">The server call context.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the platform response.</returns>
        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context) 
        {
            var response = new PlatformResponse();
            var platforms = _repository.GetAllPlatform();

            foreach(var plat in platforms) 
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}