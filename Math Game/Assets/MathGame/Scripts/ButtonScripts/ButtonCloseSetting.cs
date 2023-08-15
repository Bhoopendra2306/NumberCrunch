using UnityEngine;
using System.Collections;

namespace AppAdvisory.MathGame
{
	public class ButtonCloseSetting : ButtonHelper 
	{
		override public void OnClicked()
		{
			print ("OnClicked : " + gameObject.name);
			menuManager.CloseSettings ();
			RemoveListener();
		}
	}
}