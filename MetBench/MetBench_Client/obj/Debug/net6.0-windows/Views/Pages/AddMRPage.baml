   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  5      G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  5	      +    %MetBench_Client.Views.Pages.AddMRPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:MetBench_Client.Views.Pages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      A =Wpf.Ui, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  5      9
viewmodels(clr-namespace:MetBench_Client.ViewModels  5	        N� Title$  	AddMRPage��5
         TextFillColorPrimaryBrush N� 
Foreground# �   5      .��5      � -   5        .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5   �   $��15(�65      HeadersVisibility$ Column=�6C   $��Center=�6K  $	��256��6h  $��0,10,0,0q�6u  $	��Top=�6�  $
��1080��6�     AutoGenerateColumns$ False��6�     CanUserAddRows$ False��6�  4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_MR��6�   �� SelectedItem 6_   �� *ViewModel.MRSelectedItem+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=���6   �� *ViewModel.Data+$	 PropertyChanged=�
 � 	Resources
 5        CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%�   &	       �� 5      " ��67   .�5      ��  � ��$ Center=�6:   �� 5       � ��$ Center=�6<   �� 5       � ��$ WrapWithOverflow=�6?   5      5         Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6*      MinWidth$ 10Q�66      
IsReadOnly$
 True��6D      Header 5      �� 5      $��Number��6$   $��Center=�62   $��Center=�6O   6l      CellTemplate 5!      �� 5"      .R�5#      �� $��Center=�6�   $��Center=�6�   ��6(   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Path$ Header�6�   5$      5&      0  *System.Windows.Controls.DataGridTextColumn  5(      1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 6%   $	 250 6}   $
 True��6�      Binding 6]   �� *Description+ 5)      �� 5*      $��Center=�6$   $��Center=�6A   $��Description��6]   6r   5,        5.      # [ 6%   $	 250 6y   $
 True��6�    6]   �� *	Context+ 5/      �� 50      $��Center=�6$   $��Center=�6A   $��Context��6]   6n   52        54      # [ 6%   $	 200 6|   $
 True��6�    6]   �� *
Constraint+ 55      �� 56      $��Center=�6$   $��Center=�6A   $��
Constraint��6]   6q   58        5:      # [ 6%   $	 200 6{   $
 True��6�    6]   �� *	OrderOfMR+ 5;      �� 5<      $��Center=�6$   $��Center=�6A   $��	OrderOfMR��6]   6p   5>        5@      $
 True��6)    5A      �� 5B      $��Center=�6$   $��Center=�6A   $��InputPattern��6]   6s    5E      �� 5F      .R�5G      �� $
��Auto=�6+   $��Center=�6N   $��Center=�6k   .��5H   "   �� ��6(   �� *InputPatternImagepath+5I      5J      5L        5N      $
 True��6)    5O      �� 5P      $��Center=�6$   $��Center=�6A   $��OutputPattern��6]   6t    5S      �� 5T      .R�5U      �� $
