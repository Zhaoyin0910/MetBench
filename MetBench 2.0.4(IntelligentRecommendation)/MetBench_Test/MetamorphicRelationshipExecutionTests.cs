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
    public class MetamorphicRelationshipExecutionTests
    {
        //测试用例1：用户输入正确的数据，系统能否正确执行蜕变关系并显示图片。
        [TestMethod, Priority(1)]
        public void MetamorphicRelationshipExecutionTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[5].Click();
                Thread.Sleep(1000);

                //MR数据网格
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                //将DataGrid滚动栏移动至最右边
                //获取DataGrid对象的滚动模式

                var scrollPattern = dataGrid_MR.Patterns.Scroll.Pattern;

                //滚动到最右边 Scroll函数的第一个参数表示水平滚动的增量，第二个参数表示垂直滚动的增量。 ScrollAmount.LargeIncrement 大增量滚动 ScrollAmount.LargeDecrement 减少量滚动 ScrollAmount.NoAmount不滚动
                scrollPattern.Scroll(ScrollAmount.LargeIncrement, ScrollAmount.NoAmount);

                var dataGrid_MR_Rows = dataGrid_MR.Rows;
                var length = dataGrid_MR_Rows.Length;
                var dataGrid_MR_Row = dataGrid_MR_Rows[0];
                var dataGrid_Cell = dataGrid_MR_Row.Cells[9];

                var Button_MT = dataGrid_Cell.FindFirstDescendant(cf => cf.ByAutomationId("Button_MT")).AsButton();
                //点击执行MT跳转页面按钮
                Button_MT.Click();
                Thread.Sleep(1000);

                //输入最小值-10
                var MinNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_MinNumber")).AsTextBox();
                MinNumberTextBox.Text = "-10";
                Thread.Sleep(1000);
                //输入最大值10
                var MaxNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_MaxNumber")).AsTextBox();
                MaxNumberTextBox.Text = "10";
                Thread.Sleep(1000);
                //输入执行次数
                var ExecutNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ExecuteNumber")).AsTextBox();
                ExecutNumberTextBox.Text = "100";
                Thread.Sleep(1000);
                //点击执行MT
                var ExecuteMTButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AutoRunMT")).AsButton();
                ExecuteMTButton.Click();
                Thread.Sleep(5000);

                //获取通过率文本框
                var Passrate = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_Passrate")).AsTextBox().Text;

                //获取失败率文本框
                var Failurerate = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_Failurerate")).AsTextBox().Text;

                try
                {
                    //断言成功率非空
                    Assert.IsNotNull(Passrate);
                    //断言失败率非空
                    Assert.IsNotNull(Failurerate);
                }
                finally
                {
                    app.Close();
                }
            }
        }
        //测试用例2：用户不输入任何数据直接点击执行MT按钮。系统能否给出数据不能为空的提示框。
        [TestMethod, Priority(2)]
        public void MetamorphicRelationshipExecutionTests_Test002()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[5].Click();
                Thread.Sleep(1000);

                //MR数据网格
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                //将DataGrid滚动栏移动至最右边
                //获取DataGrid对象的滚动模式

                var scrollPattern = dataGrid_MR.Patterns.Scroll.Pattern;

                //滚动到最右边 Scroll函数的第一个参数表示水平滚动的增量，第二个参数表示垂直滚动的增量。 ScrollAmount.LargeIncrement 大增量滚动 ScrollAmount.LargeDecrement 减少量滚动 ScrollAmount.NoAmount不滚动
                scrollPattern.Scroll(ScrollAmount.LargeIncrement, ScrollAmount.NoAmount);

                var dataGrid_MR_Rows = dataGrid_MR.Rows;
                var length = dataGrid_MR_Rows.Length;
                var dataGrid_MR_Row = dataGrid_MR_Rows[0];
                var dataGrid_Cell = dataGrid_MR_Row.Cells[9];

                var Button_MT = dataGrid_Cell.FindFirstDescendant(cf => cf.ByAutomationId("Button_MT")).AsButton();
                //点击执行MT跳转页面按钮
                Button_MT.Click();
                Thread.Sleep(1000);

                //点击执行MT
                var ExecuteMTButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AutoRunMT")).AsButton();
                ExecuteMTButton.Click();
                Thread.Sleep(5000);

                // 获取提示信息
                var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "MinNumber不能为空\nMaxNumber不能为空\nExecuteNumber不能为空";
                try
                {
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
        //测试用例3：用户输入MinNumber的数据大于MaxNumber输入的数据，系统能否弹出提示框。
        [TestMethod, Priority(3)]
        public void MetamorphicRelationshipExecutionTests_Test003()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[5].Click();
                Thread.Sleep(1000);

                //MR数据网格
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                //将DataGrid滚动栏移动至最右边
                //获取DataGrid对象的滚动模式

                var scrollPattern = dataGrid_MR.Patterns.Scroll.Pattern;

                //滚动到最右边 Scroll函数的第一个参数表示水平滚动的增量，第二个参数表示垂直滚动的增量。 ScrollAmount.LargeIncrement 大增量滚动 ScrollAmount.LargeDecrement 减少量滚动 ScrollAmount.NoAmount不滚动
                scrollPattern.Scroll(ScrollAmount.LargeIncrement, ScrollAmount.NoAmount);

                var dataGrid_MR_Rows = dataGrid_MR.Rows;
                var length = dataGrid_MR_Rows.Length;
                var dataGrid_MR_Row = dataGrid_MR_Rows[0];
                var dataGrid_Cell = dataGrid_MR_Row.Cells[9];

                var Button_MT = dataGrid_Cell.FindFirstDescendant(cf => cf.ByAutomationId("Button_MT")).AsButton();
                //点击执行MT跳转页面按钮
                Button_MT.Click();
                Thread.Sleep(1000);

                //输入最小值10
                var MinNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_MinNumber")).AsTextBox();
                MinNumberTextBox.Text = "10";
                Thread.Sleep(1000);
                //输入最大值-10
                var MaxNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_MaxNumber")).AsTextBox();
                MaxNumberTextBox.Text = "-10";
                Thread.Sleep(1000);
                //输入执行次数100
                var ExecutNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ExecuteNumber")).AsTextBox();
                ExecutNumberTextBox.Text = "100";
                Thread.Sleep(1000);
                //点击执行MT
                var ExecuteMTButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AutoRunMT")).AsButton();
                ExecuteMTButton.Click();
                Thread.Sleep(1000);

                // 获取提示信息
                var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "MaxNumber不能小于MinNumber";
                try
                {
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
        //测试用例4：用户输入的ExecuteNumber非法，系统能否弹出提示输入非法的提示框。
        [TestMethod, Priority(4)]
        public void MetamorphicRelationshipExecutionTests_Test004()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[5].Click();
                Thread.Sleep(1000);

                //MR数据网格
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                //将DataGrid滚动栏移动至最右边
                //获取DataGrid对象的滚动模式

                var scrollPattern = dataGrid_MR.Patterns.Scroll.Pattern;

                //滚动到最右边 Scroll函数的第一个参数表示水平滚动的增量，第二个参数表示垂直滚动的增量。 ScrollAmount.LargeIncrement 大增量滚动 ScrollAmount.LargeDecrement 减少量滚动 ScrollAmount.NoAmount不滚动
                scrollPattern.Scroll(ScrollAmount.LargeIncrement, ScrollAmount.NoAmount);

                var dataGrid_MR_Rows = dataGrid_MR.Rows;
                var length = dataGrid_MR_Rows.Length;
                var dataGrid_MR_Row = dataGrid_MR_Rows[0];
                var dataGrid_Cell = dataGrid_MR_Row.Cells[9];

                var Button_MT = dataGrid_Cell.FindFirstDescendant(cf => cf.ByAutomationId("Button_MT")).AsButton();
                //点击执行MT跳转页面按钮
                Button_MT.Click();
                Thread.Sleep(1000);

                //输入最小值-10
                var MinNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_MinNumber")).AsTextBox();
                MinNumberTextBox.Text = "-10";
                Thread.Sleep(1000);
                //输入最大值10
                var MaxNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_MaxNumber")).AsTextBox();
                MaxNumberTextBox.Text = "10";
                Thread.Sleep(1000);
                //输入执行次数-10
                var ExecutNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ExecuteNumber")).AsTextBox();
                ExecutNumberTextBox.Text = "-10";
                Thread.Sleep(1000);
                //点击执行MT
                var ExecuteMTButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AutoRunMT")).AsButton();
                ExecuteMTButton.Click();
                Thread.Sleep(1000);

                // 获取提示信息
                var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual = message.Name;
                var expected = "ExecuteNumber应为正整数";
                try
                {
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
        //测试用例5：用户直接进入MT Execution页面输入数据点击执行，系统能否正确给出提示并跳转页面至MR Management页面。
        [TestMethod, Priority(5)]
        public void MetamorphicRelationshipExecutionTests_Test005()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MT Execution页面
                children[11].Click();
                Thread.Sleep(1000);

                //输入最小值-10
                var MinNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_MinNumber")).AsTextBox();
                MinNumberTextBox.Text = "-10";
                Thread.Sleep(1000);
                //输入最大值10
                var MaxNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_MaxNumber")).AsTextBox();
                MaxNumberTextBox.Text = "10";
                Thread.Sleep(1000);
                //输入执行次数
                var ExecutNumberTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ExecuteNumber")).AsTextBox();
                ExecutNumberTextBox.Text = "-10";
                Thread.Sleep(1000);
                //点击执行MT
                var ExecuteMTButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AutoRunMT")).AsButton();
                ExecuteMTButton.Click();
                Thread.Sleep(1000);

                // 获取提示信息
                var message1 = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual1 = message1.Name;
                var expected1 = "被测程序为空！";
                try
                {
                    Assert.AreEqual(expected1, actual1);

                }
                finally
                {
                    // 寻找确定按钮
                    var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                    // 点击确定按钮
                    okButton?.Click();
                    Thread.Sleep(1000);
                }
                // 获取提示信息
                var message2 = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                var actual2 = message2.Name;
                var expected2 = "请选择蜕变关系！";
                try
                {
                    Assert.AreEqual(expected2, actual2);

                }
                finally
                {
                    // 寻找确定按钮
                    var okButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
                    // 点击确定按钮
                    okButton?.Click();
                    Thread.Sleep(1000);
                }
                //切换到MR Management页面
                var textBox_IdMR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_IdMR")).AsTextBox();
                string acual = textBox_IdMR.Text;
                string expected = "0";
                try
                {
                    Assert.AreEqual(expected, acual);
                }
                finally
                {
                    app.Close();
                }
            }
        }
    }
}
