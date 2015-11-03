# Swipe10

**What is Swipe 10?**

Swipe10 is a windows applicaiton that acts as a driver overlay for a trackpad. Swipe10 processess user input and detects gestures. Currently 3 finger horizontal and vertical swipes and edge swipes are detected. These have been mapped to features like opening taskview and switching desktops to emulate windows 10's natural behaviour.

**Why is this needed?**

Synaptics, one of the worlds largest trackpad manufacturers has still not released drivers fully compatibale with windows 10 gestures. These laptops range from old old to the newest generation. Swipe10 steps in over compatibale hardware and handles gestures the right way.

**How does it work?**

Swipe 10 interfaces with the existing trackpad driver to get the number of fingers on the trackpad and the absolute positon of the fingers. Using this, the implemented algorithm detects if the a gesture is made and executes the task related to the gesture.

#Currently Supported Gestures#

**Open and Close Task View**

3 finger vertical swipes are used to open the task view and to minimise all windows and show the desktop.
![Sample](https://github.com/Abhiseshan/swipe10/blob/gh-pages/assets/img/updown.gif)

**Switch between Desktops**

3 finger horizonal swipes are used to switch between different desktops provided they exist.
![Sample](https://github.com/Abhiseshan/swipe10/blob/gh-pages/assets/img/leftright.gif)

**Open the Action Center**

Swiping from the (right-center) edge of the trackpad brings up the action center.
![Sample](https://github.com/Abhiseshan/swipe10/blob/gh-pages/assets/img/action.gif)
