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
using Aspose.Pdf.Text;
using HeroLabExportToPdf.Business;
using Color = Aspose.Pdf.Color;
using Rectangle = Aspose.Pdf.Rectangle;

namespace HeroLabExportToPdf.Services
{
    public class PdfService : IPdfService
    {

        static PdfService()
        {
            AsposeLicense.Register();
        }


        

        private static Document PdfDoc { get; set; }
        public int PageCount  => PdfDoc?.Pages.Count ?? 0;

        private static List<Field> Fields { get; set; }

        private static List<BitmapImage> ImagePreviews { get; set; }

        public void Init(string pdfFileName)
        {
            PdfDoc = new Document(pdfFileName);
            Fields = PdfDoc.Form.Fields.ToList();

            foreach (var field in Fields)
            {
                //if we don't do this, page indexes are lost at some point. Hug if we know why.
                var unused = field.PageIndex;
                PdfDoc.Form.Delete(field);
            }

            
            ImagePreviews = new List<BitmapImage>();
        }

        public void Init(FileStream[] imageStreams)
        {
            PdfDoc = new Document();
            Fields = new List<Field>();

            foreach (var stream in imageStreams)
            {
                PdfDoc.Pages.Add();
                AddPageWithImage(stream, PdfDoc.Pages.Count);
            }
            
            ImagePreviews = new List<BitmapImage>();
        }

        private void AddPageWithImage(Stream imageStream, int pageIndex)
        {
            var page = PdfDoc.Pages[pageIndex];
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = imageStream;
            image.EndInit();
            page.SetPageSize(image.Width, image.Height);
            page.Resources.Images.Add(imageStream);
           
            var matrix = new Matrix(new [] { image.Width, 0, 0, image.Height, 0, 0 });
            page.Contents.Add(new Operator.ConcatenateMatrix(matrix));
            var ximage = page.Resources.Images[page.Resources.Images.Count];

            page.Contents.Add(new Operator.Do(ximage.Name));
            page.Contents.Add(new Operator.GRestore());
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
            string text, string label, double fontSize, int pageIndex)
        {
            var pageW = PdfDoc.Pages[pageIndex].Rect.Width;
            var pageH = PdfDoc.Pages[pageIndex].Rect.Height;

            var scaleW = pageW / winW;
            var scaleH = pageH / winH;

            ulx = ulx * scaleW;
            uly = uly * scaleH;
            width = width * scaleW;
            height = height * scaleH;

            fontSize = fontSize * scaleW;

            var urx = ulx + width;
            var lly = pageH - (uly + height);
            var ury = pageH - uly;

            var marginH = 2 * scaleH;
            var marginW = 2 * scaleW;

            // Create a field
            var textBoxField = new TextBoxField(PdfDoc.Pages[pageIndex], new Rectangle(ulx, lly, urx, ury))
            {
                PartialName = label,
                Value = text,
                Color = Color.Transparent,
                DefaultAppearance = { FontSize = fontSize }
                ,
                Margin = new MarginInfo(marginW, marginH, marginW, marginH)
                ,
                Characteristics = { Border = System.Drawing.Color.Transparent }
                ,
                Name = label
                ,
                
            };


            var border = new Border(textBoxField)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };
            textBoxField.Border = border;

            // Add field to the document
            PdfDoc.Form.Add(textBoxField, pageIndex);


        }


        private string TranslateFontName(string compactName)
        {
            switch (compactName)
            {
                case "Cour": return "Courier";
                case "CoBo": return "CourierBold";
                case "CoOb": return "CourierOblique";
                case "CoBO": return "CourierBoldOblique";
                case "Helv": return "Helvetica";
                case "HeBo": return "HelveticaBold";
                case "HeOb": return "HelveticaOblique";
                case "HeBO": return "HelveticaBoldOblique";
                case "Symb": return "Symbol";
                case "TiRo": return "TimesRoman";
                case "TiBo": return "TimesBold";
                case "TiIt": return "TimesItalic";
                case "TiBI": return "TimesBoldItalic";
                case "ZaDb": return "ZapfDingbats";
                default: return compactName;
            }
        }


        public IEnumerable GetFields(double wpfW, double wpfH)
        {

            var pdfForm = new Aspose.Pdf.Facades.Form(PdfDoc);
            var fl = Fields.Select(f =>
                {
                    var pageW = PdfDoc.Pages[f.PageIndex].Rect.Width;
                    var pageH = PdfDoc.Pages[f.PageIndex].Rect.Height;
                    var scaleX = wpfW / pageW ;
                    var scaleY = wpfH / pageH ;                 

                    var font = FontRepository.FindFont(TranslateFontName(f.DefaultAppearance.FontName));

                    return (
                        page: f.PageIndex
                        , label: f.PartialName
                        , value: f.Value
                        , font: font.DecodedFontName
                        , type: f is CheckboxField ? 1 : 2
                        , width: f.Rect.Width * scaleX
                        , height: f.Rect.Height * scaleY
                        , x: f.Rect.LLX * scaleX
                        , y: (pageH - f.Rect.URY) * scaleY
                        , index: f.PageIndex
                    );
                }
            );
            return fl;
        }

        public void Save(string fileName)
        {
            PdfDoc.Save(fileName);
        }


        public BitmapImage GetImagePreview(int pageIndex)
        {

            if (ImagePreviews.Count >= pageIndex)
                return ImagePreviews[pageIndex - 1];

            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ToStream(pageIndex);
            image.EndInit();

            ImagePreviews.Add(image);
            return image;
        }


        private static Stream ToStream(int pageIndex)
        {
            var memoryStream = new MemoryStream();
            if (PdfDoc.Pages.Count >= pageIndex)
            {
                var device = new BmpDevice(new Resolution(150));
                device.Process(PdfDoc.Pages[pageIndex], memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}
