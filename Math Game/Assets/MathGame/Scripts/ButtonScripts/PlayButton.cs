using UnityEngine;
using System.Collections;

namespace AppAdvisory.MathGame
{
	public class PlayButton : ButtonHelper 
	{
		override public void OnClicked()
		{
			print ("OnClicked : " + gameObject.name);
			menuManager.GoToGame();
			RemoveListener();
		}
	}
}