   M S B A M L   `   `   `  ���� J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  J FMetBench_Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:MetBench_Domain;assembly=MetBench_DomainMetBench_Domain ?$clr-namespace:MetBench_Client.ModelsMetBench_Client.Models  G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  2    ,MetBench_Client.Views.Pages.MRManagementPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:MetBench_Client.Views.Pages     A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  Cmodels6clr-namespace:MetBench_Domain;assembly=MetBench_Domain  2models1$clr-namespace:MetBench_Client.Models  9
viewmodels(clr-namespace:MetBench_Client.ViewModels     TextFillColorPrimaryBrush  N� 
Foreground#  �    N� Title$ MRManagementPage��.���  � RowDefinitions �� $��3* ��� $��3* ��� $��1* �.+�&   System.Windows.Controls.DataGrid  -     Name$ datagrid��$��0��$��15(�   HeadersVisibility$ Column=�   AutoGenerateColumns$ False��   CanUserAddRows$ False��4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_MR�� �� SelectedItem �� *ViewModel.DataGridSelectItem+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=����� *ViewModel.Data+$
 PropertyChanged=�  Stylet.Xaml.View   ActionTarget �� *	ViewModel+ �� MouseDoubleClick !  Stylet.Xaml.ActionExtension  *show+ � 	Resources   CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%o   &	       �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ WrapWithOverflow=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 70    MinWidth$ 10Q�   
IsReadOnly$
 True��   Header �� $��Number��$��Center=�$��Center=�   CellTemplate �� .R��� $��Center=�$��Center=�����  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Path$ Header�0	  *System.Windows.Controls.DataGridTextColumn	  1
  +System.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [ $	 250 $
 True�� 
  Binding �� *Description+ �� $��Center=�$��Center=�$��Description��	  # [ $	 250 $
 True�� �� *	Context+ �� $��Center=�$��Center=�$��Context��	  # [ $	 200 $
 True�� �� *
Constraint+ �� $��Center=�$��Center=�$��
Constraint��	  # [ $	 200 $
 True�� �� *	OrderOfMR+ �� $��Center=�$��Center=�$��	OrderOfMR��  $
 True�� �� $��Center=�$��Center=�$��InputPattern�� �� .R��� $
��Auto=�$��Center=�$��Center=�.���� ���� *InputPatternImagepath+  $
 True�� �� $��Center=�$��Center=�$��OutputPattern�� �� .R��� $
��Auto=�$��Center=�$��Center=�.���� ���� *OutputPatternImagepath+	  # [ $	 200 $
 True�� �� *ApplicationName+ �� $��Center=�$��Center=�$��ApplicationName��	  # [ $	 200 $
 True�� �� *
DomainName+ �� $��Center=�$��Center=�$��
DomainName��  $	 200 $
 True�� �� $��Center=�$��Center=�$��	Operation�� �� .R��� $	��100��$��50��$��14(�$��
Execute MT��$��Center=�$ 	Button_MT�� �� *	ViewModel+��  *btn_MTExcute+���� *IdMR+� $��1�� � ColumnDefinitions �� $��4* ��� $��4* ��� $��3* �.+�� $��0�� �� �� $��2* ��� $��2* ��� $��2* �.+��� -    ��Name$ stackpanel1��$��0�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $
��IdMR��$��50 0 0 0q�$��Center=�  Wpf.Ui.Controls.TextBox  -      Name$  txbIdMR��$��5 0 0 0q�$��Center=�$	��250��!   ClearButtonEnabled$! False��" �� 
IsReadOnly$
" True������ *ViewModel.IdMR+$
 PropertyChanged=��� -   $ stackpanel2��$��1��$ 
Horizontal=�$��Center=�.+��� $��Description��$��10 0 0 0q�$��Center=�  -   $  rtxbDescription��$��5 0 0 0q�$��Center=�$	��250��$��60��# �� TextWrapping$
# Wrap=����� *ViewModel.Description+$
 PropertyChanged=�$	 TwoWay=��� -   $ stackpanel3��$��2��$ 
Horizontal=�$��Center=�.+��� $��Context��$��35 0 0 0q�$��Center=�  -   $  
txbContext��$��5 0 0 0q�$��Center=�$	��250��$��60��$
# Wrap=����� *ViewModel.Context+$
 PropertyChanged=�$	 TwoWay=��� -   $ stackpanel4��$��3��$ 
Horizontal=�$��Center=�.+��� $��
Constraint��$��20 0 0 0q�$��Center=�  -	   $  txbConstraint��$��5 0 0 0q�$��Center=�$	��250��$��60��$
# Wrap=����� *ViewModel.Constraint+$
 PropertyChanged=�$	 TwoWay=�� $��1�� �� �� $��2* ��� $��2* ��� $��2* �.+��� -
   $ stackpanel5��$��0��$��1��$ 
Horizontal=�$��Center=�.+��� $��InputPattern��$��50 0 0 0q�$��Center=�  -   $  txbInputPattern��$��5 0 0 0q�$��Center=�$	��200��$
# Wrap=����� *ViewModel.InputPattern+$
 PropertyChanged=�$	 TwoWay=��� $��*��
���  ��$��5 0 0 0q�$��Center=��� -   $ stackpanel6��$��1��$��1��$ 
Horizontal=�$��Center=�.+��� $��OutputPattern��$��40 0 0 0q�$��Center=�  -   $  txbOutputPattern��$��5 0 0 0q�$��Center=�$	��200��$
# Wrap=����� *ViewModel.OutputPattern+$
 PropertyChanged=�$	 TwoWay=��� $��*��
���  ��$��5 0 0 0q�$��Center=��� -   $ stackpanel7��$��2��$��1��$ 
Horizontal=�$��Center=�.+��� $��DimensionOfInputPattern��$��15 0 0 0q�$��Center=�  -   $   txbDimensionOfInputPattern��$��5 0 0 0q�$��Center=�$	��150������ *#!ViewModel.DimensionOfInputPattern+$
 PropertyChanged=�$	 TwoWay=��� $��*��
���  ��$��5 0 0 0q�$��Center=��� -   $ stackpanel8��$��3��$��1��$ 
Horizontal=�$��Center=�.+��� $��DimensionOfOutputPattern��$��5 0 0 0q�$��Center=�  -   $!  txbDimensionOfOutputPattern��$��5 0 0 0q�$��Center=�$	��150������ *$"ViewModel.DimensionOfOutputPattern+$
 PropertyChanged=�$	 TwoWay=��� $��*��
���  ��$��5 0 0 0q�$��Center=�� $��2�� �� $��3* ��� $��2* ��� $��2* �.+��� -   $ stackpanel9��$��0��$��2��$ 
Horizontal=�$��Center=�.+��� $��ApplicationName��$��25 0 0 0q�$��Center=��� $��5 0 0 0q�$	��110��$ �� 
IsEditable$
$ True��% �� Text% �� *$"ViewModel.SelectedApplicationNames+���� *ViewModel.ApplicationExs+���� & �� DataType*   $MetBench_Client.Models.ApplicationEx"&  .R��� $ 
Horizontal=�.+��� ' d� 	IsChecked' �� *ViewModel.IsChecked+�� $��5 0 0 0q����� *Application.Name+�� $��*��
���  ��$��5 0 0 0q�$��Center=��� -   $ stackpanel10��$��1��$��2��$ 
Horizontal=�$��Center=�.+��� $��	OrderOfMR��$��55 0 0 0q�$��Center=��� -   ( ��Name$!( cmbDimensionOfOutputPattern��$��5 0 0 0q�$��Center=�$	��110������ *ViewModel.OrderOfMRList+$
 PropertyChanged=�) �� SelectedIndex) �� *+)ViewModel.OrderOfMR_ComboBoxSelectedIndex+* �� SelectedValue* �� *+)ViewModel.OrderOfMR_ComboBoxSelectedValue+�� $��*��
���  ��$��5 0 0 0q�$��Center=��� -   $ stackpanel11��$��2��$��2��$ 
Horizontal=�$��Center=�.+��� $��RepresentationType��$��0 0 0 0q�$��Center=��� -   $( cmbRepresentationType��$��5 0 0 0q�$��Center=�$	��110������ *" ViewModel.RepresentationTypeList+$
 PropertyChanged=�) �� *42ViewModel.RepresentationType_ComboBoxSelectedIndex+�� $��*��
���  ��$��5 0 0 0q�$��Center=�� $��2�� �� �� �� �� .+�  Wpf.Ui.Controls.Button  $��0��$��70,0,0,0q�+   
Appearance$+ Primary=�$	��Add��$	��100��$��40�� �� *	ViewModel+��  *btnAdd_Click+  $��1��$��70,0,0,0q�$+ Primary=�$
��Edit��$	��100��$��40�� �� *	ViewModel+��  *btnModify_Click+  $��2��$��70,0,0,0q�$+ Primary=�$	��Del��$	��100��$��40�� �� *	ViewModel+��  *btnDelect_Click+  $��3��$��70,0,0,0q�$+ Primary=�$��Reset��$	��100��$��40�� �� *	ViewModel+��  *btnCancel_Click+