using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvoiceOcr.Data;
using InvoiceOcr.Models;

namespace InvoiceOcr.Repositories
{
    public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>
    {
        public InvoiceDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<InvoiceDetail>> GetDetailsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoiceDetails
                .Where(d => d.InvoiceId == invoiceId)
                .ToListAsync();
        }
    }
}