   M S B A M L   `   `   `  ����M  IKusinskiy_Timophey, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Kusinskiy_TimopheyKusinskiy_Timophey  5   	       Kusinskiy_Timophey.info   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Kusinskiy_Timophey     5   	   -   5          � Title$(  "Информация о блюде��5   	   $	��400��5	   	   $	��700��5
   	   ���#333 � Icon$ Images/Icon_Resor.png��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��9* �6   �� 5      $	��26* �6   �� 5      $	��20* �6   �� 5      $	��15* �6   �� 5      $	��93* �6   �� 5      $
��147* �6   �� 5      $	��17* �6   �� 5      $��9* �6   �� 5      $	��31* �6   �� 5      $��4* �6    � ColumnDefinitions 5   
   �� 5      $	��13* �6   �� 5       $
��133* �6   �� 5!      $	��60* �6   �� 5"      $	��75* �6   �� 5#      $��9* �6   �� 5$      $	��65* �6   �� 5%      $
��119* �6   �� 5&      $	��81* �6   �� 5'      $
��133* �6   �� 5(      $��6* �6   .+�5*   
   �� -   5         ��Name$ imageOfProduct��5*      $��3��6(   $��1��65   $��3��6E   $��4��6Y   �� 5+   
   -   5         ��Name$ txtName��5+      $
��Wrap=�6%   $	��Top=�69   $��30��6Q    �� TextAlignment$ Center=�6]   $��6��6t   $��2��6�   $��1��6�   ���	#FFEEEFFF$��20(�6�   $��2��6�   �� 5,   
   -   5        $ txtDescription��5,      $
��Wrap=�6,   $��5��6@   $��4��6P   $��4��6]   ���	#FFEEEFFF6q   �� 5-   
   -   5        	 ��Name$	 btnExit��5-      $
��Ок��6"   $��1��6/   $��8��6?   6L   �� 5.   
   -   5        $ txtCost��5.      $
��Wrap=�6%   $��6��69   $��6��6I   ���	#FFEEEFFF$��3��6m   �� 5/   
   $��5��6   $��3��6$   $
��Wrap=�61   $��Состав:��6E   ���	#FFEEEFFF$��2��6k   �� 50   
   $��5��6   $��6��6$   $
��Wrap=�61   $��Стоимость:��6E   ���	#FFEEEFFF6W   52      53      