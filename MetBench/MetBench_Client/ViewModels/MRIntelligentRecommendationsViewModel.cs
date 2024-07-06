using CommunityToolkit.Mvvm.ComponentModel;
using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_IDAL;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common.Interfaces;

namespace MetBench_Client.ViewModels
{
    public class MRIntelligentRecommendationsViewModel : ObservableObject, INavigationAware
    {
        public MRIntelligentRecommendationsViewModel()
        {
        }
        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo()
        {

        }
        //智能推荐的蜕变关系
        public ObservableCollection<MRIntelligentRecommendations> Data{get;set;}
        //被测程序的AST
        public BitmapImage Image { get;set;}
        //绑定Code
        public byte[] Code { get; set; }
        //ProgrammingLanguage 属性
        public string ProgrammingLanguage { get; set; } = string.Empty;
        //上传的压缩文件名
        public string Codename { get; set; } = string.Empty;
        //显示的代码文件名
        public string CodeName { get; set; } = string.Empty;

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
                if (Codename != null)
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

        public void btn_MRIntelligentRecommendations()
        {
            ////例子：获取Asserts中的图片文件
            //string currentFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            ////Directory.GetParent(solutionPath).Parent.FullName;
            //string parentFilePath = Directory.GetParent(currentFilePath).Parent.Parent.FullName;
            //string ImagePath = Path.Combine(parentFilePath, "Assets", "example_sin_c.pdf");
            
            //Image = GetImage(ImagePath);
            //cos
            MRIntelligentRecommendations mr1 = new MRIntelligentRecommendations() { Program="cos-Java",Similarity="80.91%",InputPattern= "x_{21}=x_{11} + 3*\\pi", OutputPattern= "y_{21} = -y_{11}" };
            MRIntelligentRecommendations mr2 = new MRIntelligentRecommendations() { Program = "cos-Java", Similarity = "80.91%", InputPattern = "x_{21}=2*\\pi - x_{11}", OutputPattern = "y_{21} = y_{11}" };
            MRIntelligentRecommendations mr3 = new MRIntelligentRecommendations() { Program = "cos-Java", Similarity = "80.91%", InputPattern = "x_{21}=-x_{11}", OutputPattern = "y_{21} = y_{11}" };
            MRIntelligentRecommendations mr4 = new MRIntelligentRecommendations() { Program = "cos-Java", Similarity = "80.91%", InputPattern = "x_{21}=x_{11} + 2*\\pi", OutputPattern = "y_{21} = y_{11}" };
            MRIntelligentRecommendations mr5 = new MRIntelligentRecommendations() { Program = "cos-Java", Similarity = "80.91%", InputPattern = "x_{21}=-x_{11} - 3*\\pi", OutputPattern = "y_{21} = -y_{11}" };

            //tan
            MRIntelligentRecommendations mr6 = new MRIntelligentRecommendations() { Program = "tan-Java", Similarity = "74.61%", InputPattern = "x_{21} = x_{11} - 3*\\pi", OutputPattern = "y_{21} = y_{11}" };
            MRIntelligentRecommendations mr7 = new MRIntelligentRecommendations() { Program = "tan-Java", Similarity = "74.61%", InputPattern = "x_{21} = 3*\\pi - x_{11}", OutputPattern = "y_{21} = -y_{11}" };
            MRIntelligentRecommendations mr8 = new MRIntelligentRecommendations() { Program = "tan-Java", Similarity = "74.61%", InputPattern = "x_{21} = -x_{11} - 3*\\pi", OutputPattern = "y_{21} = -y_{11}" };
            MRIntelligentRecommendations mr9 = new MRIntelligentRecommendations() { Program = "tan-Java", Similarity = "74.61%", InputPattern = "x_{ 21} = \\pi - x_{ 11}", OutputPattern = "y_{ 21} = -y_{ 11}" };
            MRIntelligentRecommendations mr10 = new MRIntelligentRecommendations() { Program = "tan-Java", Similarity = "74.61%", InputPattern = "x_{ 21} = x_{ 11}+3 *\\pi", OutputPattern = "y_{ 21} = y_{ 11}" };

            //asin
            MRIntelligentRecommendations mr11 = new MRIntelligentRecommendations() { Program = "arcsin-Python", Similarity = "70.5%", InputPattern = "x_{21}=-x_{11}", OutputPattern = "y_{21} = -y_{11}" };
            MRIntelligentRecommendations mr12 = new MRIntelligentRecommendations() { Program = "arcsin-Python", Similarity = "70.5%", InputPattern = "x_{21}=x_{11}", OutputPattern = "y_{21} = y_{11}" };
            //sinH
            MRIntelligentRecommendations mr13 = new MRIntelligentRecommendations() { Program = "arcsinh-python", Similarity = "70.1%", InputPattern = "x_{21}=-x_{11}", OutputPattern = "y_{21} = -y_{11}" };
            MRIntelligentRecommendations mr14 = new MRIntelligentRecommendations() { Program = "arcsinh-python", Similarity = "70.1%", InputPattern = "x_{21}=x_{11}", OutputPattern = "y_{21} = y_{11}" };
            //acos
            MRIntelligentRecommendations mr15 = new MRIntelligentRecommendations() { Program = "arccos-python", Similarity = "69.02%", InputPattern = "x_{21}=x_{11}", OutputPattern = "y_{21} = y_{11}" };
            MRIntelligentRecommendations mr16 = new MRIntelligentRecommendations() { Program = "arccos-python", Similarity = "69.02%", InputPattern = "x_{21}=-x_{11}", OutputPattern = "y_{21} = \\pi - y_{11}" };

            //加入到链表中
            ObservableCollection<MRIntelligentRecommendations> mrs = new ObservableCollection<MRIntelligentRecommendations> { mr1, mr2, mr3, mr4,mr5,mr6,mr7,mr8,mr9,mr10,mr11,mr12 };
            Latextosympy latextosympy = new Latextosympy();
            for (int i = 0; i < mrs.Count; i++) 
            {
                string inputpattern = mrs[i].InputPattern;
                string outputpattern = mrs[i].OutputPattern;
                mrs[i].InputPatternImagepath = latextosympy.LatextoImage(inputpattern);
                mrs[i].OutputPatternImagepath = latextosympy.LatextoImage(outputpattern);
            }
            Data = mrs;
        }
    }
}
