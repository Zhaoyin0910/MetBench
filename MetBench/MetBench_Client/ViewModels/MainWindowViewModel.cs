﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;
using Wpf.Ui;


namespace MetBench_Client.ViewModels
{
    //主页面VM
    public partial class MainWindowViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<object> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<object> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();

        public string _headerString = string.Empty;

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Style",
            "IDE0060:Remove unused parameter",
            Justification = "Demo"
        )]
        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
            }
        }

        private void InitializeViewModel()
        {
            //ApplicationTitle = "数值表达式型蜕变关系的存储管理系统";
            ApplicationTitle = "Numerical Expression Metamorphic Relations Repository";

            NavigationItems = new ObservableCollection<object>
            {

             new NavigationViewItem()
            {
                Content = "MR Display",
                Icon = new SymbolIcon { Symbol = SymbolRegular.CalendarDataBar24 },
                TargetPageType = typeof(Views.Pages.MRDisplayPage)
            },
              new NavigationViewItem()
            {
                Content = "MR Management",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.MRManagementPage)
            },
               new NavigationViewItem()
            {
                Content = "Application Management",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.ApplicationManagementPage)
            },
                new NavigationViewItem()
            {
                Content = "Domain Management",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.DomainManagementPage)
            },
                     new NavigationViewItem()
            {
                Content = "MT Execution",
                Icon = new SymbolIcon { Symbol = SymbolRegular.PersonRunning20 },
                TargetPageType = typeof(Views.Pages.MTExecutionPage)
            },
            };

            NavigationFooter = new ObservableCollection<object>
            {
                new NavigationViewItem()
                {
                    Content = "Settings",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(Views.Pages.SettingsPage)
                }
            };



            TrayMenuItems = new ObservableCollection<MenuItem>()
            {
                new MenuItem()
                {
                    Header = "Home",
                    Tag = "tray_home"
                }
            };

            _isInitialized = true;
        }
    }
}
