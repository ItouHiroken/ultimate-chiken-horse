using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Cursor : MonoBehaviour
{
    [Tooltip("ˆÚ“®‘¬“x")] public float _speed = 10.0f;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float verticalInput = _speed * Input.GetAxisRaw("Vertical");
        float horizontalInput = _speed * Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput, verticalInput);
    }
}