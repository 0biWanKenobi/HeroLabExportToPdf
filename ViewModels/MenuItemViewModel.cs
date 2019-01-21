using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace HeroLabExportToPdf.ViewModels
{
    public class MenuItemViewModel : PropertyChangedBase
    {
        private string _text, _value;
        
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

        public string Description { get; set; }

        public string Id { get; set; }

        

       
        
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}
