   M S B A M L   `   `   `  ����>  :app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:appapp  5          app.DetailPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      #localclr-namespace:app     5        N� Title$  Detail Page��5       N� 
Background
 �   � � DataContext 5         app.RegisterViewModel  5   
   .��5
      � .+�5   
   ]� .+�5       � -   5          �Name$ DetailFrame��5      5      �� 5   
    �� Orientation$ Vertical=�6   $
��Left=�5      .+�5      �� -   5         ��Name$ 
HomeButton��5      $��80��5      $��30��5      $��10q�5      ��5      �� 5       �� 
TargetType" ��6&   .R�5      ��  �� CornerRadius$ 10��6!   #��z��5   !   #��z��5   !   #��z��5   !   ._�5      �� $��Center=�6/   $��Center=�5   /   5      5      .��5       �� $��	Ana Sayfa��6   5!      5"      �� 5#   
   $	��300��6   $
��auto��5$      $��30q�5%      
�������$ 6��5'       J� Effect 5(      3  -System.Windows.Media.Effects.DropShadowEffect  5)      	   
BlurRadius$	 15Q�6#   
   	Direction$	
 -90Q�5*   #      RenderingBias$ Quality=�5+   #      ShadowDepth$ 2Q�5,   #      Color$
 Gray��5-   #   ._�5/      �� $ Vertical=�6   $��20q�50      .+�51      �� -   5         ��Name$ RadioListBox��51      $	��Top=�52      ���#f2f2f2
������ ��55      �� 56      .R�57      �� ���#b3b3b3$��0 0 0 1q�58   %   $��0 0 0 5q�59   %   ._�5:   "   �  � ColumnDefinitions 5;   &   �� 5<   *   $
��Auto �6;   �� 5=   *   $��* �6;   �� 5>   *   $
��Auto �6;   .+�5A   &   �� $��2��5B   0   
���   �$��10,0,0,0q�5D   0   ��5A   0   �� 5D   0   5E   #   5F      5G      5I      5J      5K      5L      5M      