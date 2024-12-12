# GalacticTitansTARpe23
 The most epic lootbox free browser gaem evar! :3 
-----------------------------------------------------
Branchlist:

Purposeful branches:
- Main: mainbranch which is now the main releasebranch (implements first object, titans)
- Unstable: experimental branch. Mostly for frontend development (examples/templates and such).
- 2-Users: Branch for implementation of user system with login and registration
- 3-Planets: Branch for implementing second object to the game necessary for gameplay
- 3-1-SolarSystems: Branch for implementing helper object to second object, necessary for gameplay
- 3-2-Galaxy: Branch for implementing second helper object to helper object, necessary for gameplay, completing object 3.
- 4-Email: Implementation of email sending functionality aswell as sending emails whenever someone registers
- 5-Player: Branch for the start of implementation for Users to have a PlayerProfile, and Titans additional helper object - TitanOwnership. This branch needed work to be completed in 3-2 first, and thus, is succeeded by 5-2
- 5-2-PlayerContinue: Branch for the continued implementation for users, playerprofile and titanownership.

Merge branches (the purpose of these is to merge other branches and to base off new objects):
- Merge(main-x-unstable) <- this branch implemented style into the main
- Merge(unstable-x-2-Users) <- merged users into unstable branch for frontend development
- Merge-3-(main-x-3-2-Galaxy) <- merged titan in main with astralbodies, solarsystems, galaxies.
- Merge-4-(Merge-3-x-5-Player) <- merged conflict solution result of merge 4 with branch 5-Player. Solved migration and dbcontext conflicts. is now the current startpoint for development at 12.12.2024

-----------------------------------------------------
How to run:
- fetch
- pull
- update local database, or make new migration and then update
- run on IIS Express 
-----------------------------------------------------
See the .docx for current software requirements
-----------------------------------------------------
