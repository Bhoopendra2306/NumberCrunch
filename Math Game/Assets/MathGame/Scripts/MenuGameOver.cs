using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AppAdvisory.MathGame
{
	public class MenuGameOver : MonoBehaviour 
	{
		public Text level;
		public Text score;
		public Text bestScore;

		public GameObject newBestScoreLabel;


		void OnEnable(){
			level.text = ScoreManager.GetLastLevel ().ToString ();
			score.text = ScoreManager.GetLastScore ().ToString ();
			bestScore.text = ScoreManager.GetBestScore ().ToString ();

			bool isNewBest = ScoreManager.GetLastScoreIsBest ();

			if (isNewBest) {
				newBestScoreLabel.SetActive (true);
			} else {
				newBestScoreLabel.SetActive (false);
			}
		}
	}
}