using System;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using HeroLabExportToPdf.Entities.Messages;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace HeroLabExportToPdf.ViewModels
{
    public class DrawingCanvasViewModel :PropertyChangedBase
    {
        private bool _canDraw;
        private bool _isDrawing;
        private bool _startedDrawing;
        private const double DragDelta = 5.0;

        private double _xOrigin;
        private double _yOrigin;
        private double _height;
        private double _width;

        private readonly IEventAggregator _eventAggregator;

        public bool CanDraw
        {
            get => _canDraw;
            set

            {

                if (value == _canDraw)
                    return;

                _canDraw = value;
                NotifyOfPropertyChange(() => CanDraw);

            }
        }
        
        public double XOrigin
        {
            get => _xOrigin;
            set
            {
                if (value == _xOrigin) return;
                else _xOrigin = value;
                NotifyOfPropertyChange(() => XOrigin);
            }
        }
        
        public double YOrigin
        {
            get => _yOrigin;
            set
            {
                if (value == _yOrigin) return;
                else _yOrigin = value;
                NotifyOfPropertyChange(() => YOrigin);
            }

        }

        public double Width
        {
            get => _width;
            set
            {
                if (value == _width) return;
                else _width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (value == _height) return;
                else _height = value;
                NotifyOfPropertyChange(() => Height);
            }

        }

        private double _initSelX, _initSelY;



        


        public DrawingCanvasViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }


        public bool CanInitCanvasDrawing => true;
        public void InitCanvasDrawing(double x, double y)
        {
            _startedDrawing = true;
            XOrigin = x;
            YOrigin = y;
            _initSelX = x;
            _initSelY = y;
        }

        public bool CanCanvasDrawing => _startedDrawing;
        public void CanvasDrawing(double x, double y)
        {
            if (_isDrawing)
            {
                UpdateCanvasPosition(x, y);
            }

            else if (_startedDrawing)
            {
                var dragDelta = (new Point(x, y) - new Point(XOrigin, YOrigin));
                if (Math.Abs(dragDelta.Length) > DragDelta)
                {
                    _isDrawing = true;
                    InitDrawing(x, y);
                }

            }
        }


        private void UpdateCanvasPosition(double x, double y)
        {
            XOrigin = Math.Min(_initSelX, x);
            Width = Math.Abs(x - _initSelX);

            YOrigin = Math.Min(_initSelY, y);
            Height = Math.Abs(y - _initSelY);
        }

        private void InitDrawing(double x, double y)
        {
            UpdateCanvasPosition(x, y);
            CanDraw = true;
        }

        public bool CanStopDrawing => true;
        public void StopDrawing()
        {
            if(_isDrawing)
            {
                _isDrawing = false;
                _eventAggregator.Publish(new CanvasDrawn
                {
                    X = XOrigin,
                    Y = YOrigin,
                    Width =  Width,
                    Height = Height
                }, action => {
                    Task.Factory.StartNew(action);
                });
            }
            if (!_startedDrawing)
                return;
            _startedDrawing = false;
            CanDraw = false;

        }
    }
}
