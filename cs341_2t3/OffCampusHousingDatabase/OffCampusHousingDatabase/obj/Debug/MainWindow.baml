   M S B A M L   `   `   `  ����  OffCampusHousingDatabase)    #OffCampusHousingDatabase.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Rent Oshkosh��5   	   $	��450��6   $	��800��6+   
������� � WindowStyle$ ThreeDBorderWindow=�6J    � 
ResizeMode$ NoResize=�6k    � WindowStartupLocation$ CenterScreen=�6�   .��5      � $
��Left=�6   $	��450��6&   $	��800��63   $��0,0,0,0q�6?   $	��Top=�6P   .+�5   
   �� ���	#FF90BCFD$
��Left=�6%   $��46��6@   
���   �$	��Top=�6[   $	��818��6s   $��	-18,0,0,0q�6   �� 5   
   -   5         ��Name$ loginTextblock��5      $
��Left=�6,   $��
624,13,0,0q�6G   $
��Wrap=�6[   $��Login��6o   $	��Top=�6|   
����  � � Cursor$
 Hand��6�   $��Normal$�6  �� 5	   
   -   5        $ signupTextblock��5	      $
��Left=�6-   $��
718,13,0,0q�6H   $
��Wrap=�6\   $��Sign Up��6p   $	��Top=�6   
����  �$
 Hand��6  $��Normal$�6  �� 5
   
   -   5        $ OrTextblock��5
      $
��Left=�6)   $��
683,13,0,0q�6D   $
��Wrap=�6X   $��or��6l   $	��Top=�6v   �� 5   
   -   5        $ userEmailTextBlock��5      $
��Left=�60   $��	24,13,0,0q�6K   $
��Wrap=�6^   $�� ��6r   $	��Top=�6z   
����  �$
 Hand��6�   $��Normal$�6
  �� 5   
   $
��Left=�6   $��	10,86,0,0q�6/   $
��Wrap=�6B   $��
Properties��6V   $	��Top=�6h   $	��136��6�   $��21��6�   $��16(�6�   $
��Bold"�6�   �� 5   
   -   5        $ filterLabel��5      $
��Left=�6)   $��617,244,0,0q�6D   $
��Wrap=�6Y   $	��Top=�6m   $��19��6�   $	��167��6�   
���  �� � UseLayoutRounding . $��Please use correct Value��6�   $y�Hidden=�6�   �� 5   
   $��Filters��6   $
��Left=�6"   $��668,127,0,0q�6=   $	��Top=�6R   $��14(�6j   $��SemiBold"�6x   �� 5   
   $��Rent Less Than: ��6   $
��Left=�6+   $��617,161,0,0q�6F   $	��Top=�6[   �� 5   
   $��Rooms More Than: ��6   $
��Left=�6,   $��599,189,0,0q�6G   $	��Top=�6\   �� 5   
   $��Available By:��6   $
��Left=�6(   $��608,216,0,0q�6C   $	��Top=�6X   �� 5   
   -   5         ��Name$ propertyListView��5      $
��Left=�6-   $	��294��6H   $��
10,112,0,0q�6U   $	��Top=�6i   $	��576��6�    �� View 5      �� 5      .P�5      �� $��Address��6%   	 �� Width$		 175��6\   
 �� DisplayMemberBinding
 66   �� *Addr+6\   �� 5      $��Monthly Rent��6%   $		 100��6a   
 6;   �� *Rent+6a   �� 5      $��Number of Rooms��6%   $		 130��6m   
 6>   �� *NumberOfRooms+6m   �� 5      $��Date Available By��6%   $		 170��6o   
 6@   �� *DateAvailable+6o   5      5      �� 5   
   -   5         ��Name$ filterRentTextbox��5      $
��Left=�6-   $��23��6H   $��709,161,0,0q�6T    �� TextWrapping$
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $��67��6�   �� 5    
   -   5        $ filterRoomTextbox��5       $
��Left=�6-   $��23��6H   $��709,189,0,0q�6T   $
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $��67��6�   �� 5!   
   -	   5        $  filterDateAvailableTextbox��5!      $
��Left=�66   $��23��6Q   $��689,218,0,0q�6]   $
 Wrap=�6r   $	��Top=�6�   $��87��6�   $��
mm/dd/yyyy��6�   6�   �� 5#   
   $��Sort By��6   $
��Left=�6$   $��637,263,0,0q�6?   $	��Top=�6T   $��96��6l   $	��113��6x   	� 5$   
   -
   5         	�Name$ sortByRentRadioButton��5$      $
��Rent��65   $
��Left=�6D   $��661,285,0,0q�6_   $	��Top=�6t   	� 5%   
   -   5        $ sortByRoomRadioButton��5%      $��Rooms��65   $
��Left=�6E   $��661,301,0,0q�6`   $	��Top=�6u   	� 5&   
   -   5        $ sortByDateRadioButton��5&      $
��Date��65   $
��Left=�6D   $��661,317,0,0q�6_   $	��Top=�6t   	� 5'   
   -   5        $ sortByNoneRadioButton��5'      $
��None��65   $
��Left=�6D   $��661,333,0,0q�6_   $	��Top=�6t    d� 	IsChecked$
 TrueY�6�   �� 5)   
   -   5         ��Name$ filterButton��5)      $��Filter��6(   $
��Left=�69   $��617,364,0,0q�6T   $	��Top=�6i   $��75��6�   6�   �� 5*   
   -   5        $ resetButton��5*      $��Reset��6&   $
��Left=�66   $��701,364,0,0q�6Q   $	��Top=�6f   $��75��6~   6�   5,      5-      