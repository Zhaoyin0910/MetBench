using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;
using FlaUI.Core.Definitions;
using FlaUI.Core.WindowsAPI;

namespace MetBench_Test
{
    [TestClass]
    public class MutationRelationshipResetTests
    {
        //测试用例1：用户点击重置，系统能否正确清除文本框信息。
        [TestMethod, Priority(1)]
        public void MutationRelationshipResetTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //进入MR Management页面
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                children[5].Click();
                Thread.Sleep(1000);

                // 双击数据网格第一行数据
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                var rows = dataGrid_MR.Rows;
                var lastRow = rows[1].Cells[2];
                if (lastRow != null)
                {
                    lastRow.DoubleClick();
                    Thread.Sleep(1000);
                }

                //点击重置按钮
                var ResetButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Reset")).AsButton();
                ResetButton.Click();

                //获取文本框的值
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                var DimensionOfInputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfInputPattern")).AsTextBox();
                var DimensionOfOutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfOutputPattern")).AsTextBox();
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                //断言
                try
                {
                    Assert.AreEqual(string.Empty, InputPatternTextBox.Text);
                    Assert.AreEqual(string.Empty, OutputPatternTextBox.Text);
                    Assert.AreEqual(string.Empty, DimensionOfInputPatternTextBox.Text);
                    Assert.AreEqual(string.Empty, DimensionOfOutputPatternTextBox.Text);
                    Assert.AreEqual(string.Empty, ApplicationNameTextBox.Text);

                }
                finally
                {
                    app.Close();
                }
            }
        }

    }
}
