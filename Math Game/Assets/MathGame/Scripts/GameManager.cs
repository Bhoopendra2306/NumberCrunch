using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using MenuBarouch;
#if APPADVISORY_LEADERBOARD
using AppAdvisory.social;
#endif
#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif

namespace AppAdvisory.MathGame
{
	public class GameManager : MonoBehaviour 
	{
		public int numberOfPlayToShowInterstitial = 5;

		public string VerySimpleAdsURL = "http://u3d.as/oWD";

		static System.Random _random = new System.Random();

		public AudioClip musicBackground;
		public AudioClip goodAnswerSound;
		public AudioClip falsedAnswerSound;

		public int timeTotalGame;
		public int timeMalus;
		public int timeBonus;

		public Color NORMAL_COLOR;
		public Color GOOD_COLOR;
		public Color FAIL_COLOR;
		public Image BACKGROUND_BACK;

		public ParticleSystem particleSuccessPrefab;

		public Text point;

		public GameObject QUESTIONS_GO;

		public GameObject BUTTONS_GO;

		public GameObject POINTS;
		public Text pointsText;

		public Text questionNumber1;
		public Text questionOperator; 
		public Text questionNumber2;
		public Text questionResult;

		public Text[] reponses;

		public int level; 

		public int _score;

		public int GOODANSWER; 

		public Slider slider; 

		public Text levelText; 

		private Vector2 pivotPoint;

		int _result = 0;
		int _number1 = 0;
		int _number2 = 0;
		int _operateur = 0;

		public delegate void _GameOver();
		public static event _GameOver OnGameOver;


		void PlaySoundFalse()
		{
			GetComponent<AudioSource> ().PlayOneShot (falsedAnswerSound);
		}

		void PlaySoundGood()
		{
			GetComponent<AudioSource> ().PlayOneShot (goodAnswerSound);
		}

		void PlayMusic()
		{
			GetComponent<AudioSource> ().Play ();
		}

		void StopMusic()
		{
			GetComponent<AudioSource> ().Stop ();
		}

		void OnEnable()
		{
			Application.targetFrameRate = 60;
			StartGame ();
		}

		void OnDisable()
		{
			StopMusic ();
		}

		private void StartGame()
		{
			PlayMusic ();

			_score = 0;

			level = 1;

			point.text = _score.ToString ();

			levelText.text = "Level " + level.ToString ();

			ChooseOperator ();

			StartCoroutine(TimerStart ());
		}

		IEnumerator TimerStart()
		{
			slider.maxValue = timeTotalGame;
			slider.value = timeTotalGame;

			while (true) 
			{

				float timer = 0.01f + ((float)Mathf.Sqrt(level)) / 100f;

				slider.value -= timer;


				if (slider.value == 0) 
				{
					break;
				}


				yield return new WaitForSeconds(0.01f);
			}

			GameOver ();
		}


		private void GameOver()
		{
			ScoreManager.SaveScore (_score, level);

			FindObjectOfType<MenuBarouch.MenuManager> ().GoToMenu ();

			ReportScoreToLeaderboard(_score);

			ShowAds();

			if(OnGameOver != null)
				OnGameOver();
		}

		void ChooseOperator()
		{
			int operateur = 0;

			if (level == 1) {
				operateur = UnityEngine.Random.Range (0, 2);
			} else if (level <= 3) {
				operateur = UnityEngine.Random.Range (0, 3);
			} else {
				operateur = UnityEngine.Random.Range (0, 4);
			}

			CreateQuestion (operateur);
		}

