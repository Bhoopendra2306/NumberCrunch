

FIRST OF ALL: You need to import Dotween.


HOW TO IMPORT DOTWEEN? Have a look here: https://appadvisory.zendesk.com/hc/en-us/article_attachments/204212345/__HOW_TO_IMPORT_DOTWEEN.pdf





Thanks for your puchase.

video tutorial : https://youtu.be/Ba1vKG6deh4



To begin, open the scene "MathGame" in MathGame/Scenes.


Scripts descrîptions: 


- GameManager.cs:

This script is attached to the GameObject "Game" (child of the GameOject "Canvas").

This script is charge of the logic of the game. 

To change the music, just drag your own music in the field "Music Bacjground" (Same thing for the FX for the good or wrong answer).

The game start with 10 seconds. If you want to change it, change the value "TimeTotalGame".

Everytime the player find a good answer, he earns some more times. By default the Time Bonus = 5. To change it, change the value "Time Bonus".
Everytime the player make a mistake, he losses some times. By default the Time Malus = 2. To change it, change the value "Time Malus".



- MenuGameOver.cs:

This script is attached to the GameObject "MenuGameOver" (child of the GameOject "Canvas/Menu").

This script is in charge to display the Game Over menu, the last score, the best score, and eventually a label "New best" if the score is a new best score.



- MenuLogic.cs:

This script is attached to the GameObject "Menu" (child of the GameOject "Canvas").

This script is in charge to display the title "Math Game" if it's a fresh start, or the title "Game Over" if not.
This script is in charge too to open the "FirstTimeMenu" or the "GameOverMenu" (both are child of the GameOject "Canvas/Menu").




- MenuManager.cs:

This script is attached to the GameObject "Canvas".

This script contain the animations you can see between screen (Menu to Game, Game to GameOver, GameOver to Setting...).
This script is use by the different button scripts. Please see the description of this different scripts above.



- ScoreManager.cs:

A class in charge to save and get the score, the last score and to know if the last score is the best.




- RateUsManager.cs:

This script is attached to the GameObject "RateUseManager".
Change the value of the variable NumberOfLevelPlayedToShowRateUS. By default = 10. It means we will ask the player to rate the game when he loses 10 times.
Don't forget to change the URLs.




- Button Scripts: 

All the button scripts are in the folder MathGame/Scripts/ButtonScripts.
All the button scripts derives from ButtonHelper.

* ButtonCloseSetting.cs : to close the setting view.
* ButtonFacebook.cs : to open a Facebook fan page. To change the URLs, open the script and change them (one for mobile, one for desktop).
* ButtonLeaderboard.cs : if you want a leaderboard in your game, please add your code inside the "OnClicked" method who is called when the button is pressed.
* ButtonSetting.cs : to open the setting view.
* ButtonShare.cs : if you want a share button in your game, please add your code inside the "OnClicked" method who is called when the button is pressed.
* MoreGamesButton.cs : if you want a link to your game catalog, change the string URL inside.
* PlayButton.cs : close the menu and launch the game.
* rateUs.cs : put your method to open the link of the rating page of your game inside the "OnClicked" method.




ADS :
Everything is done for you : « Very Simple Ad » is already implemented. 
Get it here : http://u3d.as/oWD



Thanks ! 

Our other assets : http://u3d.as/9cs
Contact : contact@app-advisory.com
