using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using SampleCode.Services;
using SampleCode.ViewModels;

namespace SampleCode
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
            _container = new SimpleContainer();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.PerRequest<IOpenFileService, OpenFileService>();
            _container.PerRequest<MainViewModel>();
           
        }

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
