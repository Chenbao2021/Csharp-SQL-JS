Source : https://javascript.info/array-methods

#### Add/Remove items
- arr.push(...items) : Adds items to the end
- arr.pop() : Remove the last element and returns the element it removed
- arr.unshift() : adds new elements to the beginning of an array
- arr.shift() : Shift(remove) the first element of the array and returns the shifted element.

#### splice
- The splice method is a swiss army knife for arrays, it can do:
    - Insert
    - Remove
    - Replace elements.

- The syntax is : arr.splice(start[, deleteCount, elem1, ..., elemN])
- It modifies __arr__ starting from the index __start__ : removes __deleteCount__ elements and then inserts __elem1, elem2, ... , elemN__ at their place. Returns the array of remvoed element.

#### slice
- The syntax is: arr.slice([start], [end])
    It returns a new array copying to it all items from index __start__ to __end__(not include). Both __start__ and __end__ can be negative.
- We can also call it without arguments: arr.slice() creates a copy of arr. That is often used to obtain a copy for further transformation that should not affect the original array.

#### concat
- The method arr.concat creates a new array that includes values from other arrays or additional items.
- The syntax is : arr.concat(arg1, arg2 ...)
    It accepts any number of arguments - either arrays or values.
- Symbol.isConcatSpreadable

#### Iterate: forEach
- The arr.forEach method allows to run a function for every element of the array.
- arr.forEach(function(item [, index][, array]) {
    // ... Do something with item
}
    Exemple: 
    ````
    ["Bilbo", "Gandalf", "Nazgul"].forEach((item, index, array) => {
      alert(`${item} is at index ${index} in ${array}`);
    });
    ````
- ! The result of the function is thrown away and ignored, it is used just for print something ,and not for manipulations.

#### Searching in array
- arr.indexOf(item [, from]) : Looks for __item__ starting from index __from__, and returns the index where it was found, otherwise -1
    - indexOf uses the strict equality for comparauison. So if we look for __false__, it finds exactly __false__ and not the zero.
    - The method arr.lastIndexOf is the same as __indexOf__, but looks for from right to left.
- arr.includes(item [, from]) : looks for __item__ starting from index __from__, returns __true__ if found.
    - If we want to check if __item__ exists in the array, and don't need the index, then arr.includes is preferred
    - The includes method handles NaN correctly.

#### find and findIndex/findLastIndex
- arr.find() : We have an array of objects, how do we find an object with the specific condition ?
    - Syntax : 
        arr.find(function(item [, index, array]) {
            // If true is returned, item is returned and iteration is stopped
            // for falsy scenario returns undefined
        }
    - The function is called for elements of the array, one after another.
    Exemple : 
        ````
        let users = [
          {id: 1, name: "John"},
          {id: 2, name: "Pete"},
          {id: 3, name: "Mary"}
        ];
        let user = users.find(item => item.id == 1);
        alert(user.name); // John
        ````
    - In real life arrays of objects is a common thing, so the __find__ method is very useful.

- arr.findIndex: This method has the same syntax, but returns the index where the element was found instead of the element itself.
    - The value of __-1__ is returned if nothing found.
- arr.findLastIndex : idem.

#### filter
- The __find__ method looks for a single(first) element that makes the function return __true__
    If there may be many elements, we can use arr.filter(fn)
    ````
    let results = arr.filter(function(item, index, array) {
      // if true item is pushed to results and the iteration continues
      // returns empty array if nothing found
    });
    ````
- Exemple :
    ````
    let users = [
      {id: 1, name: "John"},
      {id: 2, name: "Pete"},
      {id: 3, name: "Mary"}
    ];
    
    // returns array of the first two users
    let someUsers = users.filter(item => item.id < 3);
    
    alert(someUsers.length); // 2
    ````

#### Transform an array : Methods that transform and reorder an array
- arr.map : It calls the function for each element of the array and returns the array of results.
    - The syntax is : 
        ````
        let result = arr.map(function(item, index, array) {
          // returns the new value instead of item
        });
        ````
    - Exemple:
       ````
       let lengths = ["Bilbo", "Gandalf", "Nazgul"].map(item => item.length);
        alert(lengths); // 5,7,6
       ````

- arr.sort([compare(a, b)])
    - The function compare is look like:
    ````
    function compare(a, b) {
      if (a > b) return 1; // if the first value is greater than the second
      if (a == b) return 0; // if values are equal
      if (a < b) return -1; // if the first value is less than the second
    }
    // 1
    let arr = [ 1, 2, 15 ];
    arr.sort(compareNumeric);
    alert(arr);  // 1, 2, 15
    // 2
    [1, -2, 15, 2, 0, 8].sort(function(a, b) {
      alert( a + " <> " + b );
      return a - b;
    });
    ````
    - Arrow function for the best :
    ````
    arr.sort( (a, b) => a - b );
    ````
    - Use localeCompare for strings
    ````
    let countries = ['Österreich', 'Andorra', 'Vietnam'];
    alert( countries.sort( (a, b) => a > b ? 1 : -1) ); // Andorra, Vietnam, Österreich (wrong)
    alert( countries.sort( (a, b) => a.localeCompare(b) ) ); // Andorra,Österreich,Vietnam (correct!)
    ````
- arr.reverse() : It reverses the order of elements in __arr__
- arr.split(delim, limitOfArrayLength) : Splits the string into an array by the given delimiter __delim__
    Ex : 'Bilbo, Gandalf, Nazgul'.split(', ', 2); // ['Bilbo', 'Gandalf']
- arr.join(glue) : Create a string of __arr__ items joined by __glue__ between them.
- arr.reduce() / arr.reduceRight() : Used to calculate a single value based on the array.
    Syntaxe:
    ````
    let value = arr.reduce(function(accumulator, item, index, array) {
      // ...
    }, [initial]);
    ````
- Array.isArray: 
    - Array do not form a separate language type, they are based on objects .
        typeof {} = typeof [] = object
        Array.isArray({}); // false 
        Array.isArray([]); // true
- Most methods support "thisArg"
    Almost all array methos that call functions (find, filter, map, etc), exempt sort, accept an optional additional parameter __thisArg__
    It is rarely used, and it is optional.
    - The value of __thisArg__ parameter becomes __this__ for __func__.
    ````
    let army = {
      minAge: 18,
      maxAge: 27,
      canJoin(user) {
        return user.age >= this.minAge && user.age < this.maxAge;
      }
    };
    
    let users = [
      {age: 16},
      {age: 20},
      {age: 23},
      {age: 30}
    ];
    
    // find users, for who army.canJoin returns true
    let soldiers = users.filter(army.canJoin, army);
    
    alert(soldiers.length); // 2
    alert(soldiers[0].age); // 20
    alert(soldiers[1].age); // 23
    ````
