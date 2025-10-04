using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMeteorController : MonoBehaviour
{
    public float moveSpeed = 3.0f;        
    public Vector2 moveDirection;         

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