��Auto=�6+   $��Center=�6N   $��Center=�6k   .��5V   "   �� ��6(   �� *OutputPatternImagepath+5W      5X      5Z        5^      # [ 6%   $	 200 6�   $
 True��6�    6]   �� *ApplicationName+ 5_      �� 5`      $��Center=�6$   $��Center=�6A   $��ApplicationName��6]   6v   5b        5d      # [ 6%   $	 200 6|   $
 True��6�    6]   �� *
DomainName+ 5e      �� 5f      $��Center=�6$   $��Center=�6A   $��
DomainName��6]   6q   5h        5k      $	 200 6*   $
 True��66    5l      �� 5m      $��Center=�6$   $��Center=�6A   $��	Operation��6]   6p    5o      �� 5p      .R�5q      �� -   5        $	��100��5q   <   $��50��6H   $��14(�6T   $��执行MT��6b   $��Center=�6q   $ 	Button_MT��6�   5r      5t      5w      �� 5y   
   $��12(�6   $
��Left=�6"   $��26��6=   $��
82,331,0,0q�6I   $
��Wrap=�6]   $
��IdMR��6q   $	��Top=�6}   $��44��6�   �� 5z   
   $��12(�6   $
��Left=�6"   $��26��6=   $��
48,370,0,0q�6I   $
��Wrap=�6]   $��Descripttion��6q   $	��Top=�6�   $��81��6�   �� 5{   
   $��12(�6   $
��Left=�6"   $��26��6=   $��
67,414,0,0q�6I   $
��Wrap=�6]   $��Context��6q   $	��Top=�6�   $��56��6�    J� RenderTransformOrigin$ 0.746,0.477&�6�   �� 5|   
   $��12(�6   $
��Left=�6  $��137,502,0,0q�66  $	��Top=�6K  $	��120��6c  $��40��6o  $ ComboBox_OrderOfMR��6{   �� SelectedValue 6!   �� *%#ViewModel.CbOrderOfMR_SelectedValue+$	 PropertyChanged=���6�   �� *ViewModel.OrderOfMRList+$	 PropertyChanged=� �� SelectedIndex 6�   �� *+)ViewModel.OrderOfMR_ComboBoxSelectedIndex+6{  �� 5}   
   $��	OrderOfMR��6   $��12(�6%   $
��Left=�63   $��
54,509,0,0q�6N   $
��Wrap=�6b   $	��Top=�6w   $��26��6�   $��75��6�   �� 5~   
   $��RepresentationType��6   $��12(�6.   $
��Left=�6<   $��
10,560,0,0q�6W   $
��Wrap=�6k   $	��Top=�6�   $��22��6�   $	��127��6�   $ 
1.69,2.013&�6�   �� 5   
   $��12(�6   $
��Left=�6�   $��36��6�   $��137,553,0,0q�6�   $	��Top=�6  $	��100��6,  $! ComboBox_RepresentationType��68   6!   �� *(&ViewModel.RtType_ComboBoxSelectedIndex+$	 PropertyChanged=���6�   �� *ViewModel.RtTypeList+$	 PropertyChanged=�68  �� 5�   
   $��12(�6   $
��Left=�6"   $��26��6=   $��408,324,0,0q�6I   $
��Wrap=�6^   $��InputPattertn��6r   $	��Top=�6�   $��81��6�   �� 5�   
   $��12(�6   $
��Left=�6"   $��26��6=   $��392,367,0,0q�6I   $
��Wrap=�6^   $��OutputPattern��6r   $	��Top=�6�   $��94��6�   �� 5�   
   $��12(�6   $
��Left=�6"   $��26��6=   $��332,413,0,0q�6I   $
��Wrap=�6^   $��DimensionOfInputPattern��6r   $	��Top=�6�   $	��159��6�   �� 5�   
   $��12(�6   $
��Left=�6"   $��26��6=   $��326,461,0,0q�6I   $
��Wrap=�6^   $��DimensionOfOutputPattern��6r   $	��Top=�6�   $	��159��6�   	  Wpf.Ui.Controls.Button	  5�   
   $	��Add��6`   $��30��6n   $��352,559,0,0q�6z   $	��Top=�6�   $��77��6�   $ Button_AddMR��6�   
  Stylet.Xaml.View 
  ActionTarget 6   �� *	ViewModel+��6>   !  Stylet.Xaml.ActionExtension  *btnAdd_Click+6�   	  5�   
   $
��Edit��6c   $��30��6r   $��503,558,0,0q�6~   $	��Top=�6�   $��76��6�   $ Button_ModifyMR��6�    6   �� *	ViewModel+��6>     *btnModify_Click+6�   	  5�   
   $	��Del��6c   $��30��6q   $��676,556,0,0q�6}   $	��Top=�6�   $��76��6�   $ Button_DeleteMR��6�    6   �� *	ViewModel+��6>     *btnDelect_Click+6�   �� 5�   
   $
��Left=�6   $��20��6/   $��
56,461,0,0q�6;   $
��Wrap=�6O   $��
Constraint��6c   $	��Top=�6u   $��70��6�     Wpf.Ui.Controls.TextBox  5�   
      ClearButtonEnabled$ False��6    �� 
