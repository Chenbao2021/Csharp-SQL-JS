Source: https://mui.com/system/getting-started/the-sx-prop/

#The sx prop
The sx prop is a shortcut for defining custom styles that has access to the theme.

# Exemple of theme: 
````
const theme = createTheme({
  palette: {
    background: {
      paper: '#fff',
    },
    text: {
      primary: '#173A5E',
      secondary: '#46505A',
    },
    action: {
      active: '#001E3C',
    },
    success: {
      dark: '#009688',
    },
  },
});
````

# Theme-aware properties
Borders:
- border: This property can only receive a number as a value. It creates a solid black border using the number to define the width in pixels:
    ````
    <Box sx={{ border: 1 }} />
    // equivalent to border: '1px solid black'
    ````
- borderColor: Property can receive a string, which represents the path in __theme.palette__:
    ````
    <Box sx={{ borderColor: 'primary.main' }} />
    // equivalent to borderColor: theme => theme.palette.primary.main
    ````
- borderRadius: Property multiplies the value it received by the __theme.shape.borderRadius__ value (default : 4px):
    ````
    <Box sx={{ borderRadius: 2 }} />
    // equivalent to borderRadius: theme => 2 * theme.shape.borderRadius
    ````

Palette: 
- color : Can receive a string, which represents the path in the __theme.palette__
- backgrundColor:  Same

Position
- zIndex : maps its values to the __theme.zIndex__ value:
    ... < appBar < drawer < modal < snackbar < tooltip

Sizing(width, height, minHeight, maxHeight, minWidth, maxWidth)
- If the value is between (0, 1], it's converted to a percentage. Otherwise, it is directly set on the CSS property)
    ````
    <Box sx={{ width: 1/2 }} /> // equivalent to width: '50%'
    <Box sx={{ width: 20 }} /> // equivalent to width: '20px'
    ````

Spacing(margin, padding):
- The corresponding longhand properties multiply the values they receive by the __theme.spacing__ value(the default for the value is 8px).
- List des Props : m(margin),mt,mr,mb,ml, mx(horizontal), my(vertical), p,pt,pr,pb,pl,px,pt=y.

Typography:
- The fontFamily, fontSize, fontStyle, fontWeight properties map their value to the __theme.typography__ value:
    `````
    <Box sx={{ fontWeight: 'fontWeightLight' }} />
    // equivalent to fontWeight: theme.typography.fontWeightLight
    ````



