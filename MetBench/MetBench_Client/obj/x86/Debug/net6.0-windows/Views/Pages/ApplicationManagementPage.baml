   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  5      I)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  5      ?$clr-namespace:MetBench_Client.ModelsMetBench_Client.Models  5	      ;    5MetBench_Client.Views.Pages.ApplicationManagementPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  5      9
viewmodels(clr-namespace:MetBench_Client.ViewModels  5      5local)clr-namespace:MetBench_Client.Views.Pages  5      2models1$clr-namespace:MetBench_Client.Models  5	      A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5
         TextFillColorPrimaryBrush  N� 
Foreground#  �   5       N� Title$ ApplicationManagementPage��5      .��5      �  � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��1* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5      $��15(�6%      HeadersVisibility$ Column=�6T      AutoGenerateColumns$ False��5   �      CanUserAddRows$ False��6  4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_Application��6)    Stylet.Xaml.View   ActionTarget 5      �� *	ViewModel+	 �� MouseDoubleClick	 5      !  Stylet.Xaml.ActionExtension  *show+
 �� SelectedItem
 66   �� * ViewModel.DataGridSelectedItem+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=���6�   �� *ViewModel.Data+$ PropertyChanged=�$ TwoWay=� � 	Resources 5        CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%�   &	       �� 5      " ��67   .�5      ��  � ��$ Center=�6:   �� 5       � ��$ Center=�6<   �� 5        � ��$ WrapWithOverflow=�6?   5!      5"         Columns 5$      4  .System.Windows.Controls.DataGridTemplateColumn  5%      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6*      MinWidth$ 10Q�66      
IsReadOnly$
 True��6D      Header 5&      �� 5'      $��Number��6$   $��Center=�62   $��Center=�6O   6l      CellTemplate 5)      �� 5*      .R�5+      �� $��Center=�6�   $��Center=�6�   ��6(   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Path$ Header�6�   5,      5.      0	  *System.Windows.Controls.DataGridTextColumn	  50      1
  +System.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [ 6%   $	 200 6v   $
 True��6�    
  Binding 6]   �� *Name+ 51      �� 52      $��Center=�6$   $��Center=�6A   $
��Name��6]   6k   54      	  56      # [ 6%   $	 200 6}   $
 True��6�    6]   �� *Description+ 57      �� 58      $��Center=�6$   $��Center=�6A   $��Description��6]   6r   5:      	  5<      # [ 6%   $	 200 6�   $
 True��6�    6]   �� *ProgrammingLanguage+ 5=      �� 5>      $��Center=�6$   $��Center=�6A   $��ProgrammingLanguage��6]   6z   5@      	  5C      # [ 6%   $	 200 6}   $
 True��6�    6]   �� *LinesOfCode+ 5D      �� 5E      $��Center=�6$   $��Center=�6A   $��
LineOfCode��6]   6q   5G      	  5I      # [ 6%   $	 200 6z   $
 True��6�    6]   �� *
CodeName+ 5J      �� 5K      $��Center=�6$   $��Center=�6A   $��SoftwareUnderTest��6]   6x   5M      	  5O      # [ 6%   $	 200 6�   $
 True��6�    6]   �� *SourceTestCaseName+ 5P      �� 5Q      $��Center=�6$   $��Center=�6A   $��SourceTestCase��6]   6u   5S      	  5U      # [ 6%   $	 250 6u   $
 True��6�    6]   �� *DOI+ 5V      �� 5W      $��Center=�6$   $��Center=�6A   $	��DOI��6]   6j   5Y      	  5[      # [ 6%   $
 True��6u    6]   �� *Url+ 5\      �� 5]      $��Center=�6$   $��Center=�6A   $	��Url��6]   6j   5_      5a      � 5b   
   $��1��6    � ColumnDefinitions 5c      �� 5d      $��3* �6#   �� 5e      $��5* �6#   �� 5f      $��3* �6#    5h      �� 5i      �� 5j      $��2* �6    �� 5k      $��2* �6    �� 5l      $��2* �6    .+�5n      �� -   5         ��Name$ stackpanel1��5n   '   $��0��6    �� Orientation$ 
