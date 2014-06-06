﻿using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using Caliburn.Micro.Logging;
using Caliburn.Micro.Logging.NLog;
using MyGuide.DataServices;
using MyGuide.DataServices.Interfaces;
using MyGuide.Models;
using MyGuide.Services;
using MyGuide.Services.Interfaces;
using MyGuide.ViewModels;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MyGuide
{
    public class Bootstrapper : PhoneBootstrapper
    {
        private PhoneContainer container;

        static Bootstrapper()
        {
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("ISFile", typeof(IsolatedStorageTarget));
            LogManager.GetLog = type => new NLogLogger(type);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void Configure()
        {
            //It's workaround with designMode problem where rootFrame is null and exception is thrown
            if (Execute.InDesignMode)
                return;

            container = new PhoneContainer();

            //It's workaround with designMode problem where rootFrame is null and exception is thrown
            if (!Execute.InDesignMode)
                container.RegisterPhoneServices(RootFrame);

            container.PerRequest<MainPageViewModel>();
            container.PerRequest<AboutZooPageViewModel>();
            container.PerRequest<DebugOptionsPageViewModel>();
            container.PerRequest<OptionsPageViewModel>();
            container.PerRequest<SightseeingPageViewModel>();
            container.Singleton<IDataService, DataService>();
            container.Singleton<IOptionsService, OptionsService>();
            container.PerRequest<SplashScreenViewModel>();
            container.PerRequest<IMessageDialogService, MessageDialogService>();
            container.Singleton<LittleWatson>();
            container.PerRequest<ICompassService, RealCompassService>();
            container.PerRequest<IGeolocationService, GeolocationService>();

            //uncomment this when ( and comment RealCompassService) when testing on emulator
            //container.PerRequest<ICompassService, EmulatedCompassService>();

            //All VM should be add to this container, e.g. container.PerRequest<AnotherViewModel>();

            AddCustomConventions();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override async void OnUnhandledException(object sender, System.Windows.ApplicationUnhandledExceptionEventArgs e)
        {
            base.OnUnhandledException(sender, e);
            e.Handled = true;
            LittleWatson whatson = IoC.Get<LittleWatson>();
            await whatson.SendReport(e.ExceptionObject);
        }

        private static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<BindableAppBarButton>(
                Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
                Control.IsEnabledProperty, "DataContext", "Click");
        }
    }
}