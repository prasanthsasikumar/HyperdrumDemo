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
- Game Design Logic - Drum game stuff including all the spawn points, the rings that fall down etc are managed by this.
- NetworkManager - Manages all the application parameters remotely. Application Manager script has the toggles for all the parameters
- InteractionModule - Takes care of the controllers. Map the controllers to the sticks
- Camera Module - Functions relating to camera(intel D415), and the graphics processing of the image input is done here
- Camera Rig - Under camera, the camera module attaches itself on runtime to get the particle effect.

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
