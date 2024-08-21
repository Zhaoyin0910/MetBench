using CommunityToolkit.Mvvm.ComponentModel;
using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_Client.Services;
using MetBench_Client.Views.Pages;
using MetBench_Client.Views.Windows;
using MetBench_IDAL;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Wpf.Ui;
using Wpf.Ui.Controls;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MetBench_Client.ViewModels
{
    public class MTExecutionViewModel : ObservableObject, INavigationAware
    {
        // 导航服务 用于页面切换
        private INavigationService _navigationService;

        // 页面服务 用于获取页面实体
        private IPageService _pageService;

        // 应用程序管理服务
        private ApplicationSerive _applicationSerive;

        // 蜕变关系管理服务
        private MetamorphicRelationSerive _metamorphicRelationSerive;

        // DataGrid的SelectedItem数据绑定属性
        public MetamorphicRelations_QueryResultData DataGridSelectedItem { get; set; }

        // 输入模式的Latex格式
        public String InputPatternSympy { get; set; } = string.Empty;

        // 输出模式的Latex格式
        public String OutputPatternSympy { get; set; } = string.Empty;

        // 输入模式的图片
        public BitmapImage InputPatternImag { get; set; }

        // 输出模式的图片
        public BitmapImage OutputPatternImag { get; set; }

        // 蜕变关系的图片路径
        public string Imagepath { get; set; }

        public BitmapImage Image { get; set; }

        // 被测应用程序
        public MetBench_Domain.Application Application { get; set; }

        // 被测应用程序集
        public ObservableCollection<MetBench_Domain.Application> Applications { get; set; }

        // 可选程序列表
        public ObservableCollection<string> CodeNameOptions { get; set; } 

        // CodeName的ComboBox的SelectedIndex属性
        public int SelectedIndex { get; set; }

        // 
        public string SelectedValue { get; set; }

        // TextBox的可见性
        public Visibility IsTextBoxVisible { get; set; }

        // ComboBox的可见性
        public Visibility IsComboBoxVisible { get; set; }

        // 程序名
        public string CodeName { get; set; }

        // 最小值
        public string MinParam { get; set; } = String.Empty;

        // 最大值
        public string MaxParam { get; set; } = String.Empty;

        // 执行次数
        public string ExecutNumber { get; set; } = String.Empty;

        // 阈值 需要转换为double类型 double threshold = double.Parse(Threshold);
        public string Threshold { get; set; } = "1e-4";

        // 输入维度
        public string DimensionOfInputPattern { get; set; }

        // 输出维度
        public string DimensionOfOutputPattern { get; set; }

        // 通过比率
        public string Passrate { get; set; } = String.Empty;

        // 失败比率
        public string Failurerate { get; set; } = String.Empty;

        // datagrid的数据源
        public ObservableCollection<MetamorphicRelations_QueryResultData> Data { get; set; }
        // 进度条属性 IsIndeterminate 
        public bool IsIndeterminate { get; set; }
        // 进度条属性 Visibility
        public Visibility Visibility { get; set; }
        //// 私有成员应用程序集
        //private ObservableCollection<MetBench_Domain.Application> _applications;

        //// 公有属性应用程序集
        //public ObservableCollection<MetBench_Domain.Application> Applications
        //{
        //    //get => _applications;
        //    get 
        //    {
        //        if (_applications == null) 
        //        {
        //            _applications = new ObservableCollection<MetBench_Domain.Application> ();
        //        }
        //        return _applications;
        //    }
        //    set
        //    {
        //        if (_applications != null)
        //        {
        //            // 如果不想使用 Clear，这里也可以设为 null，强制 UI 更新
        //            //_applications.Clear();
        //            // 或者 
        //            _applications = null;
        //        }

        //        _applications = value;
        //        OnPropertyChanged("Applications");
        //    }
        //}
        //// 蜕变关系对应的应用程序
        //public ObservableCollection<>

        public void OnNavigatedFrom()
        {
           
        }

        public void OnNavigatedTo()
        {
            if (DataGridSelectedItem != null)
            {
                var metamorphicRelation = _metamorphicRelationSerive.GetMRById(DataGridSelectedItem.IdMR);
                string[] strarry = metamorphicRelation.ApplicationName.Split(':');
                this.InputPatternSympy = metamorphicRelation.InputPatterntosympy;
                this.OutputPatternSympy = metamorphicRelation.OutputPatterntosympy;
                this.DimensionOfInputPattern = metamorphicRelation.DimensionOfInputPattern;
                this.DimensionOfOutputPattern = metamorphicRelation.DimensionOfOutputPattern;

                reload_CodeNamecmbItemsSource();
                //默认所选择的蜕变关系记录对应着的应用程序
                var idapplication = _applicationSerive.GetApplicationId(DataGridSelectedItem.ApplicationName);
                var application = _applicationSerive.GetApplication(idapplication);
                Application =application;
                CodeName = Application.CodeName;
                for (int i = 0; i<Applications.Count;i++) 
                {
                    var app = Applications[i];
                    if (app.IdApplication == application.IdApplication) 
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public void reload_CodeNamecmbItemsSource()
        {
            var applications = new ObservableCollection<MetBench_Domain.Application>();
            var codeNameOptions = new ObservableCollection<string>();

            if (DataGridSelectedItem != null) 
            {
                var metamorphicRelation = _metamorphicRelationSerive.GetMRById(DataGridSelectedItem.IdMR);
                string[] strarry = metamorphicRelation.ApplicationName.Split(':');

                for (int i = 0; i < strarry.Length; i++)
                {
                    var applicationName = strarry[i];
                    var idApplication = _applicationSerive.GetApplicationId(applicationName);
                    var application = _applicationSerive.GetApplication(idApplication);
                    var codeName = application.CodeName;
                    codeNameOptions.Add(codeName);
                    applications.Add(application);
                }

                Applications = applications;
                CodeNameOptions = codeNameOptions;
            }
        }

        // 构造函数
        public MTExecutionViewModel(IPageService pageService , INavigationService navigationService,ApplicationSerive applicationSerive,MetamorphicRelationSerive metamorphicRelationSerive ) 
        {
            _navigationService = navigationService;
            _pageService = pageService;
            _applicationSerive = applicationSerive;
            _metamorphicRelationSerive = metamorphicRelationSerive;
            IsIndeterminate = false;
            Visibility = Visibility.Collapsed;
        }

        //MT参数验证器
        private ValidationResult GetValidationResult(MTParam mTParam)
        {
            var validator = new MTValidator();
            var result = validator.Validate(mTParam);
            return result;
        }

        //创建MT参数实例
        private MTParam Create()
        {
            var mtParam = new MTParam();
            mtParam.InputPatternSympy = InputPatternSympy;
            mtParam.OutputPatternSympy = OutputPatternSympy;
            mtParam.MinParam = MinParam;
            mtParam.MaxParam = MaxParam;
            mtParam.ExecutNumber = ExecutNumber;
            mtParam.Threshold = Threshold;
            return mtParam;
        }
        // 提示信息弹窗
        public bool showMessage(string message, string title)
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = title,
                Content = message,
            };
            uiMessageBox.CloseButtonAppearance = 0;
            uiMessageBox.CloseButtonText = "OK";
            var messageResult = uiMessageBox.ShowDialogAsync().Result.ToString();
            //primary为第一个按钮
            return messageResult == "Primary" ? true : false;
        }
        // 根据路径获取图片
        public BitmapImage GetImage(string path)
        {
            var path1 = path;
            BitmapImage bitmap = null;
            // 设置图片路径
            if (System.IO.File.Exists(path))
            {
                bitmap = new BitmapImage(new Uri(path, UriKind.Absolute));
            }
            else
            {
                System.Windows.MessageBox.Show("执行失败！", "提示", System.Windows.MessageBoxButton.OK);
            }
            return bitmap;
        }

        // 二元关系执行
        public async Task btn_AutoMR2()
        {
            if (Application != null)
            {
                var selectedIndex = SelectedIndex;
                var ApplicationsIndex = selectedIndex;
                CodeName = SelectedValue;
                // 获取应用程序的Id
                var idapplication = Applications[ApplicationsIndex].IdApplication;

                //获取应用程序的Id
                //var idapplication = Application.IdApplication;

                // 创建压缩上传文件类对象
                FileCompressionAndStorageUtility ft = new FileCompressionAndStorageUtility(_applicationSerive);
                // 接收Python脚本运行输出台的结果
                string[] splitStrings = null;

                // 解压对应的应用程序SUT
                ft.ExtractZipFilesFromDatabase(idapplication);
                var mtparam = Create();
                var validationResult = GetValidationResult(mtparam);
                if (!validationResult.IsValid)
                {
                    var message = string.Empty;
                    for (int i = 0; i < validationResult.Errors.Count; i++)
                    {
                        if (i == 0)
                        {
                            message += validationResult.Errors[i].ErrorMessage;
                        }
                        else
                        {
                            message += "\n";
                            message += validationResult.Errors[i].ErrorMessage;
                        }
                    }
                    showMessage(message, "Tips");
                    //System.Windows.MessageBox.Show(message, "提示", System.Windows.MessageBoxButton.OK);
                    return;
                }
                //var selectedMR = TransParamsService.runmetamorphicRelation;


                if (CodeName != string.Empty)
                {
                    // 原始实现
                    //AutoRunMR ar = new AutoRunMR();

                    //var progressWindow = new ProgressWindow()
                    //{

                       
                    //    WindowStartupLocation = WindowStartupLocation.CenterScreen
                    //};
                    // 设置进度条状态为可见和不确定状态  
                    IsIndeterminate = true;
                    Visibility = Visibility.Visible;
                    try
                    {
                        // 使用异步的方式实现
                        AutoRunMR_Await ar = new AutoRunMR_Await();
                        //string outputString = ar.AutorunMR2(InputPatternSympy, OutputPatternSympy, CodeName, DimensionOfInputPattern, DimensionOfOutputPattern, MinParam, MaxParam, ExecutNumber, Threshold);
                        string outputString = await ar.AutorunMR2Async(InputPatternSympy, OutputPatternSympy, CodeName, DimensionOfInputPattern, DimensionOfOutputPattern, MinParam, MaxParam, ExecutNumber, Threshold);
                        splitStrings = outputString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    finally
                    {
                        // 确保进度窗口被关闭  
                        //progressWindow.Close();
                        // 结束时将进度条状态更新为不可见和确定状态  
                        IsIndeterminate = false;
                        Visibility = Visibility.Collapsed;
                    }
                    //// 使用异步的方式实现
                    //AutoRunMR_Await ar = new AutoRunMR_Await();
                    ////string outputString = ar.AutorunMR2(InputPatternSympy, OutputPatternSympy, CodeName, DimensionOfInputPattern, DimensionOfOutputPattern, MinParam, MaxParam, ExecutNumber, Threshold);
                    //string outputString = await ar.AutorunMR2Async(InputPatternSympy, OutputPatternSympy, CodeName, DimensionOfInputPattern, DimensionOfOutputPattern, MinParam, MaxParam, ExecutNumber, Threshold);
                    //splitStrings = outputString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                }
                // 检查是否成功分割为三部分
                if (splitStrings != null && splitStrings.Length == 3)
                {
                    // 通过比率
                    Passrate = splitStrings[0];
                    // 失败比率
                    Failurerate = splitStrings[1];
                    // 图片路径
                    string imagepath = splitStrings[2];
                    Image = GetImage(imagepath);
                }
                else
                {
                    // 如果分割失败，执行相应的逻辑，例如弹出提示框
                    var msg = "执行失败！";
                    showMessage(msg, "Tips");
                    //MessageBox.Show("执行失败！", "提示", System.Windows.MessageBoxButton.OK);
                    return;
                }

            }
            else
            {

                var msg = "请选择被测程序！";
                showMessage(msg, "Tips");
                //System.Windows.MessageBox.Show("请选择被测程序！", "提示", System.Windows.MessageBoxButton.OK);
            }
            if (Data == null)
            {

                var msg = "请选择蜕变关系！";
                var res = showMessage(msg, "Tips");

                //MessageBoxResult result = System.Windows.MessageBox.Show("请选择蜕变关系！", "提示", System.Windows.MessageBoxButton.OK);
                //当点击OK时跳转到MR Mmanagement页面
                //if (result == MessageBoxResult.OK)
                if(!res)
                {
                    //进行页面跳转 跳转到MRManagement页面
                    var targetPageType = typeof(Views.Pages.MRManagementPage);
                    var page = _pageService.GetPage(targetPageType) as MRManagementPage;
                    _navigationService.Navigate(targetPageType);

                    //var mainwindow = TransParamsService.mainWindow;
                    //var Navigation = mainwindow.RootNavigation;
                    //var PageType = typeof(Views.Pages.AddMRPage);
                    //Navigation.Navigate(PageType);
                }
            }
        }

        public void btn_Cancle()
        {
            MinParam = "";
            MaxParam = "";
            ExecutNumber = "";
            Threshold = "1e-4";
            //默认第一个应用程序
            Application = Applications[0];
            CodeName = Applications[0].CodeName;
            SelectedIndex = 0;
            
        }

    }
}
