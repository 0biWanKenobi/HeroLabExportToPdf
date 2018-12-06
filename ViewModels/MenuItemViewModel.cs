using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace HeroLabExportToPdf.ViewModels
{
    public class MenuItemViewModel : PropertyChangedBase
    {
        private string _text, _value;
        private bool _isItemSet;
        private readonly IEventAggregator _eventAggregator;
        
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public bool IsItemSet
        {
            get => _isItemSet;
            set
            {
                if (_isItemSet == value) return;
                _isItemSet = value;
                NotifyOfPropertyChange(() => IsItemSet);
            }
        }

       
        
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        public MenuItemViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
       
    }
}
