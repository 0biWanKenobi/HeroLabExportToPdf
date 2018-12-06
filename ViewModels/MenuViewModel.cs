using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using HeroLabExportToPdf.Business;
using HeroLabExportToPdf.Entities;

namespace HeroLabExportToPdf.ViewModels
{
    public class MenuViewModel
    {
        public MenuViewModel(IEventAggregator eventAggregator)
        {
            Items = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel(eventAggregator) {Text = "Error! No Character Profile loaded"}
            };
        }

        public MenuViewModel(IEventAggregator eventAggregator, ICharacterService<Character> characterService)
        {
            var character = characterService.Character;
            Items = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel(eventAggregator) {Text = "Name", Value = character?.HeroName},
                new MenuItemViewModel(eventAggregator) {Text = "Class", Value = character?.Classes.FirstOrDefault()?.Name},
                new MenuItemViewModel(eventAggregator) {Text = "Level"},
                new MenuItemViewModel(eventAggregator) {Text = "Stats"},
                new MenuItemViewModel(eventAggregator) {Text = "Backpack"},
                new MenuItemViewModel(eventAggregator) {Text = "Armor", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel(eventAggregator){Text = "Name"},
                    new MenuItemViewModel(eventAggregator){Text = "Type"},
                    new MenuItemViewModel(eventAggregator){Text = "AC Bonus"},
                    new MenuItemViewModel(eventAggregator){Text = "Max Dex"},
                    new MenuItemViewModel(eventAggregator){Text = "Check Penalty"},
                    new MenuItemViewModel(eventAggregator){Text = "Spell Fail"},
                    new MenuItemViewModel(eventAggregator){Text = "Speed"},
                    new MenuItemViewModel(eventAggregator){Text = "Weight"}
                }},
                new MenuItemViewModel(eventAggregator){Text = "Weapon", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel(eventAggregator){Text = "Name"},
                    new MenuItemViewModel(eventAggregator){Text = "Type"},
                    new MenuItemViewModel(eventAggregator){Text = "Range"},
                    new MenuItemViewModel(eventAggregator){Text = "Ammunition"},
                    new MenuItemViewModel(eventAggregator){Text = "Damage"},
                    new MenuItemViewModel(eventAggregator){Text = "Attack Bonus"},
                    new MenuItemViewModel(eventAggregator){Text = "Critical"},
                }},
                new MenuItemViewModel(eventAggregator){Text = "Abilities", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel(eventAggregator){Text = "Ability1"},
                    new MenuItemViewModel(eventAggregator){Text = "Ability2"}
                }},
            };
        }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }
    }
}
