# C#
***
#### I - Faire une fonction pour une ligne, et pas faire une fonction pour un mode
***
US 69084 : J'ai fait une fonction pour un mode __G__:
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
La fonction marche correctement, mais il ne traite que pour le mode 'G', et certains parties des codes sont en commun avec les autres modes , donc on pourrait l'améliorer.
Pour améliorer cela, on peut écrire une fonction show_hide pour chaque ligne et réutiliser ces fonctions dans différents modes, par exemple :
````
public void Show_Hide_StepType(string esc_method)
{
if (string.Compare(esc_method, "W", true) == 0 || // RM 24FEB16 F34760
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

public void Show_Hide_NbDecRound(string esc_method) 
{
	if(string.Compare(esc_method, "G", true) == 0 )
		{
            m_Label_Nb_Dec_Round.Visible = false;
            m_Edit_Nb_Dec_Round.Visible = false;
         } 
		else
		{
            m_Label_Nb_Dec_Round.Visible = true;
            m_Edit_Nb_Dec_Round.Visible = true;
        }
}
````
Ainsi de suite, on fait une telle fonction pour Conv, et tous les autres lignes.

***
#### II - Différence entre string.Equals et string.Compare
***
Pour comparer deux strings, on privilège utilisation de string.Compare, car
- string.Equals est toujours case sensible.
- string.Compare nous permet de desactiver l'option case sensible
    ````
    if(string.Compare(esc_method, "G", true) == 0)
    {
        m_ckSpecConvClauseCompatibility.Visible = false;
    }
    else
    {
        m_ckSpecConvClauseCompatibility.Visible = true;
    }
    ````
String.Compare : 
- Compare deux objets String spécifiés et retourne un entier qui indique leur position relative dans l'ordre de tri.
- Il retourne toujours un nombre entier signé: 
    - Inférieur à zéro: La première sous-chaîne précède la deuxième sous-chaîne dans l'ordre de tri.
    - Les sous-chaînes ont la même position dans l'ordre de tri, ou __length__ a pour valeur zero
    - La première sous-chaîne suit la deuxième sous-chaîne dans l'ordre de trie



