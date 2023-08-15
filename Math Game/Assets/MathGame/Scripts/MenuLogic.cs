using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AppAdvisory.MathGame
{
	public class MenuLogic : MonoBehaviour
	{

		public GameObject FirstTimeMenu;

		public GameObject GameOverMenu;

		public Transform Title;


		public Text M;

		public Text A;

		public Text T;

		public Text H;

		public Text G;

		public Text A_;

		public Text M_;

		public Text E;



		bool firstTime;

		void Awake()
		{
			firstTime = true;
		}

		public void OnEnable()
		{

			foreach (Transform t in Title)
			{
				t.localScale = Vector3.one;

			}

			FirstTimeMenu.SetActive (firstTime);
			GameOverMenu.SetActive (!firstTime);

			if (!firstTime) 
			{
				M.text = "G";
				A.text = "A";
				T.text = "M";
				H.text = "E";
				G.text = "O";
				A_.text = "V";
				M_.text = "E";
				E.text = "R";
			}

			firstTime = false;
		}

		public void OnDisable()
		{
			foreach (Transform t in Title) 
			{
				t.localScale = Vector3.one;
			}
		}
	}
}