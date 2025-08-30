using bookFlow.Enum;
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
            await _deliveryRepository.SaveChangesAsync();
            return delivery;
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _deliveryRepository.GetAllAsync();
        }




        public async Task<Delivery?> GetDeliveryByIdAsync(Guid id)
        {
            return await _deliveryRepository.GetByIdAsync(id);
        }

        public async Task<Delivery> AssignDeliveryAsync(Guid deliveryId, Guid deliveryManId)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
            if (delivery == null)
                throw new Exception("Delivery not found.");

            if (delivery.UserId != null)
                throw new Exception("Delivery already assigned.");

            delivery.UserId= deliveryManId;
            delivery.Status = DeliveryStatus.EN_COURS;
            await _deliveryRepository.SaveChangesAsync();
            return delivery;
        }

        public async Task<Delivery> UpdateStatusAsync(Guid deliveryId, DeliveryStatus status)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
            if (delivery == null)
                throw new Exception("Delivery not found.");

            delivery.Status = status;

            _deliveryRepository.Update(delivery);
            await _deliveryRepository.SaveChangesAsync();
            return delivery;
        }

        public async Task<IEnumerable<Delivery>> GetDeliveriesByDeliveryManIdAsync(Guid deliveryManId)
        {
            return await _deliveryRepository.GetAllByDeliveryManIdAsync(deliveryManId);
        }

        public async Task<IEnumerable<Delivery>> GetPendingDeliveriesAsync()
        {
            return await _deliveryRepository.GetAllPendingAsync();
        }


    }
}
