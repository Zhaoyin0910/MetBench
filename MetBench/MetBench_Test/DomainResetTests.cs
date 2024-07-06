using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;
using FlaUI.Core.Definitions;

namespace MetBench_Test
{
    /// <summary>
    /// DomainResetTests 的摘要说明
    /// </summary>
    [TestClass]
    public class DomainResetTests
    {
        // 测试用例1：用户点击重置，系统能否正确清除文本框信息。
        [TestMethod, Priority(1)]
        public void DomainResetTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //Domain Management页面
                children[9].Click();
                Thread.Sleep(1000);

                //Acation
                //测试步骤 
                //Domain数据网格
                var dataGrid_Domain = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("DataGrid_Domain"))).AsDataGridView();
                var length = dataGrid_Domain.Rows.Length;

                //IdDomain文本框
                var text_Domain = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_IdDomain")).AsTextBox();
                //重置按钮
                var button_Reset = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Reset"));

                var dataGrid_Domain_Row = dataGrid_Domain.Rows[0];

                //dataGrid的第一条记录
                dataGrid_Domain_Row.DoubleClick();
                Thread.Sleep(1000);
                //点击重置按钮
                button_Reset.Click();
                Thread.Sleep(1000);
                var actual = text_Domain.Text;
                var expected = "0";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
    }
}
