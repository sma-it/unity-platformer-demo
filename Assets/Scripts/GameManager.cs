using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton part
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // limit framerate
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 30;
    }

    // gameManager part
    private Player player;
    private GameObject spawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private bool gameActive = false;

    private void InstantiatePlayer()
    {
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.transform.position = spawnPoint.transform.position;
        FindObjectOfType<CameraFollower>().FocusOn(player);
    }

    public void StartLevel(GameObject spawnPoint)
    {
        this.spawnPoint = spawnPoint;
        InstantiatePlayer();

        if (!gameActive)
        {
            ScoreManager.instance.StartNewGame();
            gameActive = true;
        }  
    }

    public void RespawnIfPossible()
    {
        if (ScoreManager.instance.Lives > 0)
        {
            InstantiatePlayer();
        } else
        {
            // game over
            gameActive = false;
            ToScoreScene();
        }
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ToScoreScene()
    {
        SceneManager.LoadScene("ScoreScene");
    }
}
