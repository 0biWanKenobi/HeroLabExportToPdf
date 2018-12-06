using System;
using System.Windows.Media;
using Caliburn.Micro;
using HeroLabExportToPdf.ViewModels;

namespace HeroLabExportToPdf.Services
{
    public class RectangleFactory
    {
        private readonly Func<MenuViewModel> _makeMenu;
        private readonly IEventAggregator _eventAggregator;
        private readonly ICoordinateTranslationService _coordinateTranslationService;

        public RectangleFactory(Func<MenuViewModel> makeMenu, ICoordinateTranslationService coordinateTranslationService,
            IEventAggregator eventAggregator)
        {
            _makeMenu = makeMenu;
            _coordinateTranslationService = coordinateTranslationService;
            _eventAggregator = eventAggregator;
        }

        public RectangleViewModel Create<T>(T parentModel, double x, double y, double width, double height, Color color)
        {
            //_coordinateTranslationService.Translate(parentModel, "Rectangles", x, y, out var tx, out var ty);
            return new RectangleViewModel(_eventAggregator, _makeMenu(), x, y, width, height, color);
        }

    }
}
