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

        public static Stream ToStream(string pdfFileName)
        {
            var pdfDoc = new Document(pdfFileName);
            var memoryStream = new MemoryStream();
            if (pdfDoc.Pages.Count > 0)
            {
                //x = (int) pdfDoc.Pages[1].Rect.Width;
                //y = (int) pdfDoc.Pages[1].Rect.Height;
                   
                //var pageSize = new PageSize(x,y);

                var device = new BmpDevice(new Resolution(150));
                device.Process(pdfDoc.Pages[1], memoryStream);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;

        }
    }
}
