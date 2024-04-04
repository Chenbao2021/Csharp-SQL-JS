***
# Partie I : Review codes
***
#### I - Créer un repertoire pour les styles
- Mettre tous les styles dans un même repertoire
- Les Import CSS est globale, donc importer tous les fichiers .css dans un même endroit, par exemple Main.tsx ou App.tsx.
- Installer l'extension __CSS Peek__ de Visual Studio.

#### II - Créer un repertoire pour les interfaces
- Un fichier par interface
- Chaque interface commence par __I__
- Une interface pour props doit terminer par __Props__

#### III - ?:
- Utiliser __?:__ data_type au : lieu de __data_type | undefined__

#### IV - Éviter au maximum "les fonctions volées"(useCallBack & useMemo)
- Tous les fonctions qui se trouve dans le projet doit se trouver dans un useCallBack, avec des dépéndences bien définit.
- Et tous les Arrays aussi doivent se retrouver dans useMemo.

#### V - Component =/= View

***
# Partie II : useReducer & useContext & Immer
***
- Dans le reducer, on ne met pas les Apis(comme gridApi) et les élements HTML(Car il y a une recursion infini).
- Les reducers ne sont pas comme redux, il ne peut pas traîter les cas asynchrone, donc on ne met pas les Apis dans des reducers, mais dans les useStates.
- Lire ces articles pour découvrir l'utilisation de useReducer + useContext + Provider:
    - https://medium.com/@seb_5882/a-short-guide-to-reacts-powerful-duo-usereducer-and-usecontext-23cea6f9ab35#:~:text=useReducer%20provides%20a%20way%20to,making%20your%20data%20globally%20available.
    - https://github.com/Chenbao2021/Csharp-SQL-REACT/tree/main/Formation_React/useReducer%2BuseContext

