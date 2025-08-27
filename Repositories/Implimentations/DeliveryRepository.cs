using bookFlow.Data;
using bookFlow.Models;
using bookFlow.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bookFlow.Repositories.Implementations
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly UserDbContext _context;

        public DeliveryRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task<Delivery?> GetByIdAsync(Guid id)
        {
            return await _context.Deliveries
                .Include(d => d.Loan)
                    .ThenInclude(l => l.Book)
                .Include(d => d.Loan)
                    .ThenInclude(l => l.User)
                .Include(d => d.DeliveryMan) // optional
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _context.Deliveries
                .Include(d => d.Loan)
                    .ThenInclude(l => l.Book)
                .Include(d => d.Loan)
                    .ThenInclude(l => l.User)
                .Include(d => d.DeliveryMan) // optional
                .ToListAsync();
        }

        public async Task UpdateAsync(Delivery delivery)
        {
            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Delivery delivery)
        {
            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();
        }

        

        

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Delivery>> FindAsync(Expression<Func<Delivery, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Delivery entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Delivery entity)
        {
            throw new NotImplementedException();
        }
    }
}
