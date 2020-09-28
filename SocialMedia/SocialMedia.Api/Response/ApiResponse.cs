namespace SocialMedia.Api.Response
{
    using SocialMedia.Core.CustomEntities;

    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public Metadata Metadata { get; set; }
    }
}