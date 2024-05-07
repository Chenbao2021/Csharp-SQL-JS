# I - JSON.parse()
Cette fonction est utilisée pour convertir une chaîne de caractères formatée en JSON (__JavaScript Object Notation__) en un objet JavaScript.
La syntaxe de base de __'JSON.parse()'__ est la suivante :
````
var obj = JSON.parse(text[, reviver]);
````
* __text__ : La chaîne de caractères JSON à analyser.
* __reviver (optionnel)__ : Une fonction qui peut être utilisée pour transformer les valeurs obtenues avant de les retourner. Cette fonction est appelée pour chaque clé et valeur et peut être utilisée pour modifier la valeur de la donnée déserialisée.


Exemple d'utilisation de JSON.parse() avec reviver
````
var text = '{"name":"John", "age":"30", "city":"New York"}';
var obj = JSON.parse(text, (key, value) => {
  return typeof value === 'string' ? value.toUpperCase() : value;
});
console.log(obj.name); // Affiche "JOHN"
````

# II - JSON.stringify()
Cette fonction prend un objet JavaScript et le convertit en une chaîne de caractères JSON.
Cela est particulièrement utile pour __sérialiser des données__ afin de les stocker ou de les envoyer à un serveur via une requête HTTP.

Syntaxe :
````
var jsonString = JSON.stringify(value[, replacer[, space]]);
````
* __value__ : L'objet JavaScript à sérialiser en chaîne JSON.
* __replacer (optionnel)__ : Une fonction qui modifie le comportement de la sérialisation ou un tableau de noms de propriétés à sérialiser. Elle est utilisée pour filtrer et transformer les résultats.
* __space (optionnel)__ : Un String ou Number qui permet d'__ajouter de l'indentation__ aux résultats pour les rendre plus lisibles.

Exemple d'utilisation de JSON.stringify() avec replacer
1. La fonction replacer, ou un tableau de chaînes, peut être utilisée pour filtrer les propriétés à inclure dans le JSON sérialisé
    ````
    var jsonString = JSON.stringify(obj, ["name", "city"]);
    console.log(jsonString);
    // Affiche '{"name":"John","city":"New York"}'
    ````
2. En utilisant une fonction replacer pour modifier la façon dont les valeurs sont sérialisées :
    ````
    var jsonString = JSON.stringify(obj, (key, value) => {
      if (typeof value === 'string') {
        return value.toUpperCase();  // Transforme toutes les chaînes en majuscules
      }
      return value;
    });
    console.log(jsonString);
    // Affiche '{"name":"JOHN","age":30,"city":"NEW YORK"}'
    ````