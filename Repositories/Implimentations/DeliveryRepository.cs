using bookFlow.Data;
using bookFlow.Models;
using bookFlow.Repositories.Implementations;
using bookFlow.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookFlow.Repositories.Implimentations
{
    public class DeliveryRepository : GenericRepository<Delivery> ,IDeliveryRepository 
    {
        private readonly UserDbContext _context;

        public DeliveryRepository(UserDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Delivery> CreateAsync(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
            return delivery;
        }

        public async Task<Delivery?> GetByIdAsync(Guid id)
        {
            return await _context.Deliveries
                .Include(d => d.Loan)
                .Include(d => d.DeliveryMan)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
