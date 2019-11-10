# HyperdrumDemo [VR App]

[![N|Solid](https://github.com/prasanthsasikumar/localMultiplayer/blob/master/powerdByLogo.png)](http://empathiccomputing.org/)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://github.com/prasanthsasikumar/HyperdrumDemo)

HyperDrum: Interactive Synchronous Drumming in Virtual Reality using Everyday Objects

HyperDrum, which is about leveraging this cognitive synchronization to create a collaborative music production experience with immersive visualization in virtual reality. Participants will wear an electroencephalography (EEG) head-mounted display to create music and VR space together using a physical drum.


This section explains the implementation of VR set up used. We used two VR headsets( HTC vive) for the experiment. Both the devices were synchronized over a network connection.


# REQUIREMENTS
- Two VR headsets
- Two Intel Realsense 400 series cameras mounted infront of the VR headsets(we used D415 cameras)
- For real-time syncing between computers, we used OSC triggers triggered from another computer
- We have used Unity 2019.1.1f1, but should be backwards compatible. 

# CREDITS
OSCCORE https://github.com/stella3d/OscCore
SMRVFX https://github.com/keijiro/Smrvfx


# TECHNICAL SPECIFICATIONS


### Structure
- Main scene name - Combined

###### Explanation of Components: 
- Everything listed below needs to change!
- Spawn Points(1,2,3) - 2 player spawn points and 1 third person viewer spawn point. 
- NetworkManagerPhoton - The script manages the networking side of things. You can pass on spawn points and player prefab names. The player prefabs have to be kept in the Asset/Resources folder. (Requirement from Photon).
- NoloManager - Takes care of the controllers. There is a development app key that is already keyed in. In case you want to make your own project, it is - 4e4f4c4f484f4d457eff82725bc694a5(Otherwise it won't work). A camera needs to be assigned to the manager script which in our case is done by a script in the player prefab. 
- Room - The environment. (Horrible rendition of Marks room at ECL, ABI)

### Interaction
- Attach controllers at the end of the drumsticks for tracking
- Produces circles to hit based on music
- changes visualizations based on PLV(brain Synchronization).


### Downloads(Source code)
- Please find the source code here - https://github.com/prasanthsasikumar/HyperdrumDemo
- Issues can be reported here - https://github.com/prasanthsasikumar/HyperdrumDemo/issues/new



### Todos

 - Add the trigger application(MAX)
 - A lot more work
 
 ### Videos
 - 

License
----

MIT


**Free Software, Hell Yeah!**

