using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CodePulse.API.Repositories.Interface
{
    public interface IPaymentRepository
    {
        Task<Payment> AddPayment(Payment request);
        Task<List<Payment>> GetAllPayment();
        Task<Payment?> GetPaymentById(int id);
        void Update(Payment payment);  
        void Delete(Payment payment);
        Task SaveAsync();
    }
}
