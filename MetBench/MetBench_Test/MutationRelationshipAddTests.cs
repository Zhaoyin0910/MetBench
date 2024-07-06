using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;
using System;
using System.Text;

namespace MetBench_Test
{

    [TestClass]
    public class MutationRelationshipAddTests
    {    //测试用例1：用户未完整输入必填信息，点击增加按钮，系统应弹出对应的非空提示信息。
        [TestMethod, Priority(1)]
        public void MutationRelationshipAddTests_Test001()
        {
            using (var app = Application.Launch("D:\\Desktop\\寒假1月份工作\\MetBench(test)\\MetBench_Client\\bin\\Debug\\net6.0-windows\\MetBench_Client.exe"))
            {
                
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                // 进入MR Management页面
                children[5].Click();
                Thread.Sleep(1000);

                // InputPattern输入x_{21}=x_{11}
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                InputPatternTextBox.Text = "x_{21}=x_{11}";
                Thread.Sleep(1000);

                // OutputPattern输入x_{21}=x_{11}
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                OutputPatternTextBox.Text = "y_{21}=y_{11}";
                Thread.Sleep(1000);

                // DimensionOfinputPattern输入1
                var DimensionOfinputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfInputPattern")).AsTextBox();
                DimensionOfinputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                // DimensionOfOutputPattern输入1
                var DimensionOfOutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfOutputPattern")).AsTextBox();
                DimensionOfOutputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                // 点击增加按钮
                var AddMRButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddMR")).AsButton();
                AddMRButton.Click();
                Thread.Sleep(6000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "ApplicationName不能为空\n请选择OrderOfMR\n请选择RepresentationType";
                try
                {
                    //断言
                    Assert.AreEqual(expected, actual);
                }
                finally
                {
                    // 寻找确定按钮
                    var okButton = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                    // 点击确定按钮
                    okButton?.Click();
                    Thread.Sleep(1000);
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        //测试用例2：用户输入非法latex格式的数据，点击增加按钮，系统弹出非法输入的提示框。
        [TestMethod, Priority(2)]
        public void MutationRelationshipAddTests_Test002()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                // 进入MR Management页面
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                children[5].Click();
                Thread.Sleep(1000);

                // InputPattern输入x_{21}=x_{11}
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                InputPatternTextBox.Text = "%%%%";
                Thread.Sleep(1000);

                // OutputPattern输入x_{21}=x_{11}
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                OutputPatternTextBox.Text = "####";
                Thread.Sleep(1000);

                // DimensionOfinputPattern输入1
                var DimensionOfinputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfInputPattern")).AsTextBox();
                DimensionOfinputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                // DimensionOfinputPattern输入1
                var DimensionOfOutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfOutputPattern")).AsTextBox();
                DimensionOfOutputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                // ApplicatiopnName输入sin
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                ApplicationNameTextBox.Text = "sin-python";
                Thread.Sleep(2000);

                //OrderOfMR选择二元关系
                var OrderOfMRComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_OrderOfMR")).AsComboBox();
                OrderOfMRComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //PepresentationType选择数值
                var PepresentationTypeComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_RepresentationType")).AsComboBox();
                PepresentationTypeComboBox.Items[1].Select();
                Thread.Sleep(1000);

                // 点击增加按钮
                var AddMRButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddMR")).AsButton();
                AddMRButton.DoubleClick();
                Thread.Sleep(6000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "InputPattern输入的latex格式非法\nOutputPattern输入的latex格式非法";
                try
                {
                    Assert.AreEqual(expected, actual);
                }
                finally
                {
                    // 寻找确定按钮
                    var okButton = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                    // 点击确定按钮
                    okButton?.Click();
                    Thread.Sleep(1000);
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        // 测试用例3：用户输入合法latex格式的数据，点击增加的按钮，系统提示添加成功并新增一行数据。
        [TestMethod, Priority(3)]
        public void MutationRelationshipAddTests_Test003()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //进入MR Management页面
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                children[5].Click();
                Thread.Sleep(1000);

                //InputPattern输入x_{21}=x_{11}
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                InputPatternTextBox.Text = "x_{21}=x_{11}";
                Thread.Sleep(1000);

                //OutputPattern输入x_{21}=x_{11}
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                OutputPatternTextBox.Text = "y_{21}=y_{11}";
                Thread.Sleep(1000);

                //DimensionOfinputPattern输入1
                var DimensionOfinputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfInputPattern")).AsTextBox();
                DimensionOfinputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                //DimensionOfinputPattern输入1
                var DimensionOfOutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfOutputPattern")).AsTextBox();
                DimensionOfOutputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                //ApplicatiopnName输入amax-java
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                ApplicationNameTextBox.Text = "amax-java";
                Thread.Sleep(2000);

                //OrderOfMR选择二元关系
                var OrderOfMRComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_OrderOfMR")).AsComboBox();
                OrderOfMRComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //PepresentationType选择数值
                var PepresentationTypeComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_RepresentationType")).AsComboBox();
                PepresentationTypeComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //点击增加按钮
                var AddMRButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddMR")).AsButton();
                AddMRButton.DoubleClick();
                Thread.Sleep(6000);

                var window = app.GetMainWindow(automation);
                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "添加记录 成功";
                try
                {
                    Assert.AreEqual(expected, actual);
                }
                finally
                {
                    // 寻找确定按钮
                    var okButton = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                    // 点击确定按钮
                    okButton?.Click();
                    Thread.Sleep(1000);
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        // 测试用例1：用户输入的ApplicationName在Application Management页面中不存在，系统应该在Application Management页面新增一行数据
        [TestMethod, Priority(4)]
        public void MutationRelationshipAddTests_Test004()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[5].Click();
                Thread.Sleep(1000);

                //InputPattern输入x_{21}=x_{11}
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                InputPatternTextBox.Text = "x_{21}=x_{11}";
                Thread.Sleep(1000);

                //OutputPattern输入x_{21}=x_{11}
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                OutputPatternTextBox.Text = "y_{21}=y_{11}";
                Thread.Sleep(1000);

                //DimensionOfinputPattern输入1
                var DimensionOfinputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfInputPattern")).AsTextBox();
                DimensionOfinputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                //DimensionOfinputPattern输入1
                var DimensionOfOutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfOutputPattern")).AsTextBox();
                DimensionOfOutputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                //ApplicatiopnName输入sin
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                ApplicationNameTextBox.Text = "test";
                Thread.Sleep(1000);

                //OrderOfMR选择二元关系
                var OrderOfMRComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_OrderOfMR")).AsComboBox();
                OrderOfMRComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //PepresentationType选择数值
                var PepresentationTypeComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_RepresentationType")).AsComboBox();
                PepresentationTypeComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //点击增加按钮
                var AddMRButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddMR")).AsButton();
                AddMRButton.DoubleClick();
                Thread.Sleep(6000);

                // 寻找确定按钮
                var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                // 点击确定按钮
                okButton?.Click();

                //切换至Application Management页面
                children[7].Click();
                Thread.Sleep(1000);

                // 找到 DataGrid
                var dataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_Application")).AsDataGridView();
                // 获取 DataGrid 中的所有行
                var rows = dataGrid.Rows;
                // 获取最后一行
                var lastRow = rows[dataGrid.Rows.Length - 1];
                // 获取第二列的数据
                var ActualApplicationName = lastRow.Cells[1].Value;
                //断言
                var expectedApplicationName = "test";
                try
                {
                    Assert.AreEqual(expectedApplicationName, ActualApplicationName);
                }
                finally
                {
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        //测试用例5：用户输入的ApplicationName在Application Management页面中存在。Application Management页面不会新增数据
        [TestMethod, Priority(5)]
        public void MutationRelationshipAddTests_Test005()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入Application Management页面
                children[7].Click();
                Thread.Sleep(1000);

                // 找到 DataGrid
                var BeforedataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_Application")).AsDataGridView();
                // 获取增加前Application Management页面 DataGrid 中的所有行数
                var BeforeRowLength = BeforedataGrid.Rows.Length;

                //切换至MR Management页面
                children[5].Click();
                Thread.Sleep(1000);

                //InputPattern输入x_{21}=x_{11}
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                InputPatternTextBox.Text = "x_{21}=x_{11}";
                Thread.Sleep(1000);

                //OutputPattern输入x_{21}=x_{11}
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                OutputPatternTextBox.Text = "y_{21}=y_{11}";
                Thread.Sleep(1000);

                //DimensionOfinputPattern输入1
                var DimensionOfinputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfInputPattern")).AsTextBox();
                DimensionOfinputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                //DimensionOfinputPattern输入1
                var DimensionOfOutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfOutputPattern")).AsTextBox();
                DimensionOfOutputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                //ApplicatiopnName输入sin
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                ApplicationNameTextBox.Text = "test";
                Thread.Sleep(1000);

                //OrderOfMR选择二元关系
                var OrderOfMRComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_OrderOfMR")).AsComboBox();
                OrderOfMRComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //PepresentationType选择数值
                var PepresentationTypeComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_RepresentationType")).AsComboBox();
                PepresentationTypeComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //点击增加按钮
                var AddMRButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddMR")).AsButton();
                AddMRButton.DoubleClick();
                Thread.Sleep(6000);

                // 寻找确定按钮
                var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                // 点击确定按钮
                okButton?.Click();

                //切换至Application management页面
                children[7].Click();

                // 找到 DataGrid
                var dataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_Application")).AsDataGridView();
                // 获取增加后Application Management页面 DataGrid 中的所有行数
                var rows = dataGrid.Rows;
                // 找到 DataGrid
                var AfterdataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_Application")).AsDataGridView();
                // 获取 DataGrid 中的所有行
                var AfterRowLength = AfterdataGrid.Rows.Length;
                try
                {
                    Assert.AreEqual(BeforeRowLength, AfterRowLength);
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
