   M S B A M L   `   `   `  ����\  XM6T.Data.Reporting.RuntimeReports, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:M6T.Data.Reporting.RuntimeReports!M6T.Data.Reporting.RuntimeReports  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   E   @    :M6T.Data.Reporting.RuntimeReports.ReportDesignerHubControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:M6T.Data.Reporting.RuntimeReports     5      X TXceed.Wpf.Toolkit, Version=2.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      3System&clr-namespace:System;assembly=mscorlib  6E   ���	#FF292929.��5	      �   � RowDefinitions  5
   
   �� 5      -   5         ��Name$ topRow��5      $��38 �6,   �� 5      -   5        $ mainRow��5      �� 5      -   5        $ 	bottomRow��5      $	��100 �6/    � ColumnDefinitions 5   
   �� 5      -   5         ��Name$ leftCol��5      �� 5      -   5        $ mainCol��5      $��5* �60   �� 5      -   5        $ rightCol��5      $��2* �61   .+�5   
   =   7M6T.Data.Reporting.RuntimeReports.ReportDesignerControl  -   5          Name$ designer��5   @   $
��Auto��6R   $
��Auto��6_   $��1q�6m   ���	#FF007ACC$��1��6�   $��1��6�   =   7M6T.Data.Reporting.RuntimeReports.ReportDesignerToolBar  5   
   -   5          Name$ toolbar��5   ?   $
��Auto��6P   $
��Auto��6^   $��1q�6k   ���	#FF007ACC$��1��6�   $��2��6�   ;   5M6T.Data.Reporting.RuntimeReports.ReportFooterControl  5   
   -	   5          Name$ footer��5   $   $��2��64   $��1��6A   ���	#FFB6B6B66Q   G   AM6T.Data.Reporting.RuntimeReports.ReportDesignerPropertiesControl  5   
   -
   5          Name$ 
properties��5   0   $��2��6D   $��1��6T   $��2��6a   � 5   
   $��3��6   $��42��6#   $	��Top=�6/   $��0,-4,0,0q�6G   ���	#FF616161.+�5      �� -   5         ��Name$ label��5      $��Belge Boyutu��6#   $
��Left=�6:   $��
586,17,0,0q�6U   $	��Top=�6i   
�������6�   �� 5      -   5        	 ��Name$	 width��5      $
��Left=�6%   $��
661,14,0,0q�6@   
 �� TextWrapping$

 Wrap=�6T   $	��500��6h   $	��Top=�6s   ���	#FF333337���	#FF434346
�������$��34��6�   6�   �� 5      -   5        $ label1��5      $��X��6$   $
��Left=�60   $��
700,17,0,0q�6K   $	��Top=�6_   
�������6w   �� 5      -   5        $	 height��5      $
��Left=�6&   $��
712,14,0,0q�6A   $

 Wrap=�6U   $	��500��6i   $	��Top=�6t   ���	#FF333337���	#FF434346
�������$��34��6�   6�   �� 5      -   5         ��Name$ button��5      $��Arka Plan Değiştir��6%   $
��Left=�6B   $��	265,9,0,0q�6]   $	��Top=�6p   $	��134��6�   6�   �� 5      -   5        $ button1��5      $��Kaydet��6&   $
��Left=�67   $��
185,10,0,0q�6R   $	��Top=�6f   $��75��6~   6�   �� 5      -   5        $ ac��5      $	��Aç��6!   $
��Left=�6.   $��
105,10,0,0q�6I   $	��Top=�6]   $��75��6u   6�   �� 5       -   5        $
 yeni��5       $
��Yeni��6#   $
��Left=�62   $��	10,10,0,0q�6M   $	��Top=�6`   $��75��6x   6�   �� 5!      -   5         ��Name$ 
arkayazdir��5!      $��Arka Planı Yazdır��6+   $
��Left=�6G   $	��Top=�6b   $��
404,13,0,0q�6z   
�������6�   �� 5"      -   5         ��Name$ comboBox��5"      $
��Left=�6)   $��
904,14,0,0q�6D   $	��Top=�6X   $��67��6p   .@�5#      ��Düz��6%   ��5$      Sağ��6%   ��5%      Sol��6%   ��5&      Ters��6&   5'      �� 5(      -   5        $ label2��5(      $ ��Yazdirmadan Önce Döndür��6$   $
��Left=�6F   $��
757,17,0,0q�6a   $	��Top=�6u   5)      5+      5,      