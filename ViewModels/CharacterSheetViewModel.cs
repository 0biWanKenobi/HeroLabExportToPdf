using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using HeroLabExportToPdf.Business;
using HeroLabExportToPdf.Entities.Messages;
using HeroLabExportToPdf.Services;

namespace HeroLabExportToPdf.ViewModels
{
    public class CharacterSheetViewModel : Screen, IHandle<CanvasDrawn>
    {

        #region injected parameters
        private readonly RectangleFactory _rectangleFactory;
        private readonly IEventAggregator _eventAggregator;

        #endregion


        #region bindings helpers
        private RectangleViewModel _selectedRectangle;
        private double _rectanglesWidth, _rectanglesHeight;
        private DrawingCanvasViewModel _dragSelectionCanvas;
        #endregion

        #region bindings

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

        public DrawingCanvasViewModel DragSelectionCanvas
        {
            get => _dragSelectionCanvas;
            set
            {
                if (_dragSelectionCanvas == value) return;
                _dragSelectionCanvas = value;
                NotifyOfPropertyChange(() => DragSelectionCanvas);
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

#endregion

        private readonly SynchronizationContext _context;

        public CharacterSheetViewModel(PdfImageViewModel image, DrawingCanvasViewModel drawingCanvasViewModel,
            RectangleFactory rectangleFactory, IEventAggregator eventAggregator)
        {
            PdfImage = image;
            _rectangleFactory = rectangleFactory;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            DragSelectionCanvas = drawingCanvasViewModel;
            _context = SynchronizationContext.Current;
        }


        public void DeleteRectangle(bool deleted)
        {
            if (deleted)
            {
                while(_selectedRectangle != null)
                {
                    PdfEdit.RemoveField(_selectedRectangle.Tooltip);
                    Rectangles.Remove(_selectedRectangle);
                }
            }
        }


        public bool CanScaleContent => true;
        public void ScaleContent((double width, double height) imageSize)
        {
            RectanglesWidth = imageSize.width;
            RectanglesHeight = imageSize.height;
            _eventAggregator.Publish(
                new ImageResize{ScaleX = PdfImage.ScaleX, ScaleY = PdfImage.ScaleY },
                action => {
                    Task.Factory.StartNew(action);
                }
            );
        }

        public void Handle(CanvasDrawn message)
        {
            var newField = _rectangleFactory.Create(this, message.X, message.Y, message.Width, message.Height,
                Color.FromArgb(200, 129, 63, 191));
            _context.Send(c =>
                    Rectangles.Add(newField)
                , null);

        }
    }
}
