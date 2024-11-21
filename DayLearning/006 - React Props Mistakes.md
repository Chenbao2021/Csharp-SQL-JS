[Original Text](https://dev.to/peboycodes/9-react-props-mistakes-that-are-hurting-your-apps-performance-3emc)
Props in React might feel straightforward, but mishandling them can slow your app to a crawl!

### I. Stop Passing Entire Objects When Only Specific Properties are Needed.
Passing entire objects as props when your component only needs a couple of properties leads to unnecessary re-renders anytime that object updates - even if the values you are using remain the same.
* Object : Reference
* string, int, boolean : the important is its values.
Donc au lieu de faire: ``function UserProfile({user}) {}``
On fait: ``function UserProfile({name, email}) {}``

### II. Avoid Creating New Objects in Props on Every Render
If you do this, you cerate new object reference every time your component renders.
And these new references force child components to re-render, even if the values are identical.
Ex: ``<Child style={{margin: '20px'}} config={{theme: 'dark'}} />``
Mais de la définir ailleur, ou dans un useMemo:
* ``const styles = React.useMemo(() => ({ margin: '20px', padding: '10px' }), []);``

### III - Avoid Unnecessarily Spreading Props
Using prop spreading(...props) might feel convenient, but it often does more harm than good.
Ensure all the props passed are needed.

### IV - Always Memoize Callback Props
This ensure stable references and avoid unnecessary updates.
````js
function TodoList() {
  const handleClick = useCallback((id) => {
    // handle click
  }, []); // Include dependencies if needed

  return <TodoItem onClick={handleClick} />;
}
````

### V - Don't Use Array Indexes as Keys
Always use stable, unique identifiers as keys!

### VI - Stop Passing Down Unused Props
Passing unnecessary props can bloat your components and trigger avoidable re-renders.

### VII - Never Mutate Props Directly
Directly changing props goes against React's immutability principle, often leading to unexpected bugs and missed updates.
So better updating through props (Create a update function and passed it in props to child component)

