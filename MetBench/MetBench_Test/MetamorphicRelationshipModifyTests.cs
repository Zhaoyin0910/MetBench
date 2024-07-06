using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;
using FlaUI.Core.Definitions;
using System;

namespace MetBench_Test
{
    [TestClass]
    public class MetamorphicRelationshipModifyTests
    {
        // 测试用例1：用户未完整输入必填信息，点击修改按钮，系统应弹出对应的输入非空提示框。
        [TestMethod, Priority(1)]
        public void MetamorphicRelationshipModifyTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[5].Click();
                Thread.Sleep(1000);

                // 双击数据网格的数据                                                                     
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                var rows = dataGrid_MR.Rows;
                var lastRow = rows[1].Cells[2];
                if (lastRow != null)
                {
                    lastRow.DoubleClick();
                    Thread.Sleep(1000);
                }
                //ApplicatiopnName置空
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                ApplicationNameTextBox.Text = string.Empty;
                Thread.Sleep(2000);

                //点击修改按钮
                var ModifyButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyMR")).AsButton();
                ModifyButton.Click();
                Thread.Sleep(6000);

                // 获取提示信息
                var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "ApplicationName不能为空";
                try
                {
                    //断言
                    Assert.AreEqual(expected, actual);
                    Thread.Sleep(1000);
                }
                finally
                {
                    // 寻找确定按钮
                    var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                    // 点击确定按钮
                    okButton?.Click();
                    Thread.Sleep(1000);
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        // 测试用例2：用户输入非法latex格式的数据，点击修改按钮，系统弹出输入非法的提示框。
        [TestMethod, Priority(2)]
        public void MetamorphicRelationshipModifyTests_Test002()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //进入MR Management页面
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                children[5].Click();
                Thread.Sleep(1000);

                // 双击数据网格的数据                                                                     
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                var rows = dataGrid_MR.Rows;
                var lastRow = rows[1].Cells[2];
                if (lastRow != null)
                {
                    lastRow.DoubleClick();
                    Thread.Sleep(1000);
                }

                //InputPattern改为%%%%
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                InputPatternTextBox.Text = "%%%%";
                Thread.Sleep(1000);

                //OutputPattern改为####
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                OutputPatternTextBox.Text = "####";
                Thread.Sleep(1000);

                //点击修改按钮
                var ModifyButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyMR")).AsButton();
                ModifyButton.Click();
                Thread.Sleep(6000);

                var window = app.GetMainWindow(automation);

                // 获取提示信息
                var message = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var expected = "InputPattern输入的latex格式非法\nOutputPattern输入的latex格式非法";
                var actual = message.Name;
                try
                {
                    //断言
                    Assert.AreEqual(expected, actual);
                }
                finally
                {
                    // 寻找弹窗中的根控件，你可以根据实际情况选择适当的查找条件
                    var alertDialog = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window)).AsWindow();

