using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Caliburn.Micro;

namespace HeroLabExportToPdf.ViewModels
{
    public class CharacterSheetViewModel : Screen
    {
        private RectangleViewModel _selectedRectangle;

        private double _rectanglesWidth, _rectanglesHeight;


        public PdfImageViewModel PdfImage { get; set; }

        public double RectanglesWidth
        {
            get => _rectanglesWidth;
            set
            {
                if (_rectanglesWidth.Equals(value)) return;
                _rectanglesWidth = value;
                NotifyOfPropertyChange(() => RectanglesWidth);
            }
        }

        public double RectanglesHeight
        {
            get => _rectanglesHeight;
            set
            {
                if (_rectanglesHeight.Equals(value)) return;
                _rectanglesHeight = value;
                NotifyOfPropertyChange(() => RectanglesHeight);
            }
        }

       

        /// <summary>
        /// The list of rectangles that is displayed in the ListBox.
        /// </summary>
        public ObservableCollection<RectangleViewModel> Rectangles { get; } = new ObservableCollection<RectangleViewModel>();

        public RectangleViewModel SelectedRectangle
        {
            get => _selectedRectangle;
            set
            {
                if(_selectedRectangle != null)
                    _selectedRectangle.Selected = false;
                _selectedRectangle = value;
                if(value != null)
                _selectedRectangle.Selected = true;
                NotifyOfPropertyChange(() => SelectedRectangle);
            }
        }

  

        public CharacterSheetViewModel(string filePath)
        {
            PdfImage = new PdfImageViewModel(filePath);
            PdfImage.PropertyChanged += ScaleContent;
        }

      

        public void DeleteRectangle(ActionExecutionContext context)
        {
            if (context.EventArgs is KeyEventArgs keyArgs && keyArgs.Key == Key.Delete)
            {
                while(_selectedRectangle != null)
                {
                    _selectedRectangle.ResetSelectedItem();
                    Rectangles.Remove(_selectedRectangle);
                }
            }
        }

        public void ScaleContent(object o, PropertyChangedEventArgs  eventArgs)
        {

            
            if (eventArgs == null) return;

            switch (eventArgs.PropertyName)
            {
                case "Width": RectanglesWidth = PdfImage.Width;
                    break;
                case "Height":RectanglesHeight = PdfImage.Height;
                    break;
                case "ScaleX":
                    foreach (var rectangle in Rectangles) rectangle.ScaleX = PdfImage.ScaleX;
                    break;
                case "ScaleY": 
                    foreach (var rectangle in Rectangles) rectangle.ScaleY = PdfImage.ScaleY;
                    break;
            }
        }
    }
}
