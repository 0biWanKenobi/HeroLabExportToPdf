using System;
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

        private ObservableCollection<MenuItemViewModel> Weapon(string name, string category, string type, string attack,
            string crit, string damage, string qty, string weight, string cost, string desc, List<string> weapTypes,
            List<string> weapCats)
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

        private ObservableCollection<MenuItemViewModel> RangedWeapon(RangedWeapon rw)
        {
            var collection = Weapon(rw.Name, rw.CategoryText, rw.Type, rw.Attack, rw.Crit, rw.Damage, rw.Quantity,
                rw.Weight.Text, rw.Cost.Text, rw.Description, rw.WeaponTypes, rw.WeaponCategories);
            collection.Insert(4,
                new MenuItemViewModel{Text = "Ranged Attack", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel{Text = "Attack",      Value = rw.RangeAttack.Attack,     Description = "Weapon Ranged Attack Bonus",          Id = $"{rw.Name}.weapon.ranged.attackbonus"},
                    new MenuItemViewModel{Text = "Range Text",  Value = rw.RangeAttack.RangeText,  Description = "Weapon Ranged Attack Distance Text",  Id = $"{rw.Name}.weapon.ranged.attackdistancetext"},
                    new MenuItemViewModel{Text = "Range Value", Value = rw.RangeAttack.RangeValue, Description = "Weapon Ranged Attack Distance Value", Id = $"{rw.Name}.weapon.ranged.attackdistancevalue"},
                }}
                );
            return collection;
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
                new MenuItemViewModel {Text = "Armor Check",       Value = armorCheck,   Description = $"{name} skill armor check",  Id = $"skills.{baseId}.armorcheck", Type = MenuItemType.Checkbox},
                new MenuItemViewModel {Text = "Class Skill",       Value = classSkill,   Description = $"{name} skill class",        Id = $"skills.{baseId}.classskill", Type = MenuItemType.Checkbox},
                new MenuItemViewModel {Text = "Tools",             Value = tools,        Description = $"{name} skill tools",        Id = $"skills.{baseId}.tools"},
                new MenuItemViewModel {Text = "Usable",            Value = usable,       Description = $"{name} skill usable",       Id = $"skills.{baseId}.usable", Type = MenuItemType.Checkbox},
                new MenuItemViewModel {Text = "Trained Only",      Value = trainedOnly,  Description = $"{name} skill trained only", Id = $"skills.{baseId}.trainedonly", Type = MenuItemType.Checkbox},
               


            };
        }

        private ObservableCollection<MenuItemViewModel> Feat(string name, string descr, string category, string categoryText, string profGroup, string userAdded)
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

        private ObservableCollection<MenuItemViewModel> Trait(string name, string categoryText, string description,
            List<string> traitCategories)
        {
            var baseId = name.Replace(" ", ".").ToLower();
            return new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel{Text = "Name",        Value = name,          Description = $"{name} trait name",              Id = $"traits.{baseId}.name"},
                new MenuItemViewModel{Text = "Source",      Value = categoryText,  Description = $"{name} trait source",            Id = $"traits.{baseId}.source"},
                new MenuItemViewModel{Text = "Description", Value = description,   Description = $"{name} trait description",       Id = $"traits.{baseId}.description"},
                new MenuItemViewModel{Text = "Categories", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    traitCategories.Select( tc =>
                        new MenuItemViewModel{Text = tc, Value = tc, Description = $"{name} trait category", Id = $"traits.{name}.categories.{tc}"}    
                    )    
                )},


            };
        }

        private ObservableCollection<MenuItemViewModel> Spell(Spell spell)
        {
            var baseId = spell.Name.Replace(" ", ".").ToLower();
            return new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel {Text = "Name",                 Value = spell.Name,         Description = $"{spell.Name} spell Name",          Id = $"spells.{baseId}.name"},
                new MenuItemViewModel {Text = "Level",                Value = spell.Level,        Description = $"{spell.Name} spell Level",         Id = $"spells.{baseId}.level"},
                new MenuItemViewModel {Text = "Class",                Value = spell.Class,        Description = $"{spell.Name} spell Class",         Id = $"spells.{baseId}.class"},
                new MenuItemViewModel {Text = "Cast Time",            Value = spell.CastTime,     Description = $"{spell.Name} spell Cast Time",     Id = $"spells.{baseId}.casttime"},
                new MenuItemViewModel {Text = "Range",                Value = spell.Range,        Description = $"{spell.Name} spell Range",         Id = $"spells.{baseId}.range"},
                new MenuItemViewModel {Text = "Target",               Value = spell.Target,       Description = $"{spell.Name} spell Target",        Id = $"spells.{baseId}.target"},
                new MenuItemViewModel {Text = "Area",                 Value = spell.Area,         Description = $"{spell.Name} spell Area",          Id = $"spells.{baseId}.area"},
                new MenuItemViewModel {Text = "Effect",               Value = spell.Effect,       Description = $"{spell.Name} spell Effect",        Id = $"spells.{baseId}.effect"},
                new MenuItemViewModel {Text = "Duration",             Value = spell.Duration,     Description = $"{spell.Name} spell Duration",      Id = $"spells.{baseId}.duration"},
                new MenuItemViewModel {Text = "Save",                 Value = spell.Save,         Description = $"{spell.Name} spell Save",          Id = $"spells.{baseId}.save"},
                new MenuItemViewModel {Text = "Resist",               Value = spell.Resist,       Description = $"{spell.Name} spell Resistance",    Id = $"spells.{baseId}.resist", Type = MenuItemType.Checkbox},
                new MenuItemViewModel {Text = "Dc",                   Value = spell.Dc,           Description = $"{spell.Name} spell Dc",            Id = $"spells.{baseId}.dc"},
                new MenuItemViewModel {Text = "Caster Level",         Value = spell.CasterLevel,  Description = $"{spell.Name} spell Caster Level",  Id = $"spells.{baseId}.casterlevel"},
                new MenuItemViewModel {Text = "Component",            Value = spell.Component,    Description = $"{spell.Name} spell Component",     Id = $"spells.{baseId}.component"},
                new MenuItemViewModel {Text = "School",               Value = spell.School,       Description = $"{spell.Name} spell School",        Id = $"spells.{baseId}.school"},
                new MenuItemViewModel {Text = "Subschool",            Value = spell.Subschool,    Description = $"{spell.Name} spell Subschool",     Id = $"spells.{baseId}.subschool"},
                new MenuItemViewModel {Text = "Descriptor",           Value = spell.Descriptor,   Description = $"{spell.Name} spell Descriptor",    Id = $"spells.{baseId}.descriptor"},
                new MenuItemViewModel {Text = "Save Text",            Value = spell.SaveText,     Description = $"{spell.Name} spell Save Text",     Id = $"spells.{baseId}.savetext"},
                new MenuItemViewModel {Text = "Resistance Text",      Value = spell.ResistText,   Description = $"{spell.Name} spell ResistText",    Id = $"spells.{baseId}.resisttext"},
                new MenuItemViewModel {Text = "Description",          Value = spell.Description,  Description = $"{spell.Name} spell Description",   Id = $"spells.{baseId}.description"},
                new MenuItemViewModel {Text = "Components",   MenuItems = new ObservableCollection<MenuItemViewModel>(
                    
                        spell.SpellComponents.Select( sc => new MenuItemViewModel{Text = sc, Value = sc})
                    )},
                new MenuItemViewModel {Text = "Schools",  MenuItems = new ObservableCollection<MenuItemViewModel>(
                        spell.SpellSchools.Select(ss => new MenuItemViewModel{Text = ss, Value = ss})
                    )},
                new MenuItemViewModel {Text = "Arcane School",    Value = spell.ArcaneSchool,   Description = $"{spell.Name} spell Arcane School",   Id = $"spells.{baseId}.arcaneschool"},


            };
        }

        private ObservableCollection<MenuItemViewModel> Maneuver(string name, string bonus, string cmb, string cmd,
            SituationalModList situationalModifiers)
        {
            var baseId = name.Replace(" ", ".").ToLower();
            return new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel{Text = "Name",  Value = name,   Description = $"{name} maneuver name", Id = $"maneuvers.{baseId}.name"},
                new MenuItemViewModel{Text = "Bonus", Value = bonus,  Description = $"{name} maneuver bonus",Id = $"maneuvers.{baseId}.bonus"},
                new MenuItemViewModel{Text = "Cmb",   Value = cmb,    Description = $"{name} maneuver cmb",  Id = $"maneuvers.{baseId}.cmb"},
                new MenuItemViewModel{Text = "Cmd",   Value = cmd,    Description = $"{name} maneuver cmd",  Id = $"maneuvers.{baseId}.cmd"},
                new MenuItemViewModel
                {
                    Text = "Situational mods", MenuItems = new ObservableCollection<MenuItemViewModel>(
                        situationalModifiers.SituationalMods.Select(sm =>
                            new MenuItemViewModel
                            {
                                Text = sm.Source, MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel{Text = "Source",      Value = sm.Source, Description = $"{name} maneuver modifier source",      Id = $"maneuvers.modifiers.{baseId}.source"},
                                    new MenuItemViewModel{Text = "Description", Value = sm.Text,   Description = $"{name} maneuver modifier description", Id = $"maneuvers.modifiers.{baseId}.descr"}
                                }
                            }
                        )
                    )
                }
            };
        }

        private ObservableCollection<MenuItemViewModel> Special(string name, string shortname, string description)
        {
            var baseId = name.Replace(" ", ".").ToLower();
            return new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel {Text = "Name",        Value = name,        Description = $"{name} special attribute name",        Id = $"specials.{baseId}.name"},
                new MenuItemViewModel {Text = "Short Name",  Value = shortname,   Description = $"{name} special attribute shortname",   Id = $"specials.{baseId}.shortname"},
                new MenuItemViewModel {Text = "Description", Value = description, Description = $"{name} special attribute description", Id = $"specials.{baseId}.description"}

            };
        }

        public MenuViewModel(ICharacterService<Character> characterService)
        {
            var character = characterService.Character;
            var emptyOption = new ObservableCollection<MenuItemViewModel> {new MenuItemViewModel {Text = "-"}};
          

            Items = new ObservableCollection<MenuItemViewModel>
            {
                // character name
                new MenuItemViewModel {Text = "Character Name", Value = character?.HeroName, Description = "Character Name", Id = "character.name"},
                new MenuItemViewModel
                {
                    Text = "Basic Info", MenuItems =
                        
                        character == null ? emptyOption :
                        new ObservableCollection<MenuItemViewModel>
                        {
                        new MenuItemViewModel{Text = "Alignment", Value = character.Alignment.Name, Description = "Character Alignment", Id = "character.alignment"},
                        new MenuItemViewModel{Text = "Deity", Value = character.Deity.Name, Description = "Character Deity", Id="character.deity"},
                        new MenuItemViewModel{Text = "Languages", MenuItems = new ObservableCollection<MenuItemViewModel>(
                            character.Languages.Select( l => new MenuItemViewModel{Text = l.Name})
                        )},
                        new MenuItemViewModel{Text = "Looks", MenuItems = new ObservableCollection<MenuItemViewModel>
                        {
                            new MenuItemViewModel{Text = "Age",    Value = character.Personal.Age.ToString(), Description = "Character Age",    Id = "character.personal.age"},
                            new MenuItemViewModel{Text = "Eyes",   Value = character.Personal.Eyes,           Description = "Character Eyes",   Id = "character.personal.eyes"},
                            new MenuItemViewModel{Text = "Gender", Value = character.Personal.Gender,         Description = "Character Gender", Id = "character.personal.gender"},
                            new MenuItemViewModel{Text = "Hair",   Value = character.Personal.Gender,         Description = "Character Hair",   Id = "character.personal.hair"},
                            new MenuItemViewModel{Text = "Skin",   Value = character.Personal.Skin,           Description = "Character Skin",   Id = "character.personal.skin"},
                        }}, // looks
                        new MenuItemViewModel{Text = "Size", Value = character?.Size.Name, Description = "Character Size", Id = "character.size"},
                        new MenuItemViewModel
                        {
                            Text="Race",
                            Value = character?.Race.Name, 
                            Description = "Character Race", 
                            MenuItems = 
                                new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel{ Text = "Ethnicity", Value = character?.Race.Ethnicity, Id = "character.ethnicity"}
                                },
                            Id = "character.race"
                        } // race
                    }
                }, // basic info
                new MenuItemViewModel
                {
                    Text = "Initiative", 
                    Value = character?.Initiative.Total, 
                    Description = "Character Total Initiative",
                    MenuItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new MenuItemViewModel{Text = "Misc", Value = character?.Initiative.Misctext, Description = "Character Misc Initiative Bonus", Id = "character.initiative.miscbonus"},
                        new MenuItemViewModel{Text = "Situational Modifiers", MenuItems = new ObservableCollection<MenuItemViewModel>
                        {
                            new MenuItemViewModel{}
                        }},

                    },
                    Id = "character.initiative.total"
                },
                new MenuItemViewModel 
                {
                      Text = "Classes"
                    , MenuItems = new ObservableCollection<MenuItemViewModel>( 
                        character?.Classes != null 
                            ? character.Classes.Select( c => new MenuItemViewModel{Text = c.Name, Value = c.Name, Description = "Class"}) 
                            : emptyOption
                      )
                }, // classes
                new MenuItemViewModel {
                      Text = "Levels"
                    , MenuItems = new ObservableCollection<MenuItemViewModel>(
                        character?.Classes != null
                        ? character.Classes.Select( c => new MenuItemViewModel{ Text = $"{c.Name} - {c.Level}", Value = c.Level, Description = "Level"})
                        : emptyOption
                    )
                }, // levels
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
                }, // stats
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
                    
                )}, // saves
                new MenuItemViewModel{Text="Maneuvers", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    
                    character?.Maneuvers != null 
                        ? new ObservableCollection<MenuItemViewModel>
                        {
                            new MenuItemViewModel{Text = "Cmb", Value = character.Maneuvers.Cmb,                        Description = "Character Cmb",              Id="character.maneuvers.cmb"},
                            new MenuItemViewModel{Text = "Cmd", Value = character.Maneuvers.Cmd,                        Description = "Character Cmd",              Id="character.maneuvers.cmd"},
                            new MenuItemViewModel{Text = "Cmd Flat Footed", Value = character.Maneuvers.Cmdflatfooted,  Description = "Character Cmd Flat Footed",  Id="character.maneuvers.cmd.flatfooted"}
                        }.Concat(
                            character.Maneuvers.ManeuverTypes.Select( m => new MenuItemViewModel
                            {
                                Text = m.Name,
                                MenuItems = Maneuver( m.Name,  m.Bonus,  m.Cmb,  m.Cmb, m.Situationalmodifiers)
                            })
                        )
                        : emptyOption
                    )}, // maneuvers
                new MenuItemViewModel {Text = "Skills", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    character?.Skills != null
                        ? character.Skills.Select( s => new MenuItemViewModel
                        {
                            Text = s.Name,
                            MenuItems = Skill(s.Description, s.Name, s.Ranks, s.AttrBonus, s.AttrName, s.Value, s.ArmorCheck, s.ClassSkill, s.Usable, s.TrainedOnly, s.Tools)
                        })
                        : emptyOption
                )}, // skills
                new MenuItemViewModel {Text = "Feats", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    character?.Feats != null
                        ? character.Feats.Select( f => new MenuItemViewModel
                        {
                            Text = f.Name,
                            MenuItems = Feat(f.Name, f.Description, f.FeatCategory, f.CategoryText, f.ProfGroup, f.UserAdded)
                        })
                        : emptyOption
                )}, // feats
                new MenuItemViewModel{Text = "Traits", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    
                    character?.Traits != null
                        ? character.Traits.Select( t => new MenuItemViewModel
                        {
                            Text = t.Name,
                            MenuItems = Trait(t.Name, t.CategoryText, t.Description, t.TraitCategories) 
                        })
                        : emptyOption
                )}, // traits
                new MenuItemViewModel{Text = "Spells", MenuItems = new ObservableCollection<MenuItemViewModel>{
                    
                    new MenuItemViewModel{Text = "Known", MenuItems = new ObservableCollection<MenuItemViewModel>(
                        
                        character?.SpellsMemorized != null
                            ? character.SpellsKnown.Select( spell => new MenuItemViewModel
                            {
                                Text = spell.Name,
                                MenuItems = Spell(spell) 
                            })
                            : emptyOption    
                        
                        )}, // known
                    new MenuItemViewModel{Text = "Memorized", MenuItems = new ObservableCollection<MenuItemViewModel>(
                        
                        character?.SpellsMemorized != null
                            ? character.SpellsMemorized.Select( spell => new MenuItemViewModel
                            {
                                Text = spell.Name,
                                MenuItems = Spell(spell) 
                            })
                            : emptyOption    
                        
                    )}  // memorized
                }}, // spells
                new MenuItemViewModel {Text = "Items", MenuItems = new ObservableCollection<MenuItemViewModel>()

                    {
                        new MenuItemViewModel{ Text="Normal", MenuItems = new ObservableCollection<MenuItemViewModel>(
                            character?.Gear != null
                                ? character.Gear.Select( g => new MenuItemViewModel
                                {
                                    Text = g.Name,
                                    MenuItems = GearItem(g.Name, g.Quantity, g.Weight.Text, g.Cost.Text)
                                })
                                : emptyOption
                        )},
                        new MenuItemViewModel{ Text="Magic", MenuItems = new ObservableCollection<MenuItemViewModel>(
                            character?.MagicItems != null
                                ? character.MagicItems.Select( g => new MenuItemViewModel
                                {
                                    Text = g.Name,
                                    MenuItems = GearItem(g.Name, g.Quantity, g.Weight.Text, g.Cost.Text)
                                })
                                : emptyOption
                        )}
                    }


                    
                }, // gear items
                new MenuItemViewModel {Text = "Armor", MenuItems = new ObservableCollection<MenuItemViewModel>(
                    
                   character?.Defenses != null
                        ? character.Defenses.Select( d => new MenuItemViewModel
                       {
                           Text = d.Name,
                           MenuItems = ArmorKit(d.Name, d.Ac, d.Quantity, d.Weight.Text, d.Cost.Text)
                       })
                        : emptyOption
                    
                    )
                }, // armor
                new MenuItemViewModel{Text = "Weapons", MenuItems = new ObservableCollection<MenuItemViewModel>
                {
                    new MenuItemViewModel{Text = "Melee", MenuItems = new ObservableCollection<MenuItemViewModel>
                    (
                        character?.MeleeWeapons != null
                            ? character.MeleeWeapons.Select( w =>  new MenuItemViewModel
                            {
                                Text = w.Name,
                                MenuItems = Weapon(w.Name, w.CategoryText, w.Type, w.Attack, w.Crit, w.Damage, w.Quantity, w.Weight?.Text, w.Cost?.Text, w.Description, w.WeaponTypes, w.WeaponCategories)
                            })
                            : emptyOption
                    )},
                    new MenuItemViewModel{Text = "Ranged", MenuItems = new ObservableCollection<MenuItemViewModel>
                    (
                        character?.RangedWeapons != null
                            ? character.RangedWeapons.Select( rw =>  new MenuItemViewModel
                            {
                                Text = rw.Name,
                                MenuItems = RangedWeapon(rw)
                            })
                            : emptyOption
                    )}

                        

                    
                }}, // weapons
                new MenuItemViewModel{Text = "Special", MenuItems = new ObservableCollection<MenuItemViewModel>
                (
                    character?.Specials != null
                        ? character.Specials.Select(s => new MenuItemViewModel
                        {
                            Text = s.Name,
                            MenuItems = Special(s.Name, s.ShortName, s.Description)
                        })
                        : emptyOption
                )} // specials
            };
        }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }
    }
}
