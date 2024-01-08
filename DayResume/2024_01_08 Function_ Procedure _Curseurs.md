# SQL
#### 1 - Function
- A function is a tool that used __to propduce an output__ for the provided inputs.
- In function, we can't use some DML statements like 'INSERT', 'DELETE', 'UPDATE', etc . (When asking a question,  we shouldn't change the result).
- It is a function, just like "SUM, COUNT, ..." that we used everytime.
- a function can be called by procedure, within a query(like SELECT)
- a scalar function must called by a username (or scheme).
- 
#### 2 - PROCEDURE
- A set of instruction which takes input and __performs a certain tasks__
- Its can also return values by OUTPUT parameters, and can return various values at once.
- A procedure cannot be called by function, within a query.

#### 3 - DIfférents cas d'utiliation de curseurs : 
##### Exemple 1:  Le procédure stockée utilise un curseur pour récupérer des informations sur des produits dans une table, et deux paramètres OUTPUT sont utilisés pour renvoyer des résultats agrégés : 
- Le nombre total de produits.
- La somme des prix de tous les produits.

##### Exemple 2: Nous ayons une table d'employés et que nous voulions calculer le salaire total de tous les employés dans un département spécifique et également compter le nombre total d'employés dans ce département.

##### Exemple 3: Nous voulons calculer la moyenne et le nombre total de produits vendus pour chaque catégorie de produits.

##### Exemple 4: NOus voulons récupérer des informations sur les employés dans une entreprise enfonction de leur salaire.
La procédure stockée doit renvoyer le nombre total d'employés et le nombre d'employés dont le salaire est supérieur à une valeur donnée.

##### Exemple 5: Nous voulions compter le nombre d'étudiants inscrits dans chaque département d'une université.
La procédure stockée devrait revoyer le nombre total d'étudiants ainsi que le nombre d'étudiants par département.

    CREATE PROCEDURE CountStudentsByDepartment
        @TotalStudents INT OUTPUT,
        @DepartmentCounts TABLE (
            DepartmentID INT,
            DepartmentName NVARCHAR(100),
            StudentCount INT
        ) OUTPUT
    AS
    BEGIN
    DECLARE @StudentID INT, @StudentName NVARCHAR(100), @DepartmentID INT

    -- Initialisation des variables de sortie
    SET @TotalStudents = 0

    -- Initialisation de la table de sortie
    INSERT INTO @DepartmentCounts (DepartmentID, DepartmentName, StudentCount)
    SELECT DepartmentID, DepartmentName, 0
    FROM DepartmentsTable

    -- Déclaration du curseur
    DECLARE cursor_name CURSOR FOR
    SELECT StudentID, StudentName, DepartmentID
    FROM StudentsTable

    -- Ouverture du curseur
    OPEN cursor_name

    -- Récupération de la première ligne
    FETCH NEXT FROM cursor_name INTO @StudentID, @StudentName, @DepartmentID

    -- Boucle de traitement ligne par ligne
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Logique de traitement pour chaque ligne
        SET @TotalStudents = @TotalStudents + 1

        -- Mise à jour du compteur par département
        UPDATE @DepartmentCounts
        SET StudentCount = StudentCount + 1
        WHERE DepartmentID = @DepartmentID

        -- Récupération de la ligne suivante
        FETCH NEXT FROM cursor_name INTO @StudentID, @StudentName, @DepartmentID
    END

    -- Fermeture et libération des ressources du curseur
    CLOSE cursor_name
    DEALLOCATE cursor_name
    END
    
##### Exemple 6 : Nous supposons que nous voulions récupérer des informations sur les commandes passées par chaque client.
Le procédure stockée doit renvoyer le nombre total de client ainsi que le nombre de commandes pour chaque client.



