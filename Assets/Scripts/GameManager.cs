using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton part
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
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
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private bool gameActive = false;

    private void Update()
    {
        if (!gameActive && Input.GetKeyDown(KeyCode.Space)) {
            StartNewGame();
        }
    }

    private void InstantiatePlayer()
    {
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.transform.position = spawnPoint.transform.position;
        FindObjectOfType<CameraFollower>().FocusOn(player);
    }

    public void StartNewGame()
    {
        if (spawnPoint == null)
        {
            Debug.Log("GameManager has no spawn point");
        } else
        {
            InstantiatePlayer();

            ScoreManager.instance.StartNewGame();
            gameActive = true;
        }
    }

    public void RespawnIfPossible()
    {
        if (ScoreManager.instance.Lives > 0)
        {
            InstantiatePlayer();
        }
    }
}
