   M S B A M L   `   `   `  ���� J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  I)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  ?$clr-namespace:MetBench_Client.ModelsMetBench_Client.Models  ;    5MetBench_Client.Views.Pages.ApplicationManagementPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  9
viewmodels(clr-namespace:MetBench_Client.ViewModels  5local)clr-namespace:MetBench_Client.Views.Pages  2models1$clr-namespace:MetBench_Client.Models  A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml     TextFillColorPrimaryBrush  N� 
Foreground#  �    N� Title$ ApplicationManagementPage��.���  � RowDefinitions �� $��3* ��� $��3* ��� $��1* �.+�&   System.Windows.Controls.DataGrid  -     Name$ datagrid��$��15(�   HeadersVisibility$ Column=�   AutoGenerateColumns$ False��   CanUserAddRows$ False��4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_Application��  Stylet.Xaml.View   ActionTarget �� *	ViewModel+	 �� MouseDoubleClick	 !  Stylet.Xaml.ActionExtension  *show+
 �� SelectedItem
 �� * ViewModel.DataGridSelectedItem+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=����� *ViewModel.Data+$ PropertyChanged=�$ TwoWay=� � 	Resources   CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%o   &	       �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ WrapWithOverflow=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 70    MinWidth$ 10Q�   
IsReadOnly$
 True��   Header �� $��Number��$��Center=�$��Center=�   CellTemplate �� .R��� $��Center=�$��Center=�����  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Path$ Header�0	  *System.Windows.Controls.DataGridTextColumn	  1
  +System.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [ $	 200 $
 True�� 
  Binding �� *Name+ �� $��Center=�$��Center=�$
��Name��	  # [ $	 200 $
 True�� �� *Description+ �� $��Center=�$��Center=�$��Description��	  # [ $	 200 $
 True�� �� *ProgrammingLanguage+ �� $��Center=�$��Center=�$��ProgrammingLanguage��	  # [ $	 200 $
 True�� �� *LinesOfCode+ �� $��Center=�$��Center=�$��
LineOfCode��	  # [ $	 200 $
 True�� �� *
CodeName+ �� $��Center=�$��Center=�$��SoftwareUnderTest��	  # [ $	 200 $
 True�� �� *SourceTestCaseName+ �� $��Center=�$��Center=�$��SourceTestCase��	  # [ $	 250 $
 True�� �� *DOI+ �� $��Center=�$��Center=�$	��DOI��	  # [ $
 True�� �� *Url+ �� $��Center=�$��Center=�$	��Url��� $��1�� � ColumnDefinitions �� $��3* ��� $��5* ��� $��3* � �� �� $��2* ��� $��2* ��� $��2* �.+��� -    ��Name$ stackpanel1��$��0�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $��IdApplication��$��5 0 0 0q�$��Center=�  Wpf.Ui.Controls.TextBox  -      Name$  txbIdApplication��$��5 0 0 0q�$��Center=�$	��150��!   ClearButtonEnabled$! False��" �� 
IsReadOnly$
" True��# �� TextWrapping$
# Wrap=����� *ViewModel.IdApplication+$ PropertyChanged=�$ TwoWay=��� -   $ stackpanel2��$��1��$ 
Horizontal=�$��Center=�.+��� $
��Name��$��55 0 0 0q�$��Center=�  -   $  txbName��$��5 0 0 0q�$��Center=�$	��150��$
# Wrap=����� *ViewModel.Name+$ PropertyChanged=�$ TwoWay=��� $��*��
���  ��$��5 0 0 0q�$��Center=��� -   $ stackpanel3��$��2��$ 
Horizontal=�$��Center=�.+��� $��Description��$��20 0 0 0q�$��Center=�  -   $  rtxbDescriptiont��$��5 0 0 0q�$��Center=�$	��150��$��60��$
# Wrap=����� *ViewModel.Description+$ PropertyChanged=�$ TwoWay=��� -   $ stackpanel4��$��3��$ 
Horizontal=�$��Center=�.+��� $��ProgrammingLanguage��$��5 0 0 0q�$��Center=�  -	   $  txbProgrammingLanguage��$��5 0 0 0q�$��Center=�$	��100��$
# Wrap=����� *ViewModel.ProgrammingLanguage+$ PropertyChanged=�$ TwoWay=��� $��*��
���  ��$��5 0 0 0q�$��Center=��� -
   $ stackpanel5��$��0��$��1��$ 
Horizontal=�$��Center=�.+��� $��
LineOfCode��$��54 0 0 0q�$��Center=�  -   $  txbLineOfCode��$��5 0 0 0q�$��Center=�$	��200��$
# Wrap=����� *ViewModel.LineOfCode+$ PropertyChanged=�$ TwoWay=��� -   $ stackpanel6��$��1��$��1��$ 
Horizontal=�$��Center=�.+��� $��SoftwareUnderTest��$��5 0 0 0q�$��Center=�  -   $  txbSoftwareUnderTest��$��5 0 0 0q�$��Center=�$	��100��$
# Wrap=����� *ViewModel.Codename+  Wpf.Ui.Controls.Button  $   
Appearance$
$ Info=�$��X�� �� *	ViewModel+��  *btnBack_Click+  $$ Transparent=�$	��Upl��$��5 0 0 0q� �� *	ViewModel+��  *btnAddCode_Click+  $$ Transparent=�$��Unzip��$��5 0 0 0q� �� *	ViewModel+��  *btnExtractCode_Click+�� $��*��
���  ��$��5 0 0 0q�$��Center=��� -   $ stackpanel7��$��2��$��1��$ 
Horizontal=�$��Center=�.+��� $��SourceTestCase��$��28 0 0 0q�$��Center=�  -   $  txbSourceTestCase��$��5 0 0 0q�$��Center=�$	��100��$
# Wrap=����� *ViewModel.SourceTestCasename+  $
$ Info=�$��X�� �� *	ViewModel+��  *btnSourceTestCaseBack_Click+  $$ Transparent=�$	��Upl��$��5 0 0 0q� �� *	ViewModel+��  *btnAddSourceTestCase_Click+�� -   $ stackpanel8��$��3��$��1��$ 
Horizontal=�$��Center=�.+��� $	��DOI��$��	105 0 0 0q�$��Center=�  -   $  txbDOI��$��5 0 0 0q�$��Center=�$	��200��$
# Wrap=����� *ViewModel.DOI+$ PropertyChanged=�$ TwoWay=��� -   $ stackpanel9��$��0��$��2��$ 
Horizontal=�$��Center=�.+��� $	��Url��$��55 0 0 0q�$��Center=�  -   $  txbUrl��$��5 0 0 0q�$��Center=�$	��160��$
# Wrap=����� *ViewModel.Url+$ PropertyChanged=�$ TwoWay=��� -   $ stackpanel10��$��1��$��2��$ 
Horizontal=�$��Center=�.+��� $��
DomainName��$��20 0 0 0q�$��Center=��� $��5 0 0 0q�$	��120��% �� 
IsEditable$
% True��& �� Text& �� *ViewModel.SelectedDomainNames+���� *ViewModel.DomainExs+���� ' �� DataType%   MetBench_Client.Models.DomainEx"'  .R��� $ 
Horizontal=�.+��� ( d� 	IsChecked( �� *ViewModel.IsChecked+�� $��5 0 0 0q����� *Domain.Name+� $��2�� �� �� �� �� .+�  $��0��$��70,0,0,0q�$$ Primary=�$	��Add��$	��100��$��40�� �� *	ViewModel+��  *btnAdd_Click+  $��1��$��70,0,0,0q�$$ Primary=�$
��Edit��$	��100��$��40�� �� *	ViewModel+��  *btnModify_Click+  $��2��$��70,0,0,0q�$$ Primary=�$	��Del��$	��100��$��40�� �� *	ViewModel+��  *btnDelect_Click+  $��3��$��70,0,0,0q�$$ Primary=�$��Reset��$	��100��$��40�� �� *	ViewModel+��  *btnCancel_Click+