using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

namespace AppAdvisory.MathGame
{
	public class RateUsManager : MonoBehaviour 
	{
		public int NumberOfLevelPlayedToShowRateUs = 30;
		public string iOSURL = "itms://itunes.apple.com/us/app/apple-store/id963781532?mt=8";
		public string ANDROIDURL = "http://app-advisory.com";

		public Button btnYes;
		public Button btnLater;
		public Button btnNever;

		public CanvasGroup popupCanvasGroup;

		void Awake()
		{
			popupCanvasGroup.alpha = 0;
			popupCanvasGroup.gameObject.SetActive(false);
		}

		void OnEnable()
		{
			GameManager.OnGameOver += CheckIfPromptRateDialogue;
		}

		void OnDisable()
		{
			GameManager.OnGameOver -= CheckIfPromptRateDialogue;
		}

		void AddButtonListeners()
		{
			btnYes.onClick.AddListener(OnClickedYes);
			btnLater.onClick.AddListener(OnClickedLater);
			btnNever.onClick.AddListener(OnClickedNever);
		}

		void RemoveButtonListener()
		{
			btnYes.onClick.RemoveListener(OnClickedYes);
			btnLater.onClick.RemoveListener(OnClickedLater);
			btnNever.onClick.RemoveListener(OnClickedNever);
		}

		void OnClickedYes()
		{
			#if UNITY_IPHONE
			Application.OpenURL(iOSURL);
			#endif

			#if UNITY_ANDROID
			Application.OpenURL(ANDROIDURL);
			#endif

			PlayerPrefs.SetInt("NUMOFLEVELPLAYED",-1);
			PlayerPrefs.Save();
			HidePopup();
		}

		void OnClickedLater()
		{
			PlayerPrefs.SetInt("NUMOFLEVELPLAYED",0);
			PlayerPrefs.Save();
			HidePopup();
		}

		void OnClickedNever()
		{
			PlayerPrefs.SetInt("NUMOFLEVELPLAYED",-1);
			PlayerPrefs.Save();
			HidePopup();
		}

		void CheckIfPromptRateDialogue()
		{
			int count = PlayerPrefs.GetInt("NUMOFLEVELPLAYED",0);

			if(count == -1)
				return;

			count ++;

			if(count > NumberOfLevelPlayedToShowRateUs)
			{
				PromptPopup();
			}
			else
			{
				PlayerPrefs.SetInt("NUMOFLEVELPLAYED",count);
			}

			PlayerPrefs.Save();
		}

		public void PromptPopup()
		{

			popupCanvasGroup.alpha = 0;
			popupCanvasGroup.gameObject.SetActive(true);

			StartCoroutine(DoLerpAlpha(popupCanvasGroup, 0, 1, 1, () => {
				AddButtonListeners();
			}));
		}

		void HidePopup()
		{
			StartCoroutine(DoLerpAlpha(popupCanvasGroup, 1, 0, 1, () => {
				popupCanvasGroup.gameObject.SetActive(false);
				RemoveButtonListener();
			}));
		}

		public IEnumerator DoLerpAlpha(CanvasGroup c, float from, float to, float time, Action callback)
		{
			float timer = 0;

			c.alpha = from;

			while (timer <= time)
			{
				timer += Time.deltaTime;
				c.alpha = Mathf.Lerp(from, to, timer / time);
				yield return null;
			}

			c.alpha = to;

			if (callback != null)
				callback ();
		}
	}
}