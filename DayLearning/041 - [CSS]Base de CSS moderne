Voici quelques proprietés fait partie de la base du CSS moderne, qui sont très utile à connaîre, et savoir comment les utiliser.

## ``letter-spacing``
__But__: Gérer l'espacement entre les lettres.
````css
p {
	letter-spacing: 2px;
}
````
* Cela rend le texte un peu plus "aéréé entre les lettres.
* On peut même réduire l'espace avec une valeur négative: ``letter-spacing: -1px``

## ``white-space``
__But__: Contrôler le comportement des __espace__ et des retour à la ligne.
````css
p {
	white-space: nowrap;
}
````
* ``normal(par défaut)``: Retour à la ligne automatique, les espaces multiples sont réduits.
* ``nowrap``: Tout reste sur une seule ligne, même si ça déborde.
* ``pre``(_présentation_): Les espaces et retours à la ligne sont respectés, comme dans ``<pre>``.
* ``pre-wrap``: Même chose que ``pre``, mais le texte peut retourner à la ligne s'il est trop long.

### Qu'est ce que ``pre`` ?
On fait référence à un comportement __identique__ à la balise HTML ``<pre>``: Afficher les espaces et les retours à la ligne exactement comme ils sont écrits dans le HTML.
Car le __HTML normal__:
* Réduit les espaces multiples à un seul.
* Ignore les __retours à la ligne__(sauf ``<br>``)

Mais avec ``white-space: pre``, les espaces et les sauts de ligne sont conservés à l'écran!
Et à quoi ça peut servir?
* Afficher du __code source__ ou des __fichiers texte__.
* Afficher des __poèmes__, des __formats fixes__ (ex: tableau en texte brut).
* Simuler une __console__, des __logs__ ou un affichage terminal.
* Exemple:
	````html
	<div style="white-space: pre;">
		Bonjour      le monde !
		C’est    un test.
	</div>
	````
	Donne le résultat:
	````html
  Bonjour      le monde !
  C’est    un test.
	````

## ``Overflow``
__But__: Gérer le contenu qui dépasse la boîte.
````css
div {
	overflow: auto;
}
````
* ``visible``: Tout déborde.
* ``hidden``: Ce qui dépasse est __caché__.
* ``scroll``: Ajoute toujours une __barre de défilement__.
* ``auto``: Ajoute une barre seulement __si besoin__.

## ``z-index``
__But__: Gérer les superpositions d'éléments(Comme des calques Photoshop).
````css
.modal {
	position: absolute;
	z-index: 1000;
}
````
* Ne fonctionne que si ``position`` a une valeur autre que ``static`` .

### Les positions ``static``, ``relative`` et ``absolute``, ``fixed`` et ``sticky``.
* ``static``: C'est le comportement normal d'un élément.
	* Il est dans le flux du document(empilement naturel).
	* Tu ne peux pas le déplacer avec ``top``, ``left``, etc.
	* Il suit simplment l'ordre du HTML.

* ``relative``: L'élément reste dans le flux normal, mais tu peux le déplacer légèrement.
	* Il occupe toujours sa place d'origine.
	* Le déplacement est __visuel seulement__, basé sur sa __propre position de départ__.
	* C'est très utile pour:
		* Créer des effets subtils (Comme vibration?)
		* Ajouter des éléments __absolus à l'intérieur__(Voir juste après)

* ``absolute``: L'élément est retiré du flux( Comme s'il n'existait pas à cet endroit)
	* Il se positionne __par rapport__ à son parent positionné(``relative``, ``absolute`` ou ``fixed``).
	* S'il n'y en a pas, il se positionne par rapport au ``<body>``/ ``<html>``.

* ``fixed``: À connaître absolument.
	* L'élément est fixé par rapport à la fenêtre.
	* Il ne bouge pas quand tu scrolles.
	* Il est retiré du flux, comme ``absolute``.
	* Usages typiques:
		* Barre de navigation en haut de page.
		* Boutons "retour en haut".
		* Popups ou modales toujours visibles.
		* Élements persistants dans une application(ex: chat, panier).

* ``sticky``: Moins connue, mais super pratique.
	* L'élément se comporte comme ``relative`` au début.
	* Jusqu'à ce qu'il atteigne la position qu'on lui a donnée(``top``, ``left``, ``bottom``, ``right``).
	* À ce moment-là, il devient fixé(comme ``fixed``), mais seulement dans son conteneur(Le bloc de défilement le plus proche. Mais si un de ses parents sont ``auto/hidden/scroll``, mais qui ne scroll pas,  ``sticky`` va coller sur eux,et ça peut créer aucun effet).

## ``flex-wrap``
__But__: Autoriser ou non les __retours à la ligne__ quand on utilise ``flex``.
````css
.container {
	display: flex;
	flex-wrap: wrap;
}
````
* Si tu ne mets pas ``wrap``, tous les éléments se mettent sur une seule ligne( Parfois ils sortent même de la page).

## ``gap``
__But__: Ajouter un espacement régulier entre les éléments d'un ``flex`` ou ``grid``.
````css
.container {
	display: flex;
	gap: 16px;
}
````
* Fonctionne avec ``flex`` et ``grid``.
* ``row-gap`` et ``column-gap`` existent aussi pour séparer les directions.

## ``transform``
__But: Appliquer des transformations visuelles(déplacement, rotation, zoom...)__
````css
.box {
	transform: scale(1.2) rotate(10deg)
}
````
* ``scale(1.5)``: Zoomer
* ``rotate(45deg)``: Pivoter
* ``translateX(50px)``: Déplacer sur l'axe X.
* ``skew(10deg)``: Déformer.

## ``transition``
__But: Animer un changement de style(Comme un effet de survol doux)__
````css
.button {
	transition: background-color 0.3s ease;
}

.button:hover {
	background-color: blue;
}
````
* On peut aussi animer: ``color``, ``transform``, ``opacity``, etc.
* Si on veut appliquer le style de transition globalement, on peut faire: ``transition: background-color 0.3s ease;``.
* Généralement, on l'utilise pour modifier le transition par défaut.

Exemples:
* Une boîte qui se soulève au survol
	````css
	.card {
		transform: translateY(0);
		transition: transform 0.3s ease, box-shadow 0.3s ease;
	}

	.card:hover {
		transform: translateY(-5px);
		box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
	}
	````


## ``Animation``
__But: Créer une animation plus complexe, souvent en boucle__.
````css
@keyframes bounce  {
	0%, 100% {transform: translateY(0);}
	50% {transform: translateY(-20px);}
}

.ball {
	animation: bounce 0.5s infinite;
}
````

