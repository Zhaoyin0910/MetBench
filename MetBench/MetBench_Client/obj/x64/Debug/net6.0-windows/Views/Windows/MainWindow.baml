   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:MetBench_Client.Views.WindowsMetBench_Client.Views.Windows  5      M+clr-namespace:MetBench_Client.Views.WindowsMetBench_Client.Views.Windows  5      .    (MetBench_Client.Views.Windows.MainWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:MetBench_Client.Views.Windows     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      =pages+clr-namespace:MetBench_Client.Views.Windows     5      F BWpf.Ui.Tray, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null6tray+http://schemas.lepo.co/wpfui/2022/xaml/tray  5	      A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5
      -   5        $
��1450��5      $	��802��5         TextFillColorPrimaryBrush#���   5      "  Wpf.Ui.Controls.FluentWindow!    ExtendsContentIntoTitleBar$
  True��5         WindowBackdropType$
 Mica=�5         WindowCornerPreference$ Round=�5       � WindowStartupLocation$ CenterScreen=�5       � Title 5      �� *ViewModel.ApplicationTitle+ �� Mode$ OneWay=� J� InputBindings 5S      �� 5T   
   
 �� Key$ F��5U   
    �� 	Modifiers$ Controlf�5W   
   	 �� Command	 5V   
   �� 
 �� ElementName$
 AutoSuggestBox�� �� Path$ FocusCommand�5W   
   .��5Z      �  � RowDefinitions 5[   
   �� 5\      $
��Auto �6   �� 5]      $��* �6   .+�5`   
   $  Wpf.Ui.Controls.NavigationView  -   5          Name$ RootNavigation��5a   
   $��1��5b   
      PaneDisplayMode$
 Left=�5e   
      OpenPaneLength$	 310Q�5f   
   $��Times New Roman)�5k   
   $��40(�5l   
      FooterMenuItemsSource 5c   
   �� *ViewModel.NavigationFooter+$ OneWay=�   MenuItemsSource 5d   
   �� *ViewModel.NavigationItems+$ OneWay=�   BreadcrumbBar 5j   
   �� $
 BreadcrumbBar��   AutoSuggestBox 5o      $  Wpf.Ui.Controls.AutoSuggestBox  5p      -   5          Name$ AutoSuggestBox��5p   $      PlaceholderText$ Search��6<      Icon 5q      '  !Wpf.Ui.Controls.IconSourceElement  5r         
IconSource. 5s      &   Wpf.Ui.Controls.SymbolIconSource     Symbol$ Search24=�62   5t      5v         Header 5x      �� 5y      $��42,32,42,20q�6   .+�5z      #  Wpf.Ui.Controls.BreadcrumbBar  -   5          Name$ BreadcrumbBar��5z   '   $��Center=�6>   $
��Left=�6Z   $��Times New Roman)�6u   $��30(�6�   5{      5}        Wpf.Ui.Controls.TitleBar  5   
   $��0��5�   
      Title 5�      �� *ViewModel.ApplicationTitle+$ OneWay=�   Icon 5�        Wpf.Ui.Controls.ImageIcon  5�         Source$< 6pack://application:,,,/Assets/applicationIcon-256.png ��6   5�      5�      5�      