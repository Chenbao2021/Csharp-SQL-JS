Source: https://medium.com/hackernoon/introducing-immer-immutability-the-easy-way-9d73d8f71cb3

## How does Immer work ?
1. Copy-on-write
2. Proxies

Initially, when the producer starts, there is only one such proxy. (It is the __draft__ object that get's passed into your function)
Whenever you read any non-primitive value from that first proxy, it will in turn create a Proxy for that value.(So that means that you end up with a proxy tree, that kind of overlays(or shadows) the original base tree.)

Now, as soon as you try to change something on a proxy, it will immediately create a shallow copy of the node in the source tree it is related to , and sets a flag "modified". From now on, any future read and write to that proxy will not end up in the source tree, but in the copy.

When the producer finally ends, it will just walk through the proxy tree, and, if a proxy is modified, take the copy; or, if not modified, simply return the original node.

## Producers
Immer wprls nu writing producers, and the simplest producer possible looks like this:
````
import produce from "immer"

const nextState = produce(currentState, draft => {
  // empty function
})

console.log(nextState === currentState) // true
````
The producer function takes two arguments, the __currentState__ and a __producer function__.
- currentState : Determines our starting point
- producer function: What needs to happen to it.

The producer function receives one argument, the draft; which is a proxy to the current state you passed in.
Any modification you make to the draft will be recorded and used to produce nextState.
The currentState will be untouched during this process.

## A reducer with a producer
The products are received as an array, transformed using reduce, and then stored in a map with their id as key.
````
// Shortened, based on: https://github.com/reactjs/redux/blob/master/examples/shopping-cart/src/reducers/products.js
const byId = (state, action) => {
  switch (action.type) {
    case RECEIVE_PRODUCTS:
      return {
        ...state,
        ...action.products.reduce((obj, product) => {
          obj[product.id] = product
          return obj
        }, {})
      }
    default:      
      return state
  }
}
````
The boilerparty here is:
1. We have to construct a new state object, in which the base state is preserved and the new products map is mixed in. It is not too bad in this simple case, but this process has to be repeated for every action, and on every level in which we want.
2. We have to make sure to return the existing state if the reducer doesn't do anything.

With Immer, we only need to reason about the changes we want to make relatively to the current state. Without needing to take the effort to actually produce the next state. So, when we use produce in the reducer, our code simply becomes:
````
const byId = (state, action) =>
  produce(state, draft => {
    switch (action.type) {
      case RECEIVE_PRODUCTS:
        action.products.forEach(product => {
          draft[product.id] = product
        })
        break
    }
  })
````
