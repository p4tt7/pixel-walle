   M S B A M L   `   `   `  ����F  Bpixel_walle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:pixel_wallepixel_walle  5   	       pixel_walle.ResizeDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:pixel_walle     5   	     � Title$  Resize Canvas��5   	   $	��150��6   $	��250��6,    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�6-   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $	��0 5q�6.   .+�5      �� $��Rows:��6   $��50��6%   $��Center=�60   �� 5      -   5         ��Name$ RowsBox��5      $	��100��6'   5      �� 5   
   $ 
Horizontal=�6   $��1��6.   $	��0 5q�6;   .+�5      �� $��Columns:��6   $��50��6(   $��Center=�63   �� 5      -   5        $ 
ColumnsBox��5      $	��100��6*   5      �� 5   
   $ 
Horizontal=�6   $��2��6.   $��Right=�6;   $��0 10 0 0q�6W   .+�5      �� -   5        $��OK��5      $��60��6"   $	��5,0q�6-   6:   �� 5      -   5        $��Cancel��5      $��60��6&   $	��5,0q�61   6>   5      5      5      