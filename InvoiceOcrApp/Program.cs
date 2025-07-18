using InvoiceOcr.Data;
using InvoiceOcr.Repositories;
using InvoiceOcr.Services;
using InvoiceOcr.Data;
using InvoiceOcr.Repositories;
using InvoiceOcr.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace InvoiceOcrApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // DI for Invoice OCR 
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=invoiceocr.db").UseLazyLoadingProxies());

            // DI Repositories
            builder.Services.AddScoped<InvoiceRepository>();
            builder.Services.AddScoped<InvoiceDetailRepository>();

            // DI Services
            builder.Services.AddScoped<PdfConverter>();
            builder.Services.AddScoped<OcrService>();
            builder.Services.AddScoped<InvoiceService>();

            // إضافة Swagger مع دعم تعليقات XML
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Invoice OCR API",
                    Version = "v1",
                    Description = "API for extracting and managing invoice data from images or PDFs"
                });
            });

            // Add Logging
            builder.Services.AddLogging(logging => logging.AddConsole());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice OCR API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}