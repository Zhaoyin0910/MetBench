   M S B A M L   `   `   `  ���� J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  ?$clr-namespace:MetBench_Client.ModelsMetBench_Client.Models  /    )MetBench_Client.Views.Pages.MRDisplayPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:MetBench_Client.Views.Pages     ?
viewmodels(clr-namespace:MetBench_Client.ViewModels     A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  1models$clr-namespace:MetBench_Client.Models     TextFillColorPrimaryBrush  N� 
Foreground#  �    N� Title$ MRDisplayPage��.���  � RowDefinitions �� $��3* ��� $��2* ��� $��1* �.+�&   System.Windows.Controls.DataGrid  -   $��dataGrid��$��0��$��15(�   HeadersVisibility$ Column=�   AutoGenerateColumns$ False��   CanUserAddRows$ False��4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_MR�� �� SelectedItem �� *ViewModel.DataGridSelectItem+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� Mode$	 TwoWay=����� *ViewModel.Data+$ PropertyChanged=�$	 TwoWay=�
 � 	Resources
   CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%o   &	       �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ WrapWithOverflow=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 70    MinWidth$ 10Q�   
IsReadOnly$
 True��   Header �� $��Number��$��Center=�$��Center=�   CellTemplate �� .R��� $��Center=�$��Center=�����  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Path$ Header�0  *System.Windows.Controls.DataGridTextColumn  1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ $	 250 $
 True��   Binding �� *Description+ �� $��Center=�$��Center=�$��Description��  # [ $	 200 $
 True�� �� *	Context+ �� $��Center=�$��Center=�$��Context��  # [ $	 200 $
 True�� �� *
Constraint+ �� $��Center=�$��Center=�$��
Constraint��  # [ $	 200 $
 True�� �� *	OrderOfMR+ �� $��Center=�$��Center=�$��	OrderOfMR��  $
 True�� �� $��Center=�$��Center=�$��InputPattern�� �� .R��� $
��Auto=�$��Center=�$��Center=�.���� ���� *InputPatternImagepath+  $
 True�� �� $��Center=�$��Center=�$��OutputPattern�� �� .R��� $
��Auto=�$��Center=�$��Center=�.���� ���� *OutputPatternImagepath+  # [ $	 200 $
 True�� �� *ApplicationName+ �� $��Center=�$��Center=�$��ApplicationName��$( "DataGridTextColumn_ApplicationName��  # [ $	 200 $
 True�� �� *
CodeName+ �� $��Center=�$��Center=�$��CodeName��  # [ $	 200 $
 True�� �� *
DomainName+ �� $��Center=�$��Center=�$��
DomainName��  $	 200 $
 True�� �� $��Center=�$��Center=�$��	Operation�� �� .R�	  Wpf.Ui.Controls.Button	   	  
Appearance$ Transparent=�$	��100��$��50��$��14(�$
��Edit��$��Center=�$ Button_Modify��
  Stylet.Xaml.View 
  ActionTarget �� *	ViewModel+��!  Stylet.Xaml.ActionExtension  *EditSelectedMR+���� *IdMR+� $��1�� �� �� ��  � ColumnDefinitions �� �� �� .+��� -    ��Name$ stackpanel1��$��0�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $��	OrderOfMR��$��55 0 0 0q�$��Center=��� -    ��Name$ cmbOrderOfMR��$��15 0 0 0q�$��Center=�$	��120��  �� SelectedIndex  �� *+)ViewModel.OrderOfMR_ComboBoxSelectedIndex+$ PropertyChanged=�! �� SelectedValue! �� *+)ViewModel.OrderOfMR_ComboBoxSelectedValue+$ PropertyChanged=����� *ViewModel.OrderOfMRList+$ PropertyChanged=��� -   $ stackpanel2��$��1��$ 
Horizontal=�$��Center=�.+��� $��RepresentationType��$��10 0 0 0q�$��Center=��� -   $ cmbRepresentationType��$��5 0 0 0q�$��Center=�$	��120��  �� *42ViewModel.RepresentationType_ComboBoxSelectedIndex+$ PropertyChanged=����� *" ViewModel.RepresentationTypeList+$ PropertyChanged=��� -   $ stackpanel3��$��0��$��1��$ 
Horizontal=�$��Center=�.+��� $��DimensionOfInputPattern��$��13 0 0 0q�$��Center=�  Wpf.Ui.Controls.TextBox  -   "  Name$ " txbDimensionOfInputPattern��$��10 0 0 0q�$��Center=�$	��120������ *#!ViewModel.DimensionOfInputPattern+$ PropertyChanged=�$	 TwoWay=��� -   $ stackpanel4��$��1��$��1��$ 
Horizontal=�$��Center=�.+��� $��DimensionOfOutputPattern��$��6 0 0 0q�$��Center=�  -	   $!" txbDimensionOfOutputPattern��$��5 0 0 0q�$��Center=�$	��120������ *$"ViewModel.DimensionOfOutputPattern+$ PropertyChanged=�$	 TwoWay=��� -
   $ stackpanel5��$��0��$��2��$ 
Horizontal=�$��Center=�.+��� $��ApplicationName��$��6 0 0 0q�$��Center=�  -   $" txbApplicationName��$��5 0 0 0q�$��Center=�$	��120������ *ViewModel.ApplicationName+$ PropertyChanged=�$	 TwoWay=��� -   $ stackpanel6��$��1��$��2��$ 
Horizontal=�$��Center=�.+��� $��
DomainName��$��30 0 0 0q�$��Center=�  -   $" txbDomainName��$��5 0 0 0q�$��Center=�$	��120������ *ViewModel.DomainName+$ PropertyChanged=�$	 TwoWay=��� -   $ stackpanel7��$��2��$��2��$ 
Horizontal=�$��Center=�.+�	  -   # 	 Name$# btnQuery��$ Primary=�$ 	  Icon*  $Wpf.Ui.Controls.IconElementConverter$$ Fluent24 $��Query��$��	140 0 0 0q�$��Center=�$	��100��$��40�� �� *	ViewModel+% �� Click%   *btnQuery_Click+