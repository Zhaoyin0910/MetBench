using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;


namespace MetBench_Test
{
    [TestClass]
    public class ApplicationResetTests
    {
        //测试用例1：用户点击重置，系统能否正确清除文本框信息。
        [TestMethod, Priority(1)]
        public void ApplicationResetTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入Application Management页面
                children[7].Click();
                Thread.Sleep(1000);

                // 双击数据网格第一行数据
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_Application")).AsDataGridView();
                var rows = dataGrid_MR.Rows;
                var lastRow = rows[0].Cells[2];
                if (lastRow != null)
                {
                    lastRow.DoubleClick();
                    Thread.Sleep(1000);
                }

                //点击重置按钮
                var ResetButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddApplicatiopnReset")).AsButton();
                ResetButton.Click();

                //获取文本框的值
                var IdApplicationTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_IdaApplication")).AsTextBox();
                var NameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_Name")).AsTextBox();
                var DescriptionTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_Description")).AsTextBox();
                var ProgrammingLanguageTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ProgrammingLanguage")).AsTextBox();
                var LineOfCodeTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_LineOfCode")).AsTextBox();
                //断言
                try
                {

                    Assert.AreEqual("0", IdApplicationTextBox.Text);
                    Assert.AreEqual(string.Empty, NameTextBox.Text);
                    Assert.AreEqual(string.Empty, DescriptionTextBox.Text);
                    Assert.AreEqual(string.Empty, ProgrammingLanguageTextBox.Text);
                    Assert.AreEqual(string.Empty, LineOfCodeTextBox.Text);
               

                }
                finally
                {
                    app.Close();
                }
            }
        }
    }
}
