using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;

namespace MetBench_Test
{
    [TestClass]
    public class DomainAddTests
    {
        //测试用例1：用户未完整输入必填信息，点击增加按钮，系统应弹出对应的非空提示信息。
        [TestMethod, Priority(1)]
        public void DomainAddTests_Test001()
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
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddDomain")).AsButton();
                //1.在Description文本框输入“Description2”
                textBox_Description.Text = "Description2";

                Thread.Sleep(1000);
                //点击增加按钮
                button_Add.Click();
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
        //测试用例2：用户增加一条未入库的应用领域信息。用户完整输入所有必填项信息，点击添加，系统应弹出增加成功的提示信息。
        [TestMethod, Priority(2)]
        public void DomainAddTests_Test002()
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
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddDomain")).AsButton();
                //1.在Name文本框输入“test”，在Description文本框输入“Description2”
                textBox_Name.Text = "test";
                textBox_Description.Text = "Description";

                Thread.Sleep(1000);
                //点击增加按钮
                button_Add.Click();
                Thread.Sleep(1000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "添加记录 成功";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }
        //测试用例3：用户增加一条已入库的应用领域信息。用户双击数据网格中的一条数据，并点击添加，系统应弹出增加失败的提示信息。
        [TestMethod, Priority(3)]
        public void DomainAddTests_Test003()
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
                //增加按钮
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddDomain")).AsButton();

                //双击数据网格中Name为“test”的数据
                var dataGrid_Domain_Row = dataGrid_Domain_Rows[0];
                for (int i = 0; i < dataGrid_Domain_Rows.Length; i++)
                {
                    var row = dataGrid_Domain_Rows[i];
                    var name = row.Cells[1];
                    if (name.Equals("test1"))
                    {
                        dataGrid_Domain_Row = row;
                        break;
                    }
                }
                dataGrid_Domain_Row.DoubleClick();
                Thread.Sleep(1000);

                //点击增加按钮
                button_Add.Click();
                Thread.Sleep(1000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "添加记录 失败";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }
        //测试用例4：用户增加一条应用领域信息。用户完整输入所有必填项信息并且输入的信息与某一条已入库的信息除Id信息不同外，其余信息，点击添加，系统应弹出增加失败的提示信息。
        [TestMethod, Priority(4)]
        public void DomainAddTests_Test004()
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
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddDomain")).AsButton();
                //1.在Name文本框输入“test”,Description文本框输入“Description”
                textBox_Name.Text = "test";
                textBox_Description.Text = "Description";

                Thread.Sleep(1000);
                //点击增加按钮
                button_Add.Click();
                Thread.Sleep(1000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "添加记录 失败";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }

    }
}
