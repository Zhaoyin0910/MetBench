   M S B A M L   `   `   `  ����J  FMetBench_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:MetBench_Client.Views.WindowsMetBench_Client.Views.Windows  5      M+clr-namespace:MetBench_Client.Views.WindowsMetBench_Client.Views.Windows  5      .    (MetBench_Client.Views.Windows.MainWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:MetBench_Client.Views.Windows     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      =pages+clr-namespace:MetBench_Client.Views.Windows     5      F BWpf.Ui.Tray, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null6tray+http://schemas.lepo.co/wpfui/2022/xaml/tray  5	      A =Wpf.Ui, Version=3.0.4.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5
      S OHandyControl, Version=3.5.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      -   5        $
��1450��5      $	��820��5         TextFillColorPrimaryBrush#���   5      "  Wpf.Ui.Controls.FluentWindow!    ExtendsContentIntoTitleBar$
  True��5         WindowBackdropType$
 Mica=�5         WindowCornerPreference$ Round=�5       � WindowStartupLocation$ CenterScreen=�5       � Title 5      �� *ViewModel.ApplicationTitle+ �� Mode$ OneWay=� J� InputBindings 5T      �� 5U   
   
 �� Key$ F��5V   
    �� 	Modifiers$ Controlf�5X   
   	 �� Command	 5W   
   �� 
 �� ElementName$
 AutoSuggestBox�� �� Path$ FocusCommand�5X   
   .��5[      �  � RowDefinitions 5\   
   �� 5]      $
��Auto �6   �� 5^      $��* �6   .+�5a   
   $  Wpf.Ui.Controls.NavigationView  -   5          Name$ RootNavigation��5b   
   $��1��5c   
      PaneDisplayMode$
 Left=�5f   
      OpenPaneLength$	 310Q�5g   
   $��Times New Roman)�5l   
   $��40(�5m   
      FooterMenuItemsSource 5d   
   �� *ViewModel.NavigationFooter+$ OneWay=�   MenuItemsSource 5e   
   �� *ViewModel.NavigationItems+$ OneWay=�   BreadcrumbBar 5k   
   �� $
 BreadcrumbBar��   AutoSuggestBox 5p      $  Wpf.Ui.Controls.AutoSuggestBox  5q      -   5          Name$ AutoSuggestBox��5q   $      PlaceholderText$ Search��6<      Icon 5r      '  !Wpf.Ui.Controls.IconSourceElement  5s         
IconSource. 5t      &   Wpf.Ui.Controls.SymbolIconSource     Symbol$ Search24=�62   5u      5w         Header 5y      �� 5z      $��42,32,42,20q�6   .+�5{      #  Wpf.Ui.Controls.BreadcrumbBar  -   5          Name$ BreadcrumbBar��5{   '   $��Center=�6>   $
��Left=�6Z   $��Times New Roman)�6u   $��30(�6�   5|      5~        Wpf.Ui.Controls.TitleBar  5�   
   $��0��5�   
      Title 5�      �� *ViewModel.ApplicationTitle+$ OneWay=�   Icon 5�        Wpf.Ui.Controls.ImageIcon  5�         Source$< 6pack://application:,,,/Assets/applicationIcon-256.png ��6   5�      5�      5�      