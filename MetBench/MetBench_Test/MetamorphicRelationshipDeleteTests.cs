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
    public class MetamorphicRelationshipDeleteTests
    {
        //测试用例1：用户删除数据库中已有的数据，系统应弹出删除成功提示。
        [TestMethod, Priority(1)]
        public void MetamorphicRelationshipDeleteTests_Test001()
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
                var dataGrid_Application = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("DataGrid_MR"))).AsDataGridView();
                var length = dataGrid_Application.Rows.Length;
                var dataGrid_Application_LatestRow = dataGrid_Application.Rows[length - 1];

          
                var scrollPattern = dataGrid_Application.Patterns.Scroll.Pattern;
                // 滚动到最下边
                scrollPattern.Scroll(ScrollAmount.NoAmount, ScrollAmount.LargeIncrement);
                dataGrid_Application_LatestRow.Cells[2].DoubleClick();
                Thread.Sleep(2000);

                //点击删除按钮
                var DeleteButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_DeleteMR")).AsButton();
                DeleteButton.Click();
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
                var expected = "删除记录成功";
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
        //测试用例2：用户删除数据库不存在的数据，系统应弹出删除失败提示。
        [TestMethod, Priority(2)]
        public void MetamorphicRelationshipDeleteTests_Test002()
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

                //点击删除按钮
                var DeleteButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_DeleteMR")).AsButton();
                DeleteButton.DoubleClick();

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
                var expected = "删除记录失败";
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
        //测试用例3：用户在二次确认时点击取消按钮。系统应撤回删除。
        [TestMethod, Priority(3)]
        public void MetamorphicRelationshipDeleteTests_Test003()
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

                //点击删除按钮
                var DeleteButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_DeleteMR")).AsButton();
                DeleteButton.Click();

                // 寻找弹窗中的根控件，你可以根据实际情况选择适当的查找条件
                var alertDialog = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window)).AsWindow();

                // 在弹窗中查找“否(N)”按钮
                var noButton = alertDialog.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button).And(cf.ByText("否(N)"))).AsButton();

                Thread.Sleep(1000);
                try
                {
                    // 断言按钮成功点击
                    Assert.IsNotNull(noButton);
                }
                finally
                {
                    // 点击“否(N)”按钮
                    noButton?.Click();
                    //关闭应用程序
                    app.Close();
                }
            }
        }
    }
}