		void CreateQuestion(int operateur)
		{

			int result = 0;
			int number1 = 0;
			int number2 = 0;

			int essai = 0;

			while (true) 
			{
				essai++;

				bool isOK = true;

				if (operateur == 3) 
				{
					int mult = UnityEngine.Random.Range (2 + (int)Mathf.Log (level), 2  + 2*(int)Mathf.Log (level));

					number2 = UnityEngine.Random.Range (2 + level / 2, 3 + level);

					number1 = mult * number2;
				}
				else if (operateur == 1) 
				{
					number2 = UnityEngine.Random.Range (1 + (int)Mathf.Log (level) / 2, 5 + (int)Mathf.Log (level));

					number1 = UnityEngine.Random.Range (number2 + (int)Mathf.Log (level) / 2, number2 + 5 + 2*(int)Mathf.Log (level));

				}
				else
				{
					number1 = UnityEngine.Random.Range (1 + (int)Mathf.Log (level) / 2, 5 + 2*(int)Mathf.Log (level));
					number2 = UnityEngine.Random.Range (1 + (int)Mathf.Log (level) / 2, 5 + 2*(int)Mathf.Log (level));
				}

				result = GetResult (number1, number2, operateur);

				if (operateur == 1 || operateur == 3) {

					int resultDIV = 0;

					int resultMINUS = 0;

					resultDIV = number1 / number2;

					resultMINUS = number1 - number2;

					if (resultDIV == resultMINUS) 
					{
						isOK = false;
					}

				}


				if (operateur == 0 || operateur == 2) 
				{
					int resultMULT = 0;

					int resultPLUS = 0;

					resultMULT = number1 * number2;

					resultPLUS = number1 + number2;

					if (resultMULT == resultPLUS) 
					{
						isOK = false;
					}
				}

				if (_result == result && _number1 == number1 && _number2 == number2 && _operateur == operateur) 
				{
					isOK = false;
				}

				if (result <= 0) 
				{
					isOK = false;
				}


				if (operateur == 3) 
				{
					if (number1 % number2 != 0)
					{
						isOK = false;
					}

					if (number1 / number2 == 0)
					{
						isOK = false;
					}

					if (number1 / number2 == 1)
					{
						isOK = false;
					}
				}
				else
				{
					if (operateur == 2)
					{
						if (number1 == 0 || number1 == 1 || number2 == 0 || number2 == 1 || result == 0 || result == 1) 
						{
							isOK = false;
						}
					}
				}

				if (level <= 2) 
				{
					if (result > 9)
					{
						isOK = false;
					}
					if (result <= 0 || number1 <= 0 || number2 <= 0) 
					{
						isOK = false;
					}
				}
				else if (level <= 4) 
				{
					if (result > 50) 
					{
						isOK = false;
					}
					if (result <= 0 || number1 <= 0 || number2 <= 0) 
					{
						isOK = false;
					}
				}
				else if (level <= 6) 
				{
					if (result  > 99)
					{
						isOK = false;
					}
				}


				if (result  > 99) 
				{
					isOK = false;
				}


				//CHECK!!!
				if(isOK)
				{
					if (operateur == 0) 
					{
						int resultTest = number1 + number2;
						if (resultTest != result)
						{
							isOK = false;
						}
					}
					if (operateur == 1) 
					{
						int resultTest = number1 - number2;
						if (resultTest != result)
						{
							isOK = false;
						}
					}
					if (operateur == 2) 
					{
						int resultTest = number1 * number2;
						if (resultTest != result) 
						{
							isOK = false;
						}
					}
					if (operateur == 4) 
					{
						int resultTest = number1 / number2;
						if (resultTest != result)
						{
							isOK = false;
						}
					}

				}


				if (isOK) 
				{
					_result = result;
					_number1 = number1;
					_number2 = number2;
					_operateur = operateur;

					break;
				}
			}

			SetText (number1, number2, operateur, result);

		}

