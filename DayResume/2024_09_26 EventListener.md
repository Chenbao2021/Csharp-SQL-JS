# React
***
## I - EventListener
**Syntaxe de base d'un EventListener**
La méthode **addEventListener** est utilisé pour ajouter un EventListener à un élément DOM. Elle prend deux paramètres principaux:
1. Le type d'événement(comme ``click``, ``mouseover``, ``keydown``, etc).
2. La fonction à exécuter lorsque cet événement se produit.

**Exemple de base d'un ``addEventListener``**
````JavaScript
<div id="myDiv">Clique ici !</div>
<script>
  // Sélectionner le div par son id
  const myDiv = document.getElementById('myDiv');

  // Ajouter un EventListener pour écouter le clic sur le div
  myDiv.addEventListener('click', function() {
      alert('Le div a été cliqué !');
  });
</script>
````
1. **Sélection du composant** :  On utilise ``document.getElementById`` (ou ``document.querySelector``) pour sélectionner l'élément DOM (par exemple, un div ou un bouton).
2. **Ajout de l'EventListener**: La méthode ``addEventListener`` est appelée sur cet élément, et on spécifie l'événement que l'on souhaite écouter(ici, un clic) ainsi que la fonction qui sera exécutée.

**Supprimer un EventListener avec ``removeEventListener``**

**Écouteur d'événements dans un framework(comme React)**
En React, on n'utilise pas directement ``addEventListener`` sinon via des props d'événement comme ``onClick``(``onChange``, ``onDoubleClick``, etc.).


