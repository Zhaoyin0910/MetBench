using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.AutomationElements;
using System.Threading;
using System.Windows.Input;
using FlaUI.Core.Definitions;
using System;


namespace MetBench_Test
{
    [TestClass]
    public class PageTransitionandParameterPassTests
    {
        //测试用例1：用户点击MR  Display页面的数据网格的一条数据中的编辑按钮，系统应切换到指定页面并进行正确传参。
        [TestMethod, Priority(1)]
        public void PageTransitionandParameterPassTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {

                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Display页面
                children[3].Click();
                Thread.Sleep(2000);
                //MR数据网格
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                //将DataGrid滚动栏移动至最右边
                //获取DataGrid对象的滚动模式
                var scrollPattern = dataGrid_MR.Patterns.Scroll.Pattern;
                //滚动到最右边 Scroll函数的第一个参数表示水平滚动的增量，第二个参数表示垂直滚动的增量。 ScrollAmount.LargeIncrement 大增量滚动 ScrollAmount.LargeDecrement 减少量滚动 ScrollAmount.NoAmount不滚动
                scrollPattern.Scroll(ScrollAmount.LargeIncrement, ScrollAmount.NoAmount);
                Thread.Sleep(1000);
                //var a = 2;

                var dataGrid_MR_Rows = dataGrid_MR.Rows;
                var length = dataGrid_MR_Rows.Length;
                var dataGrid_MR_Row = dataGrid_MR_Rows[0];
                var dataGrid_Cell = dataGrid_MR_Row.Cells[10];

                var button_Modify = dataGrid_Cell.FindFirstDescendant(cf => cf.ByAutomationId("Button_Modify")).AsButton();
                //点击编辑按钮
                button_Modify.Click();
                Thread.Sleep(1000);
                //
                //切换到MR Management页面
                //children[5].Click();
                //Thread.Sleep(1000);
                var textBox_IdMR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_IdMR")).AsTextBox();
                string acual = textBox_IdMR.Text;

                //Assert
                //断言
                string expected = "3";
                Assert.AreEqual(expected, acual);
                app.Close();
            }
        }

        //测试用例2：用户在选择MR Management页面中DomainName下拉框中的页面中DomainName下拉框中的页面中DomainName下拉框中的“其他”选项时，系统应切换到指定页面
        [TestMethod, Priority(2)]
        public void PageTransitionandParameterPassTests_Test002()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[5].Click();
                Thread.Sleep(2000);
                //DomainNameComboBox
                var ComboBox_DomainName = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_DomainName")).AsComboBox();
                //点击DomainName下拉框中的其他选项
                //ComboBox的CheckBox
                var CheckBoxs = ComboBox_DomainName.Items;
                ComboBox_DomainName.Expand();
                Thread.Sleep(1000);
                var CheckBox_Other = CheckBoxs[3];
                CheckBox_Other.Click();
                for (int i = 0; i < CheckBoxs.Length; i++)
                {
                    var name = CheckBoxs[i].Name;
                    if (name.Equals("其他"))
                    {
                        CheckBoxs[i].AsCheckBox().Click();
                        CheckBox_Other = CheckBoxs[i];
                    }
                }
                Thread.Sleep(1000);
                var a = CheckBox_Other;
                Thread.Sleep(1000);
                CheckBox_Other.Focus();
                Thread.Sleep(1000);
                CheckBox_Other.AsCheckBox().Focus();
                Thread.Sleep(1000);
                CheckBox_Other.AsCheckBox().Click();

