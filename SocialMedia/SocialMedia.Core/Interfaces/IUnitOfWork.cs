namespace SocialMedia.Core.Interfaces
{
    using SocialMedia.Core.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        ISecurityRepository SecurityRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
