using UnityEngine;
using System.Collections;

#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif

namespace AppAdvisory.MathGame
{
	public class MoreGamesButton : ButtonHelper 
	{
		string URL = "http://app-advisory.com";

		override public void OnClicked()
		{
			print ("OnClicked : " + gameObject.name);
			#if APPADVISORY_ADS
			AdsManager.instance.ShowRewardedVideo ((bool success) => {
			print("add your own code here if you want to offer something to the player");
			});
			#endif
		}
	}
}