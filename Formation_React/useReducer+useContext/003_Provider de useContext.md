# Provider en useContext
On peut utiliser useContext sans utiliser Provider, mais cela nous limite à seulement la lecture des données.

Dans React, __Provider__ est un élément essentiel pour utiliser __useContext__. Il joue deux rôles clés:
- Encapsuler les valeurs et les fonctions de mise à jour
- Propager les valeurs aux composants descendants

Exemple:
````
const UserContext = createContext(null);
const UserProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  return (
    <UserContext.Provider value={{ user, setUser }}>
      {children}
    </UserContext.Provider>
  );
};
````
- Dans cet exemple, __Provider__ est utilisé pour encapsuler les valeurs __user__ et __setUser__ dans le contexte __UserContext__ . Le composant enfant peut ensuite utiliser __useContext__(Avec le nom de context crée, ici UserContext) pour accéder à ces valeurs.

# Les erreurs fréquents avec l'utilisation de Provider de useContext
1. Mauvaise utilisation des Providers imbriqués:
    - Avoir une couche juste pour le ContextProvider, et mettre tous les données à partager dédans. Et ne pas mélanger avec d'autres components
    ````
    // Mauvais
    <ParentComponent>
        <ChildComponent />
    </ParentComponent>
    // Bien
    <ParentComponent>
        <SomeContextProvider>
            <ChildComponent />
        </SomeContextProvider>
    </ParentComponent>
    ````
2. Oublier de fournir unevaleur par défaut
    Pour garantir que les composants qui consomment cet context auront toujours une valeur à utiliser, sinon accéder à un objet null peut déclancher une erreur, et provoquer des comportement inattendus ou plantages de l'applcation.
    `````
    // Bien
    const MyContext = React.createContext(defaultValue);
    ````
3. Modifer des valeurs de contexte toujours par des méthodes fournit par les provider.

4. Consommation multiple du même contexte
Cela fait référence à la situation où plusieurs composants ont besoin d'accéder à la même valeur de contexte dans une application React.


