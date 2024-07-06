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

namespace MetBench_Test
{
    [TestClass]
    public class FileUploadandUndoTests
    {
        //测试用例1：用户完成上传SoftwareUnderTest的程序文件，系统应该给出相应的提示信息并且对应的文本框显示文件名。
        [TestMethod, Priority(1)]
        public void FileUploadandUndoTests_Test001()
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
                //上传按钮
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
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


                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;

                var expected = "文件上传成功！";
                //获取弹窗 成功上传
                var confirmDialog = mainWindow.ModalWindows[0];
                var confirmButton1 = confirmDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("确定"))).AsButton();  // 确认按钮的文本为"确认"
                confirmButton1.Click();
                Thread.Sleep(1000);
                //SoftwareUnderTest文本框
                var textBox_SoftwareUnderTest = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_SoftwareUnderTest")).AsTextBox();
                var actual1 = textBox_SoftwareUnderTest.Text;
                var expected1 = "test.py";
                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected1, actual1);
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }

        //测试用例2：用户完成上传SoftwareUnderTest的文件程序文件后，进行撤销上传，撤销完成后，系统应该给出相应的提示信息并清空对应的文本框的内容。
        [TestMethod, Priority(2)]
        public void FileUploadandUndoTests_Test002()
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
                //撤销按钮
                var button_ClearSoftwareUnderTest = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ClearSoftwareUnderTest")).AsButton();
                //上传按钮
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
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

                //点击撤销上传按钮
                button_ClearSoftwareUnderTest.Click();

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;

                var expected = "撤销上传成功！";

                Thread.Sleep(1000);
                //SoftwareUnderTest文本框
                var textBox_SoftwareUnderTest = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_SoftwareUnderTest")).AsTextBox();
                var actual1 = textBox_SoftwareUnderTest.Text;
                var expected1 = string.Empty;
                //Assert
                //断言
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected1, actual1);
                Thread.Sleep(1000);
                //关闭应用
                app.Close();
            }
        }

        //测试用例3：用户完成上传SoftwareUnderTest的文件程序文件后，进行解压文件，解压完成后，系统应该给出相应的提示。
        [TestMethod, Priority(3)]
        public void FileUploadandUndoTests_Test003()
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
                //撤销按钮
                var button_ClearSoftwareUnderTest = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ClearSoftwareUnderTest")).AsButton();
                //上传按钮
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
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
                //点击解压按钮
                var UnPressButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_DecompressSoftwareUnderTest")).AsButton();
                UnPressButton.Click();
                try
                {
                    // 获取提示信息
                    var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                    var actual = message.Name;
                    var expected = "文件解压成功！";
                    //断言
                    Assert.AreEqual(expected, actual);
                }
                finally
                {
                    //关闭应用程序
                    app.Close();
                }
            }
        }

        //测试用例4：用户上传文件，上传非法格式文件，系统应该给出相应的提示信息。
        [TestMethod, Priority(4)]
        public void FileUploadandUndoTests_Test004()
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
                //撤销按钮
                var button_ClearSoftwareUnderTest = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ClearSoftwareUnderTest")).AsButton();
                //上传按钮
                var button_Button_UploadSoftw = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_UploadSoftwareUnderTest")).AsButton();
                button_Button_UploadSoftw.Click();
                Thread.Sleep(1000);
                // 等待打开文件对话框出现
                var openFileDialog = mainWindow.ModalWindows[0];
                //获取上传文件的路径
                string solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                string parentDirectory = Directory.GetParent(solutionPath).FullName;
                //合并文件路径
                string filePath = Path.Combine(parentDirectory, "File", "test.docx");
                //输入法设置成英文

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
                try
                {
                    // 获取提示信息
                    var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                    var actual = message.Name;
                    var expected = "支持上传文件格式为：.python、.java、.C、.zip";
                    //断言
                    Assert.AreEqual(expected, actual);
                }
                finally
                {
                    //关闭应用程序
                    app.Close();
                }
            }
        }

    }
}
