using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HeroLabExportToPdf.Business;
using HeroLabExportToPdf.Entities;

namespace HeroLabExportToPdf.ViewModels
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            Items = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel {Text = "Error! No Character Profile loaded"}
            };
        }


        private ObservableCollection<MenuItemViewModel> ArmorKit(string name, string acBonus, string qty, string weight, string cost)
        {
            return new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel {Text = "Name", Value = name},
                new MenuItemViewModel {Text = "AC Bonus", Value = acBonus},
                new MenuItemViewModel {Text = "Quantity", Value = qty},
                new MenuItemViewModel {Text = "Weight", Value = weight},
                new MenuItemViewModel {Text = "Cost", Value = cost}
            };
        }

        private ObservableCollection<MenuItemViewModel> GearItem(string name, string qty, string weight, string cost)
        {
            return new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel {Text = "Name", Value = name},
                new MenuItemViewModel {Text = "Quantity", Value = qty},
                new MenuItemViewModel {Text = "Weight", Value = weight},
                new MenuItemViewModel {Text = "Cost", Value = cost}
            };
        }

        private ObservableCollection<MenuItemViewModel> Weapon(string name, string category, string type, string attack, string crit, string damage, string qty, string weight, string cost, string desc, List<string> weapTypes, List<string> WeapCats)
        {
            return new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel {Text = "Name", Value = name},
                new MenuItemViewModel {Text = "Category", Value = category},
                new MenuItemViewModel {Text = "Type", Value = type},
                new MenuItemViewModel {Text = "Attack", Value = attack},
                new MenuItemViewModel {Text = "Crit", Value = crit},
                new MenuItemViewModel {Text = "Damage", Value = damage},
                new MenuItemViewModel {Text = "Qty", Value = qty},
                new MenuItemViewModel {Text = "Weight", Value = weight},
                new MenuItemViewModel {Text = "Cost", Value = cost},
                new MenuItemViewModel {Text = "Description", Value = desc},
                new MenuItemViewModel {Text = "Categories", MenuItems = new ObservableCollection<MenuItemViewModel>(WeapCats.Select(wc => new MenuItemViewModel{Text = wc, Value = wc}))},
                new MenuItemViewModel {Text = "Attack Types", MenuItems = new ObservableCollection<MenuItemViewModel>(weapTypes.Select(wt => new MenuItemViewModel{Text = wt, Value = wt}))}
            };
        }


        public MenuViewModel(ICharacterService<Character> characterService)
        {
            var character = characterService.Character;
            var emptyOption = new List<MenuItemViewModel>() {new MenuItemViewModel {Text = "-"}};
          

            Items = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel {Text = "Name", Value = character?.HeroName},
                new MenuItemViewModel
                {
                      Text = "Classes"
                    , MenuItems = new ObservableCollection<MenuItemViewModel>( 
                        character?.Classes != null 
                            ? character.Classes.Select( c => new MenuItemViewModel{Text = c.Name, Value = c.Name}) 
                            : emptyOption
                      )
                },
                new MenuItemViewModel {
                      Text = "Levels"
                    , MenuItems = new ObservableCollection<MenuItemViewModel>(
                        character?.Classes != null
                        ? character.Classes.Select( c => new MenuItemViewModel{ Text = $"{c.Name} - {c.Level}", Value = c.Level})
                        : emptyOption
                    
                    )
                },
                new MenuItemViewModel {Text = "Stats"},
                new MenuItemViewModel {Text = "Backpack", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    character?.Gear != null
                    ? character.Gear.Select( g => new MenuItemViewModel
                        {
                            Text = g.Name,
                            MenuItems = GearItem(g.Name, g.Quantity, g.Weight.Text, g.Cost.Text)
                        } )
                    : emptyOption
                )},
                new MenuItemViewModel {Text = "Armor", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    
                   character?.Defenses != null
                        ? character.Defenses.Select( d => new MenuItemViewModel
                       {
                           Text = d.Name,
                           MenuItems = ArmorKit(d.Name, d.Ac, d.Quantity, d.Weight.Text, d.Cost.Text)
                       })
                        : emptyOption
                    
                    )
                },
                new MenuItemViewModel{Text = "Weapons", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel{Text = "Melee", MenuItems = new ObservableCollection<MenuItemViewModel>
                    (
                        character?.MeleeWeapons != null
                            ? character.MeleeWeapons.Select( w =>  new MenuItemViewModel
                            {
                                Text = w.Name,
                                MenuItems = Weapon(w.Name, w.Category, w.Type, w.Attack, w.Crit, w.Damage, w.Quantity, w.Weight.Text, w.Cost.Text, w.Description, w.WeaponTypes, w.WeaponCategories)
                            })
                            : emptyOption
                    )}


                        

                    
                }},
                new MenuItemViewModel{Text = "Abilities", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel{Text = "Ability1"},
                    new MenuItemViewModel{Text = "Ability2"}
                }},
            };
        }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }
    }
}
