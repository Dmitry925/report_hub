using Aspose.Pdf;
using Aspose.Pdf.Text;
using Exadel.ReportHub.Pdf.Abstract;
using Exadel.ReportHub.Pdf.Models;
using Exadel.ReportHub.SDK.DTOs.Item;
using Microsoft.Extensions.Logging;

namespace Exadel.ReportHub.Pdf;

public class PdfInvoiceGenerator(ILogger<PdfInvoiceGenerator> logger) : IPdfInvoiceGenerator
{
    public async Task<Stream> GenerateAsync(InvoiceModel invoice, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();
        logger.LogInformation("Created stream.");
        FontRepository.Sources.Add(new FolderFontSource("/usr/share/fonts/truetype"));

        var font = FontRepository.FindFont("Liberation Sans");
        logger.LogInformation("font: {FontName}, Embedded: {Embedded}", font.FontName, font.IsEmbedded);

        // font.IsEmbedded = true;

        var doc = new Document();
        var page = doc.Pages.Add();
        page.PageInfo.Margin = new MarginInfo(Constants.MarginInfo.Page.Left, Constants.MarginInfo.Page.Bottom, Constants.MarginInfo.Page.Right, Constants.MarginInfo.Page.Top);
        logger.LogInformation("Created page.");

        // var title = new TextFragment($"{Constants.Text.Label.Invoice}: {invoice.PaymentStatus}")
        // {
        //    TextState =
        //    {
        //        Font = font,
        //        FontSize = Constants.Text.TextStyle.FontSizeTitle,
        //        FontStyle = FontStyles.Bold
        //    },
        //    HorizontalAlignment = HorizontalAlignment.Center
        // };
        // page.Paragraphs.Add(title);
        // logger.LogInformation("Created title.");

        page.Paragraphs.Add(new TextFragment($"{Constants.Text.Label.InvoiceNumber}: {invoice.InvoiceNumber}"));

        // page.Paragraphs.Add(new TextFragment($"{Constants.Text.Label.IssueDate}: {invoice.IssueDate}"));
        // page.Paragraphs.Add(new TextFragment($"{Constants.Text.Label.DueDate}: {invoice.DueDate}"));
        // page.Paragraphs.Add(new TextFragment($"{Constants.Text.Label.ClientName}: {invoice.ClientName}"));
        // page.Paragraphs.Add(new TextFragment($"{Constants.Text.Label.CustomerName}: {invoice.CustomerName}"));
        // page.Paragraphs.Add(new TextFragment($"{Constants.Text.Label.ClientBankAccountNumber}: {invoice.ClientBankAccountNumber}"));
        // logger.LogInformation("Created invoice info.");

        // page.Paragraphs.Add(Constants.Text.NewLine);

        // var table = new Table
        // {
        //     DefaultCellPadding = new MarginInfo(
        //         Constants.MarginInfo.InvoiceTable.Left,
        //         Constants.MarginInfo.InvoiceTable.Bottom,
        //         Constants.MarginInfo.InvoiceTable.Right,
        //         Constants.MarginInfo.InvoiceTable.Top),
        //     Border = new BorderInfo(BorderSide.All, Constants.BorderInfo.IvoiceTable.Border),
        //     DefaultCellBorder = new BorderInfo(BorderSide.All, Constants.BorderInfo.IvoiceTable.CellBorder),
        //     ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
        // };
        // logger.LogInformation("Created table.");

        // table.Rows.Add().Cells.Add(nameof(ItemDTO.Name));
        // table.Rows[0].Cells.Add(nameof(ItemDTO.Description));
        // table.Rows[0].Cells.Add(nameof(ItemDTO.Price));

        // foreach (var item in invoice.Items)
        // {
        //     var row = table.Rows.Add();
        //     row.Cells.Add(item.Name);
        //     row.Cells.Add(item.Description);
        //     row.Cells.Add($"{item.Price} {item.CurrencyCode}");
        // }

        // page.Paragraphs.Add(table);
        // logger.LogInformation("Created table data.");

        // page.Paragraphs.Add(Constants.Text.NewLine);
        // var total = new TextFragment($"{Constants.Text.Label.Total}: {invoice.Amount} {invoice.CurrencyCode}")
        // {
        //     TextState =
        //     {
        //         Font = font,
        //         FontSize = Constants.Text.TextStyle.FontSize,
        //         FontStyle = FontStyles.Bold
        //     },
        //     HorizontalAlignment = HorizontalAlignment.Left
        // };

        // page.Paragraphs.Add(total);
        // logger.LogInformation("Created total.");

        await doc.SaveAsync(stream, cancellationToken);
        stream.Position = 0;
        logger.LogInformation("Saved doc.");

        return stream;
    }
}
