# SQL 
### I - IF/ELSE VS CASE
* IF/ELSE: Structure de côntrol
* CASE: Une expression.

Les deux ont des rôles différents et sont utilisées dans des contextes spécifiques.
1. IF/ELSE
    * __Contexte d'utilisation__ : Cette structure est utilisée dans les blocs de contrôle pour diriger __le flux du programme__ (Du coup son but n'est pas de renvoyer une valeur.
    * __Limitation__ : IF...ELSE ne peut pas être utilisée dans une requête SQL simple car elle contrôle le flux d'exécution et n'est pas destinée à être utilisée dans des expressions de requête.
2. CASE
    * __Contexte d'utilisation__ : L'expression CASE est une fonction qui retourne une valeur basée sur des conditions,et elle est souvent utilisée dans une clause __SELECT__ pour manipuler les résultats d'une requête.
3. Différence principale:
    * __IF ... ELSE__ est une structure de contrôle, donc il affecte le flux d'exécution et ne peut pas être utilisé dans une requête SQL.
    * __CASE__ est une expression qui évalue des conditions et retourne une valeur, et peut donc être utilisée directement dans les requêtes SQL pour manipuler les données renvoyées par ces requêtes.

### II - Case Dans une Select
Dans Select, on ne peut pas utiliser IF ELSE pour exécuter des codes selon une condition. Mais on peut utiliser CASE à la place.
Par exemple:
```SQL
Select
    ...
    CASE
        WHEN EXISTS (SELECT 1 FROM ...)
        THEN
            ...
        ELSE
            ...
    END as [columnName] 
```

### III - Revision jointures
* __INNER JOIN__
    * __Fonctionnement__ : Renvoie uniquement les lignes qui ont des correspondances dans les deux tables (La table de gauche, et celle de droite).
    * __Utilisation courante__ : Lorsqu'on souhaite récupérer uniquement les données qui ont des correspondances dans les deux tables.
* __LEFT JOIN (ou LEFT OUTER JOIN)__ : 
    * __Fonctionnement__ : Renvoie toutes les lignes de la table gauche, même si elles n'ont pas de correspondance dans la table de droite.
    Si aucune correspondance n'est trouvée, les colonnes de la table de droite seront remplie avec des valeurs NULL.
    * __Utilisation courante__ : Lorsque vous voulez toutes les données de la première table, avec les informations correspondantes(ou nulles) de la second table.
* __RIGHT JOIN (ou RIGHT OUTER JOIN)__ : idem que LEFT JOIN, sauf à l'inverse.
* __FULL JOIN (ou FULL OUTER JOIN)__:
    * __Fonctionnement__ : Renvoie toutes les lignes des deux tables , qu'elles aient ou non une correspondance.

* __CROSS JOIN (Ou produit cartésien)__:
    * Cette jointure retourne le produit cartésien des deux tables, c'est à dire que chaque ligne de la première table est combinée avec chaque ligne de la deuxième table.

* __SELF JOIN__ : 
    * C'est une jointure où une table est jointe à elle-même, utile lorsqu'on doit comparer des lignes dans la même table.
    * Par exemple:
        `````
        SELECT A.*
        FROM TABLEA A
        INNER JOIN TABLE A B
        IN A.manager_id = B.employee_id
        `````

#### IV - Transaction
Les transactions en SQL Server permettent d'assurer que des opérations de modifications de données (Comme les __INSERT__, __UPDATE__ et __DELETE__) se déroulent de manière __atomique__(Réussissent complètement ou échouent complètement), cela garantit l'intégrité des données, même en cas de panne ou d'erreur.

__Instructions pour gérer une transaction__
1. __BEGIN TRANSACTION__ : Démarre une transaction.
2. __COMMIT__ : Valide les modifications effectuées dans la transaction, rendant les changements permanents.
3. __ROLLBACK__ : Annule toutes les modifications effectuées depuis le début de la transaction, rétablissant les données à leur état initial.
4. __SAVEPOINT__ : Permet de marquer un point intermédiaire dans la transaction, pour pouvoir revenir à cet état en cas d'erreur sans annuler toute la transaction.

