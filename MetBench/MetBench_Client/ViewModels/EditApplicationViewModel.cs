using CommunityToolkit.Mvvm.ComponentModel;
using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_DAL;
using MetBench_Domain;
using MetBench_IDAL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MetBench_Client.ViewModels
{
    public partial class EditApplicationViewModel : ObservableObject, INavigationAware
    {
        public void OnNavigatedTo()
        {
        }

        public void OnNavigatedFrom()
        {
        }
        private MetamorphicRelationSerive MetamorphicRelationSerive = new MetamorphicRelationSerive();
        private ApplicationRSerive ApplicationSerive = new ApplicationRSerive();
        private DomainSerive DomainSerive = new DomainSerive();

        //数据库仓库
        //MRs
        private IMetamorphicRelationRepository MRs_repository = new MetamorphicRelationRepository();
        //Apps
        private IApplicationRepository Apps_repository = new ApplicationRepository();
        //Domains
        private IDomainRepository Domains_repository = new DomainRepository();

        //IdApplication 属性
        public string IdApplication { get; set; } = "0";
        //Name 属性
        public string Name { get; set; } = string.Empty;
        //Description 属性
        public string Description { get; set; } = string.Empty;
        //ProgrammingLanguage 属性
        public string ProgrammingLanguage { get; set; } = string.Empty;
        //LineOfCode 属性
        public string LineOfCode { get; set; } = string.Empty;
        //上传的压缩文件名
        public string Codename { get; set; } = string.Empty;
        //显示的代码文件名
        public string CodeName { get; set; } = string.Empty;
        //测试用例程序
        public byte[] SourceTestCase { get; set; }
        //测试用例名(数据库)
        public string SourceTestCaseName { get; set; } = string.Empty;
        //测试用例名(前端显示)
        public string SourceTestCasename { get; set; } = string.Empty;
        //修改后
        ////Code 属性
        //public string Code { get; set; } = string.Empty;
        //SourceTestCase 属性
        //public string SourceTestCase { get; set; } = string.Empty;
        //DOI 属性
        public string DOI { get; set; } = string.Empty;
        //Url 属性
        public string Url { get; set; } = string.Empty;
        //绑定Code
        public byte[] Code { get; set; }
        //datagrid数据源
        public ObservableCollection<Application> Data { get; set; }
        //绑定datagrid的SelectedItem
        public Application AppliactionSelectedItem { get; set; }
        //DomainNameList
        public List<string> DomainNameList { get; set; } = new List<string>();

        //DomainName下拉框显示的内容
        public string DomainNames { get; set; } = string.Empty;
        //DomainNames下拉框的数据源
        public ObservableCollection<CheckBox> checkBoxes { get; set; }

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
            checkBoxes = checkBoxItems;
        }
        //构造函数
        public EditApplicationViewModel()
        {
            reload_ItemsSource();

        }
        //Application验证器
        private ValidationResult GetValidationResult(Application application)
        {
            var validator = new ApplicationValidator();
            var result = validator.Validate(application);
            return result;
        }
        private Application Create()
        {
            //数据类型转换
            var idapplication = 0;
            int.TryParse(IdApplication, out idapplication);
            var lineOfCode = 0;
            int.TryParse(LineOfCode, out lineOfCode);
            //var code = Encoding.UTF8.GetBytes(Code);
            //var sourceTestCase = Encoding.UTF8.GetBytes(SourceTestCase);

            var application = new Application();
            application.IdApplication = idapplication;
            application.Name = Name;
            application.Description = Description;
            application.ProgrammingLanguage = ProgrammingLanguage;
            application.LinesOfCode = lineOfCode;
            //修改前
            //application.Code = code;
            //application.SourceTestCase = sourceTestCase;


            //此处还需要修改 12月17日 11点35分
            //修改后
            //当Id为idapplication的application不存在于表中时
            var application1 = ApplicationSerive.GetApplication(application.IdApplication);
            //if (application1 != null)
            //{
            //    //不为null就加到application中
            //    if (application1.Code != null)
            //    {
            //        application.Code = application1.Code;
            //        application.CodeName = application1.CodeName;
            //    }
            //    if (application1.SourceTestCase!=null) 
            //    {
            //        application.SourceTestCase = application1.SourceTestCase;
            //    }
            //}
            application.Code = Code;
            application.CodeName = Codename;
            application.SourceTestCase = SourceTestCase;
            application.SourceTestCaseName = SourceTestCasename;
            application.DOI = DOI;
            application.Url = Url;
            application.DomainName = DomainNames;

            //修改后
            //当Id为idapplication的application不存在于表中时
            //var application1 = ApplicationSerive.GetApplication(idapplication);
            if (application1 != null)
            {
                if (application1.MetamorphicRelationId != null)
                {
                    application.MetamorphicRelationId = application1.MetamorphicRelationId;
                }
            }

            return application;
        }
        private void reload_ItemsSource()
        {
            var datas = ApplicationSerive.GetApplications();

            for (int i = 0; i < datas.Count; i++)
            {
                var domainname = datas[i].DomainName;
                if (domainname == null|| domainname == string.Empty)
                {
                    datas[i].DomainName = "无";
                }
                //多个DomainName进行换行处理 \n
                else
                {
                    string[] strarray = domainname.Split(":");
                    if (strarray.Length > 1)
                    {
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
            //InitCheckBoxes();
        }
        //取消 
        public void btnCancel_Click()
        {
            IdApplication = 0.ToString();
            Name = string.Empty;
            Description = string.Empty;
            ProgrammingLanguage = string.Empty;
            LineOfCode = string.Empty;
            Codename = string.Empty;
            AppliactionSelectedItem = null;
            //SourceTestCase = string.Empty;
            DOI = string.Empty;
            Url = string.Empty;
            DomainNames = string.Empty;
            DomainNameList.Clear();
            reload_ItemsSource();
        }
        //增加
        public void btnAdd_Click()
        {

            var application = Create();
            string str = DomainNames;

            
            var metamorphicRelationlist = new List<MetamorphicRelation>();
            var domainlist = new List<Domain>();
            if (application.MetamorphicRelationId == null && application.MetamorphicRelationId == string.Empty)
            {
                string str1 = application.MetamorphicRelationId;
                string[] strarray2 = str1.Split(":");
              
                    for (int i = 0; i < strarray2.Length; i++)
                    {
                        var id = int.Parse(strarray2[i]);
                        var metamorphicRelation = MetamorphicRelationSerive.GetMRById(id);
                        if (metamorphicRelation != null)
                        {

                            metamorphicRelationlist.Add(metamorphicRelation);
                        }
                        else
                        {
                            metamorphicRelation = new MetamorphicRelation() { IdMR = id };
                            metamorphicRelationlist.Add(metamorphicRelation);
                        }
                    }
                
            }



            if (str != string.Empty && str != null)
            {
                string[] strarray1 = str.Split(":");
                for (int i = 0; i < strarray1.Length; i++)
                {
                    var id = DomainSerive.GetDomainId(strarray1[i]);
                    if (id > 0)
                    {
                        var domain = DomainSerive.GetDomain(id);
                        domainlist.Add(domain);
                    }
                    else
                    {
                        var domain = new Domain() { IdDomain = id, Name = strarray1[i] };
                        domainlist.Add(domain);
                    }
                }
            }


            //var application = Create();
            var validationResult = GetValidationResult(application);
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
            var result = ApplicationSerive.AddService(application, metamorphicRelationlist, domainlist);
            if (result)
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
            if (AppliactionSelectedItem == null)
            {
                System.Windows.MessageBox.Show("请选择要修改的应用程序！", "提示", System.Windows.MessageBoxButton.OK);
                return;
            }
            var dialog = System.Windows.MessageBox.Show("是否修改该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            if (dialog.Equals(System.Windows.MessageBoxResult.Yes))
            {
                var application = Create();
                string str = DomainNames;
               
                var metamorphicRelationlist = new List<MetamorphicRelation>();
                var domainlist = new List<Domain>();

                string str1 = application.MetamorphicRelationId;
                if (str1 != string.Empty && str1 != null)
                {
                    string[] strarray2 = str1.Split(":");
                    if (str1 != string.Empty)
                    {
                        for (int i = 0; i < strarray2.Length; i++)
                        {
                            //当为空字符串跳过
                            if (strarray2[i]==string.Empty) 
                            {
                                continue;
                            }
                            var id = int.Parse(strarray2[i]);
                            var metamorphicRelation = MetamorphicRelationSerive.GetMRById(id);
                            if (metamorphicRelation != null)
                            {

                                metamorphicRelationlist.Add(metamorphicRelation);
                            }
                            else
                            {
                                metamorphicRelation = new MetamorphicRelation() { IdMR = id };
                                metamorphicRelationlist.Add(metamorphicRelation);
                            }
                        }
                    }
                }

                if (str != string.Empty)
                {
                    string[] strarray1 = str.Split(":");
                    for (int i = 0; i < strarray1.Length; i++)
                    {
                        var id = DomainSerive.GetDomainId(strarray1[i]);
                        if (id > 0)
                        {
                            var domain = DomainSerive.GetDomain(id);
                            domainlist.Add(domain);
                        }
                        else
                        {
                            var domain = new Domain() { IdDomain = id, Name = strarray1[i] };
                            domainlist.Add(domain);
                        }
                    }
                }

                var validationResult = GetValidationResult(application);
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

                var result = ApplicationSerive.UpdateService(application, metamorphicRelationlist, domainlist);
                if (result)
                {
                    System.Windows.MessageBox.Show("修改记录 成功", "提示", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    System.Windows.MessageBox.Show("修改记录 失败", "提示", System.Windows.MessageBoxButton.OK);
                }

                reload_ItemsSource();
            }
            btnCancel_Click();
        }
        //删除
        public void btnDelect_Click()
        {
            if (AppliactionSelectedItem == null)
            {
                System.Windows.MessageBox.Show("请选择要删除的应用程序！", "提示", System.Windows.MessageBoxButton.OK);
                return;
            }
            var dialog = System.Windows.MessageBox.Show("是否删除该记录?", "Alert", System.Windows.MessageBoxButton.YesNo);
            if (dialog.Equals(System.Windows.MessageBoxResult.Yes))
            {
                var application = Create();
                var result = ApplicationSerive.DeleteService(application);
                var msg = result ? "成功" : "失败";
                System.Windows.MessageBox.Show("删除记录" + msg, "提示", System.Windows.MessageBoxButton.OK);
            }
            reload_ItemsSource();
            btnCancel_Click();
        }
        //当返回的为true 选择的是每一行的应用程序数据
        public bool show()
        {

            if (AppliactionSelectedItem != null)
            {
                IdApplication = AppliactionSelectedItem.IdApplication.ToString();
                Name = AppliactionSelectedItem.Name;
                Description = AppliactionSelectedItem.Description;
                ProgrammingLanguage = AppliactionSelectedItem.ProgrammingLanguage;
                LineOfCode = AppliactionSelectedItem.LinesOfCode.ToString();
                if (AppliactionSelectedItem.CodeName != null)
                {
                    Codename = AppliactionSelectedItem.CodeName.ToString();
                }
                if (AppliactionSelectedItem.Code != null) 
                {
                    Code = AppliactionSelectedItem.Code;
                }
               
                DOI = AppliactionSelectedItem.DOI;
                Url = AppliactionSelectedItem.Url;
                //DomainNames = AppliactionSelectedItem.DomainName;

                var domainname = DomainNames;
                if (domainname == "无")
                {
                    DomainNames = string.Empty;
                }
                else
                {
                    //有多个DomainName 进行换行显示 ，将\n 转换成 :
                    string originalString = AppliactionSelectedItem.DomainName;
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
        //解压文件并导出至系统缓存目录的metlib文件夹下
        public void btnExtractCode_Click()
        {
            FileCompressionAndStorageUtility fileCompressionAndStorageUtility = new FileCompressionAndStorageUtility();
            var application = Create();
            if (AppliactionSelectedItem == null)
            {
                System.Windows.MessageBox.Show("请选择应用程序！", "提示", System.Windows.MessageBoxButton.OK);
                return;
            }
            fileCompressionAndStorageUtility.ExtractAllZipFilesFromDatabase(application);
            //获取代码的文件名
           // string Codefilename = AppliactionSelectedItem.CodeName.ToString();
        }

        //压缩文件并上传至数据库
        public void btnAddCode_Click()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框的标题
            openFileDialog.Title = "选择要上传的文件";

            // 设置对话框的初始目录（可选）
            openFileDialog.InitialDirectory = "C:\\";

            // 设置对话框允许选择的文件类型（可选）
            openFileDialog.Filter = "所有文件 (*.*)|*.*";
            // 打开对话框
            bool? result = openFileDialog.ShowDialog();

            // 检查用户是否选择了文件
            if (result == true)
            {
                // 获取用户选择的文件路径
                string filePath = openFileDialog.FileName;

                // 生成 ZipCode获取文件名
                string CodeFileName = Path.GetFileName(filePath);

                if (!IsValidFileFormat(CodeFileName))
                {
                    System.Windows.MessageBox.Show("请上传Python、Java、C/C++文件或zip压缩文件", "提示", System.Windows.MessageBoxButton.OK);
                    return;
                }
                FileCompressionAndStorageUtility fileCompressionAndStorageUtility = new FileCompressionAndStorageUtility();
                //压缩文件并上传至数据库
                fileCompressionAndStorageUtility.ProcessFileData(filePath, CodeFileName);
                Code = fileCompressionAndStorageUtility.GetFileData();
                Codename = CodeFileName;
                if (Codename!=null)
                {
                    string fileExtension = Path.GetExtension(Codename);

                    if (fileExtension.Equals(".py"))
                    {
                        ProgrammingLanguage = "Python";
                    }
                    else if (fileExtension.Equals(".java"))
                    {
                        ProgrammingLanguage = "Java";
                    }
                    else if (fileExtension.Equals(".c"))
                    {
                        ProgrammingLanguage = "C";
                    }
                }
            }
        }
        public void btnAddSourceTestCase_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框的标题
            openFileDialog.Title = "选择要上传的文件";

            // 设置对话框的初始目录（可选）
            openFileDialog.InitialDirectory = "C:\\";

            // 设置对话框允许选择的文件类型（可选）
            openFileDialog.Filter = "所有文件 (*.*)|*.*";

            // 打开对话框
            bool? result = openFileDialog.ShowDialog();

            // 检查用户是否选择了文件
            if (result == true)
            {
                // 获取用户选择的文件路径
                string filePath = openFileDialog.FileName;

                // 生成 ZipCode获取文件名
                string CodeFileName = Path.GetFileName(filePath);

                FileCompressionAndStorageUtility fileCompressionAndStorageUtility = new FileCompressionAndStorageUtility();
                //压缩文件
                fileCompressionAndStorageUtility.ProcessFileData(filePath, CodeFileName);
                SourceTestCase = fileCompressionAndStorageUtility.GetFileData();
                SourceTestCasename = CodeFileName;
            }
        }
        //撤回上传SourceTestCaseBack
        public void btnSourceTestCaseBack_Click()
        {
            if(SourceTestCase != null)
            {
                System.Windows.MessageBox.Show("撤销上传成功");
                SourceTestCase = null;
                SourceTestCasename = string.Empty;
            }  
        }
        //撤回上传
        public void btnBack_Click()
        {
            if (Code != null)
            {
                System.Windows.MessageBox.Show("撤销上传成功");
                Code = null;
                Codename = string.Empty;
                ProgrammingLanguage = string.Empty;
            }
        }
        public bool IsValidFileFormat(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                // 文件名为空或null，视为格式非法
                return false;
            }

            string fileExtension = Path.GetExtension(fileName);

            // 支持的文件格式列表
            string[] allowedExtensions = { ".py", ".java", ".c", ".cpp", ".zip" };

            // 检查文件扩展名是否在允许的列表中
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
