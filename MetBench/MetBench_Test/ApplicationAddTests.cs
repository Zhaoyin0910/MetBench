using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace MetBench_Test
{
    [TestClass]
    public class ApplicationAddTests
    {
        //测试用例1：用户未完整输入必填信息，点击增加按钮，系统应弹出对应的非空提示信息。
        [TestMethod, Priority(1)]
        public void ApplicationAddTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //Arrange
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
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Add")).AsButton();
                //1.在Name文本框输入“test”， ProgramingLanguage文本框输入“Python”
                textBox_Name.Text = "test";
                textBox_ProgramingLanguage.Text = "Python";
                Thread.Sleep(1000);
                //点击增加按钮
                button_Add.Click();
                Thread.Sleep(1000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "请上传Code";
                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }

        //测试用例2：用户完整输入所有必填项信息，点击增加一条未入库的应用程序信息，系统应弹出增加成功的提示信息。
        [TestMethod, Priority(2)]
        public void ApplicationAddTests_Test002()
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
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Add")).AsButton();
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
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
        //测试用例3：用户增加一条已入库的信息。用户双击数据网格中的一条数据，并点击添加，系统应弹出增加失败的提示信息。

        [TestMethod, Priority(3)]
        public void ApplicationAddTests_Test003()
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
                var dataGrid_Application_LatestRow = dataGrid_Application.Rows[length - 1];
                var dataGrid_Application_LatestRow_Cells = dataGrid_Application_LatestRow.Cells;
                //双击Name为“test”的记录，本用例"test"位于最新的行
                dataGrid_Application_LatestRow.DoubleClick();
                Thread.Sleep(1000);
                //点击增加按钮
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Add")).AsButton();
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
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }
        //测试用例4：用户增加一条信息。用户完整输入所有必填项信息并且输入的信息与某一条已入库的信息，除Id和Name信息不同外，其余信息相同，点击增加加按钮，系统应弹出增加失败的提示信息。
        [TestMethod, Priority(4)]
        public void ApplicationAddTests_Test004()
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
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Add")).AsButton();
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
                //1.在Name文本框输入“test”， ProgramingLanguage文本框输入“Python”
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
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }

        //测试用例5：用户增加一条未入库的应用程序信息（关联多个应用领域）。用户完整输入所有必填项信息以及关联应用领域信息，点击添加，系统应弹出增加成功的提示信息。
        [TestMethod, Priority(5)]
        public void ApplicationAddTests_Test005()
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
                //DomainNameComboBox
                var ComboBox_DomainName = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_DomainName")).AsComboBox();
                //ComboBox的CheckBox
                var CheckBoxs = ComboBox_DomainName.Items;
                var CheckBox_DomainName = CheckBoxs[0];


                //增加按钮
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Add")).AsButton();
                //SoftwareUnderTest上传文件按钮
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
                //1.在Name文本框输入“test”， ProgramingLanguage文本框输入“Python”
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

                Thread.Sleep(2000);
                //使用键盘输入文件路径
                Keyboard.Type(filePath);
                Thread.Sleep(1000);

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
                //点击DomainName ComBox的Numerical program选项
                ComboBox_DomainName.Expand();
                Thread.Sleep(1000);
                CheckBoxs = ComboBox_DomainName.Items;
                for (int i = 0; i < CheckBoxs.Length; i++)
                {
                    var checkBox = CheckBoxs[i];
                    if (checkBox.Text.Equals("Numerical program"))
                    {
                        CheckBox_DomainName = checkBox;
                    }
                }
                //获得焦点
                CheckBox_DomainName.Focus();
                Thread.Sleep(1000);
                CheckBox_DomainName.Click();
                Thread.Sleep(1000);
                //点击增加按钮
                button_Add.DoubleClick();
                Thread.Sleep(2000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "添加记录 成功";

                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }

        //测试用例6：用户增加一条未入库的应用程序信息（关联多个应用领域）。用户完整输入所有必填项信息以及关联应用领域信息，点击添加，系统应弹出增加成功的提示信息。
        [TestMethod, Priority(6)]
        public void ApplicationAddTests_Test006()
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
                //DomainNameComboBox
                var ComboBox_DomainName = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_DomainName")).AsComboBox();
                //ComboBox的CheckBox
                var CheckBoxs = ComboBox_DomainName.Items;
                var CheckBox_DomainName = CheckBoxs[0];
                var CheckBox_DomainName1 = CheckBoxs[0];

                //增加按钮
                var button_Add = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Add")).AsButton();
                //SoftwareUnderTest上传文件按钮
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
                //1.在Name文本框输入“test”， ProgramingLanguage文本框输入“Python”
                textBox_Name.Text = "test2";
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

                Thread.Sleep(2000);
                //使用键盘输入文件路径
                Keyboard.Type(filePath);
                Thread.Sleep(1000);

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
                //点击DomainName ComBox的Numerical program选项
                ComboBox_DomainName.Expand();
                Thread.Sleep(1000);
                CheckBoxs = ComboBox_DomainName.Items;
                for (int i = 0; i < CheckBoxs.Length; i++)
                {
                    var checkBox = CheckBoxs[i];
                    if (checkBox.Text.Equals("Numerical program"))
                    {
                        CheckBox_DomainName = checkBox;
                    }

                    if (checkBox.Text.Equals("Machine learning"))
                    {
                        CheckBox_DomainName1 = checkBox;
                    }
                }
                //获得焦点
                CheckBox_DomainName.Focus();
                Thread.Sleep(1000);
                CheckBox_DomainName.Click();
                Thread.Sleep(1000);
                //获得焦点
                CheckBox_DomainName1.Focus();
                Thread.Sleep(1000);
                CheckBox_DomainName1.Click();
                Thread.Sleep(1000);
                //点击增加按钮
                button_Add.DoubleClick();
                Thread.Sleep(2000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "添加记录 成功";

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
