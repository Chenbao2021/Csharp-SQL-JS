Source: https://mui.com/material-ui/customization/default-theme/

# Default theme viewer
This tree view allows you to explore how the theme object looks like with the default values.
Some importants key words: 
- palette
- components
- shape
- typography
- zIndex
- spacing
- etc.

# Theming
Customize Material UI with your theme. You can change the colors, the typography and much more.
Theme allows you to customize all design aspects of your project in order to meet the specific needs of your business or brand.

### - Theme provider
__ThemeProvider__ relies on the context feature of React to pass the theme down to the components, so you need to make sure that __ThemeProvider__ is a parent of the components you are trying to customize.

#### - Custom variables
It can be convenient to add additional variables to the theme so you can use them everywhere.
````
const theme = createTheme({
  status: {
    danger: orange[500],
  },
});
````
You have to use module augmentation to add new variables to the __Theme__ and __ThemeOptions__.

We can nest multiple theme providers.

#### - Creating themed components 
Look Documentation : https://mui.com/material-ui/customization/theme-components/

#### - Themed Components
You can customize a component's styles, default props, and more by using its component key inside the theme.
The components key in the theme helps to achieve styling consistency across your application.

Theme default props :
- Every Material UI component has default values for each of its props. To change these default values, use the defaultProps key exposed in the theme's components key:
    ````
    const theme = createTheme({
      components: {
        // Name of the component
        MuiButtonBase: {
          defaultProps: {
            // The props to change the default for.
            disableRipple: true, // No more ripple, on the whole application 💣!
          },
        },
      },
    });
    ````
