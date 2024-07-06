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
    public class DomainModifyTests
    {
        //测试用例1：用户未完整输入必填信息，点击修改按钮，系统应弹出对应的非空提示信息。
        [TestMethod, Priority(1)]
        public void DomainModifyTests_Test001()
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
                var button_Modify = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyDomain")).AsButton();
                //1.在Description文本框输入“Description”
                textBox_Description.Text = "Description";

                Thread.Sleep(1000);
                //点击修改按钮
                button_Modify.Click();
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
                var expected = "Name不能为空";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }

        //测试用例2：用户修改一条已入库的应用程序信息。用户完整输入所有必填项信息，点击修改，系统应弹出修改成功的提示信息。
        [TestMethod, Priority(2)]
        public void DomainModifyTests_Test002()
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

                //DataGrid数据网格
                var dataGrid_Domain = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_Domain")).AsDataGridView();
                var dataGrid_Domain_Rows = dataGrid_Domain.Rows;
                //修改按钮
                var button_Modify = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyDomain")).AsButton();

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
                //Description文本框修改内容为“Description1”
                textBox_Description.Text = "Description1";

                //点击修改按钮
                button_Modify.Click();
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
                var expected = "修改记录 成功";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }

        //测试用例3：用户修改一条未入库的应用程序信息。用户完整输入所有必填项信息，点击修改，系统应弹出修改失败的提示信息。
        [TestMethod, Priority(3)]
        public void DomainModifyTests_Test003()
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
                //修改按钮
                var button_Modify = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyDomain")).AsButton();

                //Description文本框修改内容为“Description1”
                textBox_Name.Text = "test1";
                textBox_Description.Text = "Description2";

                //点击修改按钮
                button_Modify.Click();
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
                var expected = "修改记录 失败";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }
    }
}
