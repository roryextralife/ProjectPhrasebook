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

### ChangeNote #1 (01-MAY-19 @ 5:00PM BST)

* Added ChangeNotes to README.md
* Updated RandomizerData.cs
  * All shop locations are now stored in arrays based on contents
  * All shop locations are now randomized, updated from a small number of them from previous version.
  * ShopItemAndPrice.dat has been updated.
    * Correct Buy/Sell values have been updated for all items that can already be bought in the base game.
    * All items that could only be Found and Sold have been given a Buy value, which in most cases is equal to 2x Sell Value
      * Some notable exceptions include items that sell for 15,000gil, as 30,000gil is not a value in the buy-price table. This will be rectified eventually.
      * 1 Duplicate and 4 VERY High Value items have been removed from the Shop Pool. These 4 items will likely be redistributed as Key Items in future updates.
* Updated Form1.cs
  * Original ROM is now copied and all changes are made to the copy of the ROM now, rather than the original ROM.
    * New file includes a seed number in filename. Seed Number will be able to be manually input in a future release.
  * Toggle added for Early Airship access
    * This includes a very primitive hot-fix to stop the player from paying Cid's Assistant for travel, as this breaks the Early Airship setup.
    * What happens after finishing Dreadnaught NEEDS to be tested as a priority as this has not been taken into account.