   M S B A M L   `   `   `  ����F  BWPFAerodrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFAerodromWPFAerodrom  5   	       WPFAerodrom.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WPFAerodrom     5   	     � Title$  Aerodrom��5   	   $	��350��6   $	��750��6'   .��5	      � $��
0,0,-6.4,0q�6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGridCentralni��5
      $��Center=�6,   $��208,132,0,0q�6J   $��Center=�6_   $
��auto��5         
IsReadOnly .    AlternatingRowBackground
 ��� �   CanUserAddRows .     SelectionUnit$ FullRow=�6j      SelectionMode$ Single=�6�   �� 5   
   $��Center=�6   $��30��62   $	��Top=�6?   $
��auto��6W    �� Orientation$ 
Horizontal=�6d   .+�5      �� -   5        $��btnAvioKompanija��5      $��AvioKompanija��6-   $
��Left=�6E   $��90��6`   $��	0,0,0,0.4q�6�   �� 5      -   5        $��
btnPrtljag��5      $��Prtljag��6'   $
��Left=�69   $	��Top=�6U   $��30��6m   $��75��6y   6�   �� 5      -   5        $��btnLet��5      $	��Let��6#   $
��Left=�61   $	��Top=�6M   $��30��6e   $��75��6q   6|   �� 5      -   5        $��btnPilot��5      $��Pilot��6%   $
��Left=�65   $	��Top=�6Q   $��30��6i   $��75��6u   6�   �� 5      -   5        $��	btnPutnik��5      $��Putnik��6&   $
��Left=�67   $	��Top=�6S   $��30��6k   $��75��6w   6�   �� 5      -   5        $��btnAvion��5      $��Avioni��6%   $
��Left=�66   $	��Top=�6R   $��30��6j   $��75��6v   6�   �� 5      -   5        $��
btnOsoblje��5      $��Osoblje��6'   $
��Left=�69   $	��Top=�6T   $��30��6l   $��75��6x   6�   �� 5      -	   5        $��	btnTicket��5      $��Ticket��6&   $
��Left=�67   $	��Top=�6R   $��30��6j   $��75��6v   6�   5      �� 5   
   $��Center=�6   $��30��62   $��Bottom=�6?   $
��auto��6Z   $ 
Horizontal=�6g   .+�5      �� -
   5        $��btnDodaj��5      $��Dodaj��6%   $	��120��65   6A   �� 5      -   5        $��	btnIzmeni��5      $��Izmeni��6&   $	��120��67   6C   �� 5      -   5        $��	btnObrisi��5      $��Obrisi��6&   $	��120��67   6C   5      5      5      