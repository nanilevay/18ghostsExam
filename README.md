# LP2 Exam project: 18 ghosts for console and unity

## Author:

**Diana Levay** - a21801515 [nanilevay](https://github.com/nanilevay)

### Task list

### Project's Git Repository

<https://github.com/nanilevay/18ghosts_lp2_exam>

### Describing Solution approach

The first solution was going to be implemented on the console and then adapted to unity, however after studying the PATTERN and seeing tutorials, it was changed so that the unity version was the "base" to create the model for both. Thus, the project is organised in the following way:

Each component in the map consists of a Tile, that implements the IMapElement interface in order to get a position, colour and way to be represented in the game, each tile can be empty or occupied, and they are aware of the kind of piece inside them by comparing their own colour to the piece the player has placed.

There's three regular tiles, blue yellow and red, and these restrict whether the player can place a ghost inside them if they're inside the Dungeon or at the beginning of the game.

There's 4 mirror at specific positions, and these check the piece's "OnMirror" bool in order to restrict the movemet.

There's also 3 portals, one of each colour, that will receive a "GhostDied" bool and enter the Rotate() method in order to check which ghost died and rotate accordingly. These portals also have an "AdjacentTileCheck" where they check the tiles adjacent to themselves and let a ghost out if they're facing a certain tile with a ghost matching its colour;

Each ghost piece implements the INTERFACE in order to know what colour it is and how to represent it, as well as if the piece is currently in the dungeon or on a mirror, in order to act accordingly to those rules.

The players implement the IPlayer interface and during the loop there's a rotation between player A and player B done by having a third instance of a player, the "CurrentPlayer" that stores the current play being made by each and then is equated to the other in each play.

### Flow Chart
DESC
![name](img)

### UML Diagram

This diagram shows the main class structure found in the programs as well as the relations between classes and interfaces used.
![name](img)

## References

Referenced the first lp2 project for the document structures, found at: <https://github.com/nanilevay/lp2_project1>, following the same report logic done by the student and others in the past, as well as how to do the doxygen by student **ana dos santos - a21801515 <https://github.com/AnSantos99>**
