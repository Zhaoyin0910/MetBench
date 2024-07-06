using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace MetBench_Client.ViewModels
{
    //主页面VM
    public partial class MainWindowViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = String.Empty;

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();

        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            //ApplicationTitle = "数值表达式型蜕变关系的存储管理系统";
            ApplicationTitle = "Numerical Expression Metamorphic Relations Repository";

            NavigationItems = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Home",
                    PageTag = "dashboard",
                    Icon = SymbolRegular.Home24,
                    PageType = typeof(Views.Pages.DashboardPage)
                },
                new NavigationItem()
                {
                    Content = "MR Display",
                    PageTag = "displaymr",
                    Icon = SymbolRegular.ZoomOut24,
                    PageType = typeof(Views.Pages.DisplayMRPage)
                },
                new NavigationItem()
                {
                    Content = "MR Management",
                    PageTag = "data",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.AddMRPage)
                },
                 new NavigationItem()
                {
                    Content = "Application Management",
                    PageTag = "editapp",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.EditApplicationPage)
                },
                 new NavigationItem()
                 {
                     Content = "Domain Management",
                    PageTag = "editdomain",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.EditDomainsPage)
                 },
                 new NavigationItem()
                 {
                     Content = "MT Execution",
                    PageTag = "mt",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.AutoMRPage)
                 },
                 new NavigationItem()
                 {
                     Content = "MR Intellingent Recommendation",
                    PageTag = "mr intellingentrecommendation",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.MRIntelligentRecommendationsPage)
                 }
            };

            NavigationFooter = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Settings",
                    PageTag = "settings",
                    Icon = SymbolRegular.Settings24,
                    PageType = typeof(Views.Pages.SettingsPage)
                }
            };

            TrayMenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Header = "Home",
                    Tag = "tray_home"
                }
            };

            _isInitialized = true;
        }
    }
}
