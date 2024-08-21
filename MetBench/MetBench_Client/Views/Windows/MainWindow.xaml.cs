using MetBench_Client.Services;
using MetBench_Client.Views.Pages;
using System;
using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Controls;
namespace MetBench_Client.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INavigationWindow
    {
        //public ViewModels.MainWindowViewModel ViewModel
        //{
        //    get;
        //}

        //public MainWindow(ViewModels.MainWindowViewModel viewModel, IPageService pageService, INavigationService navigationService)
        //{
        //    ViewModel = viewModel;
        //    DataContext = this;

        //    InitializeComponent();
        //    SetPageService(pageService);

        //    navigationService.SetNavigationControl(RootNavigation);
        //    //传递RootNavigation
        //    TransParamsService.mainWindow = this;
        //}

        //#region INavigationWindow methods

        //public Frame GetFrame()
        //    => RootFrame;

        //public INavigation GetNavigation()
        //    => RootNavigation;

        //public bool Navigate(Type pageType)
        //    => RootNavigation.Navigate(pageType);

        //public void SetPageService(IPageService pageService)
        //    => RootNavigation.PageService = pageService;

        //public void ShowWindow()
        //    => Show();

        //public void CloseWindow()
        //    => Close();

        //#endregion INavigationWindow methods

        ///// <summary>
        ///// Raises the closed event.
        ///// </summary>
        //protected override void OnClosed(EventArgs e)
        //{
        //    base.OnClosed(e);

        //    // Make sure that closing this window will begin the process of closing the application.
        //    Application.Current.Shutdown();
        //}



        //private void GobackPage(object sender, RoutedEventArgs e)
        //{
        //    if (RootNavigation.CanGoBack)
        //    {

        //        RootNavigation.NavigateBack();
        //    }
        //}

        ////导航完成后
        //private void Navigated(object sender, NavigationEventArgs e)
        //{

        //    var page = RootFrame.Content as Page;
        //    TransParamsService.Newpage = page;
        //}
        ////导航前
        //private void Navigating(object sender, NavigatingCancelEventArgs e)
        //{
        //    // 在导航开始前执行的操作
        //    if (RootFrame.Content != null)
        //    {
        //        var page = RootFrame.Content as Page;
        //        TransParamsService.Oldpage = page;
        //    }

        //}
        public ViewModels.MainWindowViewModel ViewModel { get; }
        private bool _isUserClosedPane;
        private bool _isPaneOpenedOrClosedFromCode;

        public MainWindow(ViewModels.MainWindowViewModel viewModel, IPageService pageService, INavigationService navigationService)
        {
            ViewModel = viewModel;
            DataContext = this;

            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this);
            TransParamsService.mainWindow = this;
            InitializeComponent();
            SetPageService(pageService);

            navigationService.SetNavigationControl(RootNavigation);
        }

        public INavigationView GetNavigation() => RootNavigation;

        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService) => RootNavigation.SetPageService(pageService);

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Make sure that closing this window will begin the process of closing the application.
            Application.Current.Shutdown();
        }

        INavigationView INavigationWindow.GetNavigation()
        {
            throw new NotImplementedException();
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_isUserClosedPane)
            {
                return;
            }

            _isPaneOpenedOrClosedFromCode = true;
            RootNavigation.SetCurrentValue(NavigationView.IsPaneOpenProperty, e.NewSize.Width > 1200);
            _isPaneOpenedOrClosedFromCode = false;
        }
        private void NavigationView_OnPaneOpened(NavigationView sender, RoutedEventArgs args)
        {
            if (_isPaneOpenedOrClosedFromCode)
            {
                return;
            }

            _isUserClosedPane = false;
        }

        private void NavigationView_OnPaneClosed(NavigationView sender, RoutedEventArgs args)
        {
            if (_isPaneOpenedOrClosedFromCode)
            {
                return;
            }

            _isUserClosedPane = true;
        }
        //private void OnNavigationSelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    if (sender is not Wpf.Ui.Controls.NavigationView navigationView)
        //    {
        //        return;
        //    }

        //    RootNavigation.SetCurrentValue(
        //        NavigationView.HeaderVisibilityProperty,
        //        navigationView.SelectedItem?.TargetPageType != typeof(DashboardPage)
        //            ? Visibility.Visible
        //            : Visibility.Collapsed
        //    );
        //}
    }
}