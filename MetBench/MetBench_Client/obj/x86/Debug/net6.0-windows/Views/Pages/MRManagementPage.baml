   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  5      J FMetBench_Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:MetBench_Domain;assembly=MetBench_DomainMetBench_Domain 5	      ?$clr-namespace:MetBench_Client.ModelsMetBench_Client.Models  5
      G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  5      2    ,MetBench_Client.Views.Pages.MRManagementPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:MetBench_Client.Views.Pages     5      A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  5      A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      Cmodels6clr-namespace:MetBench_Domain;assembly=MetBench_Domain  5	      2models1$clr-namespace:MetBench_Client.Models  5
      9
viewmodels(clr-namespace:MetBench_Client.ViewModels  5         TextFillColorPrimaryBrush  N� 
Foreground#  �   5       N� Title$ MRManagementPage��5      .��5      �  � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��1* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5   �   $��0��6   $��15(�6A      HeadersVisibility$ Column=�6O      AutoGenerateColumns$ False��60     CanUserAddRows$ False��6L  4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_MR��6c   �� SelectedItem 6k   �� *ViewModel.DataGridSelectItem+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=���6�   �� *ViewModel.Data+$
 PropertyChanged=�  Stylet.Xaml.View   ActionTarget 5      �� *	ViewModel+ �� MouseDoubleClick 5      !  Stylet.Xaml.ActionExtension  *show+ � 	Resources 5        CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%�   &	       �� 5      " ��67   .�5       ��  � ��$ Center=�6:   �� 5!       � ��$ Center=�6<   �� 5"       � ��$ WrapWithOverflow=�6?   5#      5$         Columns 5%      4  .System.Windows.Controls.DataGridTemplateColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6*      MinWidth$ 10Q�66      
IsReadOnly$
 True��6D      Header 5'      �� 5(      $��Number��6$   $��Center=�62   $��Center=�6O   6l      CellTemplate 5*      �� 5+      .R�5,      �� $��Center=�6�   $��Center=�6�   ��6(   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Path$ Header�6�   5-      5/      0	  *System.Windows.Controls.DataGridTextColumn	  51      1
  +System.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [ 6%   $	 250 6}   $
 True��6�    
  Binding 6]   �� *Description+ 52      �� 53      $��Center=�6$   $��Center=�6A   $��Description��6]   6r   55      	  57      # [ 6%   $	 250 6y   $
 True��6�    6]   �� *	Context+ 58      �� 59      $��Center=�6$   $��Center=�6A   $��Context��6]   6n   5;      	  5=      # [ 6%   $	 200 6|   $
 True��6�    6]   �� *
Constraint+ 5>      �� 5?      $��Center=�6$   $��Center=�6A   $��
Constraint��6]   6q   5A      	  5C      # [ 6%   $	 200 6{   $
 True��6�    6]   �� *	OrderOfMR+ 5D      �� 5E      $��Center=�6$   $��Center=�6A   $��	OrderOfMR��6]   6p   5G        5I      $
 True��6)    5J      �� 5K      $��Center=�6$   $��Center=�6A   $��InputPattern��6]   6s    5N      �� 5O      .R�5P      �� $
��Auto=�6+   $��Center=�6N   $��Center=�6k   .��5Q   "   �� ��6(   �� *InputPatternImagepath+5R      5S      5U        5W      $
 True��6)    5X      �� 5Y      $��Center=�6$   $��Center=�6A   $��OutputPattern��6]   6t    5\      �� 5]      .R�5^      �� $
��Auto=�6+   $��Center=�6N   $��Center=�6k   .��5_   "   �� ��6(   �� *OutputPatternImagepath+5`      5a      5c      	  5e      # [ 6%   $	 200 6�   $
 True��6�    6]   �� *ApplicationName+ 5f      �� 5g      $��Center=�6$   $��Center=�6A   $��ApplicationName��6]   6v   5i      	  5k      # [ 6%   $	 200 6|   $
 True��6�    6]   �� *
