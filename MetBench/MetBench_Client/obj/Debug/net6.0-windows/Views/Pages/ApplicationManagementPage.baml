   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  5      I)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  5      ?$clr-namespace:MetBench_Client.ModelsMetBench_Client.Models  5	      ;    5MetBench_Client.Views.Pages.ApplicationManagementPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  5      9
viewmodels(clr-namespace:MetBench_Client.ViewModels  5      5local)clr-namespace:MetBench_Client.Views.Pages  5      2models1$clr-namespace:MetBench_Client.Models  5	      A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5
      S OHandyControl, Version=3.5.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  69      TextFillColorPrimaryBrush  N� 
Foreground#  �   5       N� Title$ ApplicationManagementPage��5      .��5      �  � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��40 �6   �� 5      $��3* �6   �� 5      $��1* �6   .+�5   
   � $��0��6   .+�5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0 3 0 5q�66   .+�5      �� $��ApplicationName��6   $��Center=�63   $��15 0 0 0q�6N   �� 5      $��10 0 0 0q�6�   $	��150��6�   ��6   �� *" ViewModel.ApplicationNameBoxText+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�  Stylet.Xaml.View   ActionTarget 6|   �� *	ViewModel+6�     Wpf.Ui.Controls.Button  5         
Appearance$
 Info=�6   $��Query��6.   $��5 0 0 0q�6>    6O   �� *	ViewModel+��5      !  Stylet.Xaml.ActionExtension  *reload_ItemsSource+5         Wpf.Ui.Controls.DataGrid  5!      -   5          Name$ datagrid��5!      $	��320��6,   $��2q�6=   
�������$��
10 0 10 10q�6i   $��15(�6}   &   System.Windows.Controls.DataGrid	   HeadersVisibility$	 Column=�6�   
   AutoGenerateColumns$
 False��5#   �      CanUserAddRows$ False��6  4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_Application��6"   5"      �� *	ViewModel+ �� MouseDoubleClick 5#        *show+ �� SelectedItem 6/   �� * ViewModel.DataGridSelectedItem+$ PropertyChanged=�$ TwoWay=���6�   �� *ViewModel.Data+$ PropertyChanged=�$ TwoWay=� � 	Resources 5%        CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%�   &	       �� 5&      " ��6;   .�5'      ��  � ��$ Center=�6>   �� 5(       � ��$ Center=�6@   �� 5)       � ��$ WrapWithOverflow=�6C   5*      5+         Columns 5-      4  .System.Windows.Controls.DataGridTemplateColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$ 70	 6.      MinWidth$ 10Q�6:      
