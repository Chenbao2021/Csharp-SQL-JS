***
# I - .NET 1.0 et l'apparition des délégués
***
Link : https://fguillot.developpez.com/cours/dotnet/introduction-delegates-csharp/

Quand on dit que 2 méthodes ont la même signature:
- Le même nombre d'entrée
- Les paramètres d'entrée ont du même type
- Le même type de retour.

Donc pour utiliser la délégation, il faut que la délégué et la méthode à deleguer ont la même signature.

Mais qu'est ce que c'est délégation ? 
1. On enregistre une/des méthodes afin de retarder son exécution.
2. C'est une abstration des méthodes concrèts, ça facilite la lisibilité et maintenabilité des codes quand il y a une phénomène de répétition important.
3. On peut enregistrer des méthodes qui a la même signature, soit un exempme :
```
namespace DelegateProgram
{
    public delegate string[] SortingMethod(string[] data);
    
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = new string[] { "toto", "titi", "tutu" };
            SortingMethod sortingMethod = new SortingMethod(SortingAlgorithms.BubbleSort);
            ComputeArray(data, sortingMethod);
        }
    
        private static string[] ComputeArray(string[] data, SortingMethod sortingMethod)
        {
            // Opérations sur le tableau
    
            // Tri du tableau
            data = sortingMethod(data);
    
            // D'autres opérations sur le tableau
    
            return data;
        }
    }
}
```
- On déclare la méthode délégué dans le namespace
- on instance une méthode délégué par le mot clé new, et la méthode sans parenthèse(On dit passer par référence) .
- C'est dans la méthode ComputeArray qu'on exécute la méthode délégué.
- Il est possible de enregistrer plusieurs méthodes:
    ````
    SortingMethod sortingMethod = new SortingMethod(SortingAlgorithms.BubbleSort);
    sortingMethod += SortingAlgorithms.QuickSort;
    sortingMethod += SortingAlgorithms.InsertionSort;
    ````
    Et ces méthodes vont exécuter l'un après l'autre.

L'inférence de type
- Le compilateur C# est capable <d'inférer>, c'est à dire de déterminer tout seul le type d'un délégué, grâce au type de la variable qui va l'accueillir:
    ```
    SortingMethod sortingMethod = SortingAlgorithms.BubbleSort;
    ```
    Ou même
    ```
    ComputeArray(data, SortingAlgorithms.BubbleSort);
    ```

***
# II - NET2.0 et let méthodes anonymes
***
Les méthodes anonymes:
- Il ne s'agit ni plus ni moins que de déclarer directement le code que devra référencer un délégué, sans avoir à créer une méthode pour le contenir.
- La déclaration de la méthode peut ressembler à ceci:
    ````
    SortingMethod sortingMethod = new SortingMethod(delegate(string[] data)
                                  {
                                      // Tri arbitraire
                                      return data;
                                  });
    ````
    ou 
    ```
    SortingMethod sortingMethod = delegate(string[] data)
                              {
                                  // Tri arbitraire
                                  return data;
                              };
    ```

Le piège des fermetures(ou clôtures)
- La portée d'une variable change lorsqu'on la passe à une méthode anonyme.
- C'est à dire une méthode anonyme n'a pas sa propre porté, il partage la même porté que celle du code qui la déclare.
- Le compilateur n'a aucun moyen de déterminer si vous souhaitez ou non qu'il suive les changements éventuels de valeur de la variable.
- __Il faut se méfier des méthodes anonymes, et penser à vérifier la portée des variables que l'on utilise dans le corps de celles-ci.

***
# III - .NET 3.5 et les expressions lambda
***
Les méthodes anonymes sont largement simplifiées grâce aux expressions lambda.
- Exemple:
    ```
    SortingMethod sortingMethod = new SortingMethod((string[] data) =>
                                      {
                                          // Tri arbitraire
                                          return data;
                                      });
    ```
    - Le mot-clef <delegate> a disparu
    - Un nouvel opérateur lambda "=>" qui se lit "conduit à".

- Le compilateur C# est capable d'inférer automatiquement le type de ses paramètres d'entrée et sont type de sortie à partir du type de délégué que nous indiquons voulor instancier:
    ````
    SortingMethod sortingMethod = new SortingMethod((data) =>
                                      {
                                          // Tri arbitraire
                                          return data;
                                      });
    ````

Les événements:
- Grâce aux événements, une classe a la possibilité d'effectuer du "push", c'est-à-dire d'informer le reste du code qu'il se passe quelque chose de particulier afin que celui-ci réagisse en conséquence.
    Par exemple, une classe TrainStation re présentant une gare ferroviaire, et cette classe va avoir la responsabilité d'informer de l'arrive d'un tran:
    ````
    public delegate void TrainArrivalEventHandler(object sender, EventArgs e);
    
    public class TrainStation
    {
        public event TrainArrivalEventHandler TrainArrival;
    }
    ````
    - On déclare d'abord une delegate avant la déclaration d'un event.
    - sender    : Indiquer quel objet a levé l'événement.
    - EventArgs : Encapsuler les arguments de l'événement.

