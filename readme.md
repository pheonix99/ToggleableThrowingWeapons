﻿# Toggleable Throwing Weapons

You can now use a toggle to turn daggers and starknives into ranged weapons.

## Implementation details
I overrode the BlueprintItemWeapon getter in ItemEntity and ItemEntity<BlueprintItem>. I don't believe this should interfere with anything thanks to the sea of sanity checks but cannot confirm for certain.

##Cross-mod compatibility
Works with Hambeard's Thrown Daggers mod and automatically incorperates the daggers from that (as of release 1.0). However, the RangedDaggers and RangedStars settings must be changed from their default true to false for my mod to work! There should be no conflicts with other mods but daggers from elsewhere will not be supported automatically. If/when there is a need for it I'll rig up some way to read the guids out of a supplemental JSON.

##Known Quirks
The item names don't change by mode. I'll get to this later.


#Credits
Thanks to Truinto for the AddFactOnlyParty code.

#Changelog
1.1: Fixed dex to melee damage cutting out, fixed some graphical jank