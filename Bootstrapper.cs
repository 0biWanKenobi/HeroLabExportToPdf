using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Caliburn.Micro;
using HeroLabExportToPdf.Business;
using HeroLabExportToPdf.Entities;
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
            

            #region viewmodels

            _container = new SimpleContainer();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<ICoordinateTranslationService, CoordinatesTranslationService>();
            _container.Singleton<IImageService, ImageService>();
            _container.Singleton<MenuViewModel>("StaticMenuViewModel");

            _container.Handler<DrawingCanvasViewModel>( c => new DrawingCanvasViewModel(c.GetInstance<IEventAggregator>()));

            _container.Singleton<IOpenFileService, OpenFileService>();
            _container.Singleton<ISaveFileService, SaveFileService>();

            _container.PerRequest<CharacterSheetViewModel>();
            _container.PerRequest<PdfImageViewModel>();
            _container.PerRequest<MainViewModel>();
            
            _container.Handler<RectangleFactory>(c => new RectangleFactory(
                () => c.GetInstance<MenuViewModel>("StaticMenuViewModel")
                , c.GetInstance<ICoordinateTranslationService>()
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
                        return viewbox.ActualWidth / tb.ActualWidth;
                    }
                    return double.NaN;
                });

            MessageBinder.SpecialValues
                .Add("$selecteditem", (ctx) => ctx.Source.DataContext);

            MessageBinder.SpecialValues
                .Add( "$mousex", (ctx) =>
                {
                    var s = ctx.Source;

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
