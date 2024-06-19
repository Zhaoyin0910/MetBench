using MetBench_Client.Services;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

namespace MetBench_Client.Views.Pages
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DisplayMRPage : INavigableView<ViewModels.DisplayMRViewModel>
    {
        public ViewModels.DisplayMRViewModel ViewModel
        {
            get;
        }
        public DisplayMRPage(ViewModels.DisplayMRViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }
        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainwindow = TransParamsService.mainWindow;
            var Navigation = mainwindow.RootNavigation;
            TransParamsService.page = this;
            //编辑跳转按钮被点击
            TransParamsService.isClick= true;
            var PageType = typeof(Views.Pages.AddMRPage);
            Navigation.Navigate(PageType);
        }

        
        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }


}