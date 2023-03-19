using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public Rigidbody2D player;
    float rotation;

    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            if (obj.TryGetComponent<Rigidbody2D>(out var player))
            {
                this.rotation = player.rotation;
            }
        }
    }


    void Update()
    {
        Vector2 moveDirection = new(
                Mathf.Cos(this.rotation / 180f * Mathf.PI) * 1f * Time.deltaTime,
                Mathf.Sin(this.rotation / 180f * Mathf.PI) * 1f * Time.deltaTime);

        this.rb.velocity = moveDirection;
    }
}
