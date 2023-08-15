using UnityEngine;
using System.Collections;
#if APPADVISORY_LEADERBOARD
using AppAdvisory.social;
#endif

namespace AppAdvisory.MathGame
{
	public class ButtonLeaderboard : ButtonHelper 
	{
		override public void OnClicked()
		{
			OnClickedOpenLeaderboard();
		}

		/// <summary>
		/// If player clics on the leaderbord button, we call this method. Works only on mobile (iOS & Android) if using Very Simple Leaderboard by App Advisory : http://u3d.as/qxf
		/// </summary>
		public void OnClickedOpenLeaderboard()
		{
			#if APPADVISORY_LEADERBOARD
			LeaderboardManager.ShowLeaderboardUI();
			#else
			Debug.LogWarning("OnClickedOpenLeaderboard : works only on mobile (iOS & Android), with Very Simple Leaderboard : http://u3d.as/qxf");
			#endif
		}

	}
}