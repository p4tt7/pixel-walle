   M S B A M L   `   `   `  ����F  Bpixel_walle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:pixel_wallepixel_walle  5   	       pixel_walle.ErrorWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:pixel_walle     5   	     � 
ResizeMode$  	CanResize=�5   	    � Title$ ErrorWindow��5	   	   $	��450��6   $	��800��6*   .��5
      � $��10q�6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ ErrorDataGrid��5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5         HeadersVisibility$ Column=�5         GridLinesVisibility$	 All=�5         CanUserAddRows$ False��5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Tipo��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Type+6L     5      $	 Mensaje��6%   $
 * 6R    66   �� *	Message+6R     5      $	 
Ubicación��6%   $

 Auto 6U    68   �� *
Location+6U   5      5      5      