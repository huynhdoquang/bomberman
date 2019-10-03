# bomberman

This is bomberman project.

# Mode:
- player vs player : DONE
- player vs CPU : TODO

# Create Map:
- using mouse to select tile on top of view. 
- left mouse click to add tile
- right mouse click to delete

# Features
- Make the characters distinguishable somehow (color, nameplate, ...) : TODO
- Longer bomb blasts : DONE
- More bombs : DONE
- Faster run speed : DONE
- Remote-controlled bombs (timed power-up, around 10 seconds) : DONE

 Bomb placing by the player
- Player is starting with only one bomb that can be active at a time: DONE
- Placing a bomb subtracts one from the count, when the bomb explodes the count
goes up again : DONE
- Amount is upgradable with pickups : DONE

- Once the remote detonator has been picked up only one bomb can be active until
the power-up runs out : DONE

 Player death when standing in bomb blast : DONE

Bomb blasts
- Should not be spherical but linear in the four main directions: DONE
- Can penetrate players/pickups when going off (killing/destroying them) : missing destroy item
- Are stopped by walls : DONE
- Trigger other bombs : DONE

 Differentiation between destructible and indestructible walls, destructible walls can spawn
random pickups (~30% chance to spawn something) upon destruction : DONE
 Win conditions:
- Show win screen when a player is killed : DONE
- Show a map timer, that counts down and ends the round if both players are still alive
when at 0 : DONE
- Show draw when both players die in same bomb blast (or chained bombs) or both
players are alive when the timer runs out : DONE
- After round end, freeze game in its current state: round end and back to tittle
 Reset option on end screen 
- Starts another round : DONE
 AI enemies that behave like a player : TODO


