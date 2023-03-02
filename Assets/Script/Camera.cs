using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector2 targetPos = target.position;
        this.transform.position = new(target.position.x, target.position.y, -10f);
    }
}
