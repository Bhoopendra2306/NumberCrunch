using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void RestartWithDelay()
    {
        Invoke("Restart", 1f);
    }

    private void Restart()
    {
        SceneManager.LoadScene("MathGame");
    }
}
