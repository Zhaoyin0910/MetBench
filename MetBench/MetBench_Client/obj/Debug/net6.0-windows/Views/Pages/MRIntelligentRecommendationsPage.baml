   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MetBench_Client.Views.PagesMetBench_Client.Views.Pages  5      G(clr-namespace:MetBench_Client.ViewModelsMetBench_Client.ViewModels  5      B    <MetBench_Client.Views.Pages.MRIntelligentRecommendationsPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:MetBench_Client.Views.Pages     5      ?
viewmodels(clr-namespace:MetBench_Client.ViewModels     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      A =Wpf.Ui, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      A =Stylet, Version=1.3.6.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  5	        N� Title$&   MRIntelligentRecommendationsPage��5
         TextFillColorPrimaryBrush N� 
Foreground# �   5      .��5      � -   5        $��0,-2,0,2q�5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5   �      AutoGenerateColumns$ False��6   $
��Left=�61   $	��Top=�6�      CanUserAddRows$ False��6�   $��
67,100,0,0q�6�   4  .System.Windows.Automation.AutomationProperties   AutomationId$ DataGrid_MRSelectedItem��6�   ��6M   �� *ViewModel.Data+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=� � 	Resources 5        CenteredTextBlockStyle	 �� 
TargetType
 �� Property �� Value��%�   &	       �� 5      "	 ��67   .�5      �� 
 � ��$ Center=�6:   �� 5      
 � ��$ Center=�6<   �� 5      
 � ��$ WrapWithOverflow=�6?   5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly$
 True��6&   1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 68      Binding 6p   �� *	Program+   Header 5      �� 5      $��Center=�6$   $��Center=�6A   $��Program��6]   6n   5         5!      # [ 6&    6^   �� *
Similarity+ 5"      �� 5#      $��Center=�6$   $��Center=�6A   $��
Similarity��6]   6q   5%      4  .System.Windows.Controls.DataGridTemplateColumn  5&      $
 True��6)    5'      �� 5(      $��Center=�6$   $��Center=�6A   $��InputPattern��6]   6s      CellTemplate 5*      �� 5+      .R�5,      �� $
��Auto=�6+   $��Center=�6N   $��Center=�6k   .��5-   "   �� ��6(   �� *InputPatternImagepath+5.      5/      51        52      $
 True��6)    53      �� 54      $��Center=�6$   $��Center=�6A   $��OutputPattern��6\   6s    56      �� 57      .R�58      �� $
��Auto=�6+   $��Center=�6N   $��Center=�6k   .��59   "   �� ��6(   �� *OutputPatternImagepath+5:      5;      5=        5>         Width5  /System.Windows.Controls.DataGridLengthConverter$	 170 6)   $
 True��65    5?      �� 5@      $��Center=�6$   $��Center=�6A   $��MR Information��6]   6u    5B      �� 5C      .R�5D      �� -   5        $	��100��5D   A   $��50��6M   $��14(�6Y   $��查看��6h   $��Center=�6u   $ Button_Modify��6�   5E      5G        5H      $	 170 6)   $
 True��65    5I      �� 5J      $��Center=�6$   $��Center=�6A   $��Application Information��6]   6~    5L      �� 5M      .R�5N      �� -   5        $	��100��5N   J   $��50��6V   $��14(�6b   $��查看��6q   $��Center=�6~   $ Button_Modify��6�   5O      5Q        5R      $	 170 6)   $
 True��65    5S      �� 5T      $��Center=�6$   $��Center=�6A   $��Domain Information��6]   6y    5V      �� 5W      .R�5X      �� $	��100��6&   $��50��62   $��14(�6>   $��查看��6M   $��Center=�6Z   $ Button_Modify��6w   5Y      5[      5]      �� 5^   
   $��Program��6   $��12(�6#   $
��Left=�61   $��	70,45,0,0q�6L   $
��Wrap=�6_   $	��Top=�6t   $��16��6�   $��56��6�    J� RenderTransformOrigin$ 1.278,0.662&�6�     Wpf.Ui.Controls.Button  5_   
   $	��UPL��6e   $��
132,38,0,0q�6s   $$ Button_UploadSoftwareUnderTest��6�   $	��Top=�6�   	  Stylet.Xaml.View 	  ActionTarget 6   �� *	ViewModel+��6>   !
  Stylet.Xaml.ActionExtension
  *btnAddCode_Click+6�     Wpf.Ui.Controls.TextBox  5`   
    �� TextWrapping$
 Wrap=�6   $��sin.java��6*   $��189,36,773,0q�6:   $ TextBox_SoftwareUnderTest��6P   $	��Top=�6�    � Cursor$ IBeam��6�     5a   
   $��×��6?   $��
329,37,0,0q�6K   $��39��6_   $ -1.446,-0.531&�6j   $# Button_ClearSoftwareUnderTest��6�   $	��Top=�6�    6   �� *	ViewModel+6�     5b   
   $��IR��6u   $��
385,36,0,0q�6�   $( "Button_IntellingentRecommendations��6�   $��80��6�   $	��Top=�6�    6   �� *	ViewModel+��6>   
  *" btn_MRIntelligentRecommendations+6�   5c      5e      