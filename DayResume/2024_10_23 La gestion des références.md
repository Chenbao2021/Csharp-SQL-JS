# React
***
## I - La gestion des références
Quand on fait une filtre sur une table, essaies d'affecter le nouveau table dans une nouvelle variable. Cela évite des problèmes. Par exemple:
````
let histo = JSON.parse(localStorage.getItem('INSPECTARE_HISTORY') || '[]') as HeaderClass[];
console.log('histo1', histo);

let histoFiltre = histo.filter((hh: HeaderClass) => {
    console.log('hh.idReport !== 0', hh.idReport !== 0);
    return hh.idReport !== 0;
});
````



