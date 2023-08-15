using UnityEngine;
using System.Collections;

namespace AppAdvisory.MathGame
{
	public class RateUsButton : ButtonHelper 
	{
		override public void OnClicked()
		{
			print ("OnClicked : " + gameObject.name);

		}
	}
}