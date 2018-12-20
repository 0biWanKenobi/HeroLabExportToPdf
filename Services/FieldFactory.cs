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

        public FieldViewModel Create<T>(T parentModel, int pageIndex, int tabIndex, int type, double x, double y,
            double width, double height,
            Color color)
        {
            return new FieldViewModel(_eventAggregator, _makeMenu(), pageIndex, type, null, null, "Helvetica", x, y, width, height, color);
        }

        public FieldViewModel Create<T>(T parentModel, int pageIndex, int tabIndex, int type, string text, string label,
            string fontFamily, double x,
            double y, double width, double height, Color color)
        {
            return new FieldViewModel(_eventAggregator, _makeMenu(), pageIndex, type, text, label, fontFamily, x, y, width, height, color);
        }

    }
}
