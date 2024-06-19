using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;
using FlaUI.Core.Definitions;

namespace MetBench_Test
{
    [TestClass]
    public class DomainDeleteTests
    {
        //测试用例1：用户选择删除一条已入库的应用领域信息。系统应弹出删除成功的提示信息。
        [TestMethod, Priority(1)]
        public void DomainDeleteTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //Arrange
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //Domain Management页面
                children[9].Click();
                Thread.Sleep(1000);

                //Acation
                //测试步骤

                //DataGrid数据网格
                var dataGrid_Domain = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_Domain")).AsDataGridView();
                var dataGrid_Domain_Rows = dataGrid_Domain.Rows;
                //删除按钮
                var button_Delete = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_DeleteDomain")).AsButton();

                //双击数据网格中Name为“test”的数据
                var dataGrid_Domain_Row = dataGrid_Domain_Rows[0];
                for (int i = 0; i < dataGrid_Domain_Rows.Length; i++)
                {
                    var row = dataGrid_Domain_Rows[i];
                    var name = row.Cells[1].Value;
                    if (name.Equals("test"))
                    {
                        dataGrid_Domain_Row = row;
                        break;
                    }
                }
                dataGrid_Domain_Row.DoubleClick();
                Thread.Sleep(1000);


                //点击删除按钮
                button_Delete.Click();
                Thread.Sleep(1000);

                //点击确定修改按钮
                var confirmModifyDialog = mainWindow.ModalWindows[0];
                var confirmModifyButton = confirmModifyDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("是(Y)"))).AsButton();  // 确认按钮的文本为"确认"
                confirmModifyButton.Click();
                Thread.Sleep(1000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "删除记录 成功";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }
        //测试用例2：用户选择删除一条未入库的应用领域信息。系统应弹出失败成功的提示信息。
        [TestMethod, Priority(2)]
        public void DomainDeleteTests_Test002()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //Arrange
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //Domain Management页面
                children[9].Click();
                Thread.Sleep(1000);

                //Acation
                //测试步骤
                //Description文本框
                var textBox_Description = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_Description")).AsTextBox();
                //Name文本框
                var textBox_Name = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_Name")).AsTextBox();
                var button_Delete = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_DeleteDomain")).AsButton();
                //1.在Name文本框输入“test”，在Description文本框输入“Description1”
                textBox_Name.Text = "test";
                textBox_Description.Text = "Description";

                Thread.Sleep(1000);
                //点击增加按钮
                button_Delete.Click();
                Thread.Sleep(1000);
                //点击确定修改按钮
                var confirmModifyDialog = mainWindow.ModalWindows[0];
                var confirmModifyButton = confirmModifyDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("是(Y)"))).AsButton();  // 确认按钮的文本为"确认"
                confirmModifyButton.Click();
                Thread.Sleep(1000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "删除记录 失败";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }
    }
}