DomainName+ 5l      �� 5m      $��Center=�6$   $��Center=�6A   $��
DomainName��6]   6q   5o        5r      $	 200 6*   $
 True��66    5s      �� 5t      $��Center=�6$   $��Center=�6A   $��	Operation��6]   6p    5v      �� 5w      .R�5x      �� $	��100��6&   $��50��62   $��14(�6>   $��
Execute MT��6L   $��Center=�6a   $ 	Button_MT��6~    5y   &   �� *	ViewModel+��5z   &     *btn_MTExcute+��5{   &   �� *IdMR+5}      5      5�      � 5�   
   $��1��6    � ColumnDefinitions 5�      �� 5�      $��4* �6#   �� 5�      $��4* �6#   �� 5�      $��3* �6#   .+�5�      � $��0��6    5�      �� 5�      �� 5�      $��2* �6$   �� 5�      $��2* �6$   �� 5�      $��2* �6$   .+�5�      �� -   5         ��Name$ stackpanel1��5�   +   $��0��6    �� Orientation$ 
Horizontal=�6@   $��Center=�6Y   .+�5�      �� $
��IdMR��6    $��50 0 0 0q�6,   $��Center=�6>     Wpf.Ui.Controls.TextBox  5�      -   5           Name$  txbIdMR��5�   !   $��5 0 0 0q�62   $��Center=�6C   $	��250��6e   !   ClearButtonEnabled$! False��5�      " �� 
IsReadOnly$
" True��5�      ��5�      �� *ViewModel.IdMR+$
 PropertyChanged=�5�      �� 5�      -   5        $ stackpanel2��5�   +   $��1��6   $ 
Horizontal=�6@   $��Center=�6Y   .+�5�      �� $��Description��6    $��10 0 0 0q�63   $��Center=�6E     5�      -   5        $  rtxbDescription��5�   !   $��5 0 0 0q�6:   $��Center=�6K   $	��250��6m   $��60��6y   # �� TextWrapping$
# Wrap=�5�      ��5�      �� *ViewModel.Description+$
 PropertyChanged=�$	 TwoWay=�5�      5�      �� 5�      -   5        $ stackpanel3��5�   +   $��2��6   $ 
Horizontal=�6@   $��Center=�6Y   .+�5�      �� $��Context��6    $��35 0 0 0q�6/   $��Center=�6A     5�      -   5        $  
txbContext��5�   !   $��5 0 0 0q�65   $��Center=�6F   $	��250��6h   $��60��6t   $
# Wrap=�5�      ��5�      �� *ViewModel.Context+$
 PropertyChanged=�$	 TwoWay=�5�      5�      �� 5�      -   5        $ stackpanel4��5�   +   $��3��6   $ 
Horizontal=�6@   $��Center=�6Y   .+�5�      �� $��
Constraint��6    $��20 0 0 0q�62   $��Center=�6D     5�      -	   5        $  txbConstraint��5�   !   $��5 0 0 0q�68   $��Center=�6I   $	��250��6k   $��60��6w   $
# Wrap=�5�      ��5�      �� *ViewModel.Constraint+$
 PropertyChanged=�$	 TwoWay=�5�      5�      5�      � 5�      $��1��6    5�      �� 5�      �� 5�      $��2* �6$   �� 5�      $��2* �6$   �� 5�      $��2* �6$   .+�5�      �� -
   5        $ stackpanel5��5�   ;   $��0��6   $��1��6+   $ 
Horizontal=�6P   $��Center=�6i   .+�5�      �� $��InputPattern��6    $��50 0 0 0q�64   $��Center=�6F     5�      -   5        $  txbInputPattern��5�   !   $��5 0 0 0q�6:   $��Center=�6K   $	��200��6m   $
# Wrap=�5�      ��5�      �� *ViewModel.InputPattern+$
 PropertyChanged=�$	 TwoWay=�5�      �� 5�      $��*��6    
���  ��$��5 0 0 0q�6:   $��Center=�6K   5�      �� 5�      -   5        $ stackpanel6��5�   ;   $��1��6   $��1��6+   $ 
Horizontal=�6P   $��Center=�6i   .+�5�      �� $��OutputPattern��6    $��40 0 0 0q�65   $��Center=�6G     5�      -   5        $  txbOutputPattern��5�   !   $��5 0 0 0q�6;   $��Center=�6L   $	��200��6n   $
# Wrap=�5�      ��5�      �� *ViewModel.OutputPattern+$
 PropertyChanged=�$	 TwoWay=�5�      �� 5�      $��*��6    
���  ��$��5 0 0 0q�6:   $��Center=�6K   5�      �� 5�      -   5        $ stackpanel7��5�   ;   $��2��6   $��1��6+   $ 
Horizontal=�6P   $��Center=�6i   .+�5�      �� $��DimensionOfInputPattern��6    $��15 0 0 0q�6?   $��Center=�6Q     5�      -   5        $   txbDimensionOfInputPattern��5�   !   $��5 0 0 0q�6E   $��Center=�6V   $	��150��6x   ��5�      �� *#!ViewModel.DimensionOfInputPattern+$
 PropertyChanged=�$	 TwoWay=�5�      �� 5�      $��*��6    
���  ��$��5 0 0 0q�6:   $��Center=�6K   5�      �� 5�      -   5        $ stackpanel8��5�   ;   $��3��6   $��1��6+   $ 
Horizontal=�6P   $��Center=�6i   .+�5�      �� $��DimensionOfOutputPattern��6    $��5 0 0 0q�6@   $��Center=�6Q     5�      -   5        $!  txbDimensionOfOutputPattern��5�   !   $��5 0 0 0q�6F   $��Center=�6W   $	��150��6y   ��5�      �� *$"ViewModel.DimensionOfOutputPattern+$
 PropertyChanged=�$	 TwoWay=�5�      �� 5�      $��*��6    
���  ��$��5 0 0 0q�6:   $��Center=�6K   5�      5�      � 5�      $��2��6    5�      �� 5�      $��3* �6$   �� 5�      $��2* �6$   �� 5�      $��2* �6$   .+�5�      �� -   5        $ stackpanel9��5�   ;   $��0��6   $��2��6+   $ 
Horizontal=�6P   $��Center=�6i   .+�5�      �� $��ApplicationName��6    $��25 0 0 0q�67   $��Center=�6I   �� 5�      $��5 0 0 0q�6   $	��110��60   $ �� 
IsEditable$
$ True��6p   % �� Text% 6<   �� *$"ViewModel.SelectedApplicationNames+��6�   �� *ViewModel.ApplicationExs+��5�      �� 5�      & �� DataType*   $MetBench_Client.Models.ApplicationEx"&  6+   .R�5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� ' d� 	IsChecked' 6/   �� *ViewModel.IsChecked+�� 5   &   $��5 0 0 0q�60   ��6A   �� *Application.Name+5  #   5     5     �� 5     $��*��6    
���  ��$��5 0 0 0q�6:   $��Center=�6K   5     �� 5:     -   5        $ stackpanel10��5:  ;   $��1��6   $��2��6+   $ 
Horizontal=�6Q   $��Center=�6j   .+�5;     �� $��	OrderOfMR��6    $��55 0 0 0q�61   $��Center=�6C   �� 5<     -   5        ( ��Name$!( cmbDimensionOfOutputPattern��5<     $��5 0 0 0q�6D   $��Center=�6U   $	��110��6w   ��5=     �� *ViewModel.OrderOfMRList+$
 PropertyChanged=�) �� SelectedIndex) 5>     �� *+)ViewModel.OrderOfMR_ComboBoxSelectedIndex+* �� SelectedValue* 5?     �� *+)ViewModel.OrderOfMR_ComboBoxSelectedValue+�� 5A     $��*��6    