__Utilisation Communes de Transactions__
1. __Opérations Bancaires__: Lors d'un transfert d'argent, on doit débiter un compte et créditer un autre. Une transaction assure que les deux opérations se font ensemble ou pas du tout.
2. __Traitement des commandes__ : Lors de la création d'une commande, vous devez déduire les stocks, créer la commande et générer la facture. Uns transaction s'assure que tous ces éléments sont traités ensemble.
3. __Batch Processing__ : Pour des mises à jour en masse ou des traitements complexes sur de grandes tables, les transactions assurent qu'en cas d'erreur, on peut revenir à un état stable.
4. __Gestion de Concurrence__: Les transactions permettent d'éviter les conflits d'accès concurrentiel à une base de données en verrouillant les données modifiées jusqu'à la validation ou l'annulation.

__Gestion de Concurrence__
1. Verrouillage(Locking)
    * Lorsqu'une transaction accède ou modifie des données, SQL Server place des verrous sur ces données.
    * Les verrous empêchent d'autres transactions d'accéder aux même données de manière incompatible(Par exemple, un verrou d'écriture empêchera une autre transaction de lire ou de modifier les mêmes données).
    * Selon le niveau d'isolation, les verrous sont levés au différents moments.

2. Niveaux d'Isolation
    * Les ___niveaux d'isolation__ déterminent comment les transactions sont isolés les une des autres et à quel moment une transaction peut voir les modifications d'une autre transaction.

3. Niveau d'Isolation des transactions en SQL Server
    1. Read Uncommitted(Lecture non validée) [Lectures sales]
        * Ce niveau d'isolation peut servir juste pour avoir le mécanisme de ROLLBACK.
    2. Read Committed(lecture validé - Niveau par défaut):
        * Les transactions ne peuvent lire que les données validées par d'autres transactions. Les données en cours de modification restent verrouillées jusqu'au __COMMIT__ .
        C'est à dire qu'on ne lit que les données qui sont __stable__, évite __la lecture sale__.
        * Les verrous de lecture sont appliqués uniquement pendant l'opération de lecture, permettant une lecture cohérente tout en minimisant les blocages.

4. Concernant le niveau __READ Uncommitted__
    Dans SQL Server, il est courant de voir les codes __SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED__ dans une procédure stockée sans utiliser explicitement __BEGIN TRANSACTION__ :
    1. Lecture Sales sans Transaction Explicite: Lire des données même si elles sont en cours de modification par d'autre transaction.
    2. Pour améliorer la performance en évitant les verrous de lecture.(Pour les requêtes qui ne nécessitent pas une cohérence stricte des données)
    3. Transaction implicite. Une transaction est automatiquement démarrée et validée pour chaque requête autonome(Comme un __SELECT__, __UPDATE__, __INSERT__, etc).
    
    Lorsqu'on utilise __READ UNCOMMITTED__, on dit simplement à SQL Server d'appliquer ce niveau d'isolation à ces transactions implicites sans avoir à définir explicitement une transaction avec __BEGIN TRANSACTION__.

    Il est généralement utilisé pour des __opérations de lecture__ uniquement.
    
5. Concernant le niveau __READ COMMITTED__ (Modifications verrouillées, lectures non verouillées)
    * Lectures Non répétables(Non-Repeatable Reads):
        * Ce phénomène des lectures non répétables dans ``READ COMMITTED`` survient lorsqu'une transaction lit une ligne plusieurs fois, et qu'entre ces lectures, une autre transaction valide ses modifications sur cette même ligne.
        * Car une fois lecture terminé, le verrou est débloqué. On peut utiliser un niveau plus haut comme ``REPEATABLE READ`` qui verrouille les données lu jusqu'à la transaction soit terminée.
