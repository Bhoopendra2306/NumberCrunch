using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace AppAdvisory.MathGame
{
	public class AnimTitle : MonoBehaviour
	{
		void OnEnable()
		{
			for (int i = 0; i < transform.childCount; i++) 
			{
				transform.GetChild (i).localScale = Vector3.one;
			}

			StartCoroutine (Anim ());
		}


		IEnumerator Anim()
		{
			yield return new WaitForSeconds (0.3f);

			while (true) 
			{
				float time = 0.2f*5;

				int childCount = transform.childCount;

				for (int i = 0; i < childCount; i++)
				{
					float delay = i * 0.05f*5;
					transform.GetChild (i).DOScale (Vector3.one*0.7f,time).SetDelay(delay).SetEase(Ease.InBack);
					transform.GetChild (i).DOScale (Vector3.one*1f,time).SetDelay(/*2**/time + delay).SetEase(Ease.OutBack);

				}

				yield return new WaitForSeconds (1*(time + childCount * 0.05f*4) + 2f);
			}
		}

		void OnDisable()
		{
			StopAllCoroutines ();

			for (int i = 0; i < transform.childCount; i++) 
			{
				DOTween.Kill(transform.GetChild (i));
			}
		}
	}
}