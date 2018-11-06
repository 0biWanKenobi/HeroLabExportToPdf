using System.Windows.Media.Imaging;
using Caliburn.Micro;
using HeroLabExportToPdf.Business;

namespace HeroLabExportToPdf.ViewModels
{
    public class PdfImageViewModel : PropertyChangedBase
    {
        public BitmapImage Image { get; }
        private double _width, _height, _scaleX, _scaleY;

        public double Width
        {
            get => _width;
            set
            {
                if (_width.Equals(value) || double.IsNaN(value)) return;
                
                ScaleX = _width == 0 ? 1 : value / _width;
                _width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (_height.Equals(value) || double.IsNaN(value)) return;
                ScaleY = _height == 0 ? 1 : value / _height;
                _height = value;
                NotifyOfPropertyChange(() => Height);
            }
        }

        public double ScaleX
        {
            get => _scaleX;
            set
            {
                if (_scaleX.Equals(value)) return;
                _scaleX = value;
                NotifyOfPropertyChange(() => ScaleX);
            }
        }

        public double ScaleY
        {
            get => _scaleY;
            set
            {
                if (_scaleY.Equals(value)) return;
                _scaleY = value;
                NotifyOfPropertyChange(() => ScaleY);
            }
        }

        public PdfImageViewModel(string filePath)
        {
            var imageByteArray = PdfConvert.ToImageByteArray(filePath, out var x, out var y);

            Image = new BitmapImage();
            Image.BeginInit();
            Image.DecodePixelHeight = y*2;
            Image.DecodePixelWidth = x*2;
            Image.StreamSource = imageByteArray;
            Image.EndInit();
            PdfEdit.Init(filePath);

        }

        public void AddTextBox(double llx, double lly, double urx, double ury)
        {
            PdfEdit.AddField(llx, lly, urx, ury, Width, Height);
        }

        public void SavePdf(string fileName)
        {
            PdfEdit.Save(fileName);
        }
    }
}
