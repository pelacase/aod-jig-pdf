using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Text;

namespace AodJigPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fix for: Unhandled exception. System.NotSupportedException: No data is available for encoding 1252.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            var inputFileName = "IP7-Sisters-EngravingTool-Testing.pdf"; // "Ridge IP7 artboard fit to design.pdf";
            var topGapPoints = 72;
            var rightGapPoints = 72;
            var bottomGapPoints = 140.4;
            var leftGapPoints = 72;
            var caseOffsetXPoints = 221.76;
            var caseOffsetYPoints = 436.32;
            var outputRows = 4;
            var outputCols = 15;

            XPdfForm inputPdfForm = XPdfForm.FromFile(inputFileName);

            var output = new PdfDocument();
            output.PageLayout = PdfPageLayout.SinglePage;
            var outputPage = output.AddPage();
            outputPage.Width = leftGapPoints + rightGapPoints + (caseOffsetXPoints * outputCols);
            outputPage.Height = topGapPoints + bottomGapPoints + (caseOffsetYPoints * outputRows);
            var outputGraphics = XGraphics.FromPdfPage(outputPage);

            for (var row=0; row<outputRows; row++) {
                for (var col=0; col<outputCols; col++) {
                    var x = (col * caseOffsetXPoints) + leftGapPoints;
                    var y = (row * caseOffsetYPoints) + topGapPoints;
                    outputGraphics.DrawImage(inputPdfForm, x, y);
                }
            }

            output.Save("output.pdf");
        }
    }
}