                    // 在弹窗中查找“否(N)”按钮
                    var noButton = alertDialog.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button).And(cf.ByText("否(N)"))).AsButton();

                    // 点击“否(N)”按钮
                    noButton?.Click();
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        // 测试用例3：用户输入合法latex格式的数据，点击修改的按钮，系统显示修改之后的数据。
        [TestMethod, Priority(3)]
        public void MetamorphicRelationshipModifyTests_Test003()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //进入MR Management页面
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                children[5].Click();
                Thread.Sleep(1000);

                // 双击数据网格数据
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                var rows = dataGrid_MR.Rows;
                var lastRow = rows[1].Cells[2];
                if (lastRow != null)
                {
                    lastRow.DoubleClick();
                    Thread.Sleep(1000);
                }

                //InputPattern改为x_{21}=x_{11}+3
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                InputPatternTextBox.Text = "x_{21}=x_{11}+3";
                Thread.Sleep(1000);

                //OutputPattern改为y_{21}=y_{11}+3
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                OutputPatternTextBox.Text = "y_{21}=y_{11}+3";
                Thread.Sleep(1000);

                //点击修改按钮
                var ModifyButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyMR")).AsButton();
                ModifyButton.Click();
                Thread.Sleep(6000);

                // 寻找弹窗中的根控件，你可以根据实际情况选择适当的查找条件
                var alertDialog = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window)).AsWindow();

                // 在弹窗中查找“是(Y)”按钮
                var YesButton = alertDialog.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button).And(cf.ByText("是(Y)"))).AsButton();

                // 点击“是(Y)”按钮
                YesButton?.Click();
                Thread.Sleep(1000);
                // 获取提示信息
                var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "修改记录 成功";
                try
                {
                    //断言
                    Assert.AreEqual(expected, actual);
                }
                finally
                {
                    // 寻找确定按钮
                    var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                    // 点击确定按钮
                    okButton?.Click();
                    Thread.Sleep(1000);
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        // 测试用例4：用户修改数据库中不存在的数据。系统应弹出修改失败的提示框。
        [TestMethod, Priority(4)]
        public void MetamorphicRelationshipModifyTests_Test004()
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

                //OutputPattern输入y_{21}=y_{11}
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
                ApplicationNameTextBox.Text = "sin";
                Thread.Sleep(2000);

                //OrderOfMR选择二元关系
                var OrderOfMRComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_OrderOfMR")).AsComboBox();
                OrderOfMRComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //PepresentationType选择数值
                var PepresentationTypeComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_RepresentationType")).AsComboBox();
                PepresentationTypeComboBox.Items[1].Select();
                Thread.Sleep(1000);

                //点击修改按钮
                var ModifyButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyMR")).AsButton();
                ModifyButton.DoubleClick();
                Thread.Sleep(6000);

                // 寻找弹窗中的根控件，你可以根据实际情况选择适当的查找条件
                var alertDialog = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window)).AsWindow();

                // 在弹窗中查找“是(Y)”按钮
                var YesButton = alertDialog.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button).And(cf.ByText("是(Y)"))).AsButton();

                // 点击“是(Y)”按钮
                YesButton?.Click();
                Thread.Sleep(1000);
                // 获取提示信息
                var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "添加记录 失败";
                try
                {
                    //断言
                    Assert.AreEqual(expected, actual);
                }
                finally
                {
                    // 寻找确定按钮
                    var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                    // 点击确定按钮
                    okButton?.Click();
                    Thread.Sleep(1000);
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        //测试用例5：用户在二次确认时点击取消按钮。系统应撤回修改。
        [TestMethod, Priority(5)]
        public void MetamorphicRelationshipModifyTests_Test005()
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
                string BeforeInputPatternText = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox().Text;
                string BeforOutputPatternText = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox().Text;
                //InputPattern输入x_{21}=x_{11}
                var InputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_InputPattern")).AsTextBox();
                InputPatternTextBox.Text = "x_{21}=x_{11}+3";
                Thread.Sleep(1000);

                //OutputPattern输入y_{21}=y_{11}
                var OutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_OutputPattern")).AsTextBox();
                OutputPatternTextBox.Text = "y_{21}=y_{11}+3";
                Thread.Sleep(1000);

                //点击修改按钮
                var ModifyButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyMR")).AsButton();
                ModifyButton.Click();
                Thread.Sleep(6000);

                // 寻找弹窗中的根控件，你可以根据实际情况选择适当的查找条件
                var alertDialog = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window)).AsWindow();

                // 在弹窗中查找“否(N)”按钮
                var noButton = alertDialog.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button).And(cf.ByText("否(N)"))).AsButton();


                Thread.Sleep(1000);
                try
                {
                    Assert.IsNotNull(noButton);
                }
                finally
                {
                    // 点击“否(N)”按钮
                    noButton?.Click();
                    app.Close();
                }
            }
        }
        //测试用例6：用户修改的ApplicationName在Application Management页面不存在。Application Management页面会新增一行数据。
        [TestMethod,Priority(6)]
        public void MetamorphicRelationshipModifyTests_Test006()
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

                //ApplicatiopnName改为cos
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                ApplicationNameTextBox.Text = "cos";
                Thread.Sleep(2000);

                //点击修改按钮
                var ModifyButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyMR")).AsButton();
                ModifyButton.Click();
                Thread.Sleep(6000);

                // 寻找弹窗中的根控件，你可以根据实际情况选择适当的查找条件
                var alertDialog = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window)).AsWindow();

                // 在弹窗中查找“是(Y)”按钮
                var YesButton = alertDialog.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button).And(cf.ByText("是(Y)"))).AsButton();

                // 点击“是(Y)”按钮
                YesButton?.Click();
                Thread.Sleep(1000);

                // 寻找确定按钮
                var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                // 点击确定按钮
                okButton?.Click();
                Thread.Sleep(1000);

                //切换至Application Management页面
                children[7].Click();
                Thread.Sleep(1000);

                // 找到 DataGrid
                var dataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_Application")).AsDataGridView();
                // 获取 DataGrid 中的所有行
                var allrows = dataGrid.Rows;
                Console.WriteLine(allrows.Length);
                // 获取最后一行
                var LastRow = allrows[dataGrid.Rows.Length - 1];
                // 获取第二列的数据
                var ActualApplicationName = LastRow.Cells[1].Value;
                //断言
                var expectedApplicationName = "cos";
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
        //测试用例7：用户修改的ApplicationName在Application Management页面已存在。Application Management页面不会新增数据。
        [TestMethod, Priority(7)]
        public void MetamorphicRelationshipModifyTests_Test007()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                //进入Application Management页面
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                children[7].Click();
                Thread.Sleep(1000);

                var beforedataGrid = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("DataGrid_Application"))).AsDataGridView();
                var beforelength = beforedataGrid.Rows.Length;
                //进入MR Management页面
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

                //ApplicatiopnName改为cos-python
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                ApplicationNameTextBox.Text = "cos-python";
                Thread.Sleep(2000);

                //点击修改按钮
                var ModifyButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_ModifyMR")).AsButton();
                ModifyButton.Click();
                Thread.Sleep(6000);

                // 寻找弹窗中的根控件，你可以根据实际情况选择适当的查找条件
                var alertDialog = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window)).AsWindow();

                // 在弹窗中查找“是(Y)”按钮
                var YesButton = alertDialog.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button).And(cf.ByText("是(Y)"))).AsButton();

                // 点击“是(Y)”按钮
                YesButton?.Click();
                Thread.Sleep(1000);

                // 寻找确定按钮
                var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                // 点击确定按钮
                okButton?.Click();
                Thread.Sleep(1000);

                //切换至Application Management页面
                children[7].Click();
                Thread.Sleep(1000);

                var afterdataGrid = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("DataGrid_Application"))).AsDataGridView();
                var afterlength = afterdataGrid.Rows.Length;

                try
                {
                    Assert.AreEqual(beforelength, afterlength);
                }
                finally
                {
                    app.Close();
                }
            }
        }
    }
}
