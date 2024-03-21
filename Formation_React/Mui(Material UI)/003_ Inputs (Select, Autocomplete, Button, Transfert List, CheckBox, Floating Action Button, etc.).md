# Select : https://mui.com/material-ui/react-select/
- Multiple select : Can handle multiple choices, it is enabled with __multiple__ prop.

# Autocomplete
The autocomplete is a normal text input enhanced by a panel of suggested options.
- Combo box : The value for the textbox must be chosen from a predefined set of allowed values.
- Free solo : The textbox may contain any arbitrary value, but it is advantageous to suggest possible values to the user.

For free solo, we add the property "freeSolo".

# Button
Buttons allow users to take actions, and make choices, with a single tap.

- #### Contained button
    Contained buttons are high-emphasis, distinguished by their use of elevation and fill. They contain actions that are primary to your app.

- #### Outlined button
    Outlined buttons are medium-emphasis buttons. They contain actions that are important but aren't the primary action in an app.

- #### Text button
    Text buttons are typically used for less-pronounced actions, including those located: in dialogs, in cards. In cards, text buttons help maintain an emphasis on card content.

- #### Buttons with icons and label
    - startIcon : <Button variant="outlined" startIcon={<DeleteIcon />}>
    - endIcon : <Button variant="contained" endIcon={<SendIcon />}>

- #### Icon button 
    ````
    <IconButton aria-label="delete">
      <DeleteIcon />
    </IconButton>
    ````


Handling clicks : __onClick__
Color : __color__ , "scondary", "success", "error", etc.
Sizes : __size__, "small", "medium", "large"

# Toggle Button : https://mui.com/material-ui/react-toggle-button/
A Toggle Button can be used to group related options.
Add "exclusive" when we can select max one.
- #### Exclusive selection
    With exclusive selection, selecting one option deselects any other.
    code here : https://mui.com/material-ui/react-toggle-button/#exclusive-selection

- #### Multiple selection
    Multiple selection allows for logically-grouped options, like bold, italic, and underline, to have multiple options selected.
    code here: https://mui.com/material-ui/react-toggle-button/#multiple-selection

Size: __size__ , "small", "medium", "large"
Color: __color__, 
Vertical buttons: __vertical__

# Radio Group : https://mui.com/material-ui/react-radio-button/
The Radio Group allows the user to select one option from a set.
Direction : __row__ prop
Controlled : __value__ and __onChange__ props.

# Floating Action Button: https://mui.com/material-ui/react-floating-action-button/
A floating action button appears in front of all screen content, typically as a circular shape with an icon in its center. FABs come in two types: regular, and extended.

Only use a FAB if it is the most suitable way to present a screen's primary action. Only one component is recommended per screen to represent the most common action.

# Rating : https://mui.com/material-ui/react-rating/
Ratings provide insight regarding others' opinions and experiences, and can allow the user to submit a rating of their own.

# Slider : https://mui.com/material-ui/react-slider/
Sliders reflect a range of values along a bar, from which users may select a single value. They are ideal for adjusting settings such as volume, brightness, or applying image filters.
- Continuous sliders
- Discrete sliders
- Range slider
- Slider with input field

# Switch : https://mui.com/material-ui/react-switch/
Switches toggle the state of a single setting on or off.

# TextField : https://mui.com/material-ui/react-text-field/

# Transfert List : https://mui.com/material-ui/react-transfer-list/
- Basic transfer list
- Enhanced transfer list

# Button Group : https://mui.com/material-ui/react-button-group/



