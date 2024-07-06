using MetBench_BLL;
using MetBench_Client.Helpers;
using MetBench_Client.Services;
using MetBench_DAL;
using MetBench_Domain;
using MetBench_IDAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Common.Interfaces;
using Application = MetBench_Domain.Application;

namespace MetBench_Client.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddMRPage.xaml
    /// </summary>
    public partial class AddMRPage : INavigableView<ViewModels.AddMRViewModel>
    {
        private IDomainRepository _domainRepository = new DomainRepository();
        private MetamorphicRelationSerive MetamorphicRelationSerive = new MetamorphicRelationSerive();
        private ApplicationRSerive ApplicationSerive = new ApplicationRSerive();
        private DomainSerive DomainSerive = new DomainSerive();

        //记录全选checkbox状态
        private bool chooseAllState;

        //保存Textbox的内容
        private string originalText;
        public ViewModels.AddMRViewModel ViewModel
        {
            get;
        }

        public MetamorphicRelation Relation { get; set; }
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //    // 页面渲染时的处理
            var oldpage = TransParamsService.page;
            var newpage = TransParamsService.Newpage;
            var answer = TransParamsService.isClick;
            if (oldpage != null && oldpage.Title == "DisplayMRPage" && newpage.Title == "AddMRPage" && answer)
            {
                var page = (DisplayMRPage)TransParamsService.page;
                var metamorphicRelation_QueryResultData = (MetamorphicRelations_QueryResultData)page.dataGrid.SelectedItem;
                ViewModel.MRSelectedItem = metamorphicRelation_QueryResultData;
                ViewModel.show();
                //清除TransParamsService.page
                TransParamsService.page = null;
                //重置TransParamsService.isClick
                TransParamsService.isClick = false;
                //清除ViewModel.MRSelectedItem 
                ViewModel.MRSelectedItem = null;
            }
        }

        public AddMRPage(ViewModels.AddMRViewModel viewModel)
        {

            ViewModel = viewModel;
            InitializeComponent();
        }
        //将DataGrid的数据映射到文本框中
        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bool b = ViewModel.show();
            //当双击触发的事件是其他的Item，则不会进行展示

            //当双击触发的事件是其他的Item，则不会进行展示
            if (b)
            {
                ViewModel.DomainNameList.Clear();
                var applications = new ObservableCollection<Application>();
                //DataGrid显示的记录是只显示一个应用程序名
                var metamorphicRelationServer = new MetamorphicRelationSerive();
                string str = ViewModel.MRSelectedItem.ApplicationName; //DataGrid显示的记录是只显示一个应用程序名
                var domainNames = ViewModel.DomainNames;
                var applicationname = ViewModel.MRSelectedItem.ApplicationName;
                var domains = new ObservableCollection<Domain>();
                if (domainNames != "无" && domainNames != null)
                {
                    string[] strarray1 = domainNames.Split(':');
                    for (int i = 0; i < strarray1.Length; i++)
                    {
                        var st = strarray1[i];
                        var id = DomainSerive.GetDomainId(strarray1[i]);
                        var domain = DomainSerive.GetDomain(id);
                        //当domain不存在时不加入到domains中
                        if (domain != null)
                        {
                            domains.Add(domain);
                        }
                    }

                    foreach (var domain in domains)
                    {

                        foreach (var c in DomainName_Cbox.Items)
                        {
                            //判断是否为checkbox类型
                            if (c is CheckBox)
                            {
                                CheckBox checkBox = (CheckBox)c;
                                var n = checkBox.IsChecked;
                                if ((checkBox.Content.ToString() == domain.Name))
                                {
                                    checkBox.IsChecked = true;
                                }
                            }
                        }
                        ViewModel.DomainNameList.Add(domain.Name);
                    }
                    var str1 = "";
                    for (var i = 0; i < ViewModel.DomainNameList.Count; i++)
                    {
                        if (ViewModel.DomainNameList[i] != "全选")
                        {
                            str1 += ViewModel.DomainNameList[i];
                            if (i < ViewModel.DomainNameList.Count - 1)
                            {
                                str1 += ":";
                            }
                        }
                    }
                    ViewModel.DomainNames = str1.ToString();
                }
                else
                {
                    ViewModel.DomainNames = "";
                    ViewModel.DomainNameList.Clear();

                }
            }
        }
        private void comboBox_ItemSourceInit()
        {
            // 生成并添加Checkbox
            //checkbox数量由Domain数量决定

            //获取全部的Domain
            var domains = _domainRepository.GetAll();
            var length = domains.Count;
            var checkBoxItems = new List<CheckBox>();
            //第一个为 全部
            checkBoxItems.Add(new CheckBox() { Content = "全选", IsChecked = false, IsEnabled = true });
            //将全部的Domain的Name变成数据源
            for (var i = 0; i < length; i++)
            {
                var checkboxItem = new CheckBox() { Content = domains[i].Name, IsChecked = false, IsEnabled = true };
                checkBoxItems.Add(checkboxItem);
            }
            //最后一个为 其他
            checkBoxItems.Add(new CheckBox() { Content = "其他", IsChecked = false, IsEnabled = true });
            //清空ComboBox的ItemsSource
            var items = DomainName_Cbox.Items;
            DomainName_Cbox.ItemsSource = checkBoxItems;
        }
        private void checkBox_domian_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            //
            var checkbox = (CheckBox)sender;
            //当选择了全部就进行全选
            var content = checkbox.Content.ToString();
            if (content == "全选")
            {
                for (int i = 0; i < DomainName_Cbox.Items.Count; i++)
                {
                    var c = DomainName_Cbox.Items[i];
                    if (c is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)c;
                        //除了其他全部都要选择
                        var selectcontent = checkBox.Content.ToString();
                        if (!ViewModel.DomainNameList.Contains(selectcontent) && selectcontent != "其他" && selectcontent != "全选")
                        {
                            checkBox.IsChecked = true;
                            var s = checkBox.Content;
                            ViewModel.DomainNameList.Add(checkBox.Content.ToString());//加到里面
                        }

                    }
                }
            }
            else if (content == "其他")
            {
                var mainwindow = TransParamsService.mainWindow;
                var Navigation = mainwindow.RootNavigation;
                var PageType = typeof(Views.Pages.EditDomainsPage);
                Navigation.Navigate(PageType);
                checkbox.IsChecked = false;
            }
            var a = (CheckBox)DomainName_Cbox.Items[0];
            if (a.IsChecked != true && content != "其他" && content != "全部")
            {
                checkbox.IsChecked = true;
                ViewModel.DomainNameList.Add(checkbox.Content.ToString());//加到里面
            }

            DomainName_Cbox.SelectedValue = ViewModel.DomainNameList;

            var str = "";
            for (var i = 0; i < ViewModel.DomainNameList.Count; i++)
            {
                if (ViewModel.DomainNameList[i] != "全选")
                {
                    //str += ViewModel.DomainNameList[i] + " ";
                    str += ViewModel.DomainNameList[i];
                    if (i < ViewModel.DomainNameList.Count - 1)
                    {
                        str += ":";
                    }
                }
            }
            ViewModel.DomainNames = str.ToString();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            //渲染页面时触发的事件
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // 取消选择全部
            var checkbox = (CheckBox)sender;
            //当选择了全部就进行全选
            var content = checkbox.Content.ToString();
            if (content == "全选")
            {
                foreach (var c1 in DomainName_Cbox.Items)
                {
                    if (c1 is CheckBox)
                    {
                        CheckBox checkBox1 = (CheckBox)c1;
                        if (checkBox1.Content.ToString() == "其他") { continue; }
                        else
                        {
                            if (ViewModel.DomainNameList.Contains(checkBox1.Content.ToString()))
                            {

                                checkBox1.IsChecked = false;
                                ViewModel.DomainNameList.Remove(checkBox1.Content.ToString());

                            }
                            DomainName_Cbox.SelectedValue = ViewModel.DomainNameList;
                        }
                    }
                }

                ViewModel.DomainNames = "";

            }
            else
            {
                checkbox.IsChecked = false;
                ViewModel.DomainNameList.Remove(checkbox.Content.ToString());
                DomainName_Cbox.SelectedValue = ViewModel.DomainNameList;
                var str1 = "";
                for (var i = 0; i < ViewModel.DomainNameList.Count; i++)
                {
                    if (ViewModel.DomainNameList[i] != "全选")
                    {
                        str1 += ViewModel.DomainNameList[i];
                        if (i < ViewModel.DomainNameList.Count - 1)
                        {
                            str1 += ":";
                        }
                    }
                }
                ViewModel.DomainNames = str1.ToString();

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DomainName_Cbox_DropDownOpened(object sender, EventArgs e)
        {

            List<string> list = new List<string>();
            ViewModel.InitCheckBoxes();
            var domains = new ObservableCollection<Domain>();
            var domainNames = ViewModel.DomainNameList;
            for (int i = 0; i < domainNames.Count; i++)
            {
                if (domainNames[i] == "全选")
                {
                    continue;
                }
                var id = DomainSerive.GetDomainId(domainNames[i]);
                var domain = DomainSerive.GetDomain(id);
                //当domain不存在时不加入到domains中
                if (domain != null)
                {
                    domains.Add(domain);
                }
            }

            foreach (var domain in domains)
            {
                foreach (var c in DomainName_Cbox.Items)
                {
                    //判断是否为checkbox类型
                    if (c is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)c;
                        var n = checkBox.IsChecked;
                        if ((checkBox.Content.ToString() == domain.Name))
                        {
                            checkBox.IsChecked = true;
                        }
                    }

                }
                bool exist = ViewModel.DomainNameList.Contains(domain.Name);
                if (exist)
                {
                    continue;
                }
                ViewModel.DomainNameList.Add(domain.Name);
            }
            var count = ViewModel.DomainNameList.Count;
            var str1 = "";
            for (var i = 0; i < ViewModel.DomainNameList.Count; i++)
            {
                if (ViewModel.DomainNameList[i] != "全选")
                {
                    str1 += ViewModel.DomainNameList[i];
                    if (i < ViewModel.DomainNameList.Count - 1)
                    {
                        str1 += ":";
                    }
                }
            }
            ViewModel.DomainNames = str1.ToString();
        }
        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

            e.Row.Header = e.Row.GetIndex() + 1;

        }
        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainwindow = TransParamsService.mainWindow;
            var Navigation = mainwindow.RootNavigation;

            ApplicationRSerive applicationSerive = new ApplicationRSerive();
            var applicationName = ViewModel.MRSelectedItem.ApplicationName;
            var idapplication = applicationSerive.GetApplicationId(applicationName);
            var runapplication = applicationSerive.GetApplication(idapplication);
            //传递应用程序
            TransParamsService.runapplication = runapplication;
            MetamorphicRelationSerive metamorphicRelationSerive = new MetamorphicRelationSerive();
            TransParamsService.runmetamorphicRelation = ViewModel.MRSelectedItem;

            //编辑跳转按钮被点击
            TransParamsService.isRun = true;
            var PageType = typeof(Views.Pages.AutoMRPage);
            Navigation.Navigate(PageType);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
           
           
            textBox.TextWrapping = TextWrapping.Wrap;
            textBox.Height = double.NaN; // Set the height to auto-expand
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto; // Show vertical scrollbar if needed
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
 
            textBox.TextWrapping = TextWrapping.NoWrap; // Restore original text wrapping
            textBox.Height = double.NaN; // Set the height back to auto
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden; // Hide vertical scrollbar
        }
    }
}