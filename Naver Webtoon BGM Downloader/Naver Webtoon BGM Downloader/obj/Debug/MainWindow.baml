   M S B A M L   `   `   `  ����W  SNaver Webtoon BGM Downloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Naver_Webtoon_BGM_DownloaderNaver_Webtoon_BGM_Downloader  5   	   -    'Naver_Webtoon_BGM_Downloader.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:Naver_Webtoon_BGM_Downloader     5   	   -   5          � Title$  Bgm Downloader��5   	   $	��315��6    $	��525��6-    � WindowStartupLocation$ CenterScreen=�69    � Icon$ Naver_Webtoon_Icon.ico��6u    � 
ResizeMode$ CanMinimize=�6�    � 	Resources 5
         TextBoxStyle1 �� 
TargetType �� BasedOn �� Property �� Value	 �� 
TargetType
 ��Name ��Name J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName��%  &	        �� 5   
   " ��69    6&   Z� .�5      ��  � ��
 �����6+   �� 5       � ��$ 1q�60   �� 5       � ��$ 1q�6(   �� 5       �@J�	AllowDrop . 6*   �� 5       �@�FocusVisualStyle 61   Z� �� 5       � �� 5      �� 5      "	 ��6&   .R�5      �� $
 bg��6!   ���	#FFABADB3$��1q�6E   ._�5      �� $ PART_ContentHost��6+   # z 6E   5       5      P� 5       �@J�IsMouseOver . . �5   "   �� $ bg��6@    � �� �#28dc196P   �� 5   "   $ bg��6D    � ��$ 1q�6T   5      P� 5       �@J�	IsFocused . . �5   "   �� $ bg��6@    � �� �#28dc196P   �� 5   "   $ bg��6D    � ��$ 1q�6T   5       5"      5$      5%      5'      .��5)      � .+�5*   
   �� $��GreenInkEffect.jpg��6   � 5+   
   -   5        $��TestGrid��5+       .   J� RenderTransformOrigin$ 0.5,0.5&�6;   
������  � ColumnDefinitions 5,      �� 5-      $
��0.5* �6#   �� 5.      $
��0.5* �6#   .+�51      � -   5        $��Test2��51      $��253,330,0,-332q�6    $��2��68   
������ .+�52      �� -   5         ��Name$ label��52      $��
BGM 목록��6'   $��7,0,0,0q�68   $��15(�6I   �� 53      -   5         ��Name$ BgmlistView��53      $
��Left=�60   $	��236��6K   $��	10,31,0,0q�6X   $	��Top=�6k   $	��238��6�   6�   54      � 56      -   5        $
��Test��56      $��0��6   $��-253,0,260,0q�6/   
������ .+�57      �� -   5        $ label2��57      $��웹툰주소��6(   $��0,0,0,0q�67   $��15(�6H   �� 58      -   5         ��Name$ textBox��58      $
��Left=�6+   $��23��6F   $��3,30,0,0q�6R    �� TextWrapping$
 Wrap=�6d   $	��Url��6x   $	��Top=�6�   $	��239��6�   #��[  6�   �� 59      -	   5         ��Name$ SearchButton��59      $��검색��6/   $
��Left=�6<   
�������$	��Top=�6j   $	��239��6�   $��3,58,0,0q�6�   ���	#FFABADB3$��23��6�   6�   � 5:      -
   5         �Name$ DownloadGrid��5:      $
��Left=�6-   $��3,86,0,0q�6H   $	��Top=�6Z   $	��239��6r   $	��180��6~   
������ .+�5;      �� �� 5<      -   5         ��Name$ DownloadImage��5<      $��Download.gif��63   $��1,0,1,0q�6o   r� 5=      -   5         r�Name$ DownloadAnim��5=   #    r� Source$ Download.gif?�69   $��1,0,1,0q�6O    r� LoadedBehavior$ Manual=�6`    r� 
SpeedRatio$ 1Q�6x   6�   5>      5?      5@      5A      5B      