

namespace InvoiceOcrApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public string customerName { get; set; } = string.Empty;
        public Decimal TotalAmount { get; set; }
        public decimal? VAT { get; set; }

    }


}