using FlaUI.UIA3;
using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using System.Threading;


namespace MetBench_Test
{
    [TestClass]
    public class MetamorphicRelationshipQueryTests
    {

        // 测试用例1：用户不输入查询条件，点击查询按钮，系统应该显示所有蜕变关系信息。
        [TestMethod, Priority(1)]
        public void MetamorphicRelationshipQueryTests_Test001()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Display页面
                children[3].Click();
                Thread.Sleep(1000);
                //点击查询按钮
                var QueryBNutton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Query")).AsButton();
                QueryBNutton.Click();
                Thread.Sleep(1000);
                // 找到 DataGrid
                var dataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                // 获取 DataGrid 中的所有行
                var rows = dataGrid.Rows;
                try
                {
                    Assert.IsNotNull(rows);
                }
                finally
                {
                    app.Close();
                }
            }
        }
        // 测试用例2：用户输入单个查询条件，点击查询按钮，系统应该显示满足该查询条件的所有蜕变关系信息。
        [TestMethod, Priority(2)]
        public void MetamorphicRelationshipQueryTests_Test002()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
               
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                //进入MR Display页面
                children[3].Click();
                Thread.Sleep(1000);

                //ApplicationName文本框输入amax-java
                var DimensionOfInputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                DimensionOfInputPatternTextBox.Text = "amax-java";
                Thread.Sleep(1000);

                //点击查询按钮
                var QueryBNutton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Query")).AsButton();
                QueryBNutton.Click();
                Thread.Sleep(1000);
                // 找到 DataGrid
                var dataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                // 获取 DataGrid 中的所有行
                var rows = dataGrid.Rows;
                try
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        Assert.AreEqual("amax-java", rows[i].Cells[7].Value);
                    }
                }
                finally
                {
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        // 测试用例3：用户输入多个查询条件，点击查询按钮，系统应该显示同时满足多个查询条件的所有蜕变关系信息。
        [TestMethod, Priority(3)]
        public void MetamorphicRelationshipQueryTests_Test003()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
              
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                // 进入MR Display页面
                children[3].Click();
                Thread.Sleep(1000);

                // ApplicationName文本框输入cos-python
                var ApplicationNameTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_ApplicationName")).AsTextBox();
                ApplicationNameTextBox.Text = "cos-python";
                Thread.Sleep(1000);

                // DimensionOfInputPattern文本框输入1
                var DimensionOfInputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfInputPattern")).AsTextBox();
                DimensionOfInputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                // DimensionOfOutputPattern文本框输入1
                var DimensionOfOutputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfOutputPattern")).AsTextBox();
                DimensionOfOutputPatternTextBox.Text = "1";
                Thread.Sleep(1000);

                // 点击查询按钮
                var QueryBNutton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Query")).AsButton();
                QueryBNutton.Click();
                Thread.Sleep(1000);

                // 找到 DataGrid
                var dataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                // 获取 DataGrid 中的所有行
                var rows = dataGrid.Rows;
                try
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        Assert.AreEqual("cos-python", rows[i].Cells[7].Value);
                    }
                }
                finally
                {
                    //关闭应用程序
                    app.Close();
                }
            }
        }
        //测试用例4：用户输入非法查询条件，点击查询按钮，系统应该弹出输入非法的提示框
        [TestMethod, Priority(4)]
        public void MetamorphicRelationshipQueryTests_Test004()
        {
            using (var app = Application.Launch(AbsolutePathResolver.GetSolutionPath()))
            {
                
                var automation = new UIA3Automation();
                var mainWindow = app.GetMainWindow(automation);
                var children = mainWindow.FindAllChildren().ToArray();
                // 进入MR Display页面
                children[3].Click();
                Thread.Sleep(1000);

                // DimensionOfInputPattern文本框输入0
                var DimensionOfInputPatternTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TextBox_DimensionOfInputPattern")).AsTextBox();
                DimensionOfInputPatternTextBox.Text = "0";
                Thread.Sleep(1000);

                // 点击查询按钮
                var QueryBNutton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Button_Query")).AsButton();
                QueryBNutton.Click();
                Thread.Sleep(1000);

                // 找到 DataGrid
                var dataGrid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("DataGrid_MR")).AsDataGridView();
                // 获取 DataGrid 中的所有行
                var rows = dataGrid.Rows;
                try
                {
                    // 获取提示信息
                    var message = mainWindow.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                    var actual = message.Name;
                    var expected = "DimensionOfInputpattern必须为正整数";
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
