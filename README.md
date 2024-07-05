# AddinDbToTextlist

#### TIA Portal addin for copying DB data or screen names to HMI textlists.

## TIA Portal version

Addin was created for V16 version. Only screen names copying can work in V17 and V18 at the current state (differences in exported XML files are the problem).

## Installation

1. Copy AddinDbToTextlist.addin file into TIA Portal addin folder. Default path:
```
C:\Program Files\Siemens\Automation\Portal V16\AddIns
```
2. Enable new addin in TIA Portal. Click Add-Ins tab on the right pane, right click on the addin and select 'Activate'
![image](https://github.com/miloszzzz/AddinDbToTextlist/assets/79056094/dec180fb-d8c2-46c8-9ee1-82533f7798f4)

## Using

1. Right click on data block (DB) or HMI screen, start addin from 'Textlists' dropdown.
2. Write text list name, optionally turn off 'Rewrite only integers' option (without this option addin will rewrite any variable and assign value 0)
3. Click OK and wait for addin to finish work.
