   M S B A M L   `   `   `  ����  OffCampusHousingDatabase)    #OffCampusHousingDatabase.UserWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  User Profile��5   	   $	��450��6   $	��800��6+   
������� � WindowStyle$ ThreeDBorderWindow=�6J    � 
ResizeMode$ NoResize=�6k    � WindowStartupLocation$ CenterScreen=�6�   .��5      � $
��Left=�6   $	��450��6&   $	��800��63   $��0,0,0,0q�6?   $	��Top=�6P    � ColumnDefinitions 5   
   �� 5      $
��601* �6   �� 5      $
��199* �6   .+�5   
   �� ���	#FF90BCFD$
��Left=�6%   $��46��6@   
���   �$	��Top=�6[   $	��818��6s   $��	-18,0,0,0q�6   $��2��6�   �� 5   
   -   5         ��Name$ 
emailLabel��5      $��Email: ��6$   $
��Left=�66   $��
10,109,0,0q�6Q   $	��Top=�6e    J� RenderTransformOrigin$ -0.675,0.606&�6}   �� 5   
   -   5        $ 
phoneLabel��5      $2��,Phone:                                      ��6$   $
��Left=�6[   $��
10,184,0,0q�6v   $	��Top=�6�   6�   �� 5   
   -   5         ��Name$ 
updatePass��5      $��Update Password��6%   $
��Left=�6?   $��
23,331,0,0q�6Z   $	��Top=�6n   $	��123��6�   6�   �� 5   
   -   5        $ 	listLabel��5      $��Watched Properties:��6#   $
��Left=�6A   $��
197,78,0,0q�6\   $	��Top=�6p   �� 5   
   -   5        $
 back��5      $
��Back��6   $
��Left=�6.   $��	10,62,0,0q�6I   $	��Top=�6\   $��75��6t   6   �� 5   
   -   5         ��Name$ userPropertyView��5      $
��Left=�6-   $	��264��6H   $��197,109,0,0q�6U   $	��Top=�6j   $	��576��6�   $��2��6�   	 �� View	 5      �� 5      .P�5      �� $��Address��6%   
 �� Width$	
 175��6\    �� DisplayMemberBinding 66   �� *Addr+6\   �� 5      $��Monthly Rent��6%   $	
 100��6a    6;   �� *Rent+6a   �� 5      $��Number of Rooms��6%   $	
 130��6m    6>   �� *NumberOfRooms+6m   �� 5      $��Date Available By��6%   $	
 170��6o    6@   �� *DateAvailable+6o   5      5      �� 5   
   -   5        $ newPropertyButton��5      $��New Property��6,   $��1��6C   $
��Left=�6S   $��
80,378,0,0q�6n   $	��Top=�6�   $��92��6�   6�   �� 5   
   -   5         ��Name$ phoneTextBox��5      $
��Left=�6(   $��23��6C   $��
10,217,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��171��6�   6�   �� 5    
   -	   5        $ changePhoneButton��5       $��Confirm Change��6,   $
��Left=�6E   $��
51,262,0,0q�6`   $	��Top=�6t   $��95��6�   $ -0.003,-0.71&�6�   6�   �� 5"   
   -
   5         ��Name$ loginTextblock��5"      $
��Left=�6,   $��	23,13,0,0q�6G   $
��Wrap=�6Z   $��Login��6n   $	��Top=�6{   
����  � � Cursor$
 Hand��6�   $��Normal$�6  $��1��6  $��19��6.  $��54��6:  �� 5#   
   -   5        $ signupTextblock��5#      $
��Left=�6-   $��
117,13,0,0q�6H   $
��Wrap=�6\   $��Sign Up��6p   $	��Top=�6   
����  �$
 Hand��6  $��Normal$�6  $��1��6%  $��22��65  $��55��6A  �� 5$   
   -   5        $ orTextblock��5$      $
��Left=�6)   $��	82,13,0,0q�6D   $
��Wrap=�6W   $��or��6k   $	��Top=�6u   $��1��6�   $��19��6�   $��20��6�   �� 5&   
   -   5        $ userEmailTextBlock��5&      $
��Left=�60   $��	23,13,0,0q�6K   $
��Wrap=�6^   $�� ��6r   $	��Top=�6z   
����  �$��19��6�   $	��193��6�   $
 Hand��6�   $��Normal$�6�   6  �� 5'   
   -   5        $ thisEmailLabel��5'      $�� ��6(   $
��Left=�63   $��
10,140,0,0q�6N   $	��Top=�6b   $ -0.271,-0.054&�6z   $	��182��6�   �� 5(   
   -   5        $ thisPhoneLabel��5(      $�� ��6(   $
��Left=�63   $��
10,215,0,0q�6N   $	��Top=�6b   $	��171��6z   6�   �� 5)   
   -   5        $ changePhoneNotification��5)      $��click to change!��61   $
��Left=�6L   $��
99,184,0,0q�6g   $	��Top=�6{   6�   �� 5+   
   -   5        $ deletePropertyButton��5+      $��Delete Property��6/   $
��Left=�6I   $��584,378,0,0q�6d   $	��Top=�6y   $��92��6�   $��2��6�   $y�Hidden=�6�   5.      5/      