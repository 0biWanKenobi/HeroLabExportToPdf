using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

namespace HeroLabExportToPdf.Business
{
    public class ExportService
    {
        private static double _pageH, _pageW;
       
        public static Document PdfDoc { get; private set; }

        public static void Init(string sourcePath, double pageH, double pageW)
        {
            PdfDoc = new Document(sourcePath);
            _pageH = pageH;
            _pageW = pageW;
        }

        public static void AddField(double ulx, double uly, double width, double height, double winW, double winH)
        {

            ulx = ulx * _pageW / winW;
            uly = uly * _pageH / winH;
            width = width * _pageW / winW;
            height = height * _pageH / winH;

            

            var urx = ulx + width;
            var lly = _pageH - ( uly + height );
            var ury = _pageH - uly;
            // Create a field
            var textBoxField = new TextBoxField(PdfDoc.Pages[1], new Rectangle(ulx, lly, urx, ury))
            {
                PartialName = "textbox1", Value = "Text Box", Color = Color.Transparent
            };

            
            var border = new Border(textBoxField)
            {
                Width = 5,
                Dash = new Dash(1, 1)
            };
            textBoxField.Border = border;

            // Add field to the document
            PdfDoc.Form.Add(textBoxField, 1);

            
        }

        

        public static void Save(string fileName)
        {
            PdfDoc.Save(fileName);
        }
    }
}
