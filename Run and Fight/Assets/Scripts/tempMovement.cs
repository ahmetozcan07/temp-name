using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class tempMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        rb.velocity = movement * speed;
    }
}

