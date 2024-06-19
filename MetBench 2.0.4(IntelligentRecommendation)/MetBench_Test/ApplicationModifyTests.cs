using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;
using System.IO;
using System.Reflection;
using FlaUI.Core.Input;
using FlaUI.Core.Definitions;

namespace MetBench_Test
{
    [TestClass]
    public class ApplicationModifyTests
    {
        //测试用例1：用户未完整输入必填信息，点击修改按钮，系统应弹出对应的非空提示信息。
        [TestMethod, Priority(1)]
        public void ApplicationModifyTests_Test001()
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
                //ProgramingLanguage文本框
                var textBox_ProgramingLanguage = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ProgrammingLanguage")).AsTextBox();

                //修改按钮
                var button_Modify = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Modify")).AsButton();
                //SoftwareUnderTest按钮
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
                //1.在Name文本框输入“test”， ProgramingLanguage文本框输入“Python”

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
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
        //测试用例2：用户修改一条已入库的应用程序信息。用户完整输入所有必填项信息，点击修改，系统应弹出修改成功的提示信息。
        [TestMethod, Priority(2)]
        public void ApplicationModifyTests_Test002()
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
                //DOI文本框
                var textBox_DOI = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DOI")).AsTextBox();
                //修改按钮
                var button_Modify = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Modify")).AsButton();

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

                //DOI文本框修改内容为“DOI”
                textBox_DOI.Text = "DOI";
                Thread.Sleep(1000);
                //修改
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
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
        //测试用例4：用户修改一条未入库的应用程序信息。用户完整输入所有必填项信息，点击修改，系统应弹出修改失败的提示信息。
        [TestMethod, Priority(3)]
        public void ApplicationModifyTests_Test003()
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
                //ProgramingLanguage文本框
                var textBox_ProgramingLanguage = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ProgrammingLanguage")).AsTextBox();
                //Name文本框
                var textBox_Name = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_Name")).AsTextBox();
                //修改按钮
                var button_Modify = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Modify")).AsButton();
                //SoftwareUnderTest按钮
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
                //1.在Name文本框输入“test”， ProgramingLanguage文本框输入“Python”
                textBox_Name.Text = "test";
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
                //点击修改按钮
                button_Modify.Click();
                Thread.Sleep(2000);

                //点击确定修改按钮
                var confirmModifyDialog = mainWindow.ModalWindows[0];
                var confirmModifyButton = confirmModifyDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("是(Y)"))).AsButton();  // 确认按钮的文本为"确认"
                confirmModifyButton.Click();
                Thread.Sleep(2000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "修改记录 失败";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
        //测试用例4：用户修改一条已入库的应用程序信息，修改关联的应用领域信息（关联一个应用领域），点击修改，系统应弹出修改成功的提示信息。
        [TestMethod, Priority(4)]
        public void ApplicationModifyTests_Test004()
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
                //修改按钮
                var button_Modify = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Modify")).AsButton();
                //DomainNameComboBox
                var ComboBox_DomainName = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_DomainName")).AsComboBox();
                //ComboBox的CheckBox
                var CheckBoxs = ComboBox_DomainName.Items;
                var CheckBox_DomainName = CheckBoxs[0];

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

                //点击DomainName ComBox的Numerical program选项
                ComboBox_DomainName.Expand();
                Thread.Sleep(1000);
                CheckBoxs = ComboBox_DomainName.Items;
                for (int i = 0; i < CheckBoxs.Length; i++)
                {
                    var checkBox = CheckBoxs[i];

                    if (checkBox.Text.Equals("Machine learning"))
                    {
                        CheckBox_DomainName = checkBox;
                    }
                }
                //获得焦点
                CheckBox_DomainName.Focus();
                Thread.Sleep(1000);
                CheckBox_DomainName.Click();
                Thread.Sleep(1000);

                //修改
                //点击修改按钮
                button_Modify.DoubleClick();
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
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
        //测试用例5：用户修改一条已入库的应用程序信息，修改关联的应用领域信息（关联多个应用领域），点击修改，系统应弹出修改成功的提示信息。
        [TestMethod, Priority(5)]
        public void ApplicationModifyTests_Test005()
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
                //修改按钮
                var button_Modify = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Modify")).AsButton();
                //DomainNameComboBox
                var ComboBox_DomainName = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_DomainName")).AsComboBox();
                //ComboBox的CheckBox
                var CheckBoxs = ComboBox_DomainName.Items;
                var CheckBox_DomainName = CheckBoxs[0];

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
                //双击Name为“test1”的记录
                dataGrid_Application_Row.DoubleClick();
                Thread.Sleep(1000);

                //点击DomainName ComBox的Numerical program选项
                ComboBox_DomainName.Expand();
                Thread.Sleep(1000);
                CheckBoxs = ComboBox_DomainName.Items;
                for (int i = 0; i < CheckBoxs.Length; i++)
                {
                    var checkBox = CheckBoxs[i];

                    if (checkBox.Text.Equals("Machine learning"))
                    {
                        CheckBox_DomainName = checkBox;
                    }
                }
                //获得焦点
                CheckBox_DomainName.Focus();
                Thread.Sleep(1000);
                CheckBox_DomainName.Click();
                Thread.Sleep(1000);

                //修改
                //点击修改按钮
                button_Modify.DoubleClick();
                button_Modify.Click();//用于获得焦点
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
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
    }
}