-  delegate se trouve dans namespace, et event se trouve dans une classe en tant qu'un field.
    En indiquant quel délégué sera chargé d'encapsuler les méthodes qui vont s'abonner à cet événement.
    Ce délégué servant de gestionnaire d'événement
-  event est soumise a un délégué ayant une signature bien précise(void + 2 paramètres).
-  Exemple d'instanciation de la classe TrainStation et s'abonner à son événement TrainArrival:
    ````
    class Program
    {
        static void Main(string[] args)
        {
            TrainStation trainStation = new TrainStation();
            trainStation.TrainArrival += OnTrainArrival;
        }
    
        private static void OnTrainArrival(object sender, EventArgs e)
        {
            Console.WriteLine("Un train est entré en gare.");
        }
    }
    ````
    - Abonner un événement se fait exactement de la même façon que s'abonner à un délégué
    - Pour que la méthode OnTrainArrival soit exécutée, il faut que la classe TrainStation exécuté l'événement.
    ````
    public class TrainStation
    {
        public event TrainArrivalEventHandler TrainArrival;
    
        public void RaiseTheEvent()
        {
            if (TrainArrival != null)
                TrainArrival(this, EventArgs.Empty);
        }
    }
    ````
    - Lever l'événement revient simplement à exécuter le délégué en lui passant l'objet à l'origine de cette levée

Finalement, on peut dire qu'un événement se comporte exactement comme le délégué dont il dépend. Pourtant, avec quelques mécanismes supplémentaires.
- Conserver la responsabilité de la levée de l'événement
    Un événement permet à la classe de conserver la responsabilité de la levée de l'événement, et donc de l'exécution du délégué. 
- Event dispose deux accesseurs add et remove, qui permet d'effectuer des actions particulières lorsqu'une méthode s'abonne ou se désabonne d'un événement.
    ```
    public class TrainStation
    {
        private TrainArrivalEventHandler _trainArrival;
    
        public event TrainArrivalEventHandler TrainArrival
        {
            add
            {
                Console.WriteLine("Quelqu'un s'est abonné à l'événement.");
                _trainArrival += value;
            }
    
            remove
            {
                Console.WriteLine("Impossible de se désabonner de l'événement.");
            }
        }
    
        public void RaiseTheEvent()
        {
            if (_trainArrival != null)
                _trainArrival(this, EventArgs.Empty);
        }
    }
    ```
__Les délégués < clef en main >__
C'est pénible de devoir déclarer pour chaque cas nécessitant leur utilisation un type de délégué approprié.
````
public delegate string[] SortingMethod(string[] data);
````
Du coup C# ont proposé certains clefs pour délégué:
- Func: Un délégué ayant un type de retour, et jusqu'à 4 paramètres d'entrée.
- Action : N'ayant pas de type de retour, et jusqu'à 4 paramètres d'entrée.
- Predicate : Toute expression , lorsqu'on l'évalue, renvoie un booléen.
- Converter
- Comparison
- EventHandler: 
    ```` 
    public event EventHandler<CustomEventArgs> onCustomEvent;
    ````

***
# IV - .NET 4.0, la covariance et la cntravariance
***
- En .NET , les objets peuvent hériter des attributs d'autres objets => L'héritage.
- Puisque les objets peuvent hériter les uns des autres selon une hiérarchie, ils peuvent aussi substituer les uns aux autres.
- Tous les objets que contient cette collection pourrons être manipulés comme des Stream, car ils en héritent tous. Mais à moins de les recaster vers leur type d'origine, seuls les membres de la classe Stream seront accessibles.
    ```
    IList<Stream> collection = new List<Stream>
                            {
                                new MemoryStream(),
                                new DeflateStream(new MemoryStream(), CompressionMode.Compress),
                                new GZipStream(new MemoryStream(), CompressionMode.Compress),
                                new FileStream(@"C:\", FileMode.Create),
                                new NetworkStream(new Socket(new SocketInformation())),
                                new PrintQueueStream(new PrintQueue(new PrintServer("foobar"), "foobar"), "foobar")
                            };
    ```

La covariance
- Grâce à la covariance, nous allons pouvoir substituer le type de sortie d'un délégué. 
    Lorsqu'on créons un délégué, nous définissons en fait une signature de méthode, qui permet de désigner quelles méthodes ce délégué va pouvoir encapsuler.
    Si le type de retour est générique, on peut indiquer que celui-ci est covariant, grâce au mot-clef __out__