IsReadOnly$
 True��60     �� TextWrapping$
  Wrap=�6B   $��136,319,843,5q�6�   $	��Top=�6�   $ TextBox_IdMR��6�   ��6V   �� *ViewModel.IdMR+$	 PropertyChanged=�6�     5�   
   -   5        $��136,362,843,0q�5�   �   $	��Top=�6�   $ TextBox_Descripttion��6�   ��6P   �� *ViewModel.Description+$	 PropertyChanged=�6�     5�   
   -   5        $
  Wrap=�5�   P   $��134,409,843,0q�6�   $	��Top=�6�   $ TextBox_Context��6�   ��6d   �� *ViewModel.Context+$	 PropertyChanged=�6�     5�   
   -   5        $
  Wrap=�5�   P   $��134,454,843,0q�6�   $	��Top=�6�   $ TextBox_Constraint��6�   ��6d   �� *ViewModel.Constraint+$	 PropertyChanged=�6�     5�   
   -   5        $
  Wrap=�5�   P   $��507,315,435,5q�6�   $	��Top=�6�   $ TextBox_InputPattern��6�   ��6d   �� *ViewModel.InputPattern+$	 PropertyChanged=�6�     5�   
   -   5        $
  Wrap=�5�   P   $��507,360,435,5q�6�   $	��Top=�6�   $ TextBox_OutputPattern��6�   ��6d   �� *ViewModel.OutputPattern+$	 PropertyChanged=�6�     5�   
   -	   5        $
  Wrap=�5�   P   $��507,405,435,5q�6�   $	��Top=�6�   $% TextBox_DimensionOfInputPattern��6�   ��6d   �� *#!ViewModel.DimensionOfInputPattern+$	 PropertyChanged=�6�     5�   
   -
   5        $
  Wrap=�5�   P   $��507,454,435,5q�6�   $	��Top=�6�   $&  TextBox_DimensionOfOutputPattern��6�   ��6d   �� *$"ViewModel.DimensionOfOutputPattern+$	 PropertyChanged=�6�   �� 5�   
   $��12(�6   $
��Left=�6"   $��26��6=   $��757,325,0,0q�6I   $
��Wrap=�6^   $��ApplicationName��6r   $	��Top=�6�   $	��108��6�     5�   
   -   5        $
  Wrap=�5�   P   $��879,317,71,0q�6�   $	��Top=�6�   $ TextBox_ApplicationName��6�   ��6d   �� *ViewModel.ApplicationName+$	 PropertyChanged=�6�   	  5�   
   $��Reset��6d   $��33��6t   $��823,553,0,0q�6�   $	��Top=�6�   $��80��6�   $ Button_Reset��6�    6   �� *	ViewModel+��6?     *btnCancel_Click+6�   �� 5�   
   -   5        ! ��Name$! DomainName_Cbox��5�     " �� 
IsEditable$
" True��6�   $
{�True��6	  $��879,370,71,0q�64  $	��Top=�6J  $ ComboBox_DomainName��6b  ��6C   �� *ViewModel.checkBoxes+$ TwoWay=�$	 PropertyChanged=�# �� Text# 6�   �� *ViewModel.DomainNames+$ TwoWay=�$	 PropertyChanged=���5�      �� 5�      .R�5�      �� -   5        ��5�       �� *	Content+$ d� 	IsChecked$ 5�       �� *	IsChecked+$ TwoWay=�$	 PropertyChanged=�5�   b   5�      5�      �� 5�   
   $��12(�6   $
��Left=�6"   $��26��6=   $��776,377,0,0q�6I   $
��Wrap=�6^   $��
DomainName��6r   $	��Top=�6�   $��80��6�   �� 5�   
   
���  ��$��15(�6%   $
��Left=�63   $��14��6N   $��731,416,0,0q�6Z   $
��Wrap=�6o   $��*��6�   $	��Top=�6�   $��21��6�   �� 5�   
   
���  ��$��15(�6%   $
��Left=�63   $��14��6N   $��731,369,0,0q�6Z   $
��Wrap=�6o   $��*��6�   $	��Top=�6�   $��21��6�   �� 5�   
   
���  ��$��15(�6%   $
��Left=�63   $��14��6N   $��731,325,0,0q�6Z   $
��Wrap=�6o   $��*��6�   $	��Top=�6�   $��21��6�   �� 5�   
   
���  ��$��15(�6%   $
��Left=�63   $��14��6N   $��732,464,0,0q�6Z   $
��Wrap=�6o   $��*��6�   $	��Top=�6�   $��16��6�   �� 5�   
   $
��Left=�6   $��19��6/   $��
17,283,0,0q�6;   $
��Wrap=�6O   $��Red indicates required��6c   $	��Top=�6�   $	��150��6�   �� 5�   
   
���  ��$��15(�6%   $
��Left=�63   $��14��6N   $��310,513,0,0q�6Z   $
��Wrap=�6o   $��*��6�   $	��Top=�6�   $��18��6�   �� 5�   
   
���  ��$��15(�6%   $
��Left=�63   $��14��6N   $��310,565,0,0q�6Z   $
��Wrap=�6o   $��*��6�   $	��Top=�6�   $��18��6�   �� 5�   
   
���  ��$��15(�6%   $
��Left=�63   $��14��6N   $��1085,326,0,0q�6Z   $
��Wrap=�6p   $��*��6�   $	��Top=�6�   $��21��6�   5�      5�      