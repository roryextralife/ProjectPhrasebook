# ChangeNotes

## ChangeNote #3 (03-MAY-19 @ 1:45PM BST)

* Updated Form1.cs
  * Basic Spoiler Log implemented.


## ChangeNote #2 (02-MAY-19 @ 1:45AM BST)

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

## ChangeNote #1 (01-MAY-19 @ 5:00PM BST)

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