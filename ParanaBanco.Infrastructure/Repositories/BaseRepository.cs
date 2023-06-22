using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ParanaBanco.Domain.Entities.SeedWork;
using ParanaBanco.Domain.Interfaces.Repositories;
using ParanaBanco.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly ApiDbContext _context;
        protected DbSet<T> _dataSet;

        public BaseRepository(ApiDbContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }

        public virtual async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAll(int offset, int limit)
        {
            try
            {
                return await _dataSet
                    .Skip(offset)
                    .Take(limit).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            try
            {
                _dataSet.Add(entity);

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));
                if (result is null)
                    return null;

                _context.Entry(result).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
                if (result is null)
                    return false;

                _dataSet.Remove(result);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await _dataSet.CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
