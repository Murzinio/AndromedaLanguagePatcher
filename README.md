# AndromedaLanguagePatcher
Simple program for changing Mass Effect Andromeda language.  
Currently supported:  
* PL
* RU
* EN_US
* EN_UK

Usage:  
1. Build the program from Visual Studio or download release.  
2. Download localization files for current game version (1.04 as for 25/03/2017):    https://mega.nz/#!yMwHwTzY!9nj7wUj7WiOJRaTA36cwulzzB8ZKhAbmJzABIBa04lc  
Mirror:  
https://www.dropbox.com/s/6nzeygjmgcz4ujm/localisation_104.zip?dl=0

3. Unpack the archive (if you'll have problems with unpacking, use 7zip), and paste "Data" and "Patch" into game directory.  
4. Run the program, select your game language in the combobox, run patch and wait for confirmation message. If it won't work, verify game files in Origin and do it again.  
  
Program creates backup of exe so you can restore it any time.  

Known issues:

Some people seem to have a problem with missing english audio when changing the language from English to Polish. Leaving only en.sb and en.toc files, and deleting the others in Data/Win32/Loc and Patch/Win32/Loc seems to fix that. 
