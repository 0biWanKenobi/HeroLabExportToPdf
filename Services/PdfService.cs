using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Forms;
using HeroLabExportToPdf.Business;

namespace HeroLabExportToPdf.Services
{
    public class PdfService : IPdfService
    {

        static PdfService()
        {
            AsposeLicense.Register();
        }


        private static Document PdfDoc { get; set; }
        private static List<Field> Fields { get; set; }
        private static double PageH { get; set; }
        private static double PageW { get; set; }



        public void Init(string pdfFileName)
        {
            PdfDoc = new Document(pdfFileName);

            Fields = PdfDoc.Form.Fields.ToList();

            foreach (var field in Fields)
            {
                PdfDoc.Form.Delete(field);
            }

            PageH = PdfDoc.Pages[1].Rect.Height;
            PageW = PdfDoc.Pages[1].Rect.Width;

        }

        public void RemoveField(string fieldName)
        {
            try
            {
                PdfDoc.Form.Delete(fieldName);
            }
            catch (Exception)
            {
                // ignored, a field without a name cannot be deleted
                // and the same is for a field that does not exist,
                // but it's not an issue if the operation fails silently
            }
        }


        public void AddField(double ulx, double uly, double width, double height, double winW, double winH,
            string text, string label, double fontSize)
        {
            var scaleW = PageW / winW;
            var scaleH = PageH / winH;

            ulx = ulx * scaleW;
            uly = uly * scaleH;
            width = width * scaleW;
            height = height * scaleH;

            fontSize = fontSize * scaleW;

            var urx = ulx + width;
            var lly = PageH - (uly + height);
            var ury = PageH - uly;
            // Create a field
            var textBoxField = new TextBoxField(PdfDoc.Pages[1], new Rectangle(ulx, lly, urx, ury))
            {
                PartialName = label,
                Value = text,
                Color = Color.Transparent,
                DefaultAppearance = { FontSize = fontSize }
                ,
                Margin = new MarginInfo(5, 0, 5, 0)
                ,
                Characteristics = { Border = System.Drawing.Color.Transparent }
                ,
                Name = label
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

        public IEnumerable GetFields(double wpfW, double wpfH)
        {

            var scaleX = wpfW /PageW ;
            var scaleY = wpfH / PageH ;

            return Fields.Select(f =>
                (
                      label: f.PartialName
                    , value: f.Value
                    , font: f.DefaultAppearance.FontName
                    , type: f is CheckboxField ? 1 : 2
                    , width: f.Rect.Width * scaleX
                    , height: f.Rect.Height * scaleY
                    , x: f.Rect.LLX * scaleX
                    , y: (PageH - f.Rect.URY) * scaleY
                    , index: f.PageIndex
                )
            );
        }

        public void Save(string fileName)
        {
            PdfDoc.Save(fileName);
        }


        public BitmapImage GetImagePreview()
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ToStream();
            image.EndInit();
            return image;
        }


        private static Stream ToStream( )
        {
          
            var memoryStream = new MemoryStream();
            if (PdfDoc.Pages.Count > 0)
            {
                var device = new BmpDevice(new Resolution(150));
                device.Process(PdfDoc.Pages[1], memoryStream);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;

        }
    }
}
