using CommunityToolkit.Mvvm.ComponentModel;
using MetBench_BLL;
using MetBench_DAL;
using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;
namespace MetBench_Client.ViewModels
{
    public class DisplayMRViewModel : ObservableObject, INavigationAware
    {
        //实现接口 INavigationAware
        public void OnNavigatedFrom()
        {

        }
        //实现接口 INavigationAware
        public void OnNavigatedTo()
        {

        }
        public DisplayMRViewModel()
        {
            reload_ItemsSource();
        }
        //蜕变关系管理服务
        MetamorphicRelationSerive MetamorphicRelationSerive;
        //OrderOfMR_ComboBox的数据源
        public List<string> OrderOfMRList { get { return MultClass.GetOrderOfMR(); } }
        //RtType为枚举类型 分别为 数值 谓词 其他 
        //RepresentationType_ComboBox的数据源
        public List<string> RtTypeList { get { return MultClass.GetRtTypeList(); } }
        //datagrid的数据源
        public ObservableCollection<MetamorphicRelations_QueryResultData> Data { get; set; }
        //绑定datagrid的SelectedItem
        public MetamorphicRelations_QueryResultData MRSelectedItem { get; set; }
        //OrderOfMR_ComboBox的selectedIndex  1 二元关系 2 三元关系
        public int OrderOfMR_ComboBoxSelectedIndex { get; set; } = 0;
        //RepresentationType_ComboBox的selectedIndex  1 数值 2 谓词 3 其他
        public int RtType_ComboBoxSelectedIndex { get; set; } = 0;
        //OrderOfMR属性
        public string CbOrderOfMR_SelectedValue { get; set; } = string.Empty;
        //DimensionOfInputPattern属性
        public string DimensionOfInputPattern { get; set; } = string.Empty;
        //DimensionOfOutputPattern属性
        public string DimensionOfOutputPattern { get; set; } = string.Empty;
        //ApplicationName属性
        public string ApplicationName { get; set; } = string.Empty;
        //DomainName属性
        public string DomainName1 { get; set; } = string.Empty;
        private void reload_ItemsSource()
        {
            MetamorphicRelationSerive = new MetamorphicRelationSerive();
            var datas = MetamorphicRelationSerive.showMultTwoTableResult();
            for (int i = 0; i < datas.Count; i++)
            {
                var domainname = datas[i].DomainName;
                if (domainname == null)
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
        public void btnQuery_Click()
        {
            var mr = new MetamorphicRelation();
            // 确保 DimensionOfInputPattern 和 DimensionOfOutputPattern 非空时为正整数
            if (DimensionOfInputPattern!=string.Empty && !IsValidPositiveInteger(DimensionOfInputPattern))
            {
                System.Windows.MessageBox.Show("DimensionOfInputPattern必须为正整数。");
                return; // 中断后续操作
            }

            if (DimensionOfOutputPattern!=string.Empty && !IsValidPositiveInteger(DimensionOfOutputPattern))
            {
                System.Windows.MessageBox.Show("DimensionOfOutputPattern必须为正整数。");
                return; // 中断后续操作
            }
            if (CbOrderOfMR_SelectedValue == "全部关系")
            {
                mr.OrderOfMR = "";
            }
            else 
            {            
                mr.OrderOfMR = CbOrderOfMR_SelectedValue;
            }
            mr.RepresentationType = (RtType)(RtType_ComboBoxSelectedIndex - 1);
            mr.DimensionOfInputPattern = DimensionOfInputPattern;
            mr.DimensionOfOutputPattern = DimensionOfOutputPattern;
            mr.InputPattern = string.Empty;
            mr.OutputPattern = string.Empty;
            mr.ApplicationName = ApplicationName;
            var domianName = DomainName1;
            var datas = MetamorphicRelationSerive.QueryService(mr, domianName);
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
        // 辅助方法，检验是否为正整数
        private bool IsValidPositiveInteger(string input)
        {
            return  int.TryParse(input, out int result) && result > 0;
        }
    }
}
