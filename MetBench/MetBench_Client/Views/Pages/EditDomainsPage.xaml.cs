using System;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

namespace MetBench_Client.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class EditDomainsPage : INavigableView<ViewModels.EditDomainsViewModel>
    {
        public ViewModels.EditDomainsViewModel ViewModel
        {
            get;
        }

        public EditDomainsPage(ViewModels.EditDomainsViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
            
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.show();
        }

        private void PageLoaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

            e.Row.Header = e.Row.GetIndex() + 1;

        }
    }
}