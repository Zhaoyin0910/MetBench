   M S B A M L   `   `   `  ���� J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  6    0MetBench_Client.Views.Pages.DomainManagementPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:MetBench_Client.Views.Pages     A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  9
viewmodels(clr-namespace:MetBench_Client.ViewModels     TextFillColorPrimaryBrush  N� 
Foreground#  �    N� Title$ DomainManagementPage��.���  � RowDefinitions �� $��3* ��� $��2* ��� $��1* ��� $��1* �.+�&   System.Windows.Controls.DataGrid  -   $��0��$��15(�   HeadersVisibility$ Column=�   AutoGenerateColumns$ False��   CanUserAddRows$ False��4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_Domain������ *ViewModel.Data+ �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem �� * ViewModel.DataGridSelectedItem+$ PropertyChanged=�	 �� Mode$	 TwoWay=�  Stylet.Xaml.View
   ActionTarget
 �� *	ViewModel+ �� MouseDoubleClick !  Stylet.Xaml.ActionExtension  *show+ � 	Resources   CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%o   &	       �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ WrapWithOverflow=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 70    MinWidth$ 10Q�   
IsReadOnly$
 True��   Header �� $��Number��$��Center=�$��Center=�   CellTemplate �� .R��� $��Center=�$��Center=�����  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Path$ Header�0	  *System.Windows.Controls.DataGridTextColumn	  1
  +System.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [ $	 200 $
 True�� 
  Binding �� *Name+ �� $��Center=�$��Center=�$
��Name��	  # [ $	 810 $
 True�� �� *Description+ �� $��Center=�$��Center=�$��Description��� $��1�� � ColumnDefinitions �� ��  �� �� .+��� -    ��Name$ stackpanel1��$��0�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $��IdDomain��$��48 0 0 0q�$��Center=�  Wpf.Ui.Controls.TextBox  -     Name$ txbIdDomain��$��5 0 0 0q�$��Center=�$	��250��    ClearButtonEnabled$  False��! �� 
IsReadOnly$
! True��" �� TextWrapping$
" Wrap=����� *ViewModel.IdDomain+$ PropertyChanged=�$	 TwoWay=��� -   $ stackpanel2��$��1��$ 
Horizontal=�$��Center=�.+��� $
��Name��$��75 0 0 0q�$��Center=�  -   $ txbName��$��5 0 0 0q�$��Center=�$	��250��$
" Wrap=����� *ViewModel.Name+$ PropertyChanged=�$	 TwoWay=��� $��*��
���  ��$��5 0 0 0q�$��Center=��� -   $ stackpanel3��$��0��$��1��$ 
Horizontal=�$��Center=�.+��� $��
Desciption��$��50 0 0 0q�$��Center=�  -   $ txbDesciption��$��5 0 0 0q�$��Center=�$	��250��$
" Wrap=����� *ViewModel.Description+$ PropertyChanged=�$	 TwoWay=�� $��2�� �� �� �� �� .+�  Wpf.Ui.Controls.Button  $��0��$��70,0,0,0q�#   
Appearance$# Primary=�$	��Add��$	��100��$��40��
 �� *	ViewModel+��  *btnAdd_Click+  $��1��$��70,0,0,0q�$# Primary=�$
��Edit��$	��100��$��40��
 �� *	ViewModel+��  *btnModify_Click+  $��2��$��70,0,0,0q�$# Primary=�$	��Del��$	��100��$��40��
 �� *	ViewModel+��  *btnDelect_Click+  $��3��$��70,0,0,0q�$# Primary=�$��Reset��$	��100��$��40��
 �� *	ViewModel+��  *btnCancel_Click+