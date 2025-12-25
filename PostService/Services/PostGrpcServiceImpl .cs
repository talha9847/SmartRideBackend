using Grpc.Core;
using PostService.Grpc;

public class PostGrpcServiceImpl : PostGrpcService.PostGrpcServiceBase
{
    public override async Task<GetPostsByUserResponse> GetPostsByUser(
        GetPostsByUserRequest request,
        ServerCallContext context)
    {
        // Read user-id from metadata
        var userIdEntry = context.RequestHeaders.FirstOrDefault(h => h.Key == "user-id");
        if (userIdEntry == null)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User ID missing"));
        }

        var userId = userIdEntry.Value;

        // Create static response
        var response = new GetPostsByUserResponse();

        response.Posts.Add(new PostModel
        {
            Id = "1",
            UserId = userId,
            Title = "Static Post 1",
            Content = "This is the first static post"
        });

        response.Posts.Add(new PostModel
        {
            Id = "2",
            UserId = userId,
            Title = "Static Post 2",
            Content = "This is the second static post"
        });

        return await Task.FromResult(response);
    }
}
