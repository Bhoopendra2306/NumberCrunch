using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AppAdvisory.MathGame
{
	public class SliderFillArea : MonoBehaviour
	{

		public RectTransform rect;

		void OnEnable(){
			rect.anchoredPosition = Vector2.zero;

		}
	}
}