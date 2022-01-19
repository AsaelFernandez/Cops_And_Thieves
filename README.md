# Cops_And_Thieves
Cops and thieves is a game where we have 3 types of characters with 4 movement behaviors each.
- All the decisions made, are fully AI and part of them developed with diffused intelligence model

## Citizens
- Default movements is just wander.
- If a thief gets to close, it will start a evade movement.
- If the player gets to colse, it will start a flee movement.
- When it harmed, it will begin seeking for a hospital.

## Thieves
- Default movement is a path between all banks.
- When harmed, just wander to heal it self.
- With a lot of money, seek for the bunker and save it
- If a guard is nearby, they try to eavde.

## Guards
- Default movement is path, surrounding the thieves bunker.
- Seek movement if a thief or a citizen gets nearby.
- Puirsuit if the player gets close
- If catch a thief, seek a bank to return the money

