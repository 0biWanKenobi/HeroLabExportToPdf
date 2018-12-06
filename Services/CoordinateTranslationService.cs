using System.Windows;
using Caliburn.Micro;

namespace HeroLabExportToPdf.Services
{
    public class CoordinatesTranslationService : ICoordinateTranslationService
    {
        public void Translate<TViewModel>(TViewModel viewModel, string childName, double origX, double origY, out double x, out double y)
        {
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            var nx = 0d;
            var ny = 0d;

            view.Dispatcher.Invoke(() =>
            {
                var childElement = (view as FrameworkElement)?.FindName(childName);
                var newCoords = view.TranslatePoint(new Point(origX, origY), childElement as UIElement);
                nx = newCoords.X;
                ny = newCoords.Y;

            });

            x = nx;
            y = ny;

        }
    }
}