                Thread.Sleep(1000);
                //Domain Management页面的数据网格
                var dataGrid_Domain = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("")).AsDataGridView();
                var actual = dataGrid_Domain.Rows.Length;
                var expected = 2;
                Assert.AreEqual(expected, actual);

                app.Close();
            }
        }

        //测试用例3：用户在Application Management页面中选择DomainName下拉框中的“其他”选项时，系统应切换到指定页面
        [TestMethod, Priority(3)]
        public void PageTransitionandParameterPassTests_Test003()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[7].Click();
                Thread.Sleep(2000);
                //DomainNameComboBox
                var ComboBox_DomainName = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ComboBox_DomainName")).AsComboBox();
                //点击DomainName下拉框中的其他选项
                //ComboBox的CheckBox
                var CheckBoxs = ComboBox_DomainName.Items;
                ComboBox_DomainName.Expand();
                Thread.Sleep(1000);
                var CheckBox_Other = CheckBoxs[3];
                CheckBox_Other.Click();
                for (int i = 0; i < CheckBoxs.Length; i++)
                {
                    var name = CheckBoxs[i].Name;
                    if (name.Equals("其他"))
                    {
                        CheckBoxs[i].AsCheckBox().Click();
                        CheckBox_Other = CheckBoxs[i];
                    }
                }
                Thread.Sleep(1000);
                var a = CheckBox_Other;
                Thread.Sleep(1000);
                CheckBox_Other.Focus();
                Thread.Sleep(1000);
                CheckBox_Other.AsCheckBox().Focus();
                Thread.Sleep(1000);
                CheckBox_Other.AsCheckBox().Click();

                Thread.Sleep(1000);
                //Domain Management页面的数据网格
                var dataGrid_Domain = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("")).AsDataGridView();
                var actual = dataGrid_Domain.Rows.Length;
                var expected = 2;
                Assert.AreEqual(expected, actual);

                app.Close();
            }
        }

        //测试用例4：用户点击MR Management页面中数据网格的一条数据中的执行MT按钮，系统应切换到指定页面并进行正确传参。
        [TestMethod, Priority(4)]
        public void PageTransitionandParameterPassTests_Test004()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {

                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Management页面
                children[5].Click();
                Thread.Sleep(2000);
                //MR数据网格
                var dataGrid_MR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                //将DataGrid滚动栏移动至最右边
                //获取DataGrid对象的滚动模式
                var scrollPattern = dataGrid_MR.Patterns.Scroll.Pattern;
                //滚动到最右边 Scroll函数的第一个参数表示水平滚动的增量，第二个参数表示垂直滚动的增量。 ScrollAmount.LargeIncrement 大增量滚动 ScrollAmount.LargeDecrement 减少量滚动 ScrollAmount.NoAmount不滚动
                scrollPattern.Scroll(ScrollAmount.LargeIncrement, ScrollAmount.NoAmount);
                Thread.Sleep(1000);
                //var a = 2;

                var dataGrid_MR_Rows = dataGrid_MR.Rows;
                var length = dataGrid_MR_Rows.Length;
                var dataGrid_MR_Row = dataGrid_MR_Rows[0];//ApplicationName为“sin-python”
                var dataGrid_Cell = dataGrid_MR_Row.Cells[9];


                var button_MT = dataGrid_Cell.FindFirstDescendant(cf => cf.ByAutomationId("Button_MT")).AsButton();
                //点击编辑按钮
                button_MT.Click();
                Thread.Sleep(1000);
                //
                //切换到MT Execution页面s
                //children[5].Click();
                //Thread.Sleep(1000);
                var textBox_Passrate = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_CodeName")).AsTextBox();
                var actual = textBox_Passrate.Text;
                //Assert
                //断言
                var expected = "sin.py";
                Assert.AreEqual(expected, actual);
                app.Close();
            }
        }

        //测试用例5：用户点击返回上一Tabbar按钮，系统应切换返回到正确的页面。
        [TestMethod, Priority(5)]
        public void PageTransitionandParameterPassTests_Test005()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var window = mainWindow;
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Diplay页面
                children[3].Click();

                Thread.Sleep(2000);
                //点击返回上一个Tabbar页面
                var button_GobackPage = window.FindFirstChild(cf => cf.ByAutomationId("Button_GobackPage")).AsButton();
                button_GobackPage.Click();
                //Home页面中的“Click me!”按钮

                var button = mainWindow.FindFirstDescendant(cf => cf.ByName("Click me!")).AsButton();
                Thread.Sleep(1000);
                //Assert
                //断言
                Assert.IsNotNull(button);
                app.Close();
            }
        }
        //测试用例6：用户点击返回上一Tabbar按钮能够返回系统首页，系统应切换返回到正确的页面。
        [TestMethod, Priority(6)]
        public void PageTransitionandParameterPassTests_Test006()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var window = mainWindow;
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Diplay页面
                children[3].Click();
                //进入MR Management页面
                children[5].Click();
                //进入Home页面
                children[1].Click();
                Thread.Sleep(2000);
                //第一次点击返回上一个Tabbar页面按钮
                var button_GobackPage = window.FindFirstChild(cf => cf.ByAutomationId("Button_GobackPage")).AsButton();
                button_GobackPage.Click();
                //MR Management页面中增加按钮
                var button_AddMR = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_AddMR")).AsButton();
                Assert.IsNotNull(button_AddMR);
                Thread.Sleep(1000);
                //第二次点击返回上一个Tabbar页面按钮
                button_GobackPage.Click();
                var button_Query = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Query")).AsButton();
                Assert.IsNotNull(button_Query);
                Thread.Sleep(1000);
                //第三次点击返回上一个Tabbar页面按钮
                //Home页面中的“Click me!”按钮
                button_GobackPage.Click();
                var button = mainWindow.FindFirstDescendant(cf => cf.ByName("Click me!")).AsButton();
                Assert.IsNotNull(button);
                Thread.Sleep(1000);
                //Assert
                //断言
                app.Close();
            }
        }
    }
}
