# LavaManager

A work-in-progress song manager for Rock Band 3 Wii (requires [RB3Enhanced](https://github.com/RBEnhanced/RB3Enhanced))

## TODO

This project is not complete. Do not expect everything (or anything) to work.

* Music re-encoding into BIK for large songs
* Properly convert RBN2 MIDI venues into the song's MILO
* Convert album art from Xbox 360 to Wii
* Import existing Wii RB DLC WAD/APP/BIN files
* (far future) Cross platform macOS and Linux builds

## Usage

1. Open the application and select your SD card (or select a folder that is the root of your SD card)
2. Import your song(s) via the Manage toolbar menu
3. To finish, select "Finalize" to save the new song database to your SD card
4. Launch Rock Band 3 with RB3Enhanced loaded

## Building

Building requires an up to date Visual Studio version. 2022 is recommended, but 2017 and 2019 should be fine.

1. Recursively clone the Git repo: `git clone https://github.com/InvoxiPlayGames/LavaManager.git --recursive`
   (Downloading the zip itself may not work due to submodules)

2. Open LavaManager.sln and build.

## Credits

* maxton, for creating the GameArchives and DtxCS libraries.
* TrojanNemo, for the RBN2 to RBN1 code in C3 CON Tools.
* The NAudio project, for their MIDI parser