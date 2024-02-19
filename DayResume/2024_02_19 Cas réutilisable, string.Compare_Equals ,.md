# C#
***
#### I - Faire une fonction réutilisable, au lieu d'une fonction pour un cas spécifique
***
US 69084 : J'ai fait une fonction pour un mode G:
````
 public void Show_Hide_G(string esc_method)
	{
   m_Label_Esc_Step_Type.Visible = true;
   m_Combo_Esc_Step_Type.Visible = true;

   m_Label_Esc_Conv.Visible = true;
   m_Combo_Esc_Conv.Visible = true;

    ...
   if(string.Compare(esc_method, "G", true) == 0)
		{
             bool eac_methodEqualToG = string.Equals(esc_method, "G");
        
             m_Label_Esc_Step_Type.Visible = !eac_methodEqualToG;
             m_Combo_Esc_Step_Type.Visible = !eac_methodEqualToG;
        
             m_Label_Esc_Conv.Visible = !eac_methodEqualToG;
             m_Combo_Esc_Conv.Visible = !eac_methodEqualToG;
            ...
        }
   }
````
La fonction marche correctement, mais il ne traite que pour le mode 'G', et est non réutilisable pour les autres modes.
Pour améliorer cela, on peut écrire une fonction show_hide pour chaque ligne par exemple :
````
public void Show_Hide_StepType(string esc_method)
{
if (string.Compare(esc_method, "D", true) == 0 ||
			string.Compare(esc_method, "C", true) == 0 ||
			string.Compare(esc_method, "W", true) == 0 || // RM 24FEB16 F34760
   string.Compare(esc_method, "G", true) == 0 // CYU
		)
	{
 m_Label_Esc_Step_Type.Visible = true;
 m_Combo_Esc_Step_Type.Visible = true;
}
	else
	{
		m_Combo_Esc_Step_Type.SelectedValue = null;
		m_Label_Esc_Step_Type.Visible = false;
		m_Combo_Esc_Step_Type.Visible = false;
	}
}
````
Ainsi de suite, on fait une telle fonction pour Conv, et tous les autres lignes.

***
#### II - Différence entre string.Equals et string.Compare
***
string.Equals est toujours case sensible.
string.Compare nous permet de desactiver l'option case sensible:
````

````


