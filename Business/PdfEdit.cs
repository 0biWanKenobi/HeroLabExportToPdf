using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

namespace SampleCode.Business
{
    public static class PdfEdit
    {

        private static Document PdfDoc { get; set; }
        private static double PageH, PageW;

        public static void Init(string pdfFileName)
        {
            PdfDoc = new Document(pdfFileName);
            PageH = PdfDoc.Pages[1].Rect.Height;
            PageW = PdfDoc.Pages[1].Rect.Width;

        }

        private static double ToPoints(this double pixels)
        {
            return pixels * 72.0 / 96.0;
        }

        public static void AddField(double ulx, double uly, double width, double height, double winW, double winH)
        {

            ulx = ulx * PageW / winW;
            uly = uly * PageH / winH;
            width = width * PageW / winW;
            height = height * PageH / winH;

            

            var urx = ulx + width;
            var lly = PageH - ( uly + height );
            var ury = PageH - uly;
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
