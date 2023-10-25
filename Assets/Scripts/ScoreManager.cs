using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton part
    public static ScoreManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // ScoreManager part
    public int Lives = 0;

    public void StartNewGame()
    {
        Lives = 3;
    }

    public void RemoveLife()
    {
        Lives--;
    }


}
