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

        

        

    

        public async Task<IEnumerable<Delivery>> FindAsync(Expression<Func<Delivery, bool>> predicate)
        {
            return await _context.Deliveries
                .Where(predicate)
                .Include(d => d.Loan)
                    .ThenInclude(l => l.User)
                .Include(d => d.Loan)
                    .ThenInclude(l => l.Book)
                .ToListAsync();
        }




        public async Task<IEnumerable<Delivery>> GetAllByDeliveryManIdAsync(Guid deliveryManId)
        {
            return await _context.Deliveries
                .Where(d => d.UserId == deliveryManId)
                .Include(d => d.Loan)
                .Include(d => d.Loan.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<Delivery>> GetAllPendingAsync()
        {
            return await _context.Deliveries
                .Where(d => d.UserId == null && d.Status == Enum.DeliveryStatus.EN_ATTENTE)
                .Include(d => d.Loan)
                .Include(d => d.Loan.User)
                .ToListAsync();
        }

        public void Update(Delivery entity)
        {
            // Mark the entity as modified
            _context.Deliveries.Update(entity);
        }

        public void Delete(Delivery entity)
        {
            _context.Deliveries.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        

    }
}
