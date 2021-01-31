using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BasicPlayer : MonoBehaviour
{
    // Struct containing player stats
    [Serializable]
    public struct PlayerStats
    {
        public float moveSpeed;
        public float jumpForce;
    }

    // Reference RigidBody
    public Rigidbody2D physicsTarget;    

    // Expose player stats
    public PlayerStats stats;

    private Vector2 move_dir;

    void Start()
    {
        
    }

    void Update()
    {
        get_input();
        apply_movement(move_dir);
    }

    // Get Input Data
    void get_input()
    {
        // Horizontal Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
<<<<<<< Updated upstream
=======
        move_dir = new Vector2(x,y);

        // Vertical Movement
        if Input
>>>>>>> Stashed changes
    }

    // Apply Movement
    void apply_movement(Vector2 direction)
    {
        physicsTarget.velocity = (new Vector2(move_dir.x * stats.moveSpeed, physicsTarget.velocity.y));
    }

    // Apply Jump
    void apply_jump()
    {
        physicsTarget.velocity = new Vector2(physicsTarget.velocity.x, 0);
        physicsTarget.velocity += Vector2.up * stats.jumpForce;
    }
}
