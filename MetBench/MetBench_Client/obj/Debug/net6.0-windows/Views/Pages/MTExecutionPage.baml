   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  5      J FMetBench_Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:MetBench_Domain;assembly=MetBench_DomainMetBench_Domain 5      G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  5
      1    +MetBench_Client.Views.Pages.MTExecutionPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:MetBench_Client.Views.Pages     5      Imodels6clr-namespace:MetBench_Domain;assembly=MetBench_Domain     5      A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  5      A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5	      9
viewmodels(clr-namespace:MetBench_Client.ViewModels  5
         TextFillColorPrimaryBrush  N� 
Foreground#  �   5       N� Title$ MTExecutionPage��5      .��5      �  � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��8* �6   �� 5      $��1* �6   .+�5   
     Wpf.Ui.Controls.DataGrid  -   5          Name$ datagrid��5   �  $��0��6   $	��Top=�6$   $��2q�6<   
�������$��
10 0 10 10q�6h   $��Center=�6|   &   System.Windows.Controls.DataGrid   AutoGenerateColumns$ False��6�      CanUserAddRows$ False��6  4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_MRSelectedItem��6�  ��6�   �� *ViewModel.Data+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�	 �� SelectedItem	 6(  �� * ViewModel.DataGridSelectedItem+$ TwoWay=�$ PropertyChanged=�  Stylet.Xaml.View
   ActionTarget
 5      �� *	ViewModel+   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header 5      �� 5       $��Center=�6$   $��Center=�6A   $��InputPattern��6]   6s      CellTemplate 5"      �� 5#      .R�5$      �� $
��Auto=�6+   $��Center=�6N   $��Center=�6k   .��5%   "   �� ��6(   �� *InputPatternImagepath+5&      5'      5)        5*       5+      �� 5,      $��Center=�6$   $��Center=�6A   $��OutputPattern��6\   6s    5.      �� 5/      .R�50      �� $
��Auto=�6+   $��Center=�6N   $��Center=�6k   .��51   "   �� ��6(   �� *OutputPatternImagepath+52      53      55      57      � 59   
   $��1��6    � ColumnDefinitions 5:      �� 5;      $��4* �6#   �� 5<      $��6* �6#   .+�5>      � $��0��6   $��0 10 0 0q�6#    5?      �� 5@      �� 5A      �� 5B      �� 5C      �� 5D      �� 5E      $��2* �6$   .+�5G      �� -   5         ��Name$ stackpanel1��5G   +   $��0��6    �� Orientation$ 
Horizontal=�6@   $��Center=�6Y   .+�5H      �� $��CodeName��6    $��25 0 0 0q�61   $��Center=�6C   �� 5Q      -   5         ��Name$ cmbCodeName��5Q      $��5 0 0 0q�64   $��Center=�6E   $	��140��6g    �� 
IsEditable$
 True��5R       �� Text 5S      �� *ViewModel.CodeName+$ PropertyChanged=���5T      �� *ViewModel.CodeNameOptions+$ PropertyChanged=� �� SelectedIndex 5U      �� *ViewModel.SelectedIndex+$ PropertyChanged=� �� SelectedValue 5V      �� *ViewModel.SelectedValue+$ PropertyChanged=�
 5W      �� *	ViewModel+5Y        Wpf.Ui.Controls.Button  5[         
Appearance$ Primary=�6    $��	Eecute MT��65   $��3 0 0 0q�6I      Icon 5\       '  !Wpf.Ui.Markup.SymbolIconExtension  *CalendarEdit24+
 5]       �� *	ViewModel+��5^       !	  Stylet.Xaml.ActionExtension	  *btn_AutoMR2+5`      �� 5a      -   5        $ stackpanel2��5a   +   $��1��6   $ 
Horizontal=�6@   $��Center=�6Y   .+�5b      �� $��	MinNumber��6    $��20 0 0 0q�62   $��Center=�6D   
  Wpf.Ui.Controls.TextBox
  5c      -   5         
 Name$ txbMinNumber��5c   !   $��5 0 0 0q�68   $��Center=�6I   $	��170��6k    �� TextWrapping$
 Wrap=�5d      ��5e      �� *ViewModel.MinParam+$ PropertyChanged=�$ TwoWay=�  5g      $ Primary=�6    $��Reset��66   $��3 0 0 0q�6F    5h   !     * TextBulletListSquareSettings20+
 5i   !   �� *	ViewModel+��5j   !   	  *
btn_Cancle+5l      �� 5m      -   5        $ stackpanel3��5m   +   $��2��6   $ 
Horizontal=�6@   $��Center=�6Y   .+�5n      �� $��	MaxNumber��6    $��15 0 0 0q�62   $��Center=�6D   
  5o      -   5        $ txbMaxNumber��5o   !   $��5 0 0 0q�68   $��Center=�6I   $	��170��6k   $
 Wrap=�5p      ��5q      �� *ViewModel.MaxParam+$ TwoWay=�$ PropertyChanged=�5s      �� 5t      -   5        $ stackpanel4��5t   +   $��3��6   $ 
Horizontal=�6@   $��Center=�6Y   .+�5u      �� $��ExecuteNumb��6    $��5 0 0 0q�64   $��Center=�6E   
  5v      -	   5        $ txbExecuteNumb��5v   !   $��5 0 0 0q�69   $��Center=�6J   $	��170��6l   $
 Wrap=�5w      ��5x      �� *ViewModel.ExecutNumber+$ TwoWay=�$ PropertyChanged=�5z      �� 5{      -
   5        $ stackpanel5��5{   +   $��4��6   $ 
Horizontal=�6@   $��Center=�6Y   .+�5|      �� $��	Threshold��6    $��32 0 0 0q�62   $��Center=�6D   
  5}      -   5        $ txbThreshold��5}   !   $��5 0 0 0q�69   $��Center=�6J   $	��170��6l   $
 Wrap=�5~      ��5      �� *ViewModel.Threshold+$ TwoWay=�$ PropertyChanged=�5�      5�      � 5�      $��1��6   $��0 10 0 0q�6#    5�      �� 5�      $��1* �6$   �� 5�      $��6* �6$   .+�5�      �  5�      �� 5�      �� 5�      .+�5�      �� -   5        $ stackpanel6��5�   ?   $��0��6"   $��0��6/   $ 
Horizontal=�6T   $��Center=�6m   $��Right=�6�   $��0 0 5 0q�6�   .+�5�      �� $
��Pass��6$   $��30 0 0 0q�61   $��Center=�6C   
  5�      -   5        $ txbPass��5�   %   $��15 0 0 0q�67   $��Center=�6I   $	��130��6k   $
 Wrap=�5�   "   ��5�   "   �� *ViewModel.Passrate+$ TwoWay=�$ PropertyChanged=�5�      �� 5�      -   5        $ stackpanel7��5�   ?   $��0��6"   $��1��6/   $ 
Horizontal=�6T   $��Center=�6m   .+�5�      �� $
��Fail��6$   $��32 0 0 0q�61   $��Center=�6C   
  5�      -   5        $ txbFail��5�   %   $��15 0 0 0q�67   $��Center=�6I   $	��130��6k   $
 Wrap=�5�   "   ��5�   "   �� *ViewModel.Failurerate+$ TwoWay=�$ PropertyChanged=�5�      5�        Wpf.Ui.Controls.Image  5�      $��1��6      CornerRadius$ 4��6(   ���	#33000000$	��420��6Q   $	��430��6^   $��Center=�6j      Source 5�      �� *ViewModel.Image+5�      5�      � 5�   
   $��2��6   .+�5�      � $��20��6�   $��2q�6�    � IsIndeterminate 6   �� *ViewModel.IsIndeterminate+$ PropertyChanged=�$ TwoWay=�y�6�   �� *ViewModel.Visibility+$ PropertyChanged=�$ TwoWay=�6�   5�      5�      5�      