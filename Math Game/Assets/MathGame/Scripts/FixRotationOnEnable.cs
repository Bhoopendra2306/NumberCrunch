using UnityEngine;
using System.Collections;

namespace AppAdvisory.MathGame
{
	public class FixRotationOnEnable : MonoBehaviour
	{
		void OnEnable()
		{
			transform.rotation = Quaternion.identity;
		}

		void OnDisable()
		{
			transform.rotation = Quaternion.identity;
		}
	}
}