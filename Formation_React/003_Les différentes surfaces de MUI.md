# I - Introduction
Materia UI propose quatre types des surfaces :
- Accordion
    The Accordion component lets users show and hide sections of related content on a page.
- App Bar
    The App Bar displays informations and actions relating to the current screen.
- Card
    Cards contain content and actions about a single subject
- Paper
    The Paper component is a container for displaying content on an elevated surface

# II - Difference between Card and Paper
### A - Card
Cards are surfaces that display content and actions on a single topic.
Ther Material UI Card component includes several complementary utility components to handle various use cases:
- Card            : <Card>, a surface-level container for grouping related components
- Card Content      : <CardContent>, The wrapper for the Card content.
- Card Header: <CardHeader>, Optional, Wrapper for the Card header
- Card Media: <CardMedia>, Optional, Container for displaying backgound images and gradient layers behind the card content.
- Card Actions: <CardActions>, Optional, Wrappper that groups a set of buttons
- Card Action Area: optional, Wrapper that allows users to interact with the specified area of the card.

Exemple1 (with Collapse) : https://mui.com/material-ui/react-card/#complex-interaction

### B - Paper
The paper component is a container for displaying content on an __elevated__ surface, that means a real-world physical effects with the props: __elevation__ .
The props accepts values from 0 to 24.
When mode dark, the more the number is bigger, the more the color is lighter.

The paper component is ideally to replicate how light casts shadows in the physical world.
_If you just need a generic container, you may prefer to use the Box or Container components._

# Accordion
The Accordion component lets users show and hide sections of related content on a page.
MUI Components: Accordion / Accordion Summary/ Accordion Details/ Accordion Actions.

The Accordion component can be controlled or uncontrolled.
- A component is cntrolled whe  it is managed by its parent using props
- A component is uncontrolled when it's managed by its own local state.

Exemple: 




