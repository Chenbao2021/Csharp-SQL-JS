
# C#
***
#### 1 - Appel à une service grâce à une référence
***
Metafactory a ses propre serveurs , qui exécutent des applications serveurs en permenance.
Une application client peut  demander un serveur de traîter sa demande et de renvoyer les données demandés.
Par exemple, pour remplir un component __MFMultiRefSelFast__, 
````
private void LoadMS_Context()
{
  m_ms_Context.LoadMultiRef(RefDirector.RefManager.GetRefTable(IORefType.IO_REF_CONTEXT), false, RefDefines.id, RefDefines.full_name);
}
````
L'appel de la service est donc: __RefDirector.RefManager.GetRefTable(IORefType.IO_REF_CONTEXT)__
- RefDirector: Accès au Manager de référence
- RefManager : Accès au ref Manager permettant de traiter les references. Celui doit etre préalablement créer grâce à CreateRefManager. (Cette méthode appel la méthode __GetOrCreateRefManager__)
- GetRefTable(IORefType RefCode): Retourne la table de références spécifié en paramètre, null sinon.
- IORefType : C'est une énumération, où chaque valeur est un type de references.
    ````
    public enum IORefType
    {
        IO_REF_INST_RELATION = 0,
        IO_REF_PUBLISHER = 1,
        IO_REF_IDX_STYLE = 2,
        IO_REF_IDX_CATEGORY = 3,
        ...
    ````


***
#### 2 - Merge two project
***
Quand tu travailles sur une copie de projet, et que tu veux la pousser dans le serveur.  Alors tu dois utiliser l'application WinMerge pour fusionner les projets.

1. D'abord ouvvres HIGGINS, trouves ton projet, et prendre la main.
2. Si c'est un projet client, Aller dans : D/medissys/client
3. __shift__ et selectionner les deux repertoires(le projet sur serveur, et la copie du projet).
4. Clique droit, puis trouves l'option __WinMerge__, normalement se trouve au-dessus de __Envoyer vers__
5. certains fichiers tu peux les ignorer : vc14, vc14d, AssemblyInfo.cs, .vs, obj, etc.
6. Par exemple, j'ai une modification dans le répertoire __Forms__, dans le fichier __FrmReferentialsMappings.cs__ .
7. Dans l'écran de merge, juste en dessous de topbar, tu trouves une liste des icons:
    * alt + down : Next difference
    * alt + up : Previous difference
    * 18eme et 19eme Icon te permet de confirmer le remplacement.
8. Une fois que le merge est terminé, tu peux trouver juste à côté des fichiers modifié, des fichiers __.back__, qui sont des backup du fichier avant la modification(Il suffit de supprimer le __.back__ en cas de problème), s'il ne t'en serviras pas, tu peux la supprimer.
***
#### 3 - Signification de N/V/E/M
***
Dans un écran de paramétrage, on peut trouver une colonne pour les droits display, edite et mandatory.
Cette colonne peut avoir 4 valeurs différents:
- N, Not Visible. 
    En effet, si c'est pas visible, alors sûrement il ne peut pas être ni modifiable, ni mandatory
- V, Visible, not editable.
    S'il n'est pas modifiable, alors il ne peut pas être obligatoire(car on a pas le droit de le toucher ! )
- E, Editable, not mandatory
    On peut la modifier, sans forcément qu'il soit coché
- M, Mandatory
    Il est visible et il est obligatoire. 