		//set the question text
		private void SetText(int n1, int n2, int oper, int result)
		{
			int TYPE_QUESTION = UnityEngine.Random.Range(0,4);

			if (TYPE_QUESTION == 0)
			{
				questionNumber1.text = "?";

				questionNumber2.text = n2.ToString ();

				questionOperator.text = GetOperator (oper);

				questionResult.text = result.ToString ();

				GOODANSWER = n1;
			}

			if (TYPE_QUESTION == 1) 
			{
				questionNumber1.text = n1.ToString ();

				questionNumber2.text = n2.ToString ();

				questionOperator.text = "?";

				questionResult.text = result.ToString ();

				GOODANSWER = oper;
			}

			if (TYPE_QUESTION == 2) 
			{
				questionNumber1.text = n1.ToString ();

				questionNumber2.text = "?";

				questionOperator.text = GetOperator (oper);

				questionResult.text = result.ToString ();

				GOODANSWER = n2;
			}

			if (TYPE_QUESTION == 3) 
			{
				questionNumber1.text = n1.ToString ();

				questionNumber2.text = n2.ToString ();

				questionOperator.text = GetOperator (oper);

				questionResult.text = "?";

				GOODANSWER = result;
			}

			questionNumber1.transform.parent.Find("Selected").gameObject.SetActive(TYPE_QUESTION == 0);
			questionOperator.transform.parent.Find("Selected").gameObject.SetActive(TYPE_QUESTION == 1);
			questionNumber2.transform.parent.Find("Selected").gameObject.SetActive(TYPE_QUESTION == 2);
			questionResult.transform.parent.Find("Selected").gameObject.SetActive(TYPE_QUESTION == 3);


			if (TYPE_QUESTION != 1) 
			{
				int[] answers = new int[4];

				List<int> l = new List<int> ();

				l.Add (GOODANSWER);

				while(true)
				{
					int ans = 0;

					int addRange = 0;

					while (true) 
					{
						bool isOk = true;

						ans =  UnityEngine.Random.Range (1, GOODANSWER*2 + 3);

						if (ans <= 0)
							isOk = false;

						if (isOk) 
							break;

						addRange++;
					}

					if (!l.Contains (ans))
						l.Add (ans);

					if (l.Count == 4)
						break;
				}

				l.Sort ();

				answers = l.ToArray ();

				Array.Sort (answers);

				for (int i = 0; i < 4; i++) 
				{
					reponses [i].fontSize = 90;
					reponses [i].text = answers [i].ToString();
				}
			}
			else
			{
				reponses [0].text = "+";
				reponses [0].fontSize = 130;
				reponses [1].text = "-";
				reponses [1].fontSize = 130;
				reponses [2].text = "x";
				reponses [3].text = "÷";
			}
		}


		public void OnClicked(Text text)
		{
			int myAnswer;

			bool isMaybeOperator = text.text.Length <= 1;

			if (text.text.Contains ("+") && isMaybeOperator) 
				myAnswer = 0;
			else if (text.text.Contains ("-") && isMaybeOperator)
				myAnswer = 1;
			else if (text.text.Contains ("x") && isMaybeOperator)
				myAnswer = 2;
			else if (text.text.Contains ("÷") && isMaybeOperator)
				myAnswer = 3;
			else
				myAnswer = int.Parse (text.text);

			if (GOODANSWER == myAnswer) 
			{
				_score++;

				if (_score % 5 == 0) 
				{
					level++;
					levelText.text = "Level " + level.ToString();
				}

				pointsText.text = _score.ToString ();

				StartCoroutine (GoodAnswerAnim ());

				slider.value += timeTotalGame;

				AnimColorBACKGROUND_BACK (true);

				BUTTONS_GO.GetComponent<Animator> ().Play ("AnimButtonGame");

				PlaySoundGood ();
			}
			else 
			{
				slider.value -= timeTotalGame / 5; 

				PlaySoundFalse ();

				AnimColorBACKGROUND_BACK (false);
			}
		}


		private int GetResult(int n1, int n2, int oper)
		{
			if (oper == 0) 
				return n1 + n2;
			else if (oper == 1) 
				return n1 - n2;

			else if (oper == 2) 
				return n1 * n2;

			else if (oper == 3) 
				return n1 / n2;
			else 
				return 0;
		}

		private string GetOperator(int oper)
		{
			if (oper == 0) 
				return "+"; 
			else if (oper == 1)
				return "-";
			else if (oper == 2)
				return "x";
			else if (oper == 3) 
				return "÷";
			else 
				return "";
		}

