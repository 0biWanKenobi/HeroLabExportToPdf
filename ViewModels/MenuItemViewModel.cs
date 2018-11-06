using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Caliburn.Micro;
using SampleCode.Commands;


namespace SampleCode.ViewModels
{
    public class MenuItemViewModel : PropertyChangedBase
    {
        private string _text;
        private bool _isItemSet;
        private RelayCommand _command;
       
        public MenuItemViewModel DataContext => this;

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

        public Action<MenuItemViewModel> CallBack { get; set; }


        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        public ICommand Command => _command ?? (_command = new RelayCommand(ActionMethod));

         


        public void ActionMethod(object param)
        {
            IsItemSet = true;
            CallBack(this);
        }
    }
}
