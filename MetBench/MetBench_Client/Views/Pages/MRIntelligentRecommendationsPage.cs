using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Xps.Packaging;
using System.Xml.Linq;
using Wpf.Ui.Common.Interfaces;
using Point = System.Windows.Point;

namespace MetBench_Client.Views.Pages
{
    /// <summary>
    /// MRIntelligentRecommendationsPage.xaml 的交互逻辑
    /// </summary>
    public partial class MRIntelligentRecommendationsPage : INavigableView<ViewModels.MRIntelligentRecommendationsViewModel>
    {
        XpsDocument readerDoc;
        public string tempPdfPreAddress = Environment.CurrentDirectory + "\\tempPdfPre\\";

        public MRIntelligentRecommendationsPage(ViewModels.MRIntelligentRecommendationsViewModel viewModel)
        {
            //例子：获取Asserts中的图片文件
            string currentFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //Directory.GetParent(solutionPath).Parent.FullName;
            string parentFilePath = Directory.GetParent(currentFilePath).Parent.Parent.FullName;
            string pdfPath = Path.Combine(parentFilePath, "Assets", "example_sin_c.jpg");



            ViewModel = viewModel;
            ViewModel.Image = ViewModel.GetImage(pdfPath);
            transGroup = new TransformGroup();

            InitializeComponent();
        }
        //用于保存鼠标点击时的位置
        System.Windows.Point mousePosition = new System.Windows.Point();
        //鼠标点击时控件位置
        System.Windows.Point contentCtlPosition = new System.Windows.Point();
        public TransformGroup transGroup;

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            transGroup.Children.Add(new ScaleTransform());
            transGroup.Children.Add(new TranslateTransform());
            
            
            //ImgCtl.RenderTransform = transGroup;
            
        }

        private void ContentControl_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            Control ctl = sender as Control;
            System.Windows.Point point = e.GetPosition(ctl);
            //滚轮滚动时控制 放大的倍数,没有固定的值，可以根据需要修改。
            double scale = e.Delta * 0.002;

            ZoomImage(transGroup, point, scale);
        }


        //对控件进行缩放。
        private void ZoomImage(TransformGroup group, Point point, double scale)
        {
            Point pointToContent = group.Inverse.Transform(point);

            var a = group.Children.Count;
            ScaleTransform scaleT = group.Children[0] as ScaleTransform;

            if (scaleT.ScaleX + scale < 1) return;

            scaleT.ScaleX += scale;

            scaleT.ScaleY += scale;

            TranslateTransform translateT = group.Children[1] as TranslateTransform;

            translateT.X = -1 * ((pointToContent.X * scaleT.ScaleX) - point.X);

            translateT.Y = -1 * ((pointToContent.Y * scaleT.ScaleY) - point.Y);

        }

      


        private void ContentControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Control ctl = sender as Control;
            mousePosition = e.GetPosition(ctl);
            TranslateTransform tt = transGroup.Children[1] as TranslateTransform;

            contentCtlPosition = new Point(tt.X, tt.Y);
        }
        private void Button_Click_LookMR(object sender, RoutedEventArgs e) 
        {
            
        }
        private void Button_Click_LookApplication(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_LookDomain(object sender, RoutedEventArgs e)
        {

        }

        private void MasterScrollViewer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Control ctl = sender as Control;
                Point desPosition = e.GetPosition(ctl);

                TranslateTransform transform = transGroup.Children[1] as TranslateTransform;
                //控件移动位置的计算方式为：鼠标点击时位置+拖拽的长度(当前坐标-鼠标点击时的坐标).
                transform.X = contentCtlPosition.X + (desPosition.X - mousePosition.X);
                transform.Y = contentCtlPosition.Y + (desPosition.Y - mousePosition.Y);

            }
        }

        public ViewModels.MRIntelligentRecommendationsViewModel ViewModel
        {
            get;
        }
       

        
    }
}
