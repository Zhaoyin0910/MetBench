   M S B A M L   `   `   `  ���� J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  1    +MetBench_Client.Views.Pages.MTExecutionPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:MetBench_Client.Views.Pages     A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  9
viewmodels(clr-namespace:MetBench_Client.ViewModels     TextFillColorPrimaryBrush  N� 
Foreground#  �    N� Title$ MTExecutionPage��.���  � RowDefinitions �� $��3* ��� $��6* �.+�&   System.Windows.Controls.DataGrid  -     Name$ datagrid��$��0��$��Center=�   AutoGenerateColumns$ False��   CanUserAddRows$ False��4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_MRSelectedItem������ *ViewModel.Data+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�	 �� SelectedItem	 �� * ViewModel.DataGridSelectedItem+$ TwoWay=�$ PropertyChanged=�
   Columns
 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header �� $��Center=�$��Center=�$��InputPattern��   CellTemplate �� .R��� $
��Auto=�$��Center=�$��Center=�.���� ���� *InputPatternImagepath+   �� $��Center=�$��Center=�$��OutputPattern�� �� .R��� $
��Auto=�$��Center=�$��Center=�.���� ���� *OutputPatternImagepath+� $��1�� � ColumnDefinitions �� $��3* ��� $��6* �.+�� $��0�� �� �� �� �� �� �� $��2* �.+��� -    ��Name$ stackpanel1��$��0�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $��CodeName��$��25 0 0 0q�$��Center=�  Wpf.Ui.Controls.TextBox  -     Name$ txbCodeName��$��5 0 0 0q�$��Center=�$	��100�� �� 
IsReadOnly$
 True��   ClearButtonEnabled$ False�� �� TextWrapping$
 Wrap=����� *ViewModel.CodeName+$ TwoWay=�$ PropertyChanged=�  Wpf.Ui.Controls.Button     
Appearance$ Primary=�$��	Eecute MT��$��3 0 0 0q�  Stylet.Xaml.View   ActionTarget �� *	ViewModel+��!  Stylet.Xaml.ActionExtension  *btn_AutoMR2+�� -   $ stackpanel2��$��1��$ 
Horizontal=�$��Center=�.+��� $��	MinNumber��$��20 0 0 0q�$��Center=�  -   $ txbMinNumber��$��5 0 0 0q�$��Center=�$	��130��$
 Wrap=����� *ViewModel.MinParam+$ PropertyChanged=�$ TwoWay=�  $ Primary=�$��Reset��$��3 0 0 0q� �� *	ViewModel+��  *
btn_Cancle+�� -   $ stackpanel3��$��2��$ 
Horizontal=�$��Center=�.+��� $��	MaxNumber��$��15 0 0 0q�$��Center=�  -   $ txbMaxNumber��$��5 0 0 0q�$��Center=�$	��130��$
 Wrap=����� *ViewModel.MaxParam+$ TwoWay=�$ PropertyChanged=��� -   $ stackpanel4��$��3��$ 
Horizontal=�$��Center=�.+��� $��ExecuteNumb��$��5 0 0 0q�$��Center=�  -	   $ txbExecuteNumb��$��5 0 0 0q�$��Center=�$	��130��$
 Wrap=����� *ViewModel.ExecutNumber+$ TwoWay=�$ PropertyChanged=��� -
   $ stackpanel5��$��4��$ 
Horizontal=�$��Center=�.+��� $��	Threshold��$��32 0 0 0q�$��Center=�  -   $ txbThreshold��$��5 0 0 0q�$��Center=�$	��130��$
 Wrap=����� *ViewModel.Threshold+$ TwoWay=�$ PropertyChanged=�� $��1�� �� $��1* ��� $��6* �.+��  �� �� .+��� -   $ stackpanel6��$��0��$��0��$ 
Horizontal=�$��Center=�$��Right=�$��0 0 5 0q�.+��� $
��Pass��$��32 0 0 0q�$��Center=�  -   $ txbPass��$��15 0 0 0q�$��Center=�$	��130��$
 Wrap=����� *ViewModel.Passrate+$ TwoWay=�$ PropertyChanged=��� -   $ stackpanel7��$��0��$��1��$ 
Horizontal=�$��Center=�.+��� $
��Fail��$��32 0 0 0q�$��Center=�  -   $ txbFail��$��15 0 0 0q�$��Center=�$	��130��$
 Wrap=����� *ViewModel.Failurerate+$ TwoWay=�$ PropertyChanged=�	  Wpf.Ui.Controls.Image	  $��1�� 	  CornerRadius$ 4�����	#33000000$	��420��$	��430��$��Center=� 	  Source �� *ViewModel.Image+