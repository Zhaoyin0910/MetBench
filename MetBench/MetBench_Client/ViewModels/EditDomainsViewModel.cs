using CommunityToolkit.Mvvm.ComponentModel;
using FluentValidation.Results;
using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_Domain;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;
//using MR_Management_Platform_SqlServerRepository;

namespace MetBench_Client.ViewModels
{
    public partial class EditDomainsViewModel : ObservableObject, INavigationAware
    {

        public void OnNavigatedTo()
        {
        }

        public void OnNavigatedFrom()
        {
        }
        //管理服务
        private DomainSerive DomainSerive = new DomainSerive();

        //datagrid的数据源
        public ObservableCollection<Domain> Data { get; set; }
        //绑定datagrid的SelectedItem
        public Domain DomainSelectedItem { get; set; }
        //IdDomain属性
        public string IdDomain { get; set; } = "0";
        //Name属性
        public string Name { get; set; } = string.Empty;
        //Description属性
        public string Description { get; set; } = string.Empty;

        private ValidationResult GetValidationResult(Domain domain)
        {
            var validator = new DomainValidator();
            var result = validator.Validate(domain);
            return result;
        }
        public EditDomainsViewModel()
        {
            reload_ItemsSource();
        }
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
                System.Windows.MessageBox.Show(message, "提示", System.Windows.MessageBoxButton.OK);
                return;
            }
            var bol = DomainSerive.AddService(Domain);
            var msg = bol ? "成功" : "失败";
            System.Windows.MessageBox.Show("添加记录 " + msg, "提示", System.Windows.MessageBoxButton.OK);
            reload_ItemsSource();
            btnCancel_Click();
        }
        public void btnModify_Click()
        {
            if (DomainSelectedItem == null)
            {
                System.Windows.MessageBox.Show("请选择要修改的领域！");
                return;
            }
            var dialog = System.Windows.MessageBox.Show("是否修改该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            if (dialog.Equals(System.Windows.MessageBoxResult.Yes))
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
                    System.Windows.MessageBox.Show(message, "提示", System.Windows.MessageBoxButton.OK);
                    return;
                }
                var bol = DomainSerive.UpdateService(Domain);
                var msg = bol ? "成功" : "失败";
                System.Windows.MessageBox.Show("修改记录 " + msg, "提示", System.Windows.MessageBoxButton.OK);
                reload_ItemsSource();
            }
            btnCancel_Click();
        }
        public void btnDelect_Click()
        {
            if (DomainSelectedItem == null)
            {
                System.Windows.MessageBox.Show("请选择要删除的领域！");
                return;
            }
            var dialog = System.Windows.MessageBox.Show("是否删除该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            if (dialog.Equals(System.Windows.MessageBoxResult.Yes))
            {
                var Domain = Create();

                var bol = DomainSerive.DeleteService(Domain);
                var msg = bol ? "成功" : "失败";
                System.Windows.MessageBox.Show("删除记录 " + msg, "提示", System.Windows.MessageBoxButton.OK);

            }
            reload_ItemsSource();
            btnCancel_Click();
        }
        public void btnCancel_Click()
        {
            IdDomain = 0.ToString();
            Name = string.Empty;
            Description = string.Empty;
            DomainSelectedItem = null;
            reload_ItemsSource();
        }

        public void reload_ItemsSource()
        {
            Data = DomainSerive.showAllResult();
        }
        public bool show()
        {
            if (DomainSelectedItem != null)
            {
                IdDomain = DomainSelectedItem.IdDomain.ToString();
                Name = DomainSelectedItem.Name;
                Description = DomainSelectedItem.Description;
                reload_ItemsSource();
                return true;

            }
            else
            {
                return false;
            }


        }
    }
}
