using Grpc.Core;
using Grpc.Net.Client;
using PostService.Grpc;

public class PostGrpcClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PostGrpcService.PostGrpcServiceClient _client;

    public PostGrpcClient(IConfiguration config, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        var channel = GrpcChannel.ForAddress("http://localhost:5011");
        _client = new PostGrpcService.PostGrpcServiceClient(channel);
    }

    public async Task<GetPostsByUserResponse> GetPostsByUserAsync()
    {
        // Get userId from claims (matches the claim you added in JWT)
        var userId = _httpContextAccessor.HttpContext.User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("User ID not found in claims");
        }

        // Add userId to gRPC metadata
        var headers = new Metadata
    {
        { "user-id", userId }
    };

        // Call PostService via gRPC
        return await _client.GetPostsByUserAsync(new GetPostsByUserRequest(), headers);
    }

}
