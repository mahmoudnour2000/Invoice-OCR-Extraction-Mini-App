using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvoiceOcr.Data;
using InvoiceOcr.Model;

namespace InvoiceOcr.Repositories
{
    public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>
    {
        #region Constructor
        public InvoiceDetailRepository(AppDbContext context) : base(context)
        {
        }
        #endregion

        #region Query Operations
        public async Task<List<InvoiceDetail>> GetDetailsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoiceDetails
                .Where(d => d.InvoiceId == invoiceId)
                .ToListAsync();
        }
        #endregion
    }
}