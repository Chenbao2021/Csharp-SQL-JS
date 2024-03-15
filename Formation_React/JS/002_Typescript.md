Source: https://www.typescriptlang.org/docs/handbook/typescript-in-5-minutes.html

### I - TypeScript's type system.
It can highlight unexpected behavior in your code, lowering the chance of bugs.

### II - Defining Types
For example, to create an object with an inferred type which includes __name:string__ and __id:number__, you can write: 
````
interface User {
  name: string;
  id: number;
}

const user: User = {
  name: "Hayes",
  id: 0,
};
````

Since JavaScript supports classes and OOP, so does TS, you can use an interface declaration with classes:
````
interface User {
  name: string;
  id: number;
}
 
class UserAccount {
  name: string;
  id: number;
 
  constructor(name: string, id: number) {
    this.name = name;
    this.id = id;
  }
}
 
const user: User = new UserAccount("Murphy", 1);
````

You can use interfaces to annotate parameters and return values to functions: 
````
function deleteUser(user: User) {
  // ...
}
 
function getAdminUser(): User {
  //...
}
````

There are two syntaxes for bulding types: __Interfaces and Types__( Interface is prefered, use type when you need specific features) .

### III  Composing Types
#### - Unions
A popular use-case for union types is to describe the set of string or number literals that a value is allowed to be:
````
type WindowStates = "open" | "closed" | "minimized";
type LockStates = "locked" | "unlocked";
type PositiveOddNumbersUnderTen = 1 | 3 | 5 | 7 | 9;
````
To handle different types :
````
function wrapInArray(obj: string | string[]) {
  if (typeof obj === "string") {
    return [obj];
  }
  return obj;
}
````

#### - Generics
An array without generics could contain anything, an array with generics can describe the values that the array contains.
````
type StringArray = Array<string>;
type NumberArray = Array<number>;
type ObjectWithNameArray = Array<{ name: string }>;
````
You can declare your own types that use generics :
````
interface Backpack<Type> {
  add: (obj: Type) => void;
  get: () => Type;
}
 
// This line is a shortcut to tell TypeScript there is a
// constant called `backpack`, and to not worry about where it came from.
declare const backpack: Backpack<string>;
 
// object is a string, because we declared it above as the variable part of Backpack.
const object = backpack.get();
 
// Since the backpack variable is a string, you can't pass a number to the add function.
backpack.add(23);
````

### IV Structural Type System
Structural type system = If two object have the same shape, they are considered to be of the same type.
````
interface Point {
  x: number;
  y: number;
}
 
function logPoint(p: Point) {
  console.log(`${p.x}, ${p.y}`);
}
 
// logs "12, 26"
const point = { x: 12, y: 26 };
logPoint(point);
````
The __point__ variable is never declared to be a Point type, however, TypeScript compares the shape of __point__ to the shape of __Point__ in the type-check. They have the same Shape, so the code passes.
The shape-matching only requires a subset of the object's fields to match:
````
const point3 = { x: 12, y: 26, z: 89 };
logPoint(point3); // logs "12, 26"
 
const rect = { x: 33, y: 3, width: 30, height: 80 };
logPoint(rect); // logs "33, 3"
 
const color = { hex: "#187ABF" };
logPoint(color);
````
There is no difference between how classes and objects conform to shapes.






