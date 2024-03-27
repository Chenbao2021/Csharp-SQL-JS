Source: https://mui.com/material-ui/react-divider/
#### Introduction
The Material UI Divider component renders as a dark gray <hr> by default, and features several useful props for quick style adjustments.

Three variants, by "variant" props:
- "fullWidth" (default)
- "inset"
- "middle"

Orientation, by "orientation" props:
- "horizontal" (default)
- "vertical"

Using the "flexItem" props to display the Divider when it's being used in a flex container.

Use the __textAlign__ prop to align elements that are wrapped by the Divider:
- Empty (default, middle)
- "left"
- "right"
````
<Divider textAlign="left">
    <Typography> Bonjour </Typography>
</Divider>
````