IsReadOnly$
 True��6H      Header 5/      �� 50      $��Number��6(   $��Center=�66   $��Center=�6S   6p      CellTemplate 52      �� 53      .R�54   "   �� $��Center=�6�   $��Center=�6�   ��6,   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)
  #System.Windows.Controls.DataGridRow" 
  �� Path$ Header�6�   55      57      0  *System.Windows.Controls.DataGridTextColumn  59      1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 6)   $	 200	 6z   $
 True��6�      Binding 6a   �� *Name+ 5:      �� 5;      $��Center=�6(   $��Center=�6E   $
��Name��6a   6o   5=        5?      # [ 6)   $	 200	 6�   $
 True��6�    6a   �� *Description+ 5@      �� 5A      $��Center=�6(   $��Center=�6E   $��Description��6a   6v   5C        5E      # [ 6)   $	 200	 6�   $
 True��6�    6a   �� *ProgrammingLanguage+ 5F      �� 5G      $��Center=�6(   $��Center=�6E   $��ProgrammingLanguage��6a   6~   5I        5L      # [ 6)   $	 200	 6�   $
 True��6�    6a   �� *LinesOfCode+ 5M      �� 5N      $��Center=�6(   $��Center=�6E   $��
LineOfCode��6a   6u   5P        5R      # [ 6)   $	 200	 6~   $
 True��6�    6a   �� *
CodeName+ 5S      �� 5T      $��Center=�6(   $��Center=�6E   $��SoftwareUnderTest��6a   6|   5V        5X      # [ 6)   $	 200	 6�   $
 True��6�    6a   �� *SourceTestCaseName+ 5Y      �� 5Z      $��Center=�6(   $��Center=�6E   $��SourceTestCase��6a   6y   5\        5^      # [ 6)   $	 250	 6y   $
 True��6�    6a   �� *DOI+ 5_      �� 5`      $��Center=�6(   $��Center=�6E   $	��DOI��6a   6n   5b        5d      # [ 6)   $
 True��6y    6a   �� *Url+ 5e      �� 5f      $��Center=�6(   $��Center=�6E   $	��Url��6a   6n   5h      5j      5k      5l      � 5n   
   $��1��6   .+�5o      &   HandyControl.Controls.Pagination  -   5          Name$ 
pagination��5o      $�� 0 2 5 0q�61   $��30��6D       IsJumpEnabled$
  True��6P   $y�Visible=�6e   $��Right=�6z   !   MaxPageInterval$! 3��5p   ]   $��Center=�6�   "   MaxPageCount" 5o   �   �� *ViewModel.MaxPageCount+$ PropertyChanged=�#   	PageIndex# 6�   �� *ViewModel.PageIndex+$ PropertyChanged=�$ TwoWay=�$   DataCountPerPage$ 5p      �� *ViewModel.DataCountPerPage+$ PropertyChanged=� 6q   �� *	ViewModel+%   PageUpdated% 6�     *reload_ItemsSource+6�   5q      � 5r   
   $��2��6   & � ColumnDefinitions& 5s      �� 5t      $��3* �6#   �� 5u      $��5* �6#   �� 5v      $��3* �6#    5x      �� 5y      �� 5z      $��2* �6    �� 5{      $��2* �6    �� 5|      $��2* �6    .+�5~      �� -   5        ' ��Name$' stackpanel1��5~   '   $��0��6   $ 
Horizontal=�6<   $��Center=�6U   .+�5      �� $��IdApplication��6   $��5 0 0 0q�62   $��Center=�6C     Wpf.Ui.Controls.TextBox  5�      -   5        (  Name$( txbIdApplication��5�      $��5 0 0 0q�68   $��Center=�6I   $	��150��6k   )   ClearButtonEnabled$) False��5�      * �� 
IsReadOnly$
* True��5�      + �� TextWrapping$
+ Wrap=�5�      ��5�      �� *ViewModel.IdApplication+$ PropertyChanged=�$ TwoWay=�5�      �� 5�      -   5        $' stackpanel2��5�   '   $��1��6   $ 
Horizontal=�6<   $��Center=�6U   .+�5�      �� $
��Name��6   $��55 0 0 0q�6(   $��Center=�6:     5�      -   5        $( txbName��5�      $��5 0 0 0q�6.   $��Center=�6?   $	��150��6a   $
+ Wrap=�5�      ��5�      �� *ViewModel.Name+$ PropertyChanged=�$ TwoWay=�5�      �� 5�      $��*��6   
���  ��$��5 0 0 0q�66   $��Center=�6G   5�      �� 5�      -   5        $' stackpanel3��5�   '   $��2��6   $ 
Horizontal=�6<   $��Center=�6U   .+�5�      �� $��Description��6   $��20 0 0 0q�60   $��Center=�6B     5�      -   5        $( rtxbDescriptiont��5�      $��5 0 0 0q�67   $��Center=�6H   $	��150��6j   $
+ Wrap=�5�      ��5�      �� *ViewModel.Description+$ PropertyChanged=�$ TwoWay=�5�      5�      �� 5�      -	   5        $' stackpanel4��5�   '   $��3��6   $ 
Horizontal=�6<   $��Center=�6U   .+�5�      �� $��ProgrammingLanguage��6   $��5 0 0 0q�68   $��Center=�6I     5�      -
   5        $( txbProgrammingLanguage��5�      $��5 0 0 0q�6>   $��Center=�6O   $	��100��6q   $
+ Wrap=�5�      ��5�      �� *ViewModel.ProgrammingLanguage+$ PropertyChanged=�$ TwoWay=�5�      �� 5�      $��*��6   
���  ��$��5 0 0 0q�66   $��Center=�6G   5�      �� 5�      -   5        $' stackpanel5��5�   7   $��0��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $��
LineOfCode��6   $��54 0 0 0q�6/   $��Center=�6A     5�      -   5        $( txbLineOfCode��5�      $��5 0 0 0q�65   $��Center=�6F   $	��200��6h   $
+ Wrap=�5�      ��5�      �� *ViewModel.LineOfCode+$ PropertyChanged=�$ TwoWay=�5�      5�      �� 5�      -   5        $' stackpanel6��5�   7   $��1��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $��SoftwareUnderTest��6   $��5 0 0 0q�66   $��Center=�6G     5�      -   5        $( txbSoftwareUnderTest��5�      $��5 0 0 0q�6=   $��Center=�6N   $	��100��6p   $
+ Wrap=�5�      ��5�      �� *ViewModel.Codename+  5�      $
 Info=�6   $��X��6.    5�      �� *	ViewModel+��5�        *btnBack_Click+  5�      $ Primary=�6   $	��Upl��61   $��5 0 0 0q�6?    5�      �� *	ViewModel+��5�        *btnAddCode_Click+,   Icon, 5�      '  !Wpf.Ui.Markup.SymbolIconExtension  *ArrowUpload24+  5�      $ Primary=�6   $��Unzip��61   $��5 0 0 0q�6A    5�      �� *	ViewModel+��5�        *btnExtractCode_Click+, 5�        *FolderZip24+�� 5�      $��*��6   
���  ��$��5 0 0 0q�66   $��Center=�6G   5�      �� 5�      -   5        $' stackpanel7��5�   7   $��2��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $��SourceTestCase��6   $��28 0 0 0q�62   $��Center=�6D     5�      -   5        $( txbSourceTestCase��5�      $��5 0 0 0q�69   $��Center=�6J   $	��100��6l   $
+ Wrap=�5�      ��5�      �� *ViewModel.SourceTestCasename+  5�      $
 Info=�6   $��X��6.    5�      �� *	ViewModel+��5�        *btnSourceTestCaseBack_Click+  5�      $ Primary=�6   $	��Upl��61   $��5 0 0 0q�6@    5�      �� *	ViewModel+��5�        *btnAddSourceTestCase_Click+5�      �� 5�      -   5        $' stackpanel8��5�   7   $��3��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $	��DOI��6   $��	105 0 0 0q�6'   $��Center=�6;     5�      -   5        $( txbDOI��5�      $��5 0 0 0q�6-   $��Center=�6?   $	��200��6a   $
+ Wrap=�5�      ��5�      �� *ViewModel.DOI+$ PropertyChanged=�$ TwoWay=�5�      5�      �� 5�      -   5        $' stackpanel9��5�   7   $��0��6   $��2��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5�      �� $	��Url��6   $��55 0 0 0q�6'   $��Center=�69     5�      -   5        $( txbUrl��5�      $��5 0 0 0q�6-   $��Center=�6>   $	��160��6a   $
+ Wrap=�5�      ��5�      �� *ViewModel.Url+$ PropertyChanged=�$ TwoWay=�5�      5�      �� 5�      -   5        $' stackpanel10��5�   7   $��1��6   $��2��6'   $ 
Horizontal=�6M   $��Center=�6f   .+�5�      �� $��
DomainName��6   $��5 0 0 0q�6.   $��Center=�6@   �� 5�      $��5 0 0 0q�6   $	��200��6,   - �� 
IsEditable$
- True��6e   . �� Text. 6=   �� *ViewModel.SelectedText+��6w   �� *ViewModel.DomainExs+��5�      �� 5�      / �� DataType%   MetBench_Client.Models.DomainEx"/  6'   .R�5�      �� $ 
Horizontal=�6)   .+�5�   "   �� 0 d� 	IsChecked0 6+   �� *	IsChecked+�� 5�   "   $��5 0 0 0q�6,   $��Center=�6Z   ��6=   �� *Domain.Name+6Z   5�      5�      5�      5�      5�      � 5�   
   $��3��6   & 5�      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�        $��0��6   $��70,0,0,0q�6(   $ Primary=�6:   $	��Add��6O   $	��100��6]   $��40��6i    5�      �� *	ViewModel+��5�        *btnAdd_Click+, 5�        *
GuestAdd24+  5�      $��1��6   $��70,0,0,0q�6(   $ Primary=�6:   $
��Edit��6O   $	��100��6^   $��40��6j    5      �� *	ViewModel+��5       *btnModify_Click+, 5       *CalendarEdit24+  5     $��2��6   $��70,0,0,0q�6(   $ Primary=�6:   $	��Del��6O   $	��100��6]   $��40��6i    5     �� *	ViewModel+��5       *btnDelect_Click+, 5       *DeleteDismiss24+  5	     $��3��6   $��70,0,0,0q�6(   $ Primary=�6:   $��Reset��6O   $	��100��6_   $��40��6k    5
     �� *	ViewModel+��5       *btnCancel_Click+, 5       * TextBulletListSquareSettings20+5     5     5     