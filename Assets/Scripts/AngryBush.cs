using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AngryBush : MonoBehaviour
{
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float color = math.sin(Time.time * 2) * 0.5f + 0.5f;
        sr.color = new Color(color, 1 - color*0.5f, 1-color*0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.DoDamage(1);
        }
    }

}
