using CommunityToolkit.Mvvm.ComponentModel;
using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_Client.Util;
using MetBench_Domain;
using MetBench_IDAL;
using Stylet;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Wpf.Ui;
using Wpf.Ui.Controls;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MetBench_Client.ViewModels
{
    public class DomainManagementViewModel : ObservableObject, INavigationAware
    {
        // 导航服务 用于页面切换
        private INavigationService _navigationService;

        // 页面服务 用于获取页面实体
        private IPageService _pageService;

        // 管理服务
        private DomainSerive _domainSerive;

        // datagrid的数据源
        public ObservableCollection<Domain> Data { get; set; }

        // 绑定datagrid的SelectedItem
        public Domain DataGridSelectedItem { get; set; }
        // 保存datagrid的SelectedItem
        public Domain DomainSelectedItem { get; set; }

        // IdDomain属性
        public string IdDomain { get; set; } = "0";

        // Name属性
        public string Name { get; set; } = string.Empty;

        // Description属性
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 事件聚合
        /// </summary>
        private IEventAggregator _eventAggregator;

        // 每页数据行数量
        public int DataCountPerPage { get; set; } = 5;

        // 每页最大数据行数量
        public int MaxPageCount { get; set; } = 10;

        // 页码
        public int PageIndex { get; set; } = 1;

        // 查询关键字
        public string DomainNameBoxText { get; set; } = string.Empty;
        // 实现接口 INavigationAware
        public void OnNavigatedFrom()
        {
        }

        public void OnNavigatedTo()
        {
        }

        // 构造函数
        public DomainManagementViewModel(IPageService pageService, INavigationService navigationService, DomainSerive domainSerive, IEventAggregator eventAggregator) 
        {
            _pageService = pageService;
            _navigationService = navigationService;
            _domainSerive = domainSerive;
            _eventAggregator = eventAggregator;

            reload_ItemsSource();
        }

        // 加载DataGrid数据源
        public void reload_ItemsSource()
        {
            var datas = _domainSerive.showAllResult();
            //Data = _domainSerive.showAllResult();

            var index = PageIndex;
            // 按关键字查询
            var mrs = datas.Where(x => x.Name.Contains(DomainNameBoxText)).ToList();
            // 分页数量
            var count = (double)(mrs.Count * 1.0 / DataCountPerPage);
            MaxPageCount = (int)Math.Ceiling(count);
            // 按页码及每页记录数
            var booksPaged = mrs.Skip((index - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();
            var paged = mrs.Skip((index - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();
            Data = new ObservableCollection<Domain>(booksPaged);

            // 查完后将搜索框的内容清空
            DomainNameBoxText = string.Empty;
        }

        // 创建应用领域实体
        private Domain Create()
        {
            var iddomain = 0;
            int.TryParse(IdDomain, out iddomain);
            var domain = new Domain();
            domain.IdDomain = iddomain;
            domain.Name = Name;
            domain.Description = Description;
            return domain;
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

        public void btnAdd_Click()
        {
            var Domain = Create();

            var validationResult = GetValidationResult(Domain);
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
            if (CheckDomainExistence(Domain))
            {
                var message = "该应用领域已存在!";
                showMessage(message, "Tips");
                //System.Windows.MessageBox.Show("该蜕变关系已存在!");
                return;
            }

            var result = _domainSerive.AddService(Domain);
            if (result)
            {
                var domainName = Domain.Name;
                DomainAddEvent domainAddEvent = new DomainAddEvent() { Name = domainName };
                _eventAggregator.Publish(domainAddEvent);
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

            //System.Windows.MessageBox.Show("添加记录 " + msg, "提示", System.Windows.MessageBoxButton.OK);
            reload_ItemsSource();
            btnCancel_Click();
        }
        public void btnModify_Click()
        {
            if (DataGridSelectedItem == null)
            {
                var msg = "请选择要修改的领域！";
                showMessage(msg, "Tips");
                //System.Windows.MessageBox.Show("请选择要修改的领域！");
                return;
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
            if(result)
            {
                var Domain = Create();
                var validationResult = GetValidationResult(Domain);
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
                    //System.Windows.MessageBox.Show(message, "提示", System.Windows.MessageBoxButton.OK);
                    return;
                }
                if (CheckDomainExistence(Domain))
                {
                    var msg1 = "该应用领域已存在";
                    showMessage(msg1, "Tips");
                    return;
                }

                if (_domainSerive.GetDomain(Domain.IdDomain) == null)
                {
                    var iddomain = _domainSerive.GetDomainId(Domain.Name);
                    if (iddomain > 0) 
                    {
                        var msg1 = "填写的Name已存在";
                        showMessage(msg1, "Tips");
                        return;
                    }
                }
                else 
                {
                    var domainName = _domainSerive.GetDomain(Domain.IdDomain).Name;
                    var newdomainName = Domain.Name;
                    if (domainName != newdomainName)
                    {
                        //若更改前后一致，无须加入任何逻辑

                        //应用程序名称进行修改，判断是否存在
                        var isExist = _domainSerive.GetDomainId(newdomainName) > 0;
                        if (isExist)
                        {
                            var msg1 = "填写的Name已存在";
                            showMessage(msg1, "Tips");
                            return;
                        }
                    }
                }
                //var domainName = _domainSerive.GetDomain(Domain.IdDomain).Name;
                //var newdomainName = Domain.Name;
                //if (domainName != newdomainName)
                //{
                //    //若更改前后一致，无须加入任何逻辑

                //    //应用程序名称进行修改，判断是否存在
                //    var isExist = _domainSerive.GetDomainId(newdomainName) > 0;
                //    if (isExist)
                //    {
                //        var msg1 = "填写的Name已存在";
                //        showMessage(msg1, "Tips");
                //        return;
                //    }
                //}

                //var domainName = _domainSerive.GetDomain(Domain.IdDomain).Name;
                var res = _domainSerive.UpdateService(Domain);
                if (res)
                {
                    var domainName = string.Empty;
                    var newdomainName = Domain.Name;

                    if (_domainSerive.GetDomain(Domain.IdDomain) !=null )
                    {
                        domainName = _domainSerive.GetDomain(Domain.IdDomain).Name;
                    }
                    else 
                    {
                        domainName = Domain.Name;
                    }

                    //var newdomainName = Domain.Name;
                    DomainModifyEvent domainModifyEvent = new DomainModifyEvent() { Name = domainName ,newName= newdomainName};
                    _eventAggregator.Publish(domainModifyEvent);
                    var message = "修改记录 成功";
                    showMessage(message, "Tips");
                    //System.Windows.MessageBox.Show("添加记录 成功", "提示", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    var message = "修改记录 失败";
                    showMessage(message, "Tips");
                    //System.Windows.MessageBox.Show("添加记录 失败", "提示", System.Windows.MessageBoxButton.OK);
                }

                //var msg = bol ? "成功" : "失败";
                //var msg1 = "修改记录" + msg;
                //showMessage(msg1, "Tips");
                //System.Windows.MessageBox.Show("修改记录 " + msg, "提示", System.Windows.MessageBoxButton.OK);
                reload_ItemsSource();
            }
            btnCancel_Click();
        }
        public void btnDelect_Click()
        {
            if (DataGridSelectedItem == null)
            {
                var msg = "请选择要删除的领域！";
                showMessage(msg, "Tips");
                //System.Windows.MessageBox.Show("请选择要删除的领域！");
                return;
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

            //var dialog = System.Windows.MessageBox.Show("是否删除该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            //if (dialog.Equals(System.Windows.MessageBoxResult.Yes))
            if(result)
            {
                var Domain = Create();

                var res = _domainSerive.DeleteService(Domain);
                if (res)
                {
                    var domainName = Domain.Name;
                    DomainDeleteEvent domainDeleteEvent = new DomainDeleteEvent() { Name = domainName};
                    _eventAggregator.Publish(domainDeleteEvent);
                    var message = "删除记录 成功";
                    showMessage(message, "Tips");
                    //System.Windows.MessageBox.Show("添加记录 成功", "提示", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    var message = "删除记录 成功";
                    showMessage(message, "Tips");
                    //System.Windows.MessageBox.Show("添加记录 失败", "提示", System.Windows.MessageBoxButton.OK);
                }

                //System.Windows.MessageBox.Show("删除记录 " + msg, "提示", System.Windows.MessageBoxButton.OK);

            }
            reload_ItemsSource();
            btnCancel_Click();
        }

        public void btnCancel_Click()
        {
            IdDomain = 0.ToString();
            Name = string.Empty;
            Description = string.Empty;
            reload_ItemsSource();
        }

        //  应用领域验证器
        private ValidationResult GetValidationResult(Domain domain)
        {
            var validator = new DomainValidator(_domainSerive);
            var result = validator.Validate(domain);
            return result;
        }

        // 查重判断
        public bool CheckDomainExistence(Domain domain)
        {

            var validator = new DomainValidator(_domainSerive);
            var Existence = validator.IsDuplicate(domain);
            if (Existence)
            {
                // 应用领域已存在
                return true;
            }

            // 应用领域不存在
            return false;
        }

        // 展示具体某条数据
        public bool show()
        {
            if (DataGridSelectedItem != null)
            {
                DomainSelectedItem = DataGridSelectedItem;
                IdDomain = DataGridSelectedItem.IdDomain.ToString();
                Name = DataGridSelectedItem.Name;
                Description = DataGridSelectedItem.Description;
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
