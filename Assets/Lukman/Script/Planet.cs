using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour, IEntity
{
    public float mass = 0f;

    public float Mass()
    {
        return this.mass;
    }

    public Vector2 Position()
    {
        return this.transform.position;
    }
}
