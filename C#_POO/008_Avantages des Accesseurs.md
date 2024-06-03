Implémenter un accesseur pour un attribut public dans une classe C# au lieu d'accéder directement à cet attribut présente plusieurs avantages importants.
````C
public class VesselContainer
{
    private List<VesselsModel> m_listVessels = new List<VesselsModel>();

    public List<VesselsModel> Vessels
    {
        get
        {
            return m_listVessels;
        }
        set
        {
            m_listVessels = value;
        }
    }
}
````
### I - Encapsulation
En utilisant des accesseurs, vous contrôlez l'accès à ``m_listVessels`` et pouvez __changer l'implémentation interne sans affecter les autres parties du code qui utilisent cette classe__.(Par exemple, utiliser une base de données au lieu d'une liste en mémoire, on a pas besoin de modfier l'utilisation de la classe)

### II - Validation des Données
Les accesseurs permettent d'ajouter des validations lorsque des données sont lues ou modifiées.
Exemple:
````C
 set
{
    if (value == null)
    {
        throw new ArgumentNullException(nameof(value), "La liste ne peut pas être nulle.");
    }
    m_listVessels = value;
}
````
* L'accesseur ``set`` assure que ``m_listVessels`` ne sera jamais assigné à ``null``

### III - Contrôle d'Accès
Les accesseurs permettent de définir différents niveaux d'accès pour la lecture et l'écriture d'un attribut.
Par exemple on peut avoir un accès public pour get, et un accès private pour set:
````C
public List<VesselsModel> Vessels
{
    get
    {
        return m_listVessels;
    }
    private set
    {
        m_listVessels = value;
    }
}
````

### IV - Notifications de Changement
Les accesseurs permettent d'ajouter des mécanismes de notification lorsque des valeurs sont modifiées.
(Par exemple, Déclencher un event lors d'ajout)

### V - Maintenabilité
Les accesseurs permettent de modifier l'implémentation interne sans affecter les autres parties du code qui utilisent la classe.

