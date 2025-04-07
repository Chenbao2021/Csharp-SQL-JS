On va faire une checklist claire et pratique des cas ou on a besoin d'utiliser ``addEventListener`` dans une application React - Que ce soit dans un ``useEffect``, via un ``ref``, ou à un autre niveau.

# I - Cas concrets où tu dois(ou devrais) utiliser ``addEventListener`` en React.
### A. Événements globaux non gérés par React.
React ne fournit pas de ``onXxx`` pour certains événements globaux comme:
|Événement|Exemple|
|--|--|
|``resize``|``window.addEventListener('resize', ...)``|
|``scroll``|``window.addEventListener('scroll', ...)``|
|``visibilitychange``|Pour savoir si l'onglet est actif.(Mettre en pause une vidéo, stopper animation, etc.)|
|``keydown``|Raccourcis clavier globaux|

Exemple:
````js
useEffect(() => {
	const handleKey = e => e.key === "Escape" && closeModal();
	window.addEventListener("keydown", handleKey);
	return () => window.removeEventListener("keydown", handleKey);
})
````

### B. Exemple avec ``mousemove``
````js
import {useEffect} from "react";

export default function MouseMoveGlobal() {
	useEffect(() => {
		const handleMouseMove = e => console.log(`Souris à: ${e.clientX},${e.clientY}`);
		window.addEventListener("mousemove", handleMouseMove);
		return () => window.removeEventListener("mousemove", handleMouseMove);
	}, [])
	return (
		<div style={{padding: "2rem"}}>
			<h2>Déplace ta souris</h2>
			<p>(Tu verras les coordonnées dans le console)</p>
		</div>
	)
}
````
* Ici, React ne peux pas intervenir, tout se fait au niveau du window(DOM pur).

### C. Exemple avec ``keydown``
React gère ``onKeyDown``, mais seulement si un élément est focus(Souvent un input).
Mais si tu veux un raccourci clavier global comme ``Escape``, ``Ctrl+S``, etc., tu dois écouter ``window``
````js
import {useEffect} from "react";

export default function KeyDownGlobal() {
	useEffect(() => {
		const handleKey = e => {
			console.log("Touche pressée :", e.key);
			if(e.key === "Escape") {
				console.log("Fermeture de la modale !");
				if(modalOpen) closeModal();
			}
		};

		window.addEventListener("keydown", handleKey);
		return () => window.removeEventListener("keydown", handleKey);
	}, [])
	return (
		<div style={{padding: "2rem"}}>
			<h2>Appuie sur une touche du clavier</h2>
			<p>Regarde dans la console !</p>
		</div>
	)
}
````
# II - Utilisation de ref pour gérer des events.
### A. Qu'est ce que "un click hors React" ou "un élément en dehors du cycle React"?
Quand on déclare ceci:
````js
<button onClick={() => console.log("click dans React")}>Click</button>
````
React intercepte le clic via son système d'événement synthétique. On n'a pas de contact direct avec le DOM natif.
Mais si on fait:
````js
const btnRef = useRef();
useEffect(() => {
	const handleClick = () => console.log("click DOM natif (hors React)");
	btnRef.current.addEventListener("click", handleClick);

	return () => btnRef.current.removeEventListener("click", handleClick);
})
````
Ici, on ajoute manuellement un event listener sur un élément réel du DOM, indépendamment de React.
Et on l'appelle "hors Rect".

### B. Pourquoi c'est "hors cycle React"?
React contrôle le rendu, le state, les props, les événements via ses propres sytèmes.
Mais quand tu touches le DOM avec ``addEventListener``, tu fais une modification directe, non suivie par React.

Par conséquent, si React re-render l'élément(``<button>``), il pourrait le remplacer dans le DOM -> Ton ``addEventListener`` est donc perdu!

D'où l'importance de le mettre dans un ``useEffect`` qui suit le DOM.

### C. Exemple: SUivre la souris uniquement dans un élément précis(``mousemove`` via ``ref``)
````js
import {useEffect, useRef} from "react";

export default function MouseMoveAvecRef() {
	const boxRef = useRef();

	useEffect(() => {
		const handleMove = e => console.log(`Souris dans la boîte: x = ${e.offsetX}, y = ${e.offsetY}`);
		const box = boxRef.current;
		box.addEventListener("mousemove", handleMove);

		return () => box.removeEventListener("mousemove", handleMove);
	})

	return(
		<div
			ref={boxRef}
			style={{ width: "300px", height: "200px" }}
		>	
			Bouge ta souris ici.
		</div>
	)
}
````

### D. Exemple avec focus automatiquement.
````js
import {useEffect, useRef} from "react";

export default function FocusInput() {
	const inputRef = useRef();
	useEffect(() => {
		inputRef.current.focus(); // DOM direct
	})

	return (
		<input ref = {inputRef} placeholder="Auto-focus via ref" />
	)
}
````

# III - keydown et la notion de focus.
C'est une question important qui va recevoir les événements ``keydown``, et quand ils sont déclenchés.

|Cas|Le ``keydown`` se déclenche?|
|--|--|
|Un élément focusable est focusé|Oui, l'événement va à cet élément|
|Un élément non focusable ou pas focus|Non, il ne reçoit rien|
|Tu écoutes ``keydown`` sur ``window`` ou ``document``|Oui, quoi qu'il arrive, tant que la page est active|

### Les événements clavier vont à l'élément focusé.
* Si tu appuies sur une touche, l'événement ``keydown`` est émis par l'élément qui a le focus.
* Il ne monte pas depuis tous les éléments DOM comme un clic(``click`` = Propagation dans le DOM).
* Donc si une ``div`` n'est pas focus, elle n'entend rien.

Exemple classique avec un input
````js
<input onKeyDown={e => console.log(e.key)}>
````
* Dès que tu tapes, l'input a le focus -> Il reçoit ``keydown``

Exemple avec un ``div`` focusable(``tabIndex={0}``)
````js
// Version React
<div tabIndex={0} onKeyDown={e => console.log(e.key)}>
	Appuie ici
</div>

// AVec eventListener
const boxRef = useRef();
useEffect(() => {
	const box = boxRef.current;
	const handleKey = e => console.log(`Touche pressée dans la div: ${e.key}`);

	box.addEventListener("keydown", handleKey);
	box.setAttribute("tabIndex", 0); // Pour qu'elle soit focusable.
	box.focus();

	return () => box.removeEventListener("keydown", handleKey);
}, []);

return (
	<div
		ref={boxRef}
	>
	...
	</div>
)
````
