# React
***
#### I - useMemo&onClick
***
__useMemo__ is not the correct hook to use for handling events like 'onClick', we should use __useCallback__ instead.
__useMemo__ is used to memoized expensive computations, not to handle event callbacks.

That means,  the principal difference between useMemo and useCallback is :
- useMemo : This hook will execute the fonction passed in argument, then return the result.
- useCallback: This hook won't execute the fonction in argument, just return the reference of fonction.

