namespace SocialMedia.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Data;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext socialMediaContext;
        private readonly DbSet<T> entities;
        public BaseRepository(SocialMediaContext socialMediaContext)
        {
            this.socialMediaContext = socialMediaContext;
            entities = socialMediaContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await this.entities.FindAsync(id);
        }
        public async Task Add(T entity)
        {
            this.entities.Add(entity);
            await this.socialMediaContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            T currentEntity = await this.GetById(id);
            this.entities.Remove(currentEntity);
            await this.socialMediaContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            entities.Update(entity);
            await this.socialMediaContext.SaveChangesAsync();
        }
    }
}
