using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Player's movement speed

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        // Getting reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Taking input for movement (WASD or Arrow keys)
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        // Normalize movementInput to prevent faster diagonal movement
        movementInput = movementInput.normalized;
    }

    void FixedUpdate()
    {
        // Move the player based on the input and speed
        rb.velocity = movementInput * moveSpeed;
    }
}