���  ��$��5 0 0 0q�6:   $��Center=�6K   5B     �� 5C     -   5        $ stackpanel11��5C  ;   $��2��6   $��2��6+   $ 
Horizontal=�6Q   $��Center=�6j   .+�5D     �� $��RepresentationType��6    $��0 0 0 0q�6:   $��Center=�6K   �� 5E     -   5        $( cmbRepresentationType��5E     $��5 0 0 0q�6>   $��Center=�6O   $	��110��6q   ��5F     �� *" ViewModel.RepresentationTypeList+$
 PropertyChanged=�) 5G     �� *42ViewModel.RepresentationType_ComboBoxSelectedIndex+�� 5I     $��*��6    
���  ��$��5 0 0 0q�6:   $��Center=�6K   5J     5K     5S     � 5T  
   $��2��6    5U     �� 5V     �� 5W     �� 5X     �� 5Y     .+�5[       Wpf.Ui.Controls.Button  $��0��6   $��70,0,0,0q�6(   +   
Appearance$+ Primary=�6:   $	��Add��6O   $	��100��6]   $��40��6i    5\     �� *	ViewModel+��5]       *btnAdd_Click+  5_     $��1��6   $��70,0,0,0q�6(   $+ Primary=�6:   $
��Edit��6O   $	��100��6^   $��40��6j    5`     �� *	ViewModel+��5a       *btnModify_Click+  5c     $��2��6   $��70,0,0,0q�6(   $+ Primary=�6:   $	��Del��6O   $	��100��6]   $��40��6i    5d     �� *	ViewModel+��5e       *btnDelect_Click+  5g     $��3��6   $��70,0,0,0q�6(   $+ Primary=�6:   $��Reset��6O   $	��100��6_   $��40��6k    5h     �� *	ViewModel+��5i       *btnCancel_Click+5k     5l     5m     