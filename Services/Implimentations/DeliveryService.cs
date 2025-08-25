using bookFlow.Models;
using bookFlow.Repositories.Implementations;
using bookFlow.Repositories.Interfaces;
using bookFlow.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookFlow.Services.Implimentations
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ILoanRepository _loanRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository, ILoanRepository loanRepository)
        {
            _deliveryRepository = deliveryRepository;
            _loanRepository = loanRepository;
        }

        public async Task<Delivery> CreateDeliveryAsync(Delivery delivery)
        {
            // Check that the Loan exists
            var loan = await _loanRepository.GetByIdAsync(delivery.LoanId);
            if (loan == null)
                throw new Exception("Loan not found for delivery.");

            // DeliveryManId can be null at creation
            delivery.Id = Guid.NewGuid();

            await _deliveryRepository.AddAsync(delivery);
            return delivery;
        }

        public async Task<Delivery?> GetDeliveryByIdAsync(Guid id)
        {
            return await _deliveryRepository.GetByIdAsync(id);
        }
    }
}
