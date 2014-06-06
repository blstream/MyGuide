﻿using Caliburn.Micro;
using MyGuide.DataServices;
using MyGuide.DataServices.Interfaces;
using MyGuide.Models;
using MyGuide.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MyGuide.ViewModels
{
    public class SplashScreenViewModel : ViewModelBase
    {
        public SplashScreenViewModel(INavigationService navigationService,
            IMessageDialogService messageDialogService, IDataService dataService, IOptionsService optionService)
            : base(navigationService, messageDialogService, dataService, optionService)
        {
        }

        public override async void OnNavigatedTo(NavigationMode navigationMode, bool isNewPageInstance)
        {
            try
            {
                await _dataService.Initialize();
                await _optionService.Initialize();
            }
            catch (LackOfXmlFileException ex)
            {
                _log.Error(ex);
                throw; // LittleWatson enters to action :D
            }

            await Task.Delay(2000);
            _navigation.UriFor<MainPageViewModel>().Navigate();
        }
    }
}