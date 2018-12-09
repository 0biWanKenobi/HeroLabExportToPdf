﻿using System.Collections.ObjectModel;
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
        private readonly FieldFactory _fieldFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPdfService _pdfService;

        #endregion


        #region bindings helpers
        private FieldViewModel _selectedField;
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
        public ObservableCollection<FieldViewModel> Rectangles { get; } = new ObservableCollection<FieldViewModel>();

        public FieldViewModel SelectedField
        {
            get => _selectedField;
            set
            {
                if(_selectedField != null)
                    _selectedField.Selected = false;
                _selectedField = value;
                if(value != null)
                _selectedField.Selected = true;
                NotifyOfPropertyChange(() => SelectedField);
            }
        }

#endregion

        private readonly SynchronizationContext _context;

        public CharacterSheetViewModel(PdfImageViewModel image, DrawingCanvasViewModel drawingCanvasViewModel,
            FieldFactory fieldFactory, IPdfService pdfService, IEventAggregator eventAggregator)
        {
            PdfImage = image;
            _fieldFactory = fieldFactory;
            _pdfService = pdfService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            DragSelectionCanvas = drawingCanvasViewModel;
            _context = SynchronizationContext.Current;


            
        }

        public void UpdateRectangles()
        {
            foreach ((string label, string value, string font, int type, double width, double height, double x, double y, int index) field in _pdfService.GetFields(PdfImage.Width, PdfImage.Height))
            {
                var r = _fieldFactory.Create(this, field.type, field.value, field.label, field.font, field.x, field.y, field.width, field.height,
                    Color.FromArgb(200, 129, 63, 191));
                
                Rectangles.Add(r);
            }
        }

        public void DeleteRectangle(bool deleted)
        {
            if (deleted)
            {
                while(_selectedField != null)
                {
                    _pdfService.RemoveField(_selectedField.Tooltip);
                    Rectangles.Remove(_selectedField);
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
            var newField = _fieldFactory.Create(this, 0, message.X, message.Y, message.Width, message.Height,
                Color.FromArgb(200, 129, 63, 191));
            _context.Send(c =>
                    Rectangles.Add(newField)
                , null);

        }
    }
}
