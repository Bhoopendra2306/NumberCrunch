using UnityEngine;
using System.Collections;

namespace AppAdvisory.MathGame
{
	public class ScoreManager 
	{
		public static void SaveScore(int lastScore, int level)
		{
			PlayerPrefs.SetInt ("LAST_SCORE", lastScore);
			PlayerPrefs.SetInt ("LAST_LEVEL", level);

			int best = GetBestScore ();

			if(lastScore > best)
				PlayerPrefs.SetInt ("LAST_SCORE_IS_NEW_BEST", 1);
			else
				PlayerPrefs.SetInt ("LAST_SCORE_IS_NEW_BEST", 0);


			if(lastScore > best)
				PlayerPrefs.SetInt ("BEST_SCORE", lastScore);


			PlayerPrefs.Save ();
		}

		public static int GetLastScore()
		{
			return PlayerPrefs.GetInt ("LAST_SCORE");
		}


		public static int GetLastLevel()
		{
			return PlayerPrefs.GetInt ("LAST_LEVEL");
		}

		public static bool GetLastScoreIsBest()
		{
			int temp = PlayerPrefs.GetInt ("LAST_SCORE_IS_NEW_BEST");
			if (temp == 1) {
				return true;
			}
			return false;
		}

		public static int GetBestScore()
		{
			return PlayerPrefs.GetInt ("BEST_SCORE");
		}
	}
}