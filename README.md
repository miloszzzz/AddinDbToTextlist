# AddinDbToTextlist

#### TIA Portal addin for copying DB data or screen names to HMI textlists.

## TIA Portal version

Addin was created for TIA Portal V16. Only screen names copying can work in V17 and V18 at the current state (differences in exported XML files are the problem).

## Installation

1. Copy AddinDbToTextlist.addin file into TIA Portal addin folder. Default path:
```
C:\Program Files\Siemens\Automation\Portal V16\AddIns
```
2. Enable new addin in TIA Portal. Click Add-Ins tab on the right pane, right click on the addin and select 'Activate'
![image](https://github.com/miloszzzz/AddinDbToTextlist/assets/79056094/dec180fb-d8c2-46c8-9ee1-82533f7798f4)

## Using

1. Right click on data block (DB) or HMI screen, start addin from 'Textlists' dropdown.
![image](https://github.com/miloszzzz/AddinDbToTextlist/assets/79056094/20573e93-f49f-49ac-ab34-83be98768cce)

2. Write text list name, optionally turn off 'Rewrite only integers' option (without this option addin will rewrite any variable and assign value 0)
![image](https://github.com/miloszzzz/AddinDbToTextlist/assets/79056094/2d880cda-0a07-4dea-8365-6a40adf23104)

3. Click OK and wait for addin to finish work.                                    
![image](https://github.com/miloszzzz/AddinDbToTextlist/assets/79056094/7923e5cd-8ef7-4482-9292-9f2de590010d)

## Variable name format

* Camel case is separated
* Underline is replaced with space
* Numbers are separated from words (except numbers on the begining of the variable name)
* Name of structures and arrays are used as prefix for variables inside them (example below)

  ![image](https://github.com/miloszzzz/AddinDbToTextlist/assets/79056094/6a91c61b-6cb3-4373-a2e5-7202d58261ae)

