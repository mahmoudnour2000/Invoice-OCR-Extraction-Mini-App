using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvoiceOcr.Data;
using InvoiceOcr.Models;

namespace InvoiceOcr.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>
    {
        public InvoiceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Invoice> GetInvoiceWithDetailsAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Details)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Invoice>> GetInvoiceByCustomerAsync(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentException("Customer name cannot be empty.", nameof(customerName));
            }

            return await _context.Invoices
                .Include(i => i.Details)
                .Where(i => i.CustomerName.Contains(customerName))
                .ToListAsync();
        }
    }
}