namespace SocialMedia.Core.Interfaces
{
    using SocialMedia.Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(int id);
    }
}
