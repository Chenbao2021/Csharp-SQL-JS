Les niveaux d'isolation déterminent la visibilité des modifications effectuées par une transaction pour les autres transactions.
Chaque niveau d'isolation offre un équilibre différent entre la concurrence et la cohérence des données.
# Les 5 niveau d'isolation
#### 1. READ UNCOMMITTED
* __Description__ :  Les transactions peuvent lire les modifications effectuées par d'autres transactions non validées (dirty reads).
* __Problèmes résolus__ : Aucun
* __Problèmes possibles__: Dirty reads, non-repeatable reads, phantom reads.
* __Utilisations__ : Utilisé lorsque la performance est plus critique que l'exactitude des données, par exemple, pour des rapports où des lectures incohérentes sont acceptables.

#### 2. READ COMMITTED (niveau par défaut)
* __Description__ : Les transactions ne peuvent lire que les données validées par d'autres transactions. Empêcher les dirty reads. Une lecture verrouille les données jusqu'à la fin de la lecture, mais une modification verouille les données jusqu'à fin de la transaction.
* __Problèmes résolus__ : Dirty reads.
* __Problèmes possibles__: Non-repeatable reads, phantom reads
* __Utilisations__ : Bon compromis entre cohérence et concurrence, utilisé par défaut dans la plupart des applications.

#### 3. REPEATABLE READ
* __Description__ : Empêche les non-repeatable reads en maintenant les verrous de lecture jusqu'à la fin de la transaction.
* __Problèmes résolus__ : Dirty reads, non-repeatable reads 
* __Problèmes possibles__: Phantom reads
* __Utilisations__ : Lorsque les même données doivent être __lues plusieurs fois__ avec garantie de stabilité

#### 4. SNAPSHOT
* __Description__ : Fournit une vue cohérente des données tell qu'elle existait au début de la transaction, __sans verrouiller les données lues__.
* __Problèmes résolus__ : Dirty reads, non-repeatable reads, phantom reads.
* __Problèmes possibles__: Utilisation élevée de l'espace de versionnage des donénes.
* __Utilisations__ : Lorsque la concurrence est élevée et qu'une vue cohérente des données est nécessaire sans bloquer d'autres transactions.

#### 5. SERIALIZABLE
* __Description__ : Le niveau d'isolation le plus strict. Les transactions sont entièrement isolées les unes des autres, empêchant toutes les lectures incohérentes.
* __Problèmes résolus__ : Dirty reads, non-repeatable reads, phantom reads.
* __Problèmes possibles__: Concurrence réduite, blocages fréquents.
* __Utilisations__ :  Scénarios où la cohérence des données est cruciale et où la performance peut être sacrifiée.

# Choix du niveau d'isolation
* __READ UNCOMMITTED__ : 
    * Maximiser la performance
    * La précision des lectures n'est pas critique
* __READ COMMITTED__ : Convient pour la plupart des applications, offrant un bon équilibre entre cohérence et performance.(Les différentes transactions peuvent exécuter en chevauchement)
* __REPEATABLE READ__: Utilisez ce niveau lorsque les transactions nécessitent de relire les mêmes données sans que celles-ci changent entre les lectures.
* __SNAPSHOT__ : Idéal pour les applications nécessitant une vue cohérente des données sans blocage, particulièrement utile dans des environnement de haute concurrence.
* __SERIALIZABLE__ : Utilisé pour garantir la plus haute intégrité des données, bine que cela puisse diminuer la concurrence et les performances.(Comme si les transactions sont exécuté en série, sans chevauchement)