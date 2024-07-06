using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;
using FlaUI.Core.Definitions;
using System.IO;
using System.Reflection;
using FlaUI.Core.Input;
using System;

namespace MetBench_Test
{
    [TestClass]
    public class ApplicationDeleteTests
    {
        //测试用例1：户选择删除一条已入库的应用程序信息。系统应弹出删除成功的提示信息。
        [TestMethod, Priority(1)]
        public void ApplicationDeleteTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入Application Management页面
                children[7].Click();
                Thread.Sleep(1000);

                //Acation
                //测试步骤 
                //Application数据网格
                var dataGrid_Application = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("DataGrid_Application"))).AsDataGridView();
                var length = dataGrid_Application.Rows.Length;

                //删除按钮
                var button_Delete = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Delete")).AsButton();

                var dataGrid_Application_Row = dataGrid_Application.Rows[0];
                for (int i = 0; i < length; i++)
                {
                    var dataGridRow = dataGrid_Application.Rows[i];
                    var dataGridRow_Cells = dataGridRow.Cells;
                    var NameCell = dataGridRow_Cells[1];//Cell为Name
                    if (NameCell.Value.Equals("test"))
                    {
                        dataGrid_Application_Row = dataGrid_Application.Rows[i];
                    }
                }
                //双击Name为“test”的记录
                dataGrid_Application_Row.DoubleClick();
                Thread.Sleep(1000);


                //删除
                //点击删除按钮
                button_Delete.Click();
                Thread.Sleep(1000);
                //点击确定删除按钮
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
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
        //测试用例2；用户选择删除一条已入库的应用程序信息。系统应删除该关联的蜕变关系信息，并弹出删除成功的提示信息。
        [TestMethod, Priority(2)]
        public void ApplicationDeleteTests_Test002()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入Application Management页面
                children[7].Click();
                Thread.Sleep(1000);

                //Acation
                //测试步骤 
                //Application数据网格
                var dataGrid_Application = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("DataGrid_Application"))).AsDataGridView();
                var length = dataGrid_Application.Rows.Length;

                //删除按钮
                var button_Delete = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Delete")).AsButton();

                var dataGrid_Application_Row = dataGrid_Application.Rows[0];
                for (int i = 0; i < length; i++)
                {
                    var dataGridRow = dataGrid_Application.Rows[i];
                    var dataGridRow_Cells = dataGridRow.Cells;
                    var NameCell = dataGridRow_Cells[1];//Cell为Name
                    if (NameCell.Value.Equals("test1"))
                    {
                        dataGrid_Application_Row = dataGrid_Application.Rows[i];
                    }
                }
                //双击Name为“test1”的记录
                dataGrid_Application_Row.DoubleClick();
                Thread.Sleep(1000);


                //删除
                //点击删除按钮
                button_Delete.Click();
                Thread.Sleep(1000);
                //点击确定删除按钮
                var confirmDeleteDialog = mainWindow.ModalWindows[0];
                var confirmDeleteButton = confirmDeleteDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("是(Y)"))).AsButton();  // 确认按钮的文本为"确认"
                confirmDeleteButton.Click();
                Thread.Sleep(1000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                //var actual = message.Name;
                //var expected = "删除记录 成功";

                var confirmDialog = mainWindow.ModalWindows[0];


                var confirmButton = confirmDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("确定"))).AsButton();  // 确认按钮的文本为"确认"
                confirmButton.Click();
                //进入MR Display页面
                //for (int i = 0; i < children.Length; i++) 
                //{
                //    var a = children[i];
                //}
                children[3].Click();


                //Application数据网格
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("DataGrid_MR"))).AsDataGridView();
                var length1 = dataGrid_MR.Rows.Length;

                bool actual = false;
                for (int i = 0; i < length1; i++)
                {
                    var dataGridRow = dataGrid_MR.Rows[i];
                    var dataGridRow_Cells = dataGridRow.Cells;
                    var NameCell = dataGridRow_Cells[7];//Cell为ApplicationName
                    if (NameCell.Value.Equals("test1"))
                    {
                        actual = true; break;
                    }
                }


                //Assert
                //断言
                Assert.AreEqual(false, actual);
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
        //测试用例3：用户选择删除一条未入库的信息。系统应弹出失败成功的提示信息。
        [TestMethod, Priority(3)]
        public void ApplicationDeleteTests_Test003()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //A
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入Application Management页面
                children[7].Click();
                Thread.Sleep(1000);

                //Acation
                //测试步骤
                //ProgramingLanguage文本框
                var textBox_ProgramingLanguage = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ProgrammingLanguage")).AsTextBox();
                //Name文本框
                var textBox_Name = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_Name")).AsTextBox();
                //删除按钮
                var button_Delete = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Delete")).AsButton();
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
                //1.在Name文本框输入“test1”， ProgramingLanguage文本框输入“Python”
                textBox_Name.Text = "test1";
                textBox_ProgramingLanguage.Text = "Python";
                Thread.Sleep(1000);
                button_Button_UploadSoftw.Click();
                Thread.Sleep(1000);
                // 等待打开文件对话框出现
                var openFileDialog = mainWindow.ModalWindows[0];
                //获取上传文件的路径
                string solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                string parentDirectory = Directory.GetParent(solutionPath).FullName;
                //合并文件路径
                string filePath = Path.Combine(parentDirectory, "File", "test.py");
                //输入法设置成英文

                Thread.Sleep(2000);
                //使用键盘输入文件路径
                Keyboard.Type(filePath);
                Thread.Sleep(1000);
                //var textBox_fileName = fileNameInput.AsTextBox();
                //textBox_fileName.Enter(filePath);

                // 找到“打开”按钮
                var openButton = openFileDialog.FindFirstDescendant(cf => cf.ByText("打开(O)")
                );
                openButton.Click();
                Thread.Sleep(2000);
                //获取弹窗 确认上传
                var uploadDialog = mainWindow.ModalWindows[0];
                var confirmButton = uploadDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByText("是(Y)")));
                confirmButton.Click();
                Thread.Sleep(1000);
                //获取弹窗 成功上传
                var confirmDialog = mainWindow.ModalWindows[0];
                var confirmButton1 = confirmDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("确定"))).AsButton();  // 确认按钮的文本为"确认"
                confirmButton1.Click();
                Thread.Sleep(1000);
                //点击删除按钮
                button_Delete.Click();
                //点击确定删除按钮
                var confirmDeleteDialog = mainWindow.ModalWindows[0];
                var confirmDeleteButton = confirmDeleteDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("是(Y)"))).AsButton();  // 确认按钮的文本为"确认"
                confirmDeleteButton.Click();
                Thread.Sleep(1000);
                Thread.Sleep(1000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "删除记录 失败";

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
