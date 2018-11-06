using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

namespace HeroLabExportToPdf.Business
{
    public class PdfConvert
    {

        static PdfConvert()
        {
            AsposeLicense.Register();
        }

        public static Stream ToImageByteArray(string pdfFileName, out int x, out int y)
        {
            var pdfDoc = new Document(pdfFileName);
            var memoryStream = new MemoryStream();
            if (pdfDoc.Pages.Count > 0)
            {
                x = (int) pdfDoc.Pages[1].Rect.Width;
                y = (int) pdfDoc.Pages[1].Rect.Height;
                var resolution = new Resolution(96);
                   
                var pageSize = new PageSize(x,y);

                var device = new BmpDevice(pageSize, resolution);
               
                device.Process(pdfDoc.Pages[1], memoryStream);
            }
            else
                x = y = 0;

            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;

        }
    }
}
