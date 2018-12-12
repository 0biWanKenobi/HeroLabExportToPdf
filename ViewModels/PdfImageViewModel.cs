using System.Windows.Media.Imaging;
using Caliburn.Micro;

namespace HeroLabExportToPdf.ViewModels
{
    public class PdfImageViewModel : PropertyChangedBase
    {
       
        private double _width, _height, _scaleX, _scaleY;
        private BitmapImage _image;

        public BitmapImage Image
        {
            get => _image;
            set  {
            if (_image == value) return;
                _image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }  

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
    }
}
