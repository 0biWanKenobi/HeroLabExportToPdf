﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using HeroLabExportToPdf.Entities.Messages;

namespace HeroLabExportToPdf.ViewModels
{
    /// <inheritdoc cref="PropertyChangedBase" />
    /// <summary>
    /// Defines the view-model for a simple displayable rectangle.
    /// </summary>
    public class FieldViewModel : PropertyChangedBase, IHandle<ImageResize>
    {
        #region Data Members

        private int _page;

        private ObservableCollection<MenuItemViewModel> _rectangleContextMenu;

        private string _tooltip;

        private double _scaleX, _scaleY;

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

        private string _text;

        #endregion Data Members

        #region Properties

        public string Id { get; private set; }

        public int Page
        {
            get => _page;
            set
            {
                if (_page == value) return;
                _page = value;
                NotifyOfPropertyChange(() => Page);
            }
        }
       
        public FontFamily FontFamily { get; set; }

        public double FontSize{get; private set; }

        public string Text
        {
            get =>  _text;
            set
            {
                if (_text == value) return;
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }
        
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

        public string Tooltip
        {
            get => _tooltip;
            set
            {
                if (_tooltip == value) return;
                _tooltip = value;
                NotifyOfPropertyChange(() => Tooltip);
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

        public bool CanDoRepositioning { get; set; }

        #endregion

       

        #region private properties
        private double ScaleX
        {
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
        private double ScaleY
        {
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
        public MenuItemType Type { get; set; }
        

        private double _initialDragX, _initialDragY;
        

        #endregion
       
        public FieldViewModel(IEventAggregator eventAggregator, MenuViewModel menuViewModel, int pageIndex, string text,
            string label, string fontFamily, double x, double y,
            double width, double height, Color color) 
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _color = color;
            _scaleX = 1;
            _scaleY = 1;
            Page = pageIndex;
            Text = text;
            Tooltip = label;
            FontFamily = new FontFamily(fontFamily);

            CanDoRepositioning = false;
            eventAggregator.Subscribe(this);
            _rectangleContextMenu = menuViewModel.Items;
        }

        public bool CanInitRepositioning => true;
        public void InitRepositioning(double x, double y)
        {
            _initialDragX = x;
            _initialDragY = y;
            CanDoRepositioning = true;
        }

        public void DoRepositioning(double x, double y)
        {
            X += x -_initialDragX;
            Y += y - _initialDragY;
          
        }

        public bool CanEndRepositioning => true;
        public void EndRepositioning()
        {
            CanDoRepositioning = false;
        }

        public void Handle(ImageResize message)
        {
            ScaleX = message.ScaleX;
            ScaleY = message.ScaleY;
        }
        

        public void UpdateField(MenuItemViewModel selectedMenuOption)
        {
            Tooltip = selectedMenuOption.Description;
            Type = selectedMenuOption.Type;
            Text = Type == MenuItemType.Checkbox 
                ?  selectedMenuOption.Value.ToLower() == "yes" 
                    ? "\u2713" 
                    : "\u20E0"
                : selectedMenuOption.Value ;
            
            Id = selectedMenuOption.Id;
        }


        public void Resize((double deltah, double deltav, bool istop, bool isbottom, bool isleft, bool isright) delta)
        {
            if (delta.istop)
                Y += delta.deltav;
            if(delta.istop || delta.isbottom)
            Height -= delta.deltav;

            if (delta.isleft)
                X += delta.deltah;
            if(delta.isleft || delta.isright)
            Width -= delta.deltah;
        }

        

        public void FontSizeChange(double scale)
        {
            FontSize = 12 * scale;
        }
    }
}
