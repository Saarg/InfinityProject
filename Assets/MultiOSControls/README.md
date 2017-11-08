# MultiOSControls
MultiOSControls is a script made to be used instead of the default input manager.
It let you setup different controls under a name and asign controler axis,
controller buttons and keyboard inputs without thinking about the OS.
for now it only uses one controller but will do more than one in the futur.
## Setup
To setup MultiOSControls just add the script to an empty GameOject which we will
call Scripts.

Go to Edit->Project Settings->Input  and add inputs for the joystick named
`joystick1 axis x`, `joystick1 axis y`, `joystick1 axis 3`, `joystick1 axis ...`,
`joystick1 axis 15` to the coresponding axis. this is necessary since you can't
bypass the input manager for axis.

Now you can add inputs in the editor, every inputs can have many positive keys,
negative keys, buttons, and controller axis. You can also set a deadzone for the
axis to ensure return position is 0.
## Use
To use the inputs in a script just define:
```
private MultiOSControls _controls;
...
_controls = GameObject.Find ("Scripts").GetComponent<MultiOSControls> ();
```

Then just use `_controls.getValue ("youControl")` wich will return between -1 to
1 with a default value of 0.

# Issues
There seems to be an issue when more than one controllers are connected.
This was seen on windows with 2 xbox controllers connected, a few on the inputs
on the second controller were registered on the second one and not the first one
Will do more testing when possible (only have one controller with me)
