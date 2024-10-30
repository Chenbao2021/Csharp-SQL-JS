# Général
***
## I - Git stash
Pour récupérer la dernière version du dépôt à distance(dernier push) sans perdre les modifications locales et pouvoir faire des tests, on peut utiliser une **beanche temporaire** ou des **commandes Git spécifiques**. 
* #### Option 1: Utiliser une branche temporaire.
    1. __Stasher les modifications__: Mettez vos changements locaux de côté.
        ``git stash``
    2. __Récupérer la dernière version depuis le dépôt distant__: 
        ``git pull origin <nom-de-la-branche>``
    3. __Créer une branche temporaire pour tester les modifications__:
        ``git checkout -b branche-temporaire``
    4. __Récupérer les modifications sauvegardées__:
        ``git stash pop``
    5. __Effectuer vos tests__ sur cette branche temporaire.
    6. __Retour à la branche principale__: Une fois le test terminé, on peut revenir au branche principale.
        ``git checkout main``
    7. __Restaurer le stash__:
        1. __Option 1__: ``git stash pop``, Applique les changements stachés et les retire du stash.
        ``git stash pop``
        2. __Option 2__: ``git stash apply``, Applique les changements stachés sans les supprimer du stash (utile si on veut garder une copie de sécurité).

# React
***
## I - moment.js
Utiliser ce librairie pour manipuler les formats de date.

## II - Valeur d'état
Dans React, ``useState`` ne se réinitialise pas automatiquement.
Voici le cas où l'état créé avec ``useState`` peut changer ou être réinitialisé:
* **Redémarrage de l'application**: Si l'application est rechargée ou redemarrée, ``useState`` reprend sa valeur intiiale.
* **Composant démonté puis remonté**: ``useState`` sera réinitialisé car React crée une nouvelle instance du composant.
* **Clé unique change**: Si le composant est associé à une clé unique(``key`` pop) qui change, React traite le composant comme un nouveau, donc ``useState`` est réinitialisé.
* **Effet direct du code**: En appelant directement le setter de ``useState``.

Lors d'un re-render, la valeur d'état initialisée avec ``useState`` **ne se réinitialise pas**.
React conserve l'état initialisé avec ``useState`` entre les re-renders.
Le seul moment où la valeur de ``useState`` serait réinitialisée automatiquement est lorsque le composant est démonté puis remonté, ou si la clé du composant change.
Exemple:
````js
function Counter() {
  const [count, setCount] = useState(0); // Initialisation à 0

  const increment = () => setCount(count + 1);

  console.log("Render:", count); // Montre la valeur actuelle de `count` à chaque rendu

  return (
    <div>
      <p>Count: {count}</p>
      <button onClick={increment}>Increment</button>
    </div>
  );
}
````

##### La valeur d'état dans le composant enfant **NE CHANGERA PAS AUTOMATIQUEMENT** si la valeur ``A`` provenant du composant parent est mise à jour.
``useState`` ne prend en compte la valeur initiale qu'au **moment de l'initialisation**. Si cette valeur ``A`` change dans le parent après l'initialisation du composant enfant, l'état du composant enfant ne se mettra pas à jour automatiquement.

Pour le synchroniser, on peut utiliser useEffect:
````JS
...
const [childValue, setChildValue] = useState(valueFromParent);

useEffect(() => {
setChildValue(valueFromParent);
}, [valueFromParent]); // Met à jour `childValue` si `valueFromParent` change
...
````

##### La clé(``key``) dans React
La clé dans React est un identifiant unique pour chaque élément dans une liste ou pour des composants réutilisés.
Lorsqu'une clé change pour un composant, React considère qu'il s'agit d'un nouveau composant et non d'une simple mise à jour du composant existant.

1. ##### Pourquoi les clés sont nécessaire dans React
    React utilise les clés pour **identifier chaque élément de manière unique** lors des opérations de rendu, en particulier dans les listes.
    Elles aident React à décider quels éléments doivent être réutilisés, mis à jour, ajoutés ou supprimés, optimisant ainsi le processus de rendu et améliorant les performances.
2. ##### Impact de changer la clé d'un composant
    Si la clé d'un composant change, React traite ce composant comme **entièrement nouveau**. Cela signifie que:
    * Toutes les valeurs d'état(``useState``) du composant sont réinitialisées.
        * Attention: On perd les états actuels!!!   
    * Toutes les effets(``useEffect``) se déclenchent comme si le composant était monté pour la première fois.
3. ##### Utilisation courante des clés
    Les clés sont essentielles dans les cas suivants:
    * **Listes dynamiques**: Dans une liste générée avec ``.map``, chaque élément doit avoir une clé pour que React puisse suivre chaque élément.
    * **Composants conditionnels ou réutilisés**: Lorsque des composants changent dynamiquement ou dépendent d'une clé pour l'identification.(Comme dans les cas d'onglets, ou de vues différentes dans une interface(Ex: vue create, vue retrieve)
4. ##### Bonnes pratiques pour les clés
    * Utiliser des valeurs uniques et stables pour les clés.
    * Éviter les indices de tableau(__index__) comme clés, car cela peut provoquer des erreurs si l'ordre des éléments change.

Exemple: 
````js
<div>
  <button onClick={() => setActiveTab("tab1")}>Tab 1</button>
  <button onClick={() => setActiveTab("tab2")}>Tab 2</button>
  
  {activeTab === "tab1" && <TabContent key="tab1" tabName="Tab 1" />}
  {activeTab === "tab2" && <TabContent key="tab2" tabName="Tab 2" />}
</div>
````
Dans l'exemple, même sans ``key`` ça fonctionnerait techniquement. Cependant __l'état interne des composants d'onglets ne se réinitialisera pas automatiquement__ lors du changement d'onglet, car React considère qu'il s'agit du même composant:
* **Pas de réinitialisation automatique de l'état**: L'état interne de chaque ``TabContent``(comme le compteur ``count``) reste le même, car React réutilise le composant existant au lieu de le recréer.
* **Comportement partagé entre onglets**: Si on passe de ``Tab 1`` à ``Tab 2`` sans ``key``, la valeur de ``count`` dans ``TabContent`` restera identique pour les deux onglets. 
    Le composant ne repartira pas de ``0``, mais gardera la dernière valeur de ``count`` quel que soit l'onglet.

Résumé:
* Avec ``key``: Chaque onglet est traité comme un composant distinct, et l'état est réinitialisé pour chaque onglet.
* Sans ``key``: L'état est partagé entre les onglets, et React ne sait pas qu'il doit remonter un nouveau composant. L'état ``count`` persiste entre les onglets.


