﻿using CommunityToolkit.Mvvm.ComponentModel;
using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_Client.Models;
using MetBench_Client.Views.Pages;
using MetBench_Domain;
using MetBench_IDAL;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Stylet;
using Wpf.Ui;
using Wpf.Ui.Controls;
using ValidationResult = FluentValidation.Results.ValidationResult;
using MetBench_Client.Util;
using System.Windows;
using System;
using System.Threading.Tasks;
using HandyControl.Data;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection;


namespace MetBench_Client.ViewModels
{

    public class MRManagementViewModel : ObservableObject, INavigationAware, IHandle<ApplicationAddEvent>, IHandle<ApplicationMoidfyEvent>, IHandle<ApplicationDeleteEvent>, IHandle<DomainAddEvent>, IHandle<DomainModifyEvent>, IHandle<DomainDeleteEvent>
    {
        //导航服务 用于页面切换
        private INavigationService _navigationService;

        //页面服务 用于获取页面实体
        private IPageService _pageService;

        // 蜕变关系管理服务
        private MetamorphicRelationSerive _metamorphicRelationSerive;

        // 应用程序管理服务
        private ApplicationSerive _applicationSerive;

        // OrderOfMR_ComboBox的数据源
        public List<string> OrderOfMRList { get { return MultClass.GetOrderOfMR(); } }

        // RtType为枚举类型 分别为 数值 谓词 其他
        // RepresentationType_ComboBox的数据源
        public List<string> RepresentationTypeList { get { return MultClass.GetRtTypeList(); } }

        // DataGrid控件的数据源
        public ObservableCollection<MetamorphicRelations_QueryResultData> Data { get; set; }

        // DataGrid的SelectedItem
        public MetamorphicRelations_QueryResultData DataGridSelectItem { get; set; }
        // 接受传递的蜕变关系

        // OrderOfMR_ComboBox的SelectedIndex
        // 0表示全部关系 1表示二元关系  2表示三元关系 3多元关系
        public int OrderOfMR_ComboBoxSelectedIndex { get; set; } = 0;

        // RepresentationType_ComboBoxSelectedIndex的SelectedIndex  1 数值 2 谓词 3 其他
        public int RepresentationType_ComboBoxSelectedIndex { get; set; } = 0;

        // OrderOfMR_ComboBox的SelectedValue
        public string OrderOfMR_ComboBoxSelectedValue { get; set; } = string.Empty;

        // txbIdMR的数据绑定属性
        public string IdMR { get; set; } = "0";

        // txbDescription的数据绑定属性
        public string Description { get; set; } = string.Empty;

        // txbContext的数据绑定属性
        public string Context { get; set; } = string.Empty;

        // txbConstraint的数据绑定属性
        public string Constraint { get; set; } = string.Empty;

        // txbInputPattern的数据绑定属性
        public string InputPattern { get; set; } = string.Empty;

        // txbOutputPattern的数据绑定属性
        public string OutputPattern { get; set; } = string.Empty;

        // txbDimensionOfInputPattern的数据绑定属性
        public string DimensionOfInputPattern { get; set; } = string.Empty;

        // txbDimensionOfOutputPattern的数据绑定属性
        public string DimensionOfOutputPattern { get; set; } = string.Empty;

        // 进度条属性 IsIndeterminate
        public bool IsIndeterminate { get; set; }

        // 进度条属性 Visibility
        public Visibility Visibility { get; set; }

        // 事件聚合
        private IEventAggregator _eventAggregator;

        // 每页数据行数量
        public int DataCountPerPage { get; set; } = 5;

        // 每页最大数据行数量
        public int MaxPageCount { get; set; } = 10;

        // 页码
        public int PageIndex { get; set; } = 1;

        // 查询关键字
        public string ApplicationNameBoxText { get; set; } = string.Empty;

        // 实现接口 INavigationAware
        public void OnNavigatedTo()
        {

            if (DataGridSelectItem != null)
            {
                //获取页面跳转所传递的参数 并将相关数据展示到控件中
                var selectedItem = DataGridSelectItem;
                show();
                reload_ItemsSource();
            }
        }

