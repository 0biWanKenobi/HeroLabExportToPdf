using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

namespace HeroLabExportToPdf.Business
{
    public static class PdfEdit
    {

        private static Document PdfDoc { get; set; }
        private static double _pageH, _pageW;

        public static void Init(string pdfFileName)
        {
            PdfDoc = new Document(pdfFileName);
            
            _pageH = PdfDoc.Pages[1].Rect.Height;
            _pageW = PdfDoc.Pages[1].Rect.Width;

        }

       

        public static void AddField(double ulx, double uly, double width, double height, double winW, double winH,
            string text, double fontSize)
        {
            var scaleW = _pageW / winW;
            var scaleH = _pageH / winH;

            ulx = ulx * scaleW;
            uly = uly * scaleH;
            width = width * scaleW;
            height = height * scaleH;

            fontSize = fontSize * scaleW;

            var urx = ulx + width;
            var lly = _pageH - ( uly + height );
            var ury = _pageH - uly;
            // Create a field
            var textBoxField = new TextBoxField(PdfDoc.Pages[1], new Rectangle(ulx, lly, urx, ury))
            {
                PartialName = $"textbox{PdfDoc.Form.Fields.Length}", Value = text, Color = Color.Transparent, DefaultAppearance = { FontSize = fontSize}
                , Margin = new MarginInfo(5,0,5,0)
                , Characteristics = { Border = System.Drawing.Color.Transparent}
            };

            
            var border = new Border(textBoxField)
            {
                Width = 1,
                Style = BorderStyle.Solid
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
