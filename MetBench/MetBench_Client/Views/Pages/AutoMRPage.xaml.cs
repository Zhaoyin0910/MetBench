using MetBench_BLL;
using MetBench_Client.Services;
using MetBench_IDAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Interfaces;

namespace MetBench_Client.Views.Pages
{
    /// <summary>
    /// AutoMRPage.xaml 的交互逻辑
    /// </summary>
    public partial class AutoMRPage : INavigableView<ViewModels.AutoMRViewModel>
    {
        public AutoMRPage(ViewModels.AutoMRViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        public ViewModels.AutoMRViewModel ViewModel
        {
            get;
        }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            //渲染页面时触发的事件
            //CompositionTarget.Rendering是委托
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //    // 页面渲染时的处理
            var oldpage = TransParamsService.Oldpage;
            var newpage = TransParamsService.Newpage;
            var answer = TransParamsService.isRun;
            if (newpage.Title == "AutoMRPage" && answer)
            {

                var selectedMR = TransParamsService.runmetamorphicRelation;
                if (selectedMR != null)
                {
                    MetamorphicRelationSerive metamorphicRelationSerive = new MetamorphicRelationSerive();
                    var metamorphicRelation = metamorphicRelationSerive.GetMRById(selectedMR.IdMR);


                    ViewModel.CodeName = TransParamsService.runapplication.CodeName;
                    //ViewModel.CodeName = "111111";
                    ViewModel.InputPatternSympy = metamorphicRelation.InputPatterntosympy;
                    ViewModel.OutputPatternSympy = metamorphicRelation.OutputPatterntosympy;
                    ViewModel.DimensionOfInputPattern = metamorphicRelation.DimensionOfInputPattern;
                    ViewModel.DimensionOfOutputPattern = metamorphicRelation.DimensionOfOutputPattern;

                    var a = TransParamsService.runmetamorphicRelation;
                    ViewModel.Data = new ObservableCollection<MetamorphicRelations_QueryResultData>() { TransParamsService.runmetamorphicRelation };
                    ViewModel.Application = TransParamsService.runapplication;
                }
                //重置TransParamsService.isClick
                TransParamsService.isRun = false;
            }
        }

    }
}
