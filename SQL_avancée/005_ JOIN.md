# I - Introduction
__"INNER JOIN"__ : Seules les lignes qui satisfont __la condition de jointure__ sont retournées.

On peut imaginer(_Mais pas tout à fait_) un INNER JOIN = Un CROSS JOIN avec une clause WHERE. Mais le premièr est beaucoup plus optimisé.

Exemple:
Supposons qu'on a deux tables: 'Commandes' et 'Produits'.
* La table 'Commandes' contient des enregistrements des commandes passées, et chaque commande peut contenir plusieurs produits.
* La table 'Produits' contient les détails de chaque produit.

Si plusieurs produits de la table 'Produits' correspondent à une commande unique dans la table 'Commandes', chaque produit correspondant générera une ligne distincte dans les résultats lorsqu'ils sont joint.

# II - INNER JOIN vs OUTER JOIN
__INNER JOIN__
* __Usage__ : Utiliser un __'INNER JOIN'__  lorsque vous avez besoin de récupérer des données qui existent dans les deux tables.

__OUTER JOIN__
* LEFT [OUTER] JOIN : Retourne toutes les lignes de la première (gauche) table et les correspondances de la deuxième (droite) table. Les lignes de la première table qui n'ont pas de correspondance dans la deuxième table apparaîtront quand même, avec des valeurs NULL pour les colonnes de la deuxième table.
* RIGHT OUTER JOIN : L'inverse du LEFT OUTER JOIN. 
* FULL OUTER JOIN : Combine les résultats des LEFT et RIGHT OUTER JOIN.

* __Usage__ : Utilisez un OUTER JOIN lorsque vous avez besoin d'inclure des lignes qui n'ont pas de correspondances dans l'autre table.

__Conseil pour éviter les confusions__
* Analyser des besoins: Avant de chosir le type de jointure, analysez clairement ce que vous voulez que votre requête retourne. [Est ce que les lignes sans correspondances doivent être incluses ou non dans les résultats]
* Testez avec des exemples.

__Question à poser__:
1. Est ce que tu veux voir toutes les lignes de la première table ?
    Si oui : OUTER JOIN
    Si non: INNER JOIN

Des exemples à mieux comprendre: 
1. Obtenir une liste des employés __qui sont assignés à un département__.  => INNER JOIN
2. Obtenir une liste de tous les employés, y compris leur département s'ils en ont un. => LEFT OUTER JOIN
3. Obtenir une liste de tous les départements, y compris les employés s'il y en a dans chaque département. => RIGHT OUTER JOIN
4. Obtenir une liste complète des employés et des départements, y compris ceux qui n'ont pas de correspondance. => FULL OUTER JOIN