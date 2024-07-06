using CommunityToolkit.Mvvm.ComponentModel;
using FluentValidation;
using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_Client.Services;
using MetBench_Domain;
using MetBench_IDAL;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common.Interfaces;
using Application = MetBench_Domain.Application;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MetBench_Client.ViewModels
{
    public class AutoMRViewModel : ObservableObject, INavigationAware
    {
        public AutoMRViewModel()
        {
        }
        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo()
        {

        }
        public MetamorphicRelations_QueryResultData SelectedMetamorphicRelation { get; set; }
        //输入模式的Latex格式
        public String InputPatternSympy { get; set; } = string.Empty;
        //输出模式的Latex格式
        public String OutputPatternSympy { get; set; } = string.Empty;
        //输入模式的图片
        public BitmapImage InputPatternImag { get; set; }
        //输出模式的图片
        public BitmapImage OutputPatternImag { get; set; }
        //蜕变关系的图片路径
        public string Imagepath { get; set; }
        public BitmapImage Image { get; set; }
        //被测程序
        public Application Application { get; set; }
        //程序名
        public string CodeName { get; set; }
        //最小值
        public string MinParam { get; set; } = String.Empty;
        //最大值
        public string MaxParam { get; set; } = String.Empty;
        //执行次数
        public string ExecutNumber { get; set; } = String.Empty;
        //阈值 需要转换为double类型 double threshold = double.Parse(Threshold);
        public string Threshold {  get; set; } = "1e-4";
        //输入维度
        public string DimensionOfInputPattern { get; set; }

        //输出维度
        public string DimensionOfOutputPattern { get; set; }
        //通过比率
        public string Passrate { get; set; } = String.Empty;
        //失败比率
        public string Failurerate { get; set; } = String.Empty;
        //datagrid的数据源
        public ObservableCollection<MetamorphicRelations_QueryResultData> Data { get; set; }

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
        //MR验证器
        private ValidationResult GetValidationResult(MTParam mTParam)
        {
            var validator = new MTValidator();
            var result = validator.Validate(mTParam);
            return result;
        }
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
        //二元关系执行
        public void btn_AutoMR2()
        {
            if (Application != null)
            {
                var idapplication = Application.IdApplication;
                FileCompressionAndStorageUtility ft = new FileCompressionAndStorageUtility();
                string[] splitStrings = null;
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
                    System.Windows.MessageBox.Show(message, "提示", System.Windows.MessageBoxButton.OK);
                    return;
                }
                var selectedMR = TransParamsService.runmetamorphicRelation;

                if (CodeName != string.Empty)
                {
                    AutoRunMR ar = new AutoRunMR();
                    string outputString = ar.AutorunMR2(InputPatternSympy, OutputPatternSympy, CodeName, DimensionOfInputPattern, DimensionOfOutputPattern, MinParam, MaxParam, ExecutNumber, Threshold);
                     splitStrings = outputString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                }
                // 检查是否成功分割为三部分
                if (splitStrings!=null&&splitStrings.Length == 3)
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
                    MessageBox.Show("执行失败！","提示",System.Windows.MessageBoxButton.OK);
                    return;
                }

            }
            else
            {
                System.Windows.MessageBox.Show("请选择被测程序！", "提示", System.Windows.MessageBoxButton.OK);
            }
            if (Data == null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("请选择蜕变关系！", "提示", System.Windows.MessageBoxButton.OK);
                //当点击OK时跳转到MR Mmanagement页面
                if (result == MessageBoxResult.OK) 
                {
                    var mainwindow = TransParamsService.mainWindow;
                    var Navigation = mainwindow.RootNavigation;
                    var PageType = typeof(Views.Pages.AddMRPage);
                    Navigation.Navigate(PageType);
                }
            }
        }
        public void btn_Cancle()
        {
            MinParam = "";
            MaxParam = "";
            ExecutNumber = "";
            Threshold = "1e-4";
        }
    }
}
