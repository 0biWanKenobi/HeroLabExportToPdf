using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;

namespace SampleCode.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Defines the view-model for a simple displayable rectangle.
    /// </summary>
    public class RectangleViewModel : PropertyChangedBase
    {
        #region Data Members

        private static ObservableCollection<MenuItemViewModel> _rectangleContextMenu;

        private MenuItemViewModel _selectedItemViewModel;

        private double _scaleX, _scaleY;

        private double _padding;

        /// <summary>
        /// The X coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        private double _x;

        /// <summary>
        /// The Y coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        private double _y;

        /// <summary>
        /// The width of the rectangle (in content coordinates).
        /// </summary>
        private double _width;

        /// <summary>
        /// The height of the rectangle (in content coordinates).
        /// </summary>
        private double _height;

        /// <summary>
        /// The color of the rectangle.
        /// </summary>
        private Color _color;

        /// <summary>
        /// The hotspot of the rectangle's connector.
        /// This value is pushed through from the UI because it is data-bound to 'Hotspot'
        /// in ConnectorItem.
        /// </summary>
        private Point _connectorHotspot;

        #endregion Data Members

        #region Properties

        public bool Selected { get; set; }

        public MenuItemViewModel SelectedItemViewModel
        {
            get => _selectedItemViewModel;
            set
            {
                if (!Selected) return;
                if(_selectedItemViewModel != null)
                    _selectedItemViewModel.IsItemSet = false;
                _selectedItemViewModel = value;
                if(value != null)
                    _selectedItemViewModel.IsItemSet = true;
                NotifyOfPropertyChange(() => SelectedItemViewModel);
            } }

        public ObservableCollection<MenuItemViewModel> RectangleContextMenu
        {
            get => _rectangleContextMenu;
            set
            {
                if (_rectangleContextMenu == value) return;
                _rectangleContextMenu = value;
                NotifyOfPropertyChange(() => RectangleContextMenu);
            }
        }

        public double ScaleX
        {
            get => _scaleX;
            set
            {
                if (_scaleX.Equals(value)) return;
                _scaleX = value;
                _x = _x * _scaleX;
                _width = _width * _scaleX;
                NotifyOfPropertyChange(() => Width);
                NotifyOfPropertyChange(() => X);
            }
        }
        public double ScaleY
        {
            get => _scaleY;
            set
            {
                if (_scaleY.Equals(value)) return;
                _scaleY = value;
                _y = _y * _scaleY;
                _height = _height * _scaleY;
                NotifyOfPropertyChange(() => Height);
                NotifyOfPropertyChange(() => Y);
            }
        }

        public double Padding
        {
            get => _padding;
            set
            {
                if (_padding.Equals(value)) return;
                _padding = value;
            }
        }

        /// <summary>
        /// The X coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        public double X
        {
            get => _x;
            set
            {
                if (_x.Equals(value))
                {
                    return;
                }
                _x = value;
                NotifyOfPropertyChange(() => X);
                
            }
        }

        /// <summary>
        /// The Y coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        public double Y
        {
            get =>_y;
            set
            {
                if (_y.Equals(value))
                {
                    return;
                }
                _y = value;
                NotifyOfPropertyChange(() => Y);
            }
        }

        /// <summary>
        /// The width of the rectangle (in content coordinates).
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                if (_width.Equals(value))
                {
                    return;
                }
                _width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }

        /// <summary>
        /// The height of the rectangle (in content coordinates).
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                if (_height.Equals(value))
                {
                    return;
                }
                _height = value;
                NotifyOfPropertyChange(() => Height);
            }
        }

        /// <summary>
        /// The color of the item.
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                if (_color == value)
                {
                    return;
                }

                _color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }

        /// <summary>
        /// The hotspot of the rectangle's connector.
        /// This value is pushed through from the UI because it is data-bound to 'Hotspot'
        /// in ConnectorItem.
        /// </summary>
        public Point ConnectorHotspot
        {
            get => _connectorHotspot;
            set
            {
                if (_connectorHotspot == value)
                {
                    return;
                }
                _connectorHotspot = value;
                NotifyOfPropertyChange(() => ConnectorHotspot);
            }
        }

        
      
        #endregion


        static RectangleViewModel()
        {
            _rectangleContextMenu = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel{Text = "Name"},
                new MenuItemViewModel{Text = "Class"},
                new MenuItemViewModel{Text = "Level"},
                new MenuItemViewModel{Text = "Stats"},
                new MenuItemViewModel{Text = "Backpack"},
                new MenuItemViewModel{Text = "Armor", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel{Text = "Name"},
                    new MenuItemViewModel{Text = "Type"},
                    new MenuItemViewModel{Text = "AC Bonus"},
                    new MenuItemViewModel{Text = "Max Dex"},
                    new MenuItemViewModel{Text = "Check Penalty"},
                    new MenuItemViewModel{Text = "Spell Fail"},
                    new MenuItemViewModel{Text = "Speed"},
                    new MenuItemViewModel{Text = "Weight"}
                }},
                new MenuItemViewModel{Text = "Weapon", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel{Text = "Name"},
                    new MenuItemViewModel{Text = "Type"},
                    new MenuItemViewModel{Text = "Range"},
                    new MenuItemViewModel{Text = "Ammunition"},
                    new MenuItemViewModel{Text = "Damage"},
                    new MenuItemViewModel{Text = "Attack Bonus"},
                    new MenuItemViewModel{Text = "Critical"},
                }},
                new MenuItemViewModel(){Text = "Abilities", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel{Text = "Ability1"},
                    new MenuItemViewModel{Text = "Ability2"}
                }},
            };
        }

        public RectangleViewModel()
        {
        }

        public RectangleViewModel(double x, double y, double width, double height, Color color)
        {
            _padding = 3.0;
            var doublep = _padding * 2.0;
            _x = x - doublep;
            _y = y - doublep;
            _width = width;
            _height = height;
            _color = color;
            _scaleX = 1;
            _scaleY = 1;



            foreach (var menuItemViewModel in RectangleContextMenu)
            {
                menuItemViewModel.CallBack += OnOptionSelected;
            }
        }

        public void ResetSelectedItem()
        {
            if (SelectedItemViewModel != null)
            {
                SelectedItemViewModel.IsItemSet = !SelectedItemViewModel.IsItemSet;
            }
        }

        public void MenuOpened()
        {
            Selected = true;
        }

        public void OnOptionSelected(MenuItemViewModel selectedItemViewModel)
        {
            if(Selected)
                SelectedItemViewModel = selectedItemViewModel;
        }
    }
}
