using System;
using System.Windows.Media;
using Caliburn.Micro;
using HeroLabExportToPdf.ViewModels;

namespace HeroLabExportToPdf.Services
{
    public class FieldFactory
    {
        private readonly Func<MenuViewModel> _makeMenu;
        private readonly IEventAggregator _eventAggregator;

        public FieldFactory(Func<MenuViewModel> makeMenu, IEventAggregator eventAggregator)
        {
            _makeMenu = makeMenu;
            _eventAggregator = eventAggregator;
        }

        public FieldViewModel Create<T>(T parentModel, int pageIndex, int tabIndex, double x, double y,
            double width, double height,
            Color color)
        {
            return new FieldViewModel(_eventAggregator, _makeMenu(), pageIndex, null, null, "Helvetica", x, y, width, height, color);
        }

        public FieldViewModel Create<T>(T parentModel, int pageIndex, int tabIndex, string text, string label,
            string fontFamily, double x,
            double y, double width, double height, Color color)
        {
            return new FieldViewModel(_eventAggregator, _makeMenu(), pageIndex, text, label, fontFamily, x, y, width, height, color);
        }

    }
}
