using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasBro : MonoBehaviour, IEntity
{
    public Rigidbody2D rb;
    public SpriteRenderer sprite;

    public float mass = 10f;
    public float rotSpeed = 5f;
    public float speed = 10f;
    public float maxSpeed = 10f;
    public float friction = 1f;

    float rotDir = 0f;
    bool gas = false;

    IEntity[] entities;

    void Start()
    {
        List<IEntity> tempEntities = new();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Entity"))
        {
            if (obj.TryGetComponent<IEntity>(out var entity))
            {
                tempEntities.Add(entity);
            }
        }
        this.entities = tempEntities.ToArray();
        // Debug.Log(this.entities.Length);
    }

    void Update()
     {
        InputEvents();
     }

    void FixedUpdate()
    {
        this.Rotate();
        this.Move();
        this.ClampSpeed();
        this.InteractWithEntities();
    }

    void InputEvents()
    {
        this.rotDir = Input.GetAxisRaw("Horizontal");
        this.gas = Input.GetKey(KeyCode.Z);
    }

    void Rotate()
    {
        this.rb.SetRotation(this.rb.rotation - this.rotDir * this.rotSpeed);

        float rotation = (90f + this.rb.rotation) % 360f;

        this.sprite.flipY = (rotation > 180f || (rotation < 0f && rotation > -180f));
    }

    void Move()
    {
        if (this.gas)
        {
            Vector2 addVel = new(
                Mathf.Cos(this.transform.eulerAngles.z / 180f * Mathf.PI) * speed * Time.deltaTime,
                Mathf.Sin(this.transform.eulerAngles.z / 180f * Mathf.PI) * speed * Time.deltaTime);

            this.rb.velocity -= addVel;
        }

        this.rb.velocity = Vector2.Lerp(this.rb.velocity, new Vector2(0f, 0f), this.friction * Time.deltaTime);
    }

    void ClampSpeed() 
    {
        this.rb.velocity = new (
            Mathf.Clamp(this.rb.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(this.rb.velocity.y, -maxSpeed, maxSpeed));
    }

    void InteractWithEntities()
    {
        foreach (IEntity entity in this.entities)
        {
            float distance = Vector2.Distance(this.rb.position, entity.Position());
            float force = this.mass * entity.Mass() / distance;
            float angle = Mathf.Atan2(this.rb.position.y - entity.Position().y, this.rb.position.x - entity.Position().x);
            this.rb.velocity -= new Vector2(Mathf.Cos(angle) * force * Time.deltaTime, Mathf.Sin(angle) * force * Time.deltaTime);
        }
    }

    public float Mass()
    {
        return this.mass;
    }

    public Vector2 Position()
    {
        return this.rb.position;
    }
}
