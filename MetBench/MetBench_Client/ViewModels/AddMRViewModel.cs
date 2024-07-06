using CommunityToolkit.Mvvm.ComponentModel;
using FluentValidation.Results;
using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_DAL;
using MetBench_Domain;
using MetBench_IDAL;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Interfaces;
using ValidationResult = FluentValidation.Results.ValidationResult;


//using MR_Management_Platform_SqlServerRepository;
namespace MetBench_Client.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AddMRViewModel : ObservableObject, INavigationAware, INotifyPropertyChanged
    {
        public void OnNavigatedTo()
        {
        }
        public void OnNavigatedFrom()
        {
        }
        //管理服务
        private MetamorphicRelationSerive MetamorphicRelationSerive = new MetamorphicRelationSerive();
        private ApplicationRSerive ApplicationSerive = new ApplicationRSerive();
        private DomainSerive DomainSerive = new DomainSerive();

        //OrderOfMR_ComboBox的数据源
        public List<string> OrderOfMRList { get { return MultClass.GetOrderOfMR(); } }
        //RtType为枚举类型 分别为 数值 谓词 其他 
        //RepresentationType_ComboBox的数据源
        //public List<RtType> RtTypeList { get { return MultClass.GetRtTypeList(); } }
        public List<string> RtTypeList { get { return MultClass.GetRtTypeList(); } }

        //datagrid的数据源
        public ObservableCollection<MetamorphicRelations_QueryResultData> Data { get; set; }
        //dataSelectItem
        public MetamorphicRelations_QueryResultData MRSelectedItem { get; set; }

        //OrderOfMR_ComboBox的selectedIndex  1 二元关系 2 三元关系
        public int OrderOfMR_ComboBoxSelectedIndex { get; set; } = 0;
        //RepresentationType_ComboBox的selectedIndex  1 数值 2 谓词 3 其他
        public int RtType_ComboBoxSelectedIndex { get; set; } = 0;
        //IdMR属性
        public string IdMR { get; set; } = "0";
        //Description属性
        public string Description { get; set; } = string.Empty;
        //Context属性
        public string Context { get; set; } = string.Empty;
        //Constraint属性
        public string Constraint { get; set; } = string.Empty;
        //OrderOfMR属性
        public string CbOrderOfMR_SelectedValue { get; set; } = string.Empty;
        //InputPattern属性
        public string InputPattern { get; set; } = string.Empty;
        //OutputPattern属性
        public string OutputPattern { get; set; } = string.Empty;
        //DimensionOfInputPattern属性
        public string DimensionOfInputPattern { get; set; } = string.Empty;
        //DimensionOfOutputPattern属性
        public string DimensionOfOutputPattern { get; set; } = string.Empty;
        ////IdApplication属性
        //public string IdApplication { get; set; } = string.Empty;
        //ApplicationName属性
        public string ApplicationName { get; set; } = string.Empty;
        //IdDomain属性
        public int IdDomain { get; set; } = 0;
        //DomainNameList 记录下拉框被选中的选项内容集合
        public List<string> DomainNameList { get; set; } = new List<string>();
        //DomainName下拉框显示的内容
        public string DomainNames { get; set; } = string.Empty;
        //DomainNames下拉框的数据源
        public ObservableCollection<CheckBox> checkBoxes { get; set; }

        public AddMRViewModel()
        {
            reload_ItemsSource();
            InitCheckBoxes();
        }
        //显示下拉框中的checkbox
        public void InitCheckBoxes()
        {
            // 生成并添加Checkbox
            //checkbox数量由Domain数量决定

            //获取全部的Domain
            var domains = DomainSerive.showAllResult();
            var length = domains.Count;
            var checkBoxItems = new ObservableCollection<CheckBox>() { new CheckBox() { Content = "全选", IsChecked = false, IsEnabled = true } };
            //第一个为 全部
            //checkBoxItems.Add(new CheckBox() { Content = "全选", IsChecked = false, IsEnabled = true });
            //将全部的Domain的Name变成数据源
            for (var i = 0; i < length; i++)
            {
                var checkboxItem = new CheckBox() { Content = domains[i].Name, IsChecked = false, IsEnabled = true };
                checkBoxItems.Add(checkboxItem);
            }
            //最后一个为 其他
            checkBoxItems.Add(new CheckBox() { Content = "其他", IsChecked = false, IsEnabled = true });
            //清空ComboBox的ItemsSource
            var s = checkBoxItems[0].Content;
            checkBoxes = checkBoxItems;
        }

        private void reload_ItemsSource()
        {
            //两表联查结果    
            var datas = MetamorphicRelationSerive.showMultTwoTableResult();

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
            Data = datas;
        }
        private MetamorphicRelation Create()
        {
            //数据类型转换
            var idmr = 0;
            int.TryParse(IdMR, out idmr);
            var mr = new MetamorphicRelation();
            mr.IdMR = idmr;
            mr.Description = Description;
            mr.Context = Context;
            mr.Constraint = Constraint;
            mr.OrderOfMR = CbOrderOfMR_SelectedValue;
            mr.RepresentationType = (RtType)(RtType_ComboBoxSelectedIndex - 1);//减掉“全部类型”这一项
            mr.InputPattern = InputPattern;
            mr.OutputPattern = OutputPattern;
            mr.DimensionOfInputPattern = DimensionOfInputPattern;
            mr.DimensionOfOutputPattern = DimensionOfOutputPattern;
            mr.ApplicationName = ApplicationName;//输入的ApplictaionName之间要用:隔开

            #region
            //var idapplication = Aps_repository.Get(ApplicationName);
            //mr.Application = new Application() { IdApplication = idapplication, Name = ApplicationName };

            //if (DomainNameList.Count > 0)
            //{
            //    var domains = new ObservableCollection<Domain>();
            //    for (int i = 0; i < DomainNameList.Count; i++)
            //    {
            //        var iddomain = Domains_Repository.Get(DomainNameList[i]);

            //        var domain = new Domain() { IdDomain = iddomain, Name = DomainNameList[i] };
            //        domains.Add(domain);
            //    }
            //    var co = domains.Count;
            //    mr.Application.Domains = domains;
            //}
            #endregion
            return mr;
        }
        //MR验证器
        private ValidationResult GetValidationResult(MetamorphicRelation metamorphicRelation)
        {
            var validator = new MetamorphicRelationValidator();
            var result = validator.Validate(metamorphicRelation);
            return result;
        }
        //查重判断
        public bool CheckMRExistence(MetamorphicRelation mr)
        {
            var domainName = DomainNames;
            var validator = new MetamorphicRelationValidator();
            var Existence = validator.IsDuplicate(mr,domainName);
            if (Existence)
            {
                return true;
            }
            return false;
        }
        //取消 
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
            RtType_ComboBoxSelectedIndex = 0;
            ApplicationName = string.Empty;
            DomainNames = string.Empty;
            DomainNameList.Clear();
            MRSelectedItem = null;
            reload_ItemsSource();
        }
        //增加
        public void btnAdd_Click()
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
                System.Windows.MessageBox.Show(message, "提示", System.Windows.MessageBoxButton.OK);
                return;
            }
            if (CheckMRExistence(metamorphicRelation))
            {
                System.Windows.MessageBox.Show("该蜕变关系已存在!");
                return;
            }
            //将Latex转为Sympy格式的字符串
            Latextosympy latextosympy = new Latextosympy();
            metamorphicRelation.InputPatterntosympy = latextosympy.latextosympy(metamorphicRelation.InputPattern);
            metamorphicRelation.OutputPatterntosympy = latextosympy.latextosympy(metamorphicRelation.OutputPattern);
            //展示Latex渲染成的图片
            //1.将Latex渲染成图片
            string InputPatternimagepath = latextosympy.LatextoImage(metamorphicRelation.InputPattern);
            string OutputPatternimagepath = latextosympy.LatextoImage(metamorphicRelation.OutputPattern);
            //2.将图片转为字节数组
            metamorphicRelation.InputPatternImageData = latextosympy.ConvertImageToByteArray(InputPatternimagepath);
            metamorphicRelation.OutputPatternImageData = latextosympy.ConvertImageToByteArray(OutputPatternimagepath);
           
            string str = ApplicationName;
            string[] strarray1 = ApplicationName.Split(":");//将:处理
            string str1 = DomainNames;

            var applicationlist = new List<Application>();
            var domainlist = new List<Domain>();

            for (int i = 0; i < strarray1.Length; i++)
            {
                var id = ApplicationSerive.GetApplicationId(strarray1[i]);
                if (id > 0)
                {
                    var application = ApplicationSerive.GetApplication(id);
                    applicationlist.Add(application);
                }
                else
                {
                    //不存在数据表中
                    var application = new Application() { IdApplication = id, Name = strarray1[i] };
                    applicationlist.Add(application);
                }
            }

            if (str1 != string.Empty && str1 != "无")
            {
                string[] strarray2 = DomainNames.Split(":");//将:处理
                for (int i = 0; i < strarray2.Length; i++)
                {
                    var id = DomainSerive.GetDomainId(strarray2[i]);
                    if (id > 0)
                    {
                        var domain = DomainSerive.GetDomain(id);
                        domainlist.Add(domain);
                    }
                    else
                    {
                        var domain = new Domain() { IdDomain = id, Name = strarray2[i] };
                        domainlist.Add(domain);
                    }
                }
            }

            bool b = MetamorphicRelationSerive.AddService(metamorphicRelation, applicationlist, domainlist);
            if (b)
            {
                System.Windows.MessageBox.Show("添加记录 成功", "提示", System.Windows.MessageBoxButton.OK);
            }
            else
            {
                System.Windows.MessageBox.Show("添加记录 失败", "提示", System.Windows.MessageBoxButton.OK);
            }
            reload_ItemsSource();
            btnCancel_Click();

        }

        //修改
        public void btnModify_Click()
        {
            if (MRSelectedItem == null)
            {
                System.Windows.MessageBox.Show("请选择要修改的蜕变关系！","提示", System.Windows.MessageBoxButton.OK);
                return;
            }
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
                System.Windows.MessageBox.Show(message, "提示", System.Windows.MessageBoxButton.OK);
                return;
            }
            if (CheckMRExistence(metamorphicRelation))
            {
                System.Windows.MessageBox.Show("该蜕变关系已存在");
                return;
            }
            //将Latex转为Sympy格式的字符串
            Latextosympy latextosympy = new Latextosympy();
            metamorphicRelation.InputPatterntosympy = latextosympy.latextosympy(metamorphicRelation.InputPattern);
            metamorphicRelation.OutputPatterntosympy = latextosympy.latextosympy(metamorphicRelation.OutputPattern);
            //展示Latex渲染成的图片
            //1.将Latex渲染成图片
            string InputPatternimagepath = latextosympy.LatextoImage(metamorphicRelation.InputPattern);
            string OutputPatternimagepath = latextosympy.LatextoImage(metamorphicRelation.OutputPattern);
            //2.将图片转为字节数组
            metamorphicRelation.InputPatternImageData = latextosympy.ConvertImageToByteArray(InputPatternimagepath);
            metamorphicRelation.OutputPatternImageData = latextosympy.ConvertImageToByteArray(OutputPatternimagepath);
            string str = ApplicationName;
            string[] strarray1 = ApplicationName.Split(":");
            string str1 = DomainNames;

            var applicationlist = new List<Application>();
            var domainlist = new List<Domain>();

            for (int i = 0; i < strarray1.Length; i++)
            {
                var id = ApplicationSerive.GetApplicationId(strarray1[i]);
                if (id > 0)
                {
                    var application = ApplicationSerive.GetApplication(id);
                    applicationlist.Add(application);
                }
                else
                {
                    var application = new Application() { IdApplication = id, Name = strarray1[i] };
                    applicationlist.Add(application);
                }
            }
            if (str1 != string.Empty && str1 != "无")
            {
                string[] strarray2 = DomainNames.Split(":");
                for (int i = 0; i < strarray2.Length; i++)
                {
                    var id = DomainSerive.GetDomainId(strarray2[i]);
                    if (id > 0)
                    {
                        var domain = DomainSerive.GetDomain(id);
                        domainlist.Add(domain);
                    }
                    else
                    {
                        var domain = new Domain() { IdDomain = id, Name = strarray2[i] };
                        domainlist.Add(domain);
                    }
                }
            }
            var dialog = System.Windows.MessageBox.Show("是否修改该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            if (dialog.Equals(System.Windows.MessageBoxResult.Yes))
            {
                var result = MetamorphicRelationSerive.UpdateService(metamorphicRelation, applicationlist, domainlist);
                if (result)
                {

                    System.Windows.MessageBox.Show("修改记录 成功", "提示", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    System.Windows.MessageBox.Show("添加记录 失败", "提示", System.Windows.MessageBoxButton.OK);
                }

            }
            reload_ItemsSource();
            btnCancel_Click();

        }
        //删除
        public void btnDelect_Click()
        {
            if (MRSelectedItem == null)
            {
                System.Windows.MessageBox.Show("请选择要删除的蜕变关系！", "提示", System.Windows.MessageBoxButton.OK);
                return;
            }
            var metamorphicRelation = Create();
            var dialog = System.Windows.MessageBox.Show("是否删除该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            if (dialog.Equals(System.Windows.MessageBoxResult.Yes))
            {
                var result = MetamorphicRelationSerive.DeleteService(metamorphicRelation);
                var msg = result ? "成功" : "失败";
                System.Windows.MessageBox.Show("删除记录" + msg, "提示", System.Windows.MessageBoxButton.OK);
            }

            reload_ItemsSource();
            btnCancel_Click();
        }
        //当返回的为true 选择的是每一行的蜕变关系数据
        public bool show()
        {
            if (MRSelectedItem != null)
            {
                IdMR = MRSelectedItem.IdMR.ToString();
                Description = MRSelectedItem.Description;
                Context = MRSelectedItem.Context;
                Constraint = MRSelectedItem.Constraint;
                CbOrderOfMR_SelectedValue = MRSelectedItem.OrderOfMR;
                RtType_ComboBoxSelectedIndex = (int)MRSelectedItem.RepresentationType + 1;//往下移动一项
                InputPattern = MRSelectedItem.InputPattern;
                OutputPattern = MRSelectedItem.OutputPattern;
                DimensionOfInputPattern = MRSelectedItem.DimensionOfInputPattern;
                DimensionOfOutputPattern = MRSelectedItem.DimensionOfOutputPattern;
                ApplicationName = MRSelectedItem.ApplicationName; //只有一个ApplicationName 暂时满足一个Apllication的情况
                var domainname = DomainNames;
                if (domainname == "无")
                {
                    DomainNames = string.Empty;
                }
                else
                {
                    //有多个DomainName 进行换行显示 ，将\n 转换成 :
                    string originalString = MRSelectedItem.DomainName;
                    string modifiedString = originalString.Replace("\n", ":");//转换后的字符串
                    DomainNames = modifiedString;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
