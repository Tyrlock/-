   M S B A M L   `   `   `  ����M  IKusinskiy_Timophey, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Kusinskiy_TimopheyKusinskiy_Timophey  5   	   "    Kusinskiy_Timophey.LoginForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Kusinskiy_Timophey     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Вход��5	   	   $	��450��6   $	��400��5
   	   ���#333 � Icon$ Images/Icon_Resor.png��5   	    � 
ResizeMode$ NoResize=�5   	   .��5      � $��0,0,2,0q�6    � RowDefinitions 5   
   �� 5      $
��110* �6   �� 5      $	��30* �6   �� 5      $	��18* �6   �� 5      $	��35* �6   �� 5      $	��18* �6   �� 5      $	��35* �6   �� 5      $	��18* �6   �� 5      $	��26* �6   �� 5      $	��90* �6   �� 5      $	��26* �6   �� 5      $��9* �6    � ColumnDefinitions 5   
   �� 5      $	��25* �6   �� 5       $	��71* �6   �� 5!      $
��198* �6   �� 5"      $	��66* �6   �� 5#      $	��30* �6   .+�5%   
   �� $��Maestro del gusto��6   $��2��6-   $��1��6=   $��20(�6J    �� TextAlignment$ Center=�6X   �� 5&   
   $��Images/Icon_Resor.png��6   $��2��60   $��0��6@   �� 5'   
   -   5         ��Name$ txtName��5'       �� TextWrapping$
 Wrap=�6#   $��3��67   $��15(�6D   $��3��6R   $��0��6f   $��1��6s   	 �� 	MaxLength$	 20��6�   6�   �� 5(   
   $
��Wrap=�6   $��
Логин��6(   $��2��65   $��1��6B   �� 5)   
   $
��Wrap=�6   $��Пароль��6(   $��4��66   $��1��6C   �� 5*   
   -   5        
 ��Name$
 btnLogin��5*      $��
Войти��6#   $��7��63   $��2��6@    �� 	IsDefault . $��2��6x    � Cursor$
 Hand��6�   �� 5+   
   -   5        $
 	btnCreate��5+      $��Регистрация��6$   $��9��6:   $��2��6G   $��3��6o   $
 Hand��6|   G� 5,   
   -   5         G�Name$ pswPass��5,      $��15(�6'   $��3��65   $��5��6I   $��1��6V   $��1��6c    G� 	MaxLength$ 20��6s   6�   5-      5.      