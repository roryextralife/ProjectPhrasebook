# ProjectPhrasebook

Final Fantasy II (FC) Randomizer Project

This is currently in development by RoryExtraLife. The README.md will be updated constantly throughout the development period.

## Setup

Find a ROM of Final Fantasy II for the NES (Famicom). Download the Current Release .zip file, and extract all of it's contents to a new folder. Run ProjectPhrasebook.exe, click button1 to select the FF2 ROM, pick your flags you want enabled, and click button2. The new ROM will be generated in the same folder as the source ROM.

## Implemented Features

* Randomization of Shop Items
  * Including option to keep items in type of store, or completely randomized

## Features in development

* Randomization of Chest Items and Key Items
* Early Ship and/or Airship access (similar to FF4:FE and FF5:CD)
  * Early Airship Access is currently implemented for testing, however implications of it are yet to be explored.
    * When using this flag, you will lose the airship if you ask Cid to take you somewhere. He'll probably just be removed entirely for whenever this flag is enabled, however I need to figure that one out.
* Seed-Based Generation
  * Allows for multiple people running the same version of the Randomizer program to generate the same game, for Races etc.

## Planned Features

* Keyword Randomization
* Boss randomization (including adjusting stats based on where and when they are encountered)
* Random Encounters to be adjusted
  * Static Step Counter Table to be randomized each seed possibly?
* Spoiler Log Generaton
  * Mainly for Testing and Debugging, but also to help new players find their feet.
* Various bug fixes
  * Weapon/Magic Skill Point glitch to be patched (standalone patch for this already exists)
  * Using Tomes as weapons to be patched (patch for this does not exist, hopefully this will be doable without much hassle though)

## ChangeNotes

### ChangeNote #2 (02-MAY-19 @ 1:45AM BST)

* Updated Form1.cs
  * Included Test Mode flag, giving Firion max stats and great weapons to help plough through seeds for test purposes
    * This will be restricted to developer releases eventually in future releases.
  * Implemented Custom Seed Input
  * Added Confirmation MessageBox for successful runs of the program.
  * Added Basic Logo, this is going to be updated before public release.
  * Some basic commenting has been done, will be fully commented eventually.
  * A number of small form design changes not worth individually mentioning.
* New Issues Found
  * Early Airship Flag is cleared upon leaving Kashuan Keep once Airship cutscene plays out. This will be looked into and hopefully rectified in next release.