# SQL
#### I - Correction SQL1
- Mettre les noms en anglais et en majuscule.
- Les variables de procedures doivent être plus clair
- Séparer les noms par des "_"
- Condenser les codes : 1 lignes par FROM/ORDER BY/ GROUP BY etc.
- Utiliser des noms techniques : "annual_salary"
- Utiliser INT pour les années : CONVERT(VARCHAR, ISNULL(@YYYY, 0))
- Pour visualiser la structure d'une table:
    1. CTR + D   : Résultat dans des grilles
    2. Selectionner le nom complet de la table.
    3. ALT + F1  : Visualiser la tructure d'une table
- Bien revoir INNER JOIN et LEFT JOIN
- Quand on a 1 seul table, utiliser COUNT(*), on a pas besoin de préciser la colonne
- Généralement on n'arrondis pas avec ROUND(), la précision peuvent être important
- Quand on utiliser AVG(), on peut fair '*1.0' pour qu'il renvoie des valeurs après la virgule.

# C#
#### I - Correction écran
- Pour chaque Amend, Delete, il faut demander une confirmation de la part de l'utilisateur(MessageBox)
- Chaque fois qu'on insert/Update/Delete dans la base de donnée, il faut qu'il renvoie un tablea, qui contient 2 colonne (erreur_code, erreur_message), pour laquelle (1, vide) veut dire sans erreur, et (-1, "existe déjà") renseigne un erreur.
- On évite les comparaison avec des string directement, sinon on utilise plutôt les enum.
- Un procedure renvoient toujours le même résultat!
- 1 méthode 1 rôle
- Chaque fois qu'on manipule avec la base de donnée, on doit côntroler la sécurité (vérifier si la requête renvoie NULL).
- 

#### II - Obtenir une valeur dans une dataTable: 
Exemple pour obtenir une valeur depuis une dataTable , ligne _index_ , et champs *m_sEmp_firstName*:
```
m_edFirstName.Value = m_dtSourceEmployee.Rows[index].Field<string>(m_sEmp_firstName);
```
#### III - Modifier les propriétés d'une ligne dans un Grid par codes:
1. (Pas recommadé)Ce code marche, mais généralement on ne modifie pas directement la grille:
```
if (m_dtSourceEmployee.Rows[i].Field<string>(m_sEmp_status) == "L")
{
    mfGridEmployee.Rows[i].DefaultCellStyle.ForeColor           = Color.Red;
    mfGridEmployee.Rows[i].DefaultCellStyle.SelectionForeColor  = Color.Red;
}
```

#### IV - Modifier les propriétés d'une colonne dans un Grid par codes:
Voici un exemple de code qui modifie le format d'affichage temps pour une colonne:
```
MFGridColumn col            = mfGridEmployee.Columns[m_sEmp_dateUpdate] as MFGridColumn;
col.DefaultCellStyle        = col.DefaultCellStyle.Clone();
col.DefaultCellStyle.Format = "mm:hh dd/MM/yy";
```


