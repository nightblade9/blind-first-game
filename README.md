# Blind First Game

An experimental prototype for a blind-first game; while the game has a UI and can handle clicks, etc., it can also be completely driven by the console. Commands entered into the console control the game.

The intention was to work out how to make a game with a standard console (for screen readers) that can be both satisfying to sighted players and blind players.

The core change necessary is to create a second thread that reads/writes from the console, and modifies the game state accordingly based on those commands.
