
using MetBench_Client.Views.Windows;
using MetBench_Domain;
using MetBench_IDAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace MetBench_Client.Services
{
    //已弃用
    public class TransParamsService
    {
        //整个Windows桌面程序的主窗体
        public static MainWindow mainWindow { get; set; }
        public static MetamorphicRelations_QueryResultData selectedmetamorphicRelation { get; set; }
        public static Page Newpage { get; set; }
        public static Page Oldpage { get; set; }
        //用于保存切换前的DisplayPage
        public static Page page { get; set; }
        //用于标记DisplayMRPage是否触发了Button_Click_1事件
        public static bool isClick { get; set; }
        public static bool isRun { get; set; }
        //执行蜕变关系自动化验证需要的
        public static Application runapplication { get; set; }
        public static MetamorphicRelations_QueryResultData runmetamorphicRelation { get; set; }
    }
}