		IEnumerator GoodAnswerAnim()
		{
			float time = 0.2f;

			QUESTIONS_GO.GetComponent<Animator> ().Play ("AnimQuestionChange");

			var goParticle = Instantiate(particleSuccessPrefab.gameObject) as GameObject;
			var tParticle = goParticle.transform;
			tParticle.position = new Vector3(point.transform.position.x,point.transform.position.y,point.transform.position.z+2);
			tParticle.rotation = Quaternion.identity;
			goParticle.SetActive(true);
			goParticle.GetComponent< ParticleSystem>().Emit(50);
			yield return new WaitForSeconds (time+0.01f);

			ChooseOperator ();
		}

		public static string[] RandomizeStrings(string[] arr)
		{
			List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
			// Add all strings from array
			// Add new random int each time
			foreach (string s in arr)
			{
				list.Add(new KeyValuePair<int, string>(_random.Next(), s));
			}
			// Sort the list by the random number
			var sorted = from item in list
				orderby item.Key
				select item;
			// Allocate new string array
			string[] result = new string[arr.Length];
			// Copy values to array
			int index = 0;
			foreach (KeyValuePair<int, string> pair in sorted)
			{
				result[index] = pair.Value;
				index++;
			}
			// Return copied array
			return result;
		}

		public void AnimColorBACKGROUND_BACK(bool isGoodAnswer)
		{
			StartCoroutine (AnimColorBACKGROUND_BACK_Corout (isGoodAnswer));
		}

		IEnumerator AnimColorBACKGROUND_BACK_Corout(bool isGoodAnswer)
		{
			Color c = FAIL_COLOR;

			var time = 0.3f;
			var originalTime = time;

			if (isGoodAnswer)
				c = GOOD_COLOR;

			while (time > 0.0f)
			{
				time -= Time.deltaTime;
				BACKGROUND_BACK.color = Color.Lerp(NORMAL_COLOR, c, time / originalTime);
				yield return 0;
			}

		}

		// <summary>
		/// If using Very Simple Leaderboard by App Advisory, report the score : http://u3d.as/qxf
		/// </summary>
		void ReportScoreToLeaderboard(int p)
		{
			#if APPADVISORY_LEADERBOARD
			LeaderboardManager.ReportScore(p);
			#else
			print("Get very simple leaderboard to use it : http://u3d.as/qxf");
			#endif
		}

		/// <summary>
		/// If using Very Simple Ads by App Advisory, show an interstitial if number of play > numberOfPlayToShowInterstitial: http://u3d.as/oWD
		/// </summary>
		public void ShowAds()
		{
			int count = PlayerPrefs.GetInt("GAMEOVER_COUNT",0);
			count++;
			PlayerPrefs.SetInt("GAMEOVER_COUNT",count);
			PlayerPrefs.Save();

			#if APPADVISORY_ADS
			if(count > numberOfPlayToShowInterstitial)
			{
			#if UNITY_EDITOR
			print("count = " + count + " > numberOfPlayToShowINterstitial = " + numberOfPlayToShowInterstitial);
			#endif
			if(AdsManager.instance.IsReadyInterstitial())
			{
			#if UNITY_EDITOR
				print("AdsManager.instance.IsReadyInterstitial() == true ----> SO ====> set count = 0 AND show interstial");
			#endif
				PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
				AdsManager.instance.ShowInterstitial();
			}
			else
			{
			#if UNITY_EDITOR
				print("AdsManager.instance.IsReadyInterstitial() == false");
			#endif
			}

		}
		else
		{
			PlayerPrefs.SetInt("GAMEOVER_COUNT", count);
		}
		PlayerPrefs.Save();
			#else
		if(count >= numberOfPlayToShowInterstitial)
		{
			Debug.LogWarning("To show ads, please have a look to Very Simple Ad on the Asset Store, or go to this link: " + VerySimpleAdsURL);
			Debug.LogWarning("Very Simple Ad is already implemented in this asset");
			Debug.LogWarning("Just import the package and you are ready to use it and monetize your game!");
			Debug.LogWarning("Very Simple Ad : " + VerySimpleAdsURL);
			PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
		}
		else
		{
			PlayerPrefs.SetInt("GAMEOVER_COUNT", count);
		}
		PlayerPrefs.Save();
			#endif
	}
}

}