﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Caliburn.Micro;
using HeroLabExportToPdf.Business;
using HeroLabExportToPdf.Converters;
using HeroLabExportToPdf.Entities;
using HeroLabExportToPdf.Input;
using HeroLabExportToPdf.Services;
using HeroLabExportToPdf.ViewModels;

namespace HeroLabExportToPdf
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        protected override object GetInstance(Type serviceType, string key) {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType) {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance) {
            _container.BuildUp(instance);
        }

        

        protected override void Configure()
        {
            

            #region DI

            _container = new SimpleContainer();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<ICoordinateTranslationService, CoordinatesTranslationService>();
            _container.Singleton<IPdfService, PdfService>();
            _container.Singleton<MenuViewModel>("StaticMenuViewModel");

            _container.Handler<DrawingCanvasViewModel>( c => new DrawingCanvasViewModel(c.GetInstance<IEventAggregator>()));

            _container.Singleton<IOpenFileService, OpenFileService>();
            _container.Singleton<ISaveFileService, SaveFileService>();

            _container.PerRequest<CharacterSheetViewModel>();
            _container.PerRequest<MainViewModel>();
            
            _container.Handler<FieldFactory>(c => new FieldFactory(
                () => c.GetInstance<MenuViewModel>("StaticMenuViewModel")
                , c.GetInstance<IEventAggregator>()
                )
            );

            #endregion

            #region special binding values
            
            MessageBinder.SpecialValues
                .Add("$imagesize", (ctx) =>
                {
                    if(ctx.Source is Image image)
                    {
                        var width = image.ActualWidth;
                        var height = image.ActualHeight;
                        return  (width: width, height: height); 
                    }
                    return null;
                });

            MessageBinder.SpecialValues
                .Add("$delta", (ctx) =>
                {
                    if(ctx.Source is Thumb thumb && ctx.EventArgs is DragDeltaEventArgs args)
                    {
                        var istop = thumb.VerticalAlignment == VerticalAlignment.Top;
                        var isleft = thumb.HorizontalAlignment == HorizontalAlignment.Left;
                        var dirV = istop ? 1 : -1;
                        var dirH = isleft ? 1 : -1;
                        return (
                              deltah: Math.Min(dirH*args.HorizontalChange, thumb.ActualWidth  - thumb.MinWidth )
                            , deltav: Math.Min(dirV*args.VerticalChange,  thumb.ActualHeight - thumb.MinHeight)
                            , istop:  istop
                            , isbottom: thumb.VerticalAlignment == VerticalAlignment.Bottom
                            , isleft: isleft
                            , isright: thumb.HorizontalAlignment == HorizontalAlignment.Right
                                     );
                    }
                    return null;
                });

            MessageBinder.SpecialValues
                .Add("$fontscale", (ctx) =>
                {
                    if(ctx.Source is Viewbox viewbox && viewbox.Child is TextBlock tb)
                    {
                        if(tb.ActualWidth > 0)
                            return viewbox.ActualWidth / tb.ActualWidth;
                        return 1;
                    }
                    return double.NaN;
                });


            MessageBinder.SpecialValues
                .Add("$menuitem", (ctx) =>
                {
                    if(ctx.EventArgs is RoutedEventArgs eventArgs && ctx.Source is TreeViewItem treeItem && treeItem.Header is MenuItemViewModel menuItem)
                    {
                        //prevent bubble up, so that parent tree nodes do not trigger an event
                        eventArgs.Handled = true;
                        return menuItem;
                    }

                    return null;
                });

            MessageBinder.SpecialValues
                .Add("$selecteditem", (ctx) => ctx.Source.DataContext);

            MessageBinder.SpecialValues
                .Add( "$mousex", (ctx) =>
                {
                    var e = ctx.EventArgs as MouseEventArgs;
                    return e?.GetPosition(ctx.Source).X;
                });

            MessageBinder.SpecialValues
                .Add("$mousey", (ctx) =>
                {
                    var e = ctx.EventArgs as MouseEventArgs;
                    return e?.GetPosition(ctx.Source).Y;
                });

            

            MessageBinder.SpecialValues
                .Add("$deletekeypress", (ctx) =>
                {
                    if (ctx.EventArgs is KeyEventArgs keyArgs && keyArgs.Key == Key.Delete)
                    {
                        return true;
                    }
                    return false;
                });

            #endregion


            _container.Singleton<IEventAggregator, EventAggregator>();

            ActionMessage.EnforceGuardsDuringInvocation = true;

            ConfigureGestures();
        }

        private static void ConfigureGestures()
        {
            var defaultCreateTrigger = Parser.CreateTrigger;

            Parser.CreateTrigger = (target, triggerText) => 
            {
                if (triggerText == null)
                {
                    return defaultCreateTrigger(target, null);
                }

                var triggerDetail = triggerText
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                var splits = triggerDetail.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                switch (splits[0])
                {
                    case "Key":
                        var key = (Key)Enum.Parse(typeof(Key), splits[1], true);
                        return new KeyTrigger { Key = key };

                    case "Gesture":
                        var mkg = (MultiKeyGesture)(new MultiKeyGestureConverter()).ConvertFrom(splits[1]);
                        return new KeyTrigger { Modifiers = mkg.KeySequences[0].Modifiers, Key = mkg.KeySequences[0].Keys[0] };
                }

                return defaultCreateTrigger(target, triggerText);
            };
        }

        static Bootstrapper()
        {
            //LogManager.GetLog = type => new Caliburn.Micro.Logging.DebugLogger(type);
        }

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
                _container.Handler<ICharacterService<Character>>(c => new CharacterService(e.Args[0]));
            else
                _container.Handler<ICharacterService<Character>>(c => new CharacterService());

            DisplayRootViewFor<MainViewModel>();
        }
    }
}
