   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  5      G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  5	      6    0MetBench_Client.Views.Pages.DomainManagementPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:MetBench_Client.Views.Pages     5      A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  5      9
viewmodels(clr-namespace:MetBench_Client.ViewModels  5	         TextFillColorPrimaryBrush  N� 
Foreground#  �   5       N� Title$ DomainManagementPage��5      .��5      �  � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��2* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��0��5      $��15(�6B      HeadersVisibility$ Column=�6P      AutoGenerateColumns$ False��5   ;      CanUserAddRows$ False��6W   4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_Domain��6n   ��5   k   �� *ViewModel.Data+ �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem 6�   �� * ViewModel.DataGridSelectedItem+$ PropertyChanged=�	 �� Mode$	 TwoWay=�  Stylet.Xaml.View
   ActionTarget
 5      �� *	ViewModel+ �� MouseDoubleClick 5      !  Stylet.Xaml.ActionExtension  *show+ � 	Resources 5        CenteredTextBlockStyle �� 
TargetType �� Property �� Value��%�   &	       �� 5      " ��67   .�5      ��  � ��$ Center=�6:   �� 5        � ��$ Center=�6<   �� 5!       � ��$ WrapWithOverflow=�6?   5"      5#         Columns 5%      4  .System.Windows.Controls.DataGridTemplateColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6*      MinWidth$ 10Q�66      
IsReadOnly$
 True��6D      Header 5)      �� 5*      $��Number��6$   $��Center=�62   $��Center=�6O   6l      CellTemplate 5,      �� 5-      .R�5.      �� $��Center=�6�   $��Center=�6�   ��6(   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� Path$ Header�6�   5/      51      0	  *System.Windows.Controls.DataGridTextColumn	  53      1
  +System.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [ 6%   $	 200 6v   $
 True��6�    
  Binding 6]   �� *Name+ 54      �� 55      $��Center=�6$   $��Center=�6A   $
��Name��6]   6k   57      	  59      # [ 6%   $	 810 6}   $
 True��6�    6]   �� *Description+ 5:      �� 5;      $��Center=�6$   $��Center=�6A   $��Description��6]   6r   5=      5@      � 5A   
   $��1��6    � ColumnDefinitions 5B      �� 5C      �� 5D       5F      �� 5G      �� 5H      .+�5J      �� -   5         ��Name$ stackpanel1��5J   '   $��0��6    �� Orientation$ 
Horizontal=�6<   $��Center=�6U   .+�5K      �� $��IdDomain��6   $��48 0 0 0q�6,   $��Center=�6>     Wpf.Ui.Controls.TextBox  5L      -   5          Name$ txbIdDomain��5L      $��5 0 0 0q�62   $��Center=�6C   $	��250��6e       ClearButtonEnabled$  False��5M      ! �� 
IsReadOnly$
! True��5N      " �� TextWrapping$
" Wrap=�5O      ��5P      �� *ViewModel.IdDomain+$ PropertyChanged=�$	 TwoWay=�5R      �� 5S      -   5        $ stackpanel2��5S   (   $��1��6   $ 
Horizontal=�6=   $��Center=�6V   .+�5T      �� $
��Name��6   $��75 0 0 0q�6(   $��Center=�6:     5U      -   5        $ txbName��5U      $��5 0 0 0q�6.   $��Center=�6?   $	��250��6a   $
" Wrap=�5V      ��5W      �� *ViewModel.Name+$ PropertyChanged=�$	 TwoWay=�5Y      �� 5[      $��*��6   
���  ��$��5 0 0 0q�66   $��Center=�6G   5\      �� 5]      -   5        $ stackpanel3��5]   7   $��0��6   $��1��6'   $ 
Horizontal=�6L   $��Center=�6e   .+�5^      �� $��
Desciption��6   $��50 0 0 0q�6.   $��Center=�6@     5_      -   5        $ txbDesciption��5_      $��5 0 0 0q�64   $��Center=�6E   $	��250��6g   $
" Wrap=�5`      ��5a      �� *ViewModel.Description+$ PropertyChanged=�$	 TwoWay=�5c      5e      5f      � 5g   
   $��2��6    5h      �� 5i      �� 5j      �� 5k      �� 5l      .+�5n        Wpf.Ui.Controls.Button  $��0��6   $��70,0,0,0q�6(   #   
Appearance$# Primary=�6:   $	��Add��6O   $	��100��6]   $��40��6i   
 5o      �� *	ViewModel+��5p        *btnAdd_Click+  5r      $��1��6   $��70,0,0,0q�6(   $# Primary=�6:   $
��Edit��6O   $	��100��6^   $��40��6j   
 5s      �� *	ViewModel+��5t        *btnModify_Click+  5v      $��2��6   $��70,0,0,0q�6(   $# Primary=�6:   $	��Del��6O   $	��100��6]   $��40��6i   
 5w      �� *	ViewModel+��5x        *btnDelect_Click+  5z      $��3��6   $��70,0,0,0q�6(   $# Primary=�6:   $��Reset��6O   $	��100��6_   $��40��6k   
 5{      �� *	ViewModel+��5|        *btnCancel_Click+5~      5      5�      