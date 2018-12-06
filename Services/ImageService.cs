using System.Windows.Media.Imaging;
using HeroLabExportToPdf.Business;

namespace HeroLabExportToPdf.Services
{
    public class ImageService : IImageService
    {
        public static string FilePath { get; set; }
        public BitmapImage Create()
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = PdfConvert.ToStream(FilePath);
            //image.DecodePixelHeight = y*2;
            //image.DecodePixelWidth = x*2;
            image.EndInit();
            return image;
        }
    }
}
