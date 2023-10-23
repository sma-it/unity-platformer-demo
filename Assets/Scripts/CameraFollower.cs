using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float distanceFromPlayer = 4;
    [SerializeField] private Vector2 bottomLeft = new Vector2(-10, -5);
    [SerializeField] private Vector2 topRight = new Vector2(10, 5);
    [SerializeField] private GameObject background;

    private Vector2 fieldSize;
    private Vector3 backgroundPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (background == null)
        {
            Debug.Log("Camera has no background set");
        } 
        fieldSize = topRight - bottomLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -distanceFromPlayer);
        }

        backgroundPosition = background.transform.position;
        backgroundPosition.x = transform.position.x + (transform.position.x / fieldSize.x);
        backgroundPosition.y = transform.position.y + (transform.position.y / fieldSize.y);

        background.transform.position = backgroundPosition;
    }

    public void FocusOn(Player player)
    {
        this.player = player;
    }
}
