using ECommerce.Application.Features.Invoices.DTO;
using ECommerce.Application.Interfaces.IServices;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ECommerce.Infrastructure.Services;

public class InvoiceService : IInvoiceService
{
    public Task<byte[]> GenerateInvoiceAsync(InvoiceDto invoice)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);

                // Header
                page.Header()
                    .Text("E-Commerce Invoice")
                    .FontSize(24)
                    .Bold()
                    .FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

                // Content
                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        // Invoice Information
                        column.Item()
                            .Text($"Invoice #: {invoice.OrderId}")
                            .FontSize(14)
                            .Bold();

                        column.Item()
                            .Text($"Date: {invoice.CreatedAt:dd/MM/yyyy}");

                        column.Item()
                            .Text($"Customer: {invoice.CustomerName}");

                        column.Item()
                            .Text($"Email: {invoice.CustomerEmail}");

                        column.Item()
                            .Text($"Status: {invoice.Status}");

                        column.Item()
                            .PaddingVertical(15);

                        // Products Table
                        column.Item()
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(2);
                                });

                                // Header Row
                                table.Header(header =>
                                {
                                    header.Cell()
                                        .Element(CellHeaderStyle)
                                        .Text("Product");

                                    header.Cell()
                                        .Element(CellHeaderStyle)
                                        .Text("Qty");

                                    header.Cell()
                                        .Element(CellHeaderStyle)
                                        .Text("Unit Price");

                                    header.Cell()
                                        .Element(CellHeaderStyle)
                                        .Text("Total");
                                });

                                // Products
                                foreach (var item in invoice.Items)
                                {
                                    table.Cell()
                                        .Element(CellStyle)
                                        .Text(item.ProductName);

                                    table.Cell()
                                        .Element(CellStyle)
                                        .Text(item.Quantity.ToString());

                                    table.Cell()
                                        .Element(CellStyle)
                                        .Text(item.UnitPrice.ToString("0.00"));

                                    table.Cell()
                                        .Element(CellStyle)
                                        .Text(item.Total.ToString("0.00"));
                                }
                            });

                        column.Item()
                            .PaddingTop(20);

                        // Totals
                        column.Item()
                            .AlignRight()
                            .Column(totalColumn =>
                            {
                                totalColumn.Item()
                                    .Text($"Subtotal: {invoice.Subtotal:0.00}");

                                totalColumn.Item()
                                    .Text($"Discount: {invoice.DiscountAmount:0.00}");

                                totalColumn.Item()
                                    .Text($"Tax: {invoice.TaxAmount:0.00}");

                                totalColumn.Item()
                                    .Text($"Shipping: {invoice.ShippingFee:0.00}");

                                totalColumn.Item()
                                    .PaddingTop(5)
                                    .Text($"Total: {invoice.TotalAmount:0.00}")
                                    .FontSize(16)
                                    .Bold();
                            });
                    });

                // Footer
                page.Footer()
                    .AlignCenter()
                    .Text("Thank you for shopping with us!")
                    .FontSize(10);
            });
        });

        var pdfBytes = document.GeneratePdf();

        return Task.FromResult(pdfBytes);
    }

    private static IContainer CellHeaderStyle(IContainer container)
    {
        return container
            .Background(QuestPDF.Helpers.Colors.Grey.Lighten2)
            .Padding(5)
            .BorderBottom(1)
            .BorderColor(QuestPDF.Helpers.Colors.Grey.Medium);
    }

    private static IContainer CellStyle(IContainer container)
    {
        return container
            .Padding(5)
            .BorderBottom(1)
            .BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2);
    }
}