Horizontal=�6<   $��Center=�6U   .+�5o      �� $��IdApplication��6   $��5 0 0 0q�61   $��Center=�6B     Wpf.Ui.Controls.TextBox  5p      -   5           Name$  txbIdApplication��5p      $��5 0 0 0q�67   $��Center=�6H   $	��150��6j   !   ClearButtonEnabled$! False��5q      " �� 
IsReadOnly$
" True��5r      # �� TextWrapping$
# Wrap=�5s      ��5t      �� *ViewModel.IdApplication+$ PropertyChanged=�$ TwoWay=�5v      �� 5w      -   5        $ stackpanel2��5w   '   $��1��6   $ 
Horizontal=�6<   $��Center=�6U   .+�5x      �� $
��Name��6   $��55 0 0 0q�6(   $��Center=�6:     5y      -   5        $  txbName��5y      $��5 0 0 0q�6.   $��Center=�6?   $	��150��6a   $
# Wrap=�5z      ��5{      �� *ViewModel.Name+$ PropertyChanged=�$ TwoWay=�5}      �� 5      $��*��6   
���  ��$��5 0 0 0q�66   $��Center=�6G   5�      �� 5�      -   5        $ stackpanel3��5�   '   $��2��6   $ 
Horizontal=�6<   $��Center=�6U   .+�5�      �� $��Description��6   $��20 0 0 0q�6/   $��Center=�6A     5�      -   5        $  rtxbDescriptiont��5�      $��5 0 0 0q�67   $��Center=�6H   $	��150��6j   $��60��6v   $
# Wrap=�5�      ��5�      �� *ViewModel.Description+$ PropertyChanged=�$ TwoWay=�5�      5�      �� 5�      -   5        $ stackpanel4��5�   '   $��3��6   $ 
Horizontal=�6<   $��Center=�6U   .+�5�      �� $��ProgrammingLanguage��6   $��5 0 0 0q�67   $��Center=�6H     5�      -	   5        $  txbProgrammingLanguage��5�      $��5 0 0 0q�6=   $��Center=�6N   $	��100��6p   $
# Wrap=�5�      ��5�      �� *ViewModel.ProgrammingLanguage+$ PropertyChanged=�$ TwoWay=�5�      �� 5�      $��*��6   
���  ��$��5 0 0 0q�66   $��Center=�6G   5�      �� 5�      -
   5        $ stackpanel5��5�   7   $��0��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $��
LineOfCode��6   $��54 0 0 0q�6.   $��Center=�6@     5�      -   5        $  txbLineOfCode��5�      $��5 0 0 0q�64   $��Center=�6E   $	��200��6g   $
# Wrap=�5�      ��5�      �� *ViewModel.LineOfCode+$ PropertyChanged=�$ TwoWay=�5�      5�      �� 5�      -   5        $ stackpanel6��5�   7   $��1��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $��SoftwareUnderTest��6   $��5 0 0 0q�65   $��Center=�6F     5�      -   5        $  txbSoftwareUnderTest��5�      $��5 0 0 0q�6;   $��Center=�6L   $	��100��6n   $
# Wrap=�5�      ��5�      �� *ViewModel.Codename+  Wpf.Ui.Controls.Button  5�      $   
Appearance$
$ Info=�6   $��X��6.    5�      �� *	ViewModel+��5�        *btnBack_Click+  5�      $$ Transparent=�6   $	��Upl��65   $��5 0 0 0q�6C    5�      �� *	ViewModel+��5�        *btnAddCode_Click+  5�      $$ Transparent=�6   $��Unzip��65   $��5 0 0 0q�6E    5�      �� *	ViewModel+��5�        *btnExtractCode_Click+�� 5�      $��*��6   
���  ��$��5 0 0 0q�66   $��Center=�6G   5�      �� 5�      -   5        $ stackpanel7��5�   7   $��2��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $��SourceTestCase��6   $��28 0 0 0q�62   $��Center=�6D     5�      -   5        $  txbSourceTestCase��5�      $��5 0 0 0q�68   $��Center=�6I   $	��100��6k   $
# Wrap=�5�      ��5�      �� *ViewModel.SourceTestCasename+  5�      $
$ Info=�6   $��X��6.    5�      �� *	ViewModel+��5�        *btnSourceTestCaseBack_Click+  5�      $$ Transparent=�6   $	��Upl��65   $��5 0 0 0q�6C    5�      �� *	ViewModel+��5�        *btnAddSourceTestCase_Click+5�      �� 5�      -   5        $ stackpanel8��5�   7   $��3��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $	��DOI��6   $��	105 0 0 0q�6'   $��Center=�6:     5�      -   5        $  txbDOI��5�      $��5 0 0 0q�6-   $��Center=�6>   $	��200��6`   $
# Wrap=�5�      ��5�      �� *ViewModel.DOI+$ PropertyChanged=�$ TwoWay=�5�      5�      �� 5�      -   5        $ stackpanel9��5�   7   $��0��6   $��2��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $	��Url��6   $��55 0 0 0q�6'   $��Center=�69     5�      -   5        $  txbUrl��5�      $��5 0 0 0q�6-   $��Center=�6>   $	��160��6`   $
# Wrap=�5�      ��5�      �� *ViewModel.Url+$ PropertyChanged=�$ TwoWay=�5�      5�      �� 5�      -   5        $ stackpanel10��5�   7   $��1��6   $��2��6'   $ 
Horizontal=�6M   $��Center=�6f   .+�5�      �� $��
DomainName��6   $��20 0 0 0q�6.   $��Center=�6@   �� 5�      $��5 0 0 0q�6   $	��120��6,   % �� 
IsEditable$
% True��6g   & �� Text& 68   �� *ViewModel.SelectedDomainNames+��6y   �� *ViewModel.DomainExs+��5�      �� 5�      ' �� DataType%   MetBench_Client.Models.DomainEx"'  6'   .R�5�      �� $ 
Horizontal=�6)   .+�5�   "   �� ( d� 	IsChecked( 6+   �� *ViewModel.IsChecked+�� 5�   "   $��5 0 0 0q�6,   ��6=   �� *Domain.Name+5�      5�      5�      5�      5�      � 5�   
   $��2��6    5�      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�        $��0��6   $��70,0,0,0q�6(   $$ Primary=�6:   $	��Add��6O   $	��100��6]   $��40��6i    5�      �� *	ViewModel+��5�        *btnAdd_Click+  5�      $��1��6   $��70,0,0,0q�6(   $$ Primary=�6:   $
��Edit��6O   $	��100��6^   $��40��6j    5�      �� *	ViewModel+��5�        *btnModify_Click+  5�      $��2��6   $��70,0,0,0q�6(   $$ Primary=�6:   $	��Del��6O   $	��100��6]   $��40��6i    5�      �� *	ViewModel+��5�        *btnDelect_Click+  5�      $��3��6   $��70,0,0,0q�6(   $$ Primary=�6:   $��Reset��6O   $	��100��6_   $��40��6k    5�      �� *	ViewModel+��5�        *btnCancel_Click+5�      5�      5�      