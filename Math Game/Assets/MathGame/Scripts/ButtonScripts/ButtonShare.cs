using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MenuBarouch;

namespace AppAdvisory.MathGame
{
	public class ButtonShare : ButtonHelper 
	{
		#if !VS_SHARE
		public RectTransform buttonVerySimpleShare;
		public string VerySimpleAdsURL = "http://u3d.as/oWD";
		#endif
		override public void OnClicked()
		{
			#if !VS_SHARE
			Debug.LogWarning("To take and share screenshots, you need Very Simple Share: " + VerySimpleAdsURL);
			Debug.LogWarning("Very Simple Share: " + VerySimpleAdsURL);
			Debug.LogWarning("Very Simple Share is ready to use in the game template: " + VerySimpleAdsURL);
			//		AnimVerySimpleShare(false);
			#endif
			RemoveListener();
		}
	}
}