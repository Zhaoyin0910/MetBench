using MetBench_Client.Services;
using Stylet;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace MetBench_Client.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INavigationWindow
    {
        public ViewModels.MainWindowViewModel ViewModel
        {
            get;
        }

        public MainWindow(ViewModels.MainWindowViewModel viewModel, IPageService pageService, INavigationService navigationService)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            SetPageService(pageService);

            navigationService.SetNavigationControl(RootNavigation);
            //传递RootNavigation
            TransParamsService.mainWindow = this;
        }

        #region INavigationWindow methods

        public Frame GetFrame()
            => RootFrame;

        public INavigation GetNavigation()
            => RootNavigation;

        public bool Navigate(Type pageType)
            => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService)
            => RootNavigation.PageService = pageService;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();

        #endregion INavigationWindow methods

        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Make sure that closing this window will begin the process of closing the application.
            Application.Current.Shutdown();
        }



        private void GobackPage(object sender, RoutedEventArgs e)
        {
            if (RootNavigation.CanGoBack)
            {

                RootNavigation.NavigateBack();
            }
        }

        //导航完成后
        private void Navigated(object sender, NavigationEventArgs e)
        {

            var page = RootFrame.Content as Page;
            TransParamsService.Newpage = page;
        }
        //导航前
        private void Navigating(object sender, NavigatingCancelEventArgs e)
        {
            // 在导航开始前执行的操作
            if (RootFrame.Content != null)
            {
                var page = RootFrame.Content as Page;
                TransParamsService.Oldpage = page;
            }

        }
    }
}