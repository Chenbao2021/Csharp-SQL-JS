Le sujet du projet: 

Ci-dessous l’énoncé des exercices de niveau 2. Ils sont à faire avant jeudi 11 janvier à 12h, en m’envoyant par retour de mail les fichiers produits. 
Sur TSIGDEV4, en réutilisant les tables villes_france_free et département des exercices de la formation niveau 1 :
1. Faire un script qui crée une table région_XXX (XXX = tes initiales), contenant une colonne IDENTITY, un short_name et un full_name.
2. Faire un script qui y insère la liste des régions de France (pour la colonne short_name : initiales ou diminutif, exemple PACA pour Provence-Alpes-Côte d’Azur). La liste attendue : https://fr.wikipedia.org/wiki/R%C3%A9gion_fran%C3%A7aise
3. Créer une table departement_XXX (XXX = tes initiales), qui contiendra les mêmes colonnes que la table departement.
4. Ajouter dans la table departement_XXX une colonne region de type int, et faire l’update nécessaire pour que chaque département soit rattaché à la bonne région

==> Si tu as un doute concernant les scripts de ces 4 premiers points, n’hésite pas à me demander de les valider avant de continuer.

5. Faire une procédure stockée qui :
- A pour nom et signature XXX_RETRIEVE @max_result int, @json_criteria varchar(max), @mode char(1) = ‘R’
- Si @mode = ‘R’, renvoie 3 tables: 
    - Table 1 : les régions, avec les colonnes
        1. Nom complet
        2. Short Name
        3. Liste des départements qui la composent, sous la forme « <Nom complet> (<CODE>) », séparés par des « ; », et triés par ordre alphabétique
        4. Liste des départements tronquée : colonne avec les 47 premiers caractères de la colonne ci-dessus, suivi de « … »
    - Table 2 : les départements, avec les colonnes
        1. Nom complet
        2. Région
        3. Nombre de villes
        4. Superficie totale
        5. Les 5 premières villes les plus peuplées en 2012, séparées par une « , », avec le chiffre entre parenthèses, de la + peuplée à la – peuplée
        6. Les 5 premières villes les plus peuplées en 2010, séparées par une « , », avec le chiffre entre parenthèses, de la + peuplée à la - peuplée
        7. Les 5 premières villes les plus peuplées en 1999, séparées par une « , », avec le chiffre entre parenthèses, de la + peuplée à la - peuplée
        
    - Table 3 : les villes, avec les colonnes
        1. Nom Réel
        2. Code postal
        3. Département sous la forme « <Nom complet> (<CODE>) »
- Si @mode = ‘C’, renvoie juste le nombre de lignes dans chacune des 3 tables
- Si @max_result > 0, je ne veux que @max_result lignes dans chaque table
- Contraintes : je veux qu’il y ait utilisation d’un CURSOR minimum, d’une procédure stockée (préfixée XXX_ , reste du nom à votre convenance) ayant au moins 2 paramètres OUTPUT, et d’une FUNCTION (préfixée F_XXX_ , reste du nom à votre convenance)

- Les 3 flux JSON ci-dessous codent des critères de recherche : je veux les résultats en fonction de ces critères de recherche quand ils sont passée dans le paramètre @json_criteria. Je pense que les mots employés dans les flux sont suffisamment explicites pour ne pas décrire les recherches. Ces critères sont a appliquer à la table la plus basse, c’est-à-dire ville :
    · '[{"name":"region_sn","type":"multiple","value":"PACA"},{"name":"region_sn","type":"multiple","value":"GE"},{"name":"departement", "type":"like", "value":"Al%"},{"name":"ville_nom_reel", "type":"like", "value":"N%"},]'
    · '[{"name":"departement_code","type":"multiple","value":"01"},{"name":"departement_code","type":"multiple","value":"02"},{"name":"superficie_departement","type":"compare|sup","value":"1000"}]'
    · '[{"name":"ville_name_reel","type":"like","value":"Pa%"},{"name":"superficie_ville","type":"compare|inf","value":"6"}]'
 
Ainsi, par exemple :
s’il n’y a qu’un critère au niveau du département, je veux toutes les villes dans les départements qui matchent avec le critère + les départements des villes filtrées + le régions des départements filtrés
s’il y a un critère ville et département, je veux toutes les villes qui matchent avec le critère ville + appartenant à un département qui matche avec le critère département + les départements des villes filtrées + le régions des départements
s’il y a un critère région, un critère département, un critère ville, et qu’aucune ville ne matche, alors les 3 tables de retour seront vides

