   M S B A M L   `   `   `  ����F  BWPFAerodrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WPFAerodrom.FormeWPFAerodrom.Forme  5   	       WPFAerodrom.Forme.FrmLet   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:WPFAerodrom.Forme     5   	     � Title$  Dodaj/Izmeni let��5   	   $	��350��6"   $	��300��6/   .��5	      �  J� RenderTransformOrigin$ 
0.36,0.576&�6   .+�5
   
   �� $��Vreme Polaska:��6   $
��Left=�6)   $��	25,26,0,0q�6D   $	��Top=�6W   �� 5   
   $��Vreme Dolaska:��6   $
��Left=�6)   $��	25,67,0,0q�6D   $	��Top=�6W   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dpDatumP��5      $
��Left=�6%   $��
121,67,0,0q�6@   $	��Top=�6T   $��26��6l     5   
   -   5        $��dpDatumD��5      $
��Left=�6%   $��
121,28,0,0q�6@   $	��Top=�6T   �� 5   
   $��Destinacija:��6   $
��Left=�6'   $��
25,107,0,0q�6B   $	��Top=�6V   �� 5   
   -   5        $��txtDestinacija��5      $
��Left=�6(   $��23��6C   $��121,107,0,0q�6O    �� TextWrapping$
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $	��120��6�   �� 5   
   $��Avion��6   $
��Left=�6    $��
25,149,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $��cbAvion��5      $
��Left=�6"   $��121,152,0,0q�6=   $	��Top=�6R   $	��120��6j    �� DisplayMemberPath$ model��6v    �� SelectedValuePath$ AvionID��6�   �� 5   
   -   5        $��
btnSacuvaj��5      $��Sacuvaj��6#   $
��Left=�65   $��
25,264,0,0q�6P   $	��Top=�6d   $��75��6|   $��28��6�   �� 5   
   -   5        $��	btnOtkazi��5      $��Otkazi��6"   $
��Left=�63   $��166,264,0,0q�6N   $	��Top=�6c   $��75��6{   $��28��6�   �� 5   
   $��	Kompanija��6   $
��Left=�6$   $��
25,191,0,0q�6?   $	��Top=�6S   �� 5   
   -   5        $��cbKompanija��5      $
��Left=�6&   $��121,194,0,0q�6A   $	��Top=�6V   $	��120��6n   $ imeKompanije��6z   $ kompanijaID��6�   �� 5   
   $��Pilot��6   $
��Left=�6    $��
25,233,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $��cbPilot��5      $
��Left=�6"   $��121,233,0,0q�6=   $	��Top=�6R   $	��120��6j   $ Pilot��6v   $ pilotID��6�   5      5      