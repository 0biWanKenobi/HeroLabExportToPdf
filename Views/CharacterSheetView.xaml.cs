using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HeroLabExportToPdf.ViewModels;

namespace HeroLabExportToPdf.Views
{
    public partial class CharacterSheetView
    {
        #region Data Members

        /// <summary>
        /// Set to 'true' when the left mouse-button is down.
        /// </summary>
        private bool _isLeftMouseButtonDownOnWindow;

        /// <summary>
        /// Set to 'true' when dragging the 'selection rectangle'.
        /// Dragging of the selection rectangle only starts when the left mouse-button is held down and the mouse-cursor
        /// is moved more than a threshold distance.
        /// </summary>
        private bool _isDraggingSelectionRect;

        /// <summary>
        /// Records the location of the mouse (relative to the window) when the left-mouse button has pressed down.
        /// </summary>
        private Point _origMouseDownPoint;

       

        /// <summary>
        /// The threshold distance the mouse-cursor must move before drag-selection begins.
        /// </summary>
        private const double DragThreshold = 5;

        #endregion Data Members

        public CharacterSheetView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Convenient accessor for the view model.
        /// </summary>
        private CharacterSheetViewModel CharacterSheetViewModel => (CharacterSheetViewModel) DataContext;

        /// <summary>
        /// Event raised when the Window has loaded.
        /// </summary>
        

        /// <summary>
        /// Event raised when the user presses down the left mouse-button.
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            _isLeftMouseButtonDownOnWindow = true;
            _origMouseDownPoint = e.GetPosition(this);
           

            CaptureMouse();

            e.Handled = true;
        }

        /// <summary>
        /// Event raised when the user releases the left mouse-button.
        /// </summary>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            if (_isDraggingSelectionRect)
            {
                //
                // Drag selection has ended, apply the 'selection rectangle'.
                //

                _isDraggingSelectionRect = false;
                ApplyDragSelectionRect();

                e.Handled = true;
            }

            if (!_isLeftMouseButtonDownOnWindow) return;
            _isLeftMouseButtonDownOnWindow = false;
            ReleaseMouseCapture();

            e.Handled = true;
        }

        /// <summary>
        /// Event raised when the user moves the mouse button.
        /// </summary>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDraggingSelectionRect)
            {
                //
                // Drag selection is in progress.
                //
                var curMouseDownPoint = e.GetPosition(this);
                UpdateDragSelectionRect(_origMouseDownPoint, curMouseDownPoint);

                e.Handled = true;
            }
            else if (_isLeftMouseButtonDownOnWindow)
            {
                //
                // The user is left-dragging the mouse,
                // but don't initiate drag selection until
                // they have dragged past the threshold value.
                //
                var curMouseDownPoint = e.GetPosition(this);
                var dragDelta = curMouseDownPoint - _origMouseDownPoint;
                var dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > DragThreshold)
                {
                    //
                    // When the mouse has been dragged more than the threshold value commence drag selection.
                    //
                    _isDraggingSelectionRect = true;

                    //
                    //  Clear selection immediately when starting drag selection.
                    //
                    Rectangles.SelectedItems.Clear();

                    InitDragSelectionRect(_origMouseDownPoint, curMouseDownPoint);
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// Initialize the rectangle used for drag selection.
        /// </summary>
        private void InitDragSelectionRect(Point pt1, Point pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);

            DragSelectionCanvas.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Update the position and size of the rectangle used for drag selection.
        /// </summary>
        private void UpdateDragSelectionRect(Point pt1, Point pt2)
        {
            //
            // Determine x,y,width and height of the rect inverting the points if necessary.
            // 

            var x = Math.Min(pt2.X, pt1.X);
            var width = Math.Abs(pt1.X - pt2.X);
     
            var y = Math.Min(pt2.Y, pt1.Y);
            var height = Math.Abs(pt1.Y - pt2.Y);

            //
            // Update the coordinates of the rectangle used for drag selection.
            //

            Canvas.SetLeft(DragSelectionBorder, x);
            Canvas.SetTop(DragSelectionBorder, y);
            DragSelectionBorder.Width = width;
            DragSelectionBorder.Height = height;
        }

        /// <summary>
        /// Select all nodes that are in the drag selection rectangle.
        /// </summary>
        private void ApplyDragSelectionRect()
        {
            DragSelectionCanvas.Visibility = Visibility.Collapsed;

            var x = Canvas.GetLeft(DragSelectionBorder);
            var y = Canvas.GetTop(DragSelectionBorder);
            var width = DragSelectionBorder.Width;
            var height = DragSelectionBorder.Height;
            var dragRect = new Rect(x, y, width, height);

            

            var relPoint  = TranslatePoint(new Point(x, y), Rectangles);
            x = relPoint.X;
            y = relPoint.Y;

            if(Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                
                var newRect = new RectangleViewModel(x, y, width, height, Color.FromArgb(200,100, 100, 250));
                
                CharacterSheetViewModel.Rectangles.Add(newRect);

                CharacterSheetViewModel.PdfImage.AddTextBox(x, y, width, height);
                CharacterSheetViewModel.PdfImage.SavePdf("output.pdf");
                return;
            }
           

            //
            // Inflate the drag selection-rectangle by 1/10 of its size to 
            // make sure the intended item is selected.
            //
            dragRect.Inflate(width / 10, height / 10);

            //
            // Clear the current selection.
            //
            Rectangles.SelectedItems.Clear();

            //
            // Find and select all the list box items.
            //
            foreach (var rectangleViewModel in CharacterSheetViewModel.Rectangles)
            {
                var itemRect = new Rect(rectangleViewModel.X, rectangleViewModel.Y, rectangleViewModel.Width, rectangleViewModel.Height);
                if (dragRect.Contains(itemRect))
                {
                    Rectangles.SelectedItems.Add(rectangleViewModel);
                }
            }
        }
    }
}