        public void OnNavigatedFrom()
        {

        }

        // 初始化Application的ComboBox的数据源
        private void comboBoxDataSourceInit()
        {
            var applications = _applicationSerive.GetApplications();
            for (int i = 0; i < applications.Count; i++)
            {
                var application = applications[i];
                ApplicationExs.Add(new ApplicationEx(application));
            }
            var last = new ApplicationEx(new MetBench_Domain.Application() { Name = "Other" });
            ApplicationExs.Add(last);
        }

        // 构造函数
        public MRManagementViewModel(MetamorphicRelationSerive metamorphicRelationSerive, ApplicationSerive applicationSerive, IPageService pageService, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            this._metamorphicRelationSerive = metamorphicRelationSerive;
            this._applicationSerive = applicationSerive;
            this._navigationService = navigationService;
            this._pageService = pageService;
            this._eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            IsIndeterminate = false;
            Visibility = Visibility.Collapsed;
            comboBoxDataSourceInit();
            reload_ItemsSource();
        }

        //加载DataGrid数据
        public void reload_ItemsSource()
        {
            //两表联查结果    
            var datas = _metamorphicRelationSerive.showMultTwoTableResult();

            for (int i = 0; i < datas.Count; i++)
            {
                var domainname = datas[i].DomainName;
                if (domainname == null || domainname == string.Empty)
                {
                    datas[i].DomainName = "无";
                }
                //多个DomainName进行换行处理 \n
                else
                {
                    string[] strarray = domainname.Split(":");
                    if (strarray.Length > 1)
                    {
                        datas[i].DomainName = string.Empty;
                        for (int j = 0; j < strarray.Length; j++)
                        {
                            if (j == 0)
                            {
                                datas[i].DomainName += strarray[j];
                            }
                            else
                            {
                                datas[i].DomainName += "\n";
                                datas[i].DomainName += strarray[j];
                            }
                        }
                    }
                }
            }

            var index = PageIndex;
            // 按关键字查询
            var mrs = datas.Where(x => x.ApplicationName.Contains(ApplicationNameBoxText)).ToList();
            // 分页数量
            var count = (double)(mrs.Count * 1.0 / DataCountPerPage);
            MaxPageCount = (int)Math.Ceiling(count);
            // 按页码及每页记录数
            var booksPaged = mrs.Skip((index - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();
            var paged = mrs.Skip((index - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();
            Data = new ObservableCollection<MetamorphicRelations_QueryResultData>(booksPaged);

            // 查完后将搜索框的内容清空
            ApplicationNameBoxText = string.Empty;

            //Data = datas;

        }

        // 创建蜕变关系实体
        private MetamorphicRelation Create()
        {
            // 数据类型转换
            var idmr = 0;
            int.TryParse(IdMR, out idmr);

            var mr = new MetamorphicRelation();
            mr.IdMR = idmr;
            mr.Description = Description;
            mr.Context = Context;
            mr.Constraint = Constraint;
            mr.OrderOfMR = OrderOfMR_ComboBoxSelectedValue;
            mr.RepresentationType = (RtType)(RepresentationType_ComboBoxSelectedIndex - 1);//减掉“全部类型”这一项
            // 还原转义
            var inputPattern = RestoreString(InputPattern);
            mr.InputPattern = inputPattern;
            // 还原转义
            var outputPattern = RestoreString(OutputPattern);
            mr.OutputPattern = outputPattern;
            mr.DimensionOfInputPattern = DimensionOfInputPattern;
            mr.DimensionOfOutputPattern = DimensionOfOutputPattern;
            mr.ApplicationName = SelectedText;
            return mr;
        }

        // 还原转义字符的方法
        private string RestoreString(string input)
        {
            // 将转义字符还原为实际字符
            string result = input
                .Replace("\\n", "\n")
                .Replace("\\t", "\t")
                .Replace("\\\\", "\\");

            return result;
        }

        // 处理字符串的方法
        private string ProcessString(string input)
        {
            // 调用还原转义字符的方法
            string restoredString = RestoreString(input);

            // 使用原始字符串字面量
            string result = $@"{restoredString}";

            // 返回处理后的字符串
            return result;
        }

        // 蜕变关系验证器
        private ValidationResult GetValidationResult(MetamorphicRelation metamorphicRelation)
        {
            var validator = new MetamorphicRelationValidator(_metamorphicRelationSerive);
            var result = validator.Validate(metamorphicRelation);
            return result;
        }

        // 查重判断
        public bool CheckMRExistence(MetamorphicRelation mr)
        {

            var validator = new MetamorphicRelationValidator(_metamorphicRelationSerive);
            var Existence = validator.IsDuplicate(mr);
            if (Existence)
            {
                // 蜕变关系已存在
                return true;
            }

            // 蜕变关系不存在
            return false;
        }

        // 重置文本框等控件内容
        public void btnCancel_Click()
        {
            //清空所有文本框，下拉框selectedIndex设置为0
            IdMR = 0.ToString();
            Description = string.Empty;
            Context = string.Empty;
            Constraint = string.Empty;
            InputPattern = string.Empty;
            OutputPattern = string.Empty;
            DimensionOfInputPattern = string.Empty;
            DimensionOfOutputPattern = string.Empty;
            OrderOfMR_ComboBoxSelectedIndex = 0;
            RepresentationType_ComboBoxSelectedIndex = 0;

            //将ComboBox的选中全部取消
            resetcmbApplicationName();

            reload_ItemsSource();
        }

        // 重置ApplicationNames选中的选项
        public void resetcmbApplicationName()
        {
            for (int i = 0; i < ApplicationExs.Count(); i++)
            {
                var applicationEx = ApplicationExs[i];
                var isCheck = applicationEx.IsChecked;
                if (isCheck == true)
                {
                    ApplicationExs[i].IsChecked = false;
                }
            }
            SelectedText = string.Empty;
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
            uiMessageBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            uiMessageBox.CloseButtonText = "OK";
            var messageResult = uiMessageBox.ShowDialogAsync().Result.ToString();
            //primary为第一个按钮
            return messageResult == "Primary" ? true : false;
        }

        // 增加蜕变关系
        public async Task btnAdd_Click()
        {
            var metamorphicRelation = Create();
            var validationResult = GetValidationResult(metamorphicRelation);//mr验证器
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
                return;
            }

            if (CheckMRExistence(metamorphicRelation))
            {
                var message = "该蜕变关系已存在!";
                showMessage(message, "Tips");
                //System.Windows.MessageBox.Show("该蜕变关系已存在!");
                return;
            }
            //使用异步方式，将Latex转换为Sympy，Latex渲染图片
            IsIndeterminate = true;
            Visibility = Visibility.Visible;
            try
            {
                // 使用异步的方式实现
                //AutoRunMR_Await ar = new AutoRunMR_Await();
                //string outputString = await ar.AutorunMR2Async(InputPatternSympy, OutputPatternSympy, CodeName, DimensionOfInputPattern, DimensionOfOutputPattern, MinParam, MaxParam, ExecutNumber, Threshold);
                //splitStrings = outputString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                // 创建 Latextosympy 实例  
                Latextosympy_Await latextosympy_Await = new Latextosympy_Await();

                // 将Latex转为Sympy格式的字符串  
                metamorphicRelation.InputPatterntosympy = await latextosympy_Await.LatextosympyAsync(metamorphicRelation.InputPattern);
                metamorphicRelation.OutputPatterntosympy = await latextosympy_Await.LatextosympyAsync(metamorphicRelation.OutputPattern);

                // 展示Latex渲染成的图片  
                // 1.将Latex渲染成图片  
                string InputPatternimagepath = await latextosympy_Await.LatextoImageAsync(metamorphicRelation.InputPattern);
                string OutputPatternimagepath = await latextosympy_Await.LatextoImageAsync(metamorphicRelation.OutputPattern);
                //2.将图片转为字节数组
                metamorphicRelation.InputPatternImageData = latextosympy_Await.ConvertImageToByteArray(InputPatternimagepath);
                metamorphicRelation.OutputPatternImageData = latextosympy_Await.ConvertImageToByteArray(OutputPatternimagepath);
            }
            finally
            {
                // 确保进度窗口被关闭  
                //progressWindow.Close();
                // 结束时将进度条状态更新为不可见和确定状态  
                IsIndeterminate = false;
                Visibility = Visibility.Collapsed;
            }


            //// 将Latex转为Sympy格式的字符串
            //Latextosympy latextosympy = new Latextosympy();
            //// 原始实现
            //metamorphicRelation.InputPatterntosympy = latextosympy.latextosympy(metamorphicRelation.InputPattern);
            //metamorphicRelation.OutputPatterntosympy = latextosympy.latextosympy(metamorphicRelation.OutputPattern);
            //// 展示Latex渲染成的图片
            //// 1.将Latex渲染成图片
            //string InputPatternimagepath = latextosympy.LatextoImage(metamorphicRelation.InputPattern);
            //string OutputPatternimagepath = latextosympy.LatextoImage(metamorphicRelation.OutputPattern);
            //// 2.将图片转为字节数组
            //metamorphicRelation.InputPatternImageData = latextosympy.ConvertImageToByteArray(InputPatternimagepath);
            //metamorphicRelation.OutputPatternImageData = latextosympy.ConvertImageToByteArray(OutputPatternimagepath);

            bool result = _metamorphicRelationSerive.AddService(metamorphicRelation);
            if (result)
            {
                var message = "添加记录 成功";
                showMessage(message, "Tips");
                //System.Windows.MessageBox.Show("添加记录 成功", "提示", System.Windows.MessageBoxButton.OK);
            }
            else
            {
                var message = "添加记录 失败";
                showMessage(message, "Tips");
                //System.Windows.MessageBox.Show("添加记录 失败", "提示", System.Windows.MessageBoxButton.OK);
            }

            var ms = "蜕变关系添加成功";
            MetamorphicRelationOperationEvent metamorphicRelationOperationEvent = new MetamorphicRelationOperationEvent() { message = ms };
            // 发布事件，刷新MRDisplay页面的蜕变关系列表
            _eventAggregator.Publish(metamorphicRelationOperationEvent);
            // 刷新数据
            reload_ItemsSource();
            // 将文本框等控件内容重置
            btnCancel_Click();

        }

        // 修改蜕变关系
        public async Task btnModify_Click()
        {
            if (DataGridSelectItem == null)
            {
                var msg = "请选择要修改的蜕变关系！";
                showMessage(msg, "Tips");
                //System.Windows.MessageBox.Show("请选择要修改的蜕变关系！", "提示", System.Windows.MessageBoxButton.OK);
                return;
            }

            var metamorphicRelation = Create();
            var validationResult = GetValidationResult(metamorphicRelation);//mr验证器
            if (!validationResult.IsValid)
            {
                var mesg = string.Empty;
                for (int i = 0; i < validationResult.Errors.Count; i++)
                {
                    if (i == 0)
                    {
                        mesg += validationResult.Errors[i].ErrorMessage;
                    }
                    else
                    {
                        mesg += "\n";
                        mesg += validationResult.Errors[i].ErrorMessage;
                    }
                }

                showMessage(mesg, "Tips");
                //System.Windows.MessageBox.Show(message, "提示", System.Windows.MessageBoxButton.OK);
                return;
            }

            if (CheckMRExistence(metamorphicRelation))
            {
                var msg1 = "该蜕变关系已存在";
                showMessage(msg1, "Tips");
                //System.Windows.MessageBox.Show("该蜕变关系已存在");
                return;
            }

            //// 将Latex转为Sympy格式的字符串
            //Latextosympy latextosympy = new Latextosympy();
            //metamorphicRelation.InputPatterntosympy = latextosympy.latextosympy(metamorphicRelation.InputPattern);
            //metamorphicRelation.OutputPatterntosympy = latextosympy.latextosympy(metamorphicRelation.OutputPattern);
            //// 展示Latex渲染成的图片
            //// 1.将Latex渲染成图片
            //string InputPatternimagepath = latextosympy.LatextoImage(metamorphicRelation.InputPattern);
            //string OutputPatternimagepath = latextosympy.LatextoImage(metamorphicRelation.OutputPattern);
            //// 2.将图片转为字节数组
            //metamorphicRelation.InputPatternImageData = latextosympy.ConvertImageToByteArray(InputPatternimagepath);
            //metamorphicRelation.OutputPatternImageData = latextosympy.ConvertImageToByteArray(OutputPatternimagepath);

            //使用异步方式，将Latex转换为Sympy，Latex渲染图片
            IsIndeterminate = true;
            Visibility = Visibility.Visible;
            try
            {
                // 使用异步的方式实现
                //AutoRunMR_Await ar = new AutoRunMR_Await();
                //string outputString = await ar.AutorunMR2Async(InputPatternSympy, OutputPatternSympy, CodeName, DimensionOfInputPattern, DimensionOfOutputPattern, MinParam, MaxParam, ExecutNumber, Threshold);
                //splitStrings = outputString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                // 创建 Latextosympy 实例  
                Latextosympy_Await latextosympy_Await = new Latextosympy_Await();

                // 将Latex转为Sympy格式的字符串  
                metamorphicRelation.InputPatterntosympy = await latextosympy_Await.LatextosympyAsync(metamorphicRelation.InputPattern);
                metamorphicRelation.OutputPatterntosympy = await latextosympy_Await.LatextosympyAsync(metamorphicRelation.OutputPattern);

                // 展示Latex渲染成的图片  
                // 1.将Latex渲染成图片  
                string InputPatternimagepath = await latextosympy_Await.LatextoImageAsync(metamorphicRelation.InputPattern);
                string OutputPatternimagepath = await latextosympy_Await.LatextoImageAsync(metamorphicRelation.OutputPattern);

                //2.将图片转为字节数组
                metamorphicRelation.InputPatternImageData = latextosympy_Await.ConvertImageToByteArray(InputPatternimagepath);
                metamorphicRelation.OutputPatternImageData = latextosympy_Await.ConvertImageToByteArray(OutputPatternimagepath);
            }
            finally
            {
                // 确保进度窗口被关闭  
                //progressWindow.Close();
                // 结束时将进度条状态更新为不可见和确定状态  
                IsIndeterminate = false;
                Visibility = Visibility.Collapsed;
            }

            var msg2 = "是否修改该记录?";
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "Tips",
                Content = msg2,
            };
            uiMessageBox.CloseButtonAppearance = 0;
            uiMessageBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            uiMessageBox.CloseButtonText = "No";
            uiMessageBox.IsPrimaryButtonEnabled = true;
            uiMessageBox.PrimaryButtonText = "Yes";
            var messageResult = uiMessageBox.ShowDialogAsync().Result.ToString();
            var result = messageResult == "Primary" ? true : false;

            //var dialog = System.Windows.MessageBox.Show("是否修改该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            //if (dialog.Equals(System.Windows.MessageBoxResult.Yes))
            if (result)
            {
                var editResult = _metamorphicRelationSerive.UpdateService(metamorphicRelation);
                if (editResult)
                {
                    var msg3 = "修改记录 成功";
                    showMessage(msg3, "Tips");
                    //System.Windows.MessageBox.Show("修改记录 成功", "提示", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    var msg3 = "修改记录 失败";
                    showMessage(msg3, "Tips");
                    //System.Windows.MessageBox.Show("修改记录 失败", "提示", System.Windows.MessageBoxButton.OK);
                }

            }

            var ms = "蜕变关系修改成功";
            MetamorphicRelationOperationEvent metamorphicRelationOperationEvent = new MetamorphicRelationOperationEvent() { message = ms };
            // 发布事件，刷新MRDisplay页面的蜕变关系列表
            _eventAggregator.Publish(metamorphicRelationOperationEvent);
            // 刷新数据
            reload_ItemsSource();
            // 将文本框等控件内容重置
            btnCancel_Click();
        }

        // 删除蜕变关系
        public void btnDelect_Click()
        {
            if (DataGridSelectItem == null)
            {
                var message = "请选择要删除的蜕变关系！";
                showMessage(message, "Tips");
                //System.Windows.MessageBox.Show("请选择要删除的蜕变关系！", "提示", System.Windows.MessageBoxButton.OK);
                return;
            }

            var metamorphicRelation = Create();
            var message1 = "是否删除该记录?";
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "Tips",
                Content = message1,
            };
            uiMessageBox.CloseButtonAppearance = 0;
            uiMessageBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            uiMessageBox.CloseButtonText = "No";
            uiMessageBox.IsPrimaryButtonEnabled = true;
            uiMessageBox.PrimaryButtonText = "Yes";
            var messageResult = uiMessageBox.ShowDialogAsync().Result.ToString();
            var result = messageResult == "Primary" ? true : false;
            //var dialog = System.Windows.MessageBox.Show("是否删除该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            //if (dialog.Equals(System.Windows.MessageBoxResult.Yes))

            if (result)
            {
                var suc = _metamorphicRelationSerive.DeleteService(metamorphicRelation);

                var msg = suc ? "成功" : "失败";
                var msgs = "删除记录" + msg;
                showMessage(msgs, "Tips");

                //System.Windows.MessageBox.Show("删除记录" + msg, "提示", System.Windows.MessageBoxButton.OK);
            }

            var ms = "蜕变关系删除成功";
            MetamorphicRelationOperationEvent metamorphicRelationOperationEvent = new MetamorphicRelationOperationEvent() { message = ms };
            // 发布事件，刷新MRDisplay页面的蜕变关系列表
            _eventAggregator.Publish(metamorphicRelationOperationEvent);
            // 刷新数据
            reload_ItemsSource();
            // 将文本框等控件内容重置
            btnCancel_Click();
        }

        // 当返回的为true 选择的是每一行的蜕变关系数据
        public bool show()
        {
            if (DataGridSelectItem != null)
            {
                IdMR = DataGridSelectItem.IdMR.ToString();
                Description = DataGridSelectItem.Description;
                Context = DataGridSelectItem.Context;
                Constraint = DataGridSelectItem.Constraint;

                if (DataGridSelectItem.OrderOfMR == "二元关系")
                {
                    OrderOfMR_ComboBoxSelectedIndex = 1;
                }

                if (DataGridSelectItem.OrderOfMR == "三元关系")
                {
                    OrderOfMR_ComboBoxSelectedIndex = 2;
                }

                if (DataGridSelectItem.OrderOfMR == "多元关系")
                {
                    OrderOfMR_ComboBoxSelectedIndex = 3;
                }

                RepresentationType_ComboBoxSelectedIndex = (int)DataGridSelectItem.RepresentationType + 1;//往下移动一项
                InputPattern = DataGridSelectItem.InputPattern;
                OutputPattern = DataGridSelectItem.OutputPattern;
                DimensionOfInputPattern = DataGridSelectItem.DimensionOfInputPattern;
                DimensionOfOutputPattern = DataGridSelectItem.DimensionOfOutputPattern;

                var idmr = DataGridSelectItem.IdMR;
                var metamorphicRelation = _metamorphicRelationSerive.GetMRById(idmr);
                var applicationName = metamorphicRelation.ApplicationName;
                string[] strarray = applicationName.Split(':');

                // 将之前选中的选项进行取消
                foreach (var appEx in ApplicationExs)
                {
                    appEx.IsChecked = false;
                }

                for (int i = 0; i < strarray.Length; i++) 
                {
                    foreach (var appEx in ApplicationExs) 
                    {
                        if (appEx.Application.Name == strarray[i]) 
                        {
                            appEx.IsChecked = true;
                        }
                    }
                }

                // Application的ComboBox进行选中
                //foreach (var appEx in ApplicationExs) 
                //{
                //    for (int j = 0; j < strarray.Length; j++)
                //    {
                //        var name = strarray[j];
                //        var application = appEx.Application;
                //        if (name == application.Name)
                //        {
                //            appEx.IsChecked = true;
                //        }
                //    }
                //}
                return true;
            }
            else
            {
                return false;
            }
        }

        //跳转MT Excute页面 并传递参数
        public void btn_MTExcute(int id)
        {
            if (DataGridSelectItem != null)
            {
                var selectedItem = DataGridSelectItem;
                if (selectedItem != null)
                {
                    var targetPageType = typeof(Views.Pages.MTExecutionPage);

                    var page = _pageService.GetPage(targetPageType) as MTExecutionPage;
                    var viewModel = page.ViewModel;
                    viewModel.Data = new ObservableCollection<MetamorphicRelations_QueryResultData>() { selectedItem };
                    viewModel.DataGridSelectedItem = selectedItem;
                    _navigationService.Navigate(targetPageType);
                }
            }
        }

        // -----------------------------------------------
        // 以下是用于Application的ComBox实现多选的交互逻辑

        // 私有成员变量 _selectedText
        private string _selectedText = string.Empty;

        // 公共属性 SelectedText
        public string SelectedText
        {
            get
            {
                // 这个方法用于获取当前 _selectedText 的值。当其他代码或视图需要访问选定文本时，会调用这个方法。
                // MultiSelectComboBox.BookEx
                if (_selectedText != "MetBench_Client.Models.ApplicationEx")
                {
                    return _selectedText;
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                // 此方法用于设置新的选择文本。
                // 首先，它会检查传入的值（value）是否与当前 _selectedText 相同，避免不必要的更新。
                // 如果新值与当前值不同，则将 _selectedText 更新为新值。
                // 更新 _selectedText 的同时，会调用 RaisePropertyChanged("SelectedText") 方法，通知 UI 该属性的值已发生变化。
                if (_selectedText != value)
                {
                    _selectedText = value;

                    OnPropertyChanged("SelectedText");
                }
            }
        }

        // 私有成员变量
        private ObservableCollection<ApplicationEx> _applications;

        // 公共属性 用于公开_applications集合
        public ObservableCollection<ApplicationEx> ApplicationExs
        {

            get
            {
                // 在get方法中，第一次访问 BookExs 时，如果 _books 还没有被初始化（值为 null）
                // ，则创建一个新的 ObservableCollection<BookEx> 实例。
                if (_applications == null)
                {
                    _applications = new ObservableCollection<ApplicationEx>();

                    // 集合变化事件处理
                    // 这一段代码为 _books 集合的变化（如添加或删除 BookEx 实例）注册了一个事件处理程序。
                    // CollectionChanged 事件在集合中的元素添加或移除时被触发。
                    _applications.CollectionChanged += (sender, e) =>
                    {
                        if (e.OldItems != null)
                        {
                            // 在事件处理程序中，首先检查 OldItems，如果有（即有书籍被移除），
                            // 则从每个移除的 BookEx 实例中注销 ItemPropertyChanged 方法。
                            foreach (ApplicationEx applicationEx in e.OldItems)
                            {
                                applicationEx.PropertyChanged -= ItemPropertyChanged;
                            }
                        }

                        // 然后检查 NewItems，如果有（即有新书籍被添加），
                        // 则为每个新增的 BookEx 实例注册 ItemPropertyChanged 方法。
                        if (e.NewItems != null)
                        {
                            foreach (ApplicationEx applicationEx in e.NewItems)
                            {
                                applicationEx.PropertyChanged += ItemPropertyChanged;
                            }
                        }
                    };
                }

                return _applications;
            }
        }

        // 这里略去的 ItemPropertyChanged 方法是在 BookEx 的 IsChecked 属性发生更改时被调用的。它负责更新 SelectedText。
        // 注册和注销事件处理程序确保了只有当前集合中的书籍实例的属性更改事件被处理。这样做可以避免潜在的内存泄漏和错误。
        // ----用于处理 BookEx 对象的 IsChecked 属性变化事件---。
        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // 检查属性名称
            if (e.PropertyName == "IsChecked")
            {
                // 获取书籍对象
                ApplicationEx applicationEx = sender as ApplicationEx;

                if (applicationEx != null)
                {
                    if (applicationEx.Application.Name == "Other" && applicationEx.IsChecked == true)
                    {
                        //进行页面跳转 跳转至ApplicationManagementPage
                        var targetPageType = typeof(Views.Pages.ApplicationManagementPage);
                        var page = _pageService.GetPage(targetPageType) as ApplicationManagementPage;
                        _navigationService.Navigate(targetPageType);
                    }
                    else
                    {
                        // 收集已选中的书籍
                        IEnumerable<ApplicationEx> applicationExs = ApplicationExs.Where(b => b.IsChecked == true);

                        // 构建选中的书籍名称字符串 StringBuilder 用于高效拼接字符串
                        StringBuilder builder = new StringBuilder();
                        //string text = string.Empty;
                        foreach (ApplicationEx item in applicationExs)
                        {
                            //text += item.Application.Name;
                            //text += ":";
                            builder.Append(item.Application.Name);
                            builder.Append(":");
                            //builder.Append(item.Book.Name + ":");
                        }
                        if (builder.Length > 0)
                        {
                            builder.Length--;// 去掉最后一个字符:
                        }

                        SelectedText = builder == null ? string.Empty : builder.ToString();
                    }
                }
            }
        }

        public void Handle(ApplicationAddEvent message)
        {
            var applicationName = message.Name;
            var application = new MetBench_Domain.Application() { Name = applicationName };
            var applicationEx = new ApplicationEx(application);
            // 插入到倒数第二项  
            if (ApplicationExs.Count > 1)
            {
                ApplicationExs.Insert(ApplicationExs.Count - 1, applicationEx);
            }
            else
            {
                // 如果列表中少于两个项，可以选择添加到末尾或根据需要处理
                ApplicationExs.Add(applicationEx);
            }

            reload_ItemsSource();
        }

        public void Handle(ApplicationMoidfyEvent message)
        {
            // 修改前的名称
            var applicationName = message.Name;

            // 修改后的名称
            var newapplicationName = message.newName;

            for (int i = 0; i < ApplicationExs.Count; i++)
            {
                var applicationEx = ApplicationExs[i];
                if (applicationEx.Application.Name == applicationName)
                {
                    var isChecked = ApplicationExs[i].IsChecked; 
                    ApplicationExs[i] = new ApplicationEx(new MetBench_Domain.Application() { Name = newapplicationName });
                    if (isChecked == true) 
                    {
                        ApplicationExs[i].IsChecked = false;
                        ApplicationExs[i].IsChecked = true;
                    }
                    
                    //ApplicationExs.Remove(ApplicationExs[i]);

                    //ApplicationExs.Add(new ApplicationEx(new Application() { Name = newapplicationName }));
                }
            }

            reload_ItemsSource();
        }

        public void Handle(ApplicationDeleteEvent message)
        {
            var applicationName = message.Name;
            for (int i = 0; i < ApplicationExs.Count; i++)
            {
                var applicationEx = ApplicationExs[i];
                if (applicationEx.Application.Name == applicationName)
                {
                    ApplicationExs.Remove(ApplicationExs[i]);
                }
            }

            reload_ItemsSource();
        }

        public void Handle(DomainModifyEvent message)
        {
            reload_ItemsSource();
        }

        public void Handle(DomainAddEvent message)
        {
            reload_ItemsSource();
        }

        public void Handle(DomainDeleteEvent message)
        {
            reload_ItemsSource();
        }
    }
}
