using FunctionsAPP.ContexBlob;
using FunctionsAPP.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Repository
{
    public class RepositoryImplemantations<T> : IRepository<T> where T : BaseEntity

    {
        private readonly ContextSQL _context;
        protected readonly DbSet<T> _dbset;

        public RepositoryImplemantations(ContextSQL context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task<T> Get(long Id)
        {
            try
            {
                return await _dbset.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _dbset.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Create(T entity)
        {
            try
            {
                _dbset.Add(entity);
                await Save();
                return entity;
            }
                catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                if (!await Exist(entity.Id)) return null;
                var result = await this.Get(entity.Id);

                _dbset.Update(result).CurrentValues.SetValues(entity);
                await Save();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Delete(long id)
        {
            try
            {
                if (!await Exist(id)) return false;
                var result = await this.Get(id);
                 _dbset.Remove(result);
                await Save();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Exist(long id) => await _dbset.AnyAsync(x => x.Id == id);
        private async Task Save() => await _context.SaveChangesAsync();

    }
}
