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
                new MenuItemViewModel {Text = "Name",     Value = name,     Description = "Armor Name",      Id = $"{name}.armor.name"},
                new MenuItemViewModel {Text = "AC Bonus", Value = acBonus,  Description = "Armor AC Bonus",  Id = $"{name}.armor.ac.bonus"    },
                new MenuItemViewModel {Text = "Quantity", Value = qty,      Description = "Armor Quantity",  Id = $"{name}.armor.quantity"    },
                new MenuItemViewModel {Text = "Weight",   Value = weight,   Description = "Armor Weight",    Id = $"{name}.armor.weight"  },
                new MenuItemViewModel {Text = "Cost",     Value = cost,     Description = "Armor Cost",      Id = $"{name}.armor.cost"}
            };
        }


        private ObservableCollection<MenuItemViewModel> GearItem(string name, string qty, string weight, string cost)
        {
            return new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel {Text = "Name",     Value = name,   Description = "Gear Name",     Id = $"{name}.gear.name"     },
                new MenuItemViewModel {Text = "Quantity", Value = qty,    Description = "Gear Quantity", Id = $"{name}.gear.quantity"     },
                new MenuItemViewModel {Text = "Weight",   Value = weight, Description = "Gear Weight",   Id = $"{name}.gear.weight"     },
                new MenuItemViewModel {Text = "Cost",     Value = cost,   Description = "Gear Cost",     Id = $"{name}.gear.cost"     }
            };
        }

        private ObservableCollection<MenuItemViewModel> Weapon(string sha, string name, string category, string type, string attack, string crit, string damage, string qty, string weight, string cost, string desc,  List<string> weapTypes, List<string> weapCats)
        {
            return new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel {Text = "Name",        Value = name,     Description = "Weapon Name",         Id = $"{name}.weapon.name"          },
                new MenuItemViewModel {Text = "Category",    Value = category, Description = "Weapon Category",     Id = $"{name}.weapon.category"      },
                new MenuItemViewModel {Text = "Type",        Value = type,     Description = "Weapon Type",         Id = $"{name}.weapon.type"          },
                new MenuItemViewModel {Text = "Attack",      Value = attack,   Description = "Weapon Attack",       Id = $"{name}.weapon.attack"        },
                new MenuItemViewModel {Text = "Crit",        Value = crit,     Description = "Weapon Crit",         Id = $"{name}.weapon.crit"          },
                new MenuItemViewModel {Text = "Damage",      Value = damage,   Description = "Weapon Damage",       Id = $"{name}.weapon.damage"        },
                new MenuItemViewModel {Text = "Qty",         Value = qty,      Description = "Weapon Qty",          Id = $"{name}.weapon.qty"           },
                new MenuItemViewModel {Text = "Weight",      Value = weight,   Description = "Weapon Weight",       Id = $"{name}.weapon.weight"        },
                new MenuItemViewModel {Text = "Cost",        Value = cost,     Description = "Weapon Cost",         Id = $"{name}.weapon.cost"          },
                new MenuItemViewModel {Text = "Description", Value = desc,     Description = "Weapon Description",  Id = $"{name}.weapon.description"   },
                new MenuItemViewModel {Text = "Categories", MenuItems = new ObservableCollection<MenuItemViewModel>(weapCats.Select(wc => new MenuItemViewModel{Text = wc,    Value = wc, Description = "Weapon Category"    , Id = $"{name}.weapon.category"  }))},
                new MenuItemViewModel {Text = "Attack Types", MenuItems = new ObservableCollection<MenuItemViewModel>(weapTypes.Select(wt => new MenuItemViewModel{Text = wt, Value = wt, Description = "Weapon Attack Type" , Id = $"{name}.weapon.attack.type"  }))}
            };
        }

        private ObservableCollection<MenuItemViewModel> Save(string name, string shortName, string save, string baseVal,
            string fromAttr, string fromResist, string fromMisc)
        {
            var baseId = name.Replace(" ", ".").ToLower();
            return new ObservableCollection<MenuItemViewModel>{
                new MenuItemViewModel {Text = "Name",            Value = name,        Description = $"{name} Name",     Id = $"{baseId}.name"},
                new MenuItemViewModel {Text = "Short Name",      Value = shortName,   Description = $"{name} Short Name", Id = $"{baseId}.shortname"},
                new MenuItemViewModel {Text = "Value",           Value = save,        Description = $"{name} value", Id = $"{baseId}.value"},
                new MenuItemViewModel {Text = "Base Value",      Value = baseVal,     Description = $"{name} base value", Id = $"{baseId}.basevalue"},
                new MenuItemViewModel {Text = "Attr. Bonus",     Value = fromAttr,    Description = $"{name} attr. bonus", Id = $"{baseId}.attrbonus"},
                new MenuItemViewModel {Text = "Resist. Bonus",   Value = fromResist,  Description = $"{name} resist. bonus", Id = $"{baseId}.resistbonus"},
                new MenuItemViewModel {Text = "Misc. Bonus",     Value = fromMisc,    Description = $"{name} misc. bonus", Id = $"{baseId}.miscbonus"},

        };
    }

        private ObservableCollection<MenuItemViewModel> Stat(string name, string attrBaseVal, string attrMod,
            string bonusBaseVal, string bonusMod, string situationalMods)
        {
            return new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel{Text = "Base",                   Value = attrBaseVal,         Description = $"Base {name}",            Id = $"character.{name}.base"},
                new MenuItemViewModel{Text = "Modified",               Value = attrMod,             Description = $"Modified {name}",        Id = $"character.{name}.modified"},
                new MenuItemViewModel{Text = "Base Bonus",             Value = bonusBaseVal,        Description = $"Base {name} Bonus",      Id = $"character.{name}.basebonus"},
                new MenuItemViewModel{Text = "Modified Bonus",         Value = bonusMod,            Description = $"Modified {name} Bonus",  Id = $"character.{name}.modifiedbonus"},
                new MenuItemViewModel{Text = "Situational Mod",        Value = situationalMods,     Description = "Situational Mod",         Id = $"character.{name}.situationalmod"},

            };
        }

        private ObservableCollection<MenuItemViewModel> Skill(string descr, string name, string ranks, string attrBonus, string attrName, string value, string armorCheck, string classSkill, string usable, string trainedOnly, string tools)
        {
            var baseId = name.Replace(" ", ".").ToLower();
            return new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel {Text = "Name",              Value = name,         Description = $"{name} skill name",         Id = $"skills.{baseId}.name"},
                new MenuItemViewModel {Text = "Value",             Value = value,        Description = $"{name} skill value",        Id = $"skills.{baseId}.value"},
                new MenuItemViewModel {Text = "Description",       Value = descr,        Description = $"{name} skill description",  Id = $"skills.{baseId}.description"},
                new MenuItemViewModel {Text = "Ranks",             Value = ranks,        Description = $"{name} skill ranks",        Id = $"skills.{baseId}.ranks"},
                new MenuItemViewModel {Text = "Attribute",         Value = attrName,     Description = $"{name} skill attribute",    Id = $"skills.{baseId}.attribute"},
                new MenuItemViewModel {Text = "Attribute Bonus",   Value = attrBonus,    Description = $"{name} skill bonus",        Id = $"skills.{baseId}.attribute.bonus"},
                new MenuItemViewModel {Text = "Armor Check",       Value = armorCheck,   Description = $"{name} skill armor check",  Id = $"skills.{baseId}.armorcheck"},
                new MenuItemViewModel {Text = "Class Skill",       Value = classSkill,   Description = $"{name} skill class",        Id = $"skills.{baseId}.classskill"},
                new MenuItemViewModel {Text = "Tools",             Value = tools,        Description = $"{name} skill tools",        Id = $"skills.{baseId}.tools"},
                new MenuItemViewModel {Text = "Usable",            Value = usable,       Description = $"{name} skill usable",       Id = $"skills.{baseId}.usable"},
                new MenuItemViewModel {Text = "Trained Only",      Value = trainedOnly,  Description = $"{name} skill trained only", Id = $"skills.{baseId}.trainedonly"},
               


            };
        }

        public ObservableCollection<MenuItemViewModel> Feat(string name, string descr, string category, string categoryText, string profGroup, string userAdded)
        {
            var baseId = name.Replace(" ", ".").ToLower();
            return new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel {Text = "Name",              Value = name,         Description = $"{name} feat name",              Id = $"feats.{baseId}.name"},
                new MenuItemViewModel {Text = "Description",       Value = descr,        Description = $"{name} feat description",       Id = $"feats.{baseId}.description"},
                new MenuItemViewModel {Text = "Category",          Value = category,     Description = $"{name} feat category",          Id = $"feats.{baseId}.category"},
                new MenuItemViewModel {Text = "Category Text",     Value = categoryText, Description = $"{name} feat category text",     Id = $"feats.{baseId}.category.text"},
                new MenuItemViewModel {Text = "Proficiency Group", Value = profGroup,    Description = $"{name} feat proficiency group", Id = $"feats.{baseId}.proficiency.group"},
                new MenuItemViewModel {Text = "User Added",        Value = userAdded,    Description = $"{name} feat user added",        Id = $"feats.{baseId}.useradded"},
            };
        }


        public MenuViewModel(ICharacterService<Character> characterService)
        {
            var character = characterService.Character;
            var emptyOption = new List<MenuItemViewModel>() {new MenuItemViewModel {Text = "-"}};
          

            Items = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel {Text = "Character Name", Value = character?.HeroName, Description = "Character Name", Id = "character.name"},
                new MenuItemViewModel
                {
                      Text = "Classes"
                    , MenuItems = new ObservableCollection<MenuItemViewModel>( 
                        character?.Classes != null 
                            ? character.Classes.Select( c => new MenuItemViewModel{Text = c.Name, Value = c.Name, Description = "Class"}) 
                            : emptyOption
                      )
                },
                new MenuItemViewModel {
                      Text = "Levels"
                    , MenuItems = new ObservableCollection<MenuItemViewModel>(
                        character?.Classes != null
                        ? character.Classes.Select( c => new MenuItemViewModel{ Text = $"{c.Name} - {c.Level}", Value = c.Level, Description = "Level"})
                        : emptyOption
                    )
                },
                new MenuItemViewModel {
                    Text = "Stats"
                    , MenuItems = new ObservableCollection<MenuItemViewModel>(
                        character?.Stats != null 
                        ? character.Stats.Select( s => new MenuItemViewModel
                            {
                                Text = s.Name,
                                MenuItems = Stat(s.Name, s.Value.Base, s.Value.Modified, s.Bonus.Base, s.Bonus.Modified, s.Modifiers.Text)
                            })
                        : emptyOption
                    )
                },
                new MenuItemViewModel {Text = "Saves", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    
                    character?.SaveRolls != null
                    ? character.SaveRolls.Rolls.Select( r => new MenuItemViewModel
                        {
                            Text = r.Name,
                            MenuItems = Save(r.Name, r.ShortName, r.Save, r.Base, r.FromAttr, r.FromResist, r.FromMisc)
                        })
                            .Append(
                                new MenuItemViewModel{
                                    Text = "Others", MenuItems = new ObservableCollection<MenuItemViewModel>(
                                        character.SaveRolls.AllSaves.SituationalModsList.SituationalMods.Select(
                                            m => new MenuItemViewModel{
                                                Text = $"{m.Source} Save", Description = $"{m.Source} Save", Value = m.Text
                                            }
                                        )
                                    )
                                }
                            )
                    : emptyOption
                    
                )},
                new MenuItemViewModel {Text = "Skills", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    character?.Skills != null
                        ? character.Skills.Select( s => new MenuItemViewModel
                        {
                            Text = s.Name,
                            MenuItems = Skill(s.Description, s.Name, s.Ranks, s.AttrBonus, s.AttrName, s.Value, s.ArmorCheck, s.ClassSkill, s.Usable, s.TrainedOnly, s.Tools)
                        })
                        : emptyOption
                )},
                new MenuItemViewModel {Text = "Feats", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    character?.Feats != null
                        ? character.Feats.Select( f => new MenuItemViewModel
                        {
                            Text = f.Name,
                            MenuItems = Feat(f.Name, f.Description, f.FeatCategory, f.CategoryText, f.ProfGroup, f.UserAdded)
                        })
                        : emptyOption
                )},

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
                                MenuItems = Weapon(w.Hash256(), w.Name, w.Category, w.Type, w.Attack, w.Crit, w.Damage, w.Quantity, w.Weight.Text, w.Cost.Text, w.Description, w.WeaponTypes, w.WeaponCategories)
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
