   M S B A M L   `   `   `  ���� \  XM6T.Data.Reporting.RuntimeReports, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:M6T.Data.Reporting.RuntimeReports!M6T.Data.Reporting.RuntimeReports  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem @    :M6T.Data.Reporting.RuntimeReports.ReportDesignerHubControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Alocal/clr-namespace:M6T.Data.Reporting.RuntimeReports     X TXceed.Wpf.Toolkit, Version=2.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  3System&clr-namespace:System;assembly=mscorlib  ���	#FF292929.���   � RowDefinitions  �� -    ��Name$ topRow��$��38 ��� -   $ mainRow���� -   $ 	bottomRow��$	��100 � � ColumnDefinitions �� -    ��Name$ leftCol���� -   $ mainCol��$��5* ��� -   $ rightCol��$��2* �.+�=   7M6T.Data.Reporting.RuntimeReports.ReportDesignerControl  -     Name$ designer��$
��Auto��$
��Auto��$��1q����	#FF007ACC$��1��$��1��=   7M6T.Data.Reporting.RuntimeReports.ReportDesignerToolBar  -     Name$ toolbar��$
��Auto��$
��Auto��$��1q����	#FF007ACC$��1��$��2��;   5M6T.Data.Reporting.RuntimeReports.ReportFooterControl  -	     Name$ footer��$��2��$��1�����	#FFB6B6B6G   AM6T.Data.Reporting.RuntimeReports.ReportDesignerPropertiesControl  -
     Name$ 
properties��$��2��$��1��$��2��� $��3��$��42��$	��Top=�$��0,-4,0,0q����	#FF616161.+��� -    ��Name$ label��$��Belge Boyutu��$
��Left=�$��
586,17,0,0q�$	��Top=�
��������� -   	 ��Name$	 width��$
��Left=�$��
661,14,0,0q�
 �� TextWrapping$

 Wrap=�$	��500��$	��Top=����	#FF333337���	#FF434346
�������$��34���� -   $ label1��$��X��$
��Left=�$��
700,17,0,0q�$	��Top=�
��������� -   $	 height��$
��Left=�$��
712,14,0,0q�$

 Wrap=�$	��500��$	��Top=����	#FF333337���	#FF434346
�������$��34���� -    ��Name$ button��$��Arka Plan Değiştir��$
��Left=�$��	265,9,0,0q�$	��Top=�$	��134���� -   $ button1��$��Kaydet��$
��Left=�$��
185,10,0,0q�$	��Top=�$��75���� -   $ ac��$	��Aç��$
��Left=�$��
105,10,0,0q�$	��Top=�$��75���� -   $
 yeni��$
��Yeni��$
��Left=�$��	10,10,0,0q�$	��Top=�$��75���� -    ��Name$ 
arkayazdir��$��Arka Planı Yazdır��$
��Left=�$	��Top=�$��
404,13,0,0q�
��������� -    ��Name$ comboBox��$
��Left=�$��
904,14,0,0q�$	��Top=�$��67��.@���Düz����Sağ����Sol����Ters���� -   $ label2��$ ��Yazdirmadan Önce Döndür��$
��Left=�$��
757,17,0,0q�$	��Top=