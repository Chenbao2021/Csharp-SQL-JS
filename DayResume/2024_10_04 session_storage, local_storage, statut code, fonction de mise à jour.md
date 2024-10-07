# React
***
## I - sessionStorage/ localStorage
* sessionStorage: Les données survivent à une actualisation de la page.
* localStorage: Les données survivent à un redémarrage complet du navigateur.

Différence avec les cookies:
* Contrairement aux cookies, les objets de stockage Web ne sont pas envoyés au serveru à chaque requête. Grâce à cela, nous pouvons stocker beaucoup plus.
* Et le serveur ne peut pas manipuler les objets de stockage via les en-têtes HTTP. Tout se fait en JavaScript.
* Le stockage est lié à l'origine(triplet domaine/protocole/port). 

Les deux objets de stockage fournissent les mêmes méthodes et propriétés:
* ``setItem(key, value)`` - stocke la paire clé/valeur.
* ``getItem(key)`` - récupère la valeur par clé.
* ``removeItem(key)`` - supprime la clé avec sa valeur.
* ``clear()`` - supprime tout.
* ``key(index)`` - récupère la clé sur une position donnée.
* ``length`` - le nombre d'éléments stockés.

La clé et la valeur doivent être des chaînes.
* On peut utiliser ``JSON`` pour sauvegarder un object:
    ````
    localStorage.user = JSON.stringify({name: "John"});
    let user = JSON.parse(localStorage.user);
    ````

L'objet ``sessionStorage`` est beaucoup moins utilisé que ``localStorage``.
* les storages n'existent que dans l'onglet actuel du navigateur.
* Mais partagé entre les iframes du même onglet.
* Les données survivent à l'actualisation de la page, mais pas à la fermeture/ouverture de l'onglet.

|Caractéristique|localStorage|sessionStorage|
|---|---|---|
|persistance|Permanente|Temporaire(Jusqu'à fermeture de l'onglet|
|Capacité|5-10Mo|5-10Mo|
|Partage entre les onglets|Oui|Non|
|Exemple d'utilisation|Thème utilisateur, panier d'achat, préférences|Données de navigation pour une session|

# II - Statut code
* 404 : Not Found (Non trouvé)
* 200 : Ok
* 403 : Forbidden, l'accès à la ressource est refusé, même si elle existe.
* 500 : Internal Server Error: Une erreur interne au serveur a empêché le traitement de la requête.

# III - Fonction de mise à jour
Soit une fonction ``setNumbers`` en React, il peut soir accepter une nouvelle valeur, soit une fonction de mise à jour.
Lorsqu'une fonction de mise à jour est fournie(ici ``(prevNumbers) => ...``), React l'exécute en passant la valeur actuelle de l'état comme argument, que l'on appelle souvent ``prevNumbers``.

Cette fonction permet de manipuler l'état de manière fiable lorsque l'état actuel est requis pour calculer le nouvel état.

**Si vous utilisiez plusieurs fois ``setNumbers(someValue)`` successivement dans un même rendu, certaines valeurs pourraient être ignorées, exemple:
````JS
const [numbers, setNumbers] = useState([1, 2, 3]);

function addMultipleNumbers() {
  setNumbers([...numbers, 4]); // Ajoute 4 à l'état actuel [1, 2, 3]
  setNumbers([...numbers, 5]); // Ajoute 5, mais l'état actuel est toujours [1, 2, 3]
  setNumbers([...numbers, 6]); // Ajoute 6, mais l'état actuel est toujours [1, 2, 3]
}

addMultipleNumbers();
console.log(numbers); // Résultat inattendu : [1, 2, 3, 6]
````
Mais avec une fonction de mise à jour, on évite ce problème:
````JS
const [numbers, setNumbers] = useState([1, 2, 3]);

function addMultipleNumbers() {
  setNumbers((prevNumbers) => [...prevNumbers, 4]); // Basé sur la valeur actuelle de prevNumbers
  setNumbers((prevNumbers) => [...prevNumbers, 5]); // Basé sur la nouvelle valeur actualisée de prevNumbers
  setNumbers((prevNumbers) => [...prevNumbers, 6]); // Basé sur la valeur de prevNumbers qui inclut 4 et 5
}

addMultipleNumbers();
console.log(numbers); // Résultat attendu : [1, 2, 3, 4, 5, 6]
````



