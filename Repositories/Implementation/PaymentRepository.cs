using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return payment;
        }

        public async Task<List<Payment>> GetAllPayment()
        {
            return await _context.Payments.ToListAsync();

        }

        public async Task<Payment?> GetPaymentById(int id)
        {
            return await _context.Payments.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(Payment payment)
        {
            _context.Payments.Update(payment);
        }
        public void Delete(Payment payment)
        {
            _context.Payments.Remove(payment);
        }

    }
}
