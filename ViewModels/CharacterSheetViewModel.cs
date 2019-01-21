using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using HeroLabExportToPdf.Entities;
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
        private ObservableCollection<MenuItemViewModel> _menu;
        private FieldViewModel _selectedField;
        private int _currentPageIndex;
        private double _formFieldsWidth, _formFieldsHeight;
        private DrawingCanvasViewModel _dragSelectionCanvas;
        #endregion

        #region bindings

        public ObservableCollection<MenuItemViewModel> Menu
        {
            get => _menu;
            set
            {
                if (_menu == value) return;
                _menu = value;
                NotifyOfPropertyChange(() => Menu);
            }
        }

        public PdfImageViewModel PdfImage { get; set; }

        public int CurrentPageIndex { 
            get => _currentPageIndex;
            set
            {
                if (_currentPageIndex == value) return;
                _currentPageIndex = value;
                NotifyOfPropertyChange(() => CurrentPageIndex);
                NotifyOfPropertyChange(() => CanNextPage);
                NotifyOfPropertyChange(() => CanPrevPage);
            }
        }

        public double FormFieldsWidth
        {
            get => _formFieldsWidth;
            set
            {
                if (_formFieldsWidth.Equals(value)) return;
                _formFieldsWidth = value;
                NotifyOfPropertyChange(() => FormFieldsWidth);
            }
        }

        public double FormFieldsHeight
        {
            get => _formFieldsHeight;
            set
            {
                if (_formFieldsHeight.Equals(value)) return;
                _formFieldsHeight = value;
                NotifyOfPropertyChange(() => FormFieldsHeight);
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
        public ObservableCollection<FieldViewModel> FormFields { get; } = new ObservableCollection<FieldViewModel>();

        public FieldViewModel SelectedField
        {
            get => _selectedField;
            set
            {
                if (value == _selectedField) return;
                _selectedField = value;
                NotifyOfPropertyChange(() => SelectedField);
            }
        }

#endregion

        private double _treeWidth;

        public double TreeWidth
        {
            get => _treeWidth;
            set
            {
                if (_treeWidth == value) return;
                _treeWidth = value;
                NotifyOfPropertyChange(() => TreeWidth);
            }
        }

        private bool _clickedToMinimize;

        public bool ClickedToMinimize
        {
            get => _clickedToMinimize;
            set
            {
                if (_clickedToMinimize == value) return;
                _clickedToMinimize = value;
                NotifyOfPropertyChange(() => ClickedToMinimize);
            }
        }

        

        #region guards

        public bool CanPrevPage => CurrentPageIndex > 1;
        public bool CanNextPage => CurrentPageIndex < _pdfService.PageCount;

        private bool _canUpdateFormFields;

        public bool CanUpdateFormFields
        {
            get => _canUpdateFormFields;
            set
            {
                if (_canUpdateFormFields == value) return;
                _canUpdateFormFields = value;
                NotifyOfPropertyChange(() => CanUpdateFormFields);
            }
        }

        #endregion

        private readonly SynchronizationContext _context;
        

        public CharacterSheetViewModel(DrawingCanvasViewModel drawingCanvasViewModel, MenuViewModel menuViewModel,
            FieldFactory fieldFactory, IPdfService pdfService, IEventAggregator eventAggregator)
        {
            _pdfService = pdfService;
            PdfImage = new PdfImageViewModel();
            _fieldFactory = fieldFactory;
            Menu = menuViewModel.Items;
            TreeWidth = 20;

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            DragSelectionCanvas = drawingCanvasViewModel;
            _context = SynchronizationContext.Current;
            CurrentPageIndex = 1;
            CanUpdateFormFields = true;


        }

        public void ToggleTree()
        {
            TreeWidth = TreeWidth > 16 ? 16 : 20;
            ClickedToMinimize = TreeWidth == 16;
        }

        public void SetSheetPreview()
        {
            PdfImage.Image = _pdfService.GetImagePreview(1);
            NotifyOfPropertyChange(() => CanNextPage);
        }


        

        public void UpdateFormFields()
        {
            FormFields.Clear();

            foreach ((int pageIndex, string label, string value, string font, int type, double width, double height, double x, double y, int index) field in _pdfService.GetFields(PdfImage.Width, PdfImage.Height))
            {
                var r = _fieldFactory.Create(this, field.pageIndex, FormFields.Count, field.type, field.value, field.label, field.font, field.x, field.y, field.width, field.height,
                    Color.FromRgb(129, 63, 191));
                
                FormFields.Add(r);
            }
        }

        public void UpdateFormFields(List<FormField> formFields)
        {
            FormFields.Clear();
            foreach (var field in formFields)
            {
                var r = _fieldFactory.Create(this, field.Page, FormFields.Count, field.Type, field.X, field.Y, field.Width, field.Height,
                    Color.FromRgb(129, 63, 191));

                var matchingMenuElement = FindById(Menu, field.Id);

                r.Text = matchingMenuElement?.Value;
                r.Tooltip = matchingMenuElement?.Text;
                

                FormFields.Add(r);
            }
        }

        private MenuItemViewModel FindById(IEnumerable<MenuItemViewModel> items,  string id)
        {
            if (items == null || id == null) return null;

            foreach (var menuItemViewModel in items)
            {
                if (menuItemViewModel.Id == id) return menuItemViewModel;
                var item = FindById(menuItemViewModel.MenuItems, id);
                if (item != null) return item;
            }
            return null;
        }

        public void DeleteFormField(bool deleted)
        {
            if (!deleted) return;
            while(_selectedField != null)
            {
                _pdfService.RemoveField(_selectedField.Tooltip);
                FormFields.Remove(_selectedField);
            }
        }

        public bool CanScaleContent => true;
        public void ScaleContent((double width, double height) imageSize)
        {
            FormFieldsWidth = imageSize.width;
            FormFieldsHeight = imageSize.height;
            _eventAggregator.Publish(
                new ImageResize{ScaleX = PdfImage.ScaleX, ScaleY = PdfImage.ScaleY },
                action => {
                    Task.Factory.StartNew(action);
                }
            );
        }

        public void Handle(CanvasDrawn message)
        {
            var newField = _fieldFactory.Create(this, CurrentPageIndex, FormFields.Count, 0, message.X, message.Y, message.Width, message.Height,
                Color.FromRgb( 129, 63, 191));
            _context.Send(c =>
                    FormFields.Add(newField)
                , null);

        }

        

        public void PrevPage()
        {
            CanUpdateFormFields = false;
            CurrentPageIndex--;
            PdfImage.Image = _pdfService.GetImagePreview(CurrentPageIndex);
            CanUpdateFormFields = true;
        }

        public void NextPage()
        {
            CanUpdateFormFields = false;
            CurrentPageIndex++;
            PdfImage.Image = _pdfService.GetImagePreview(CurrentPageIndex);
            CanUpdateFormFields = true;
        }

    }
}
