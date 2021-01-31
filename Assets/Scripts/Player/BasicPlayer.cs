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
        [Header("Movement Settings")]
        public float moveSpeed;

        [Header("Jump Settings")]
        [Range(1,10)]
        public float verticalForce;
        public float fallModifier;
        public float jumpModifier;
    }

    // Reference RigidBody
    public Rigidbody2D physicsTarget;    

    // Expose player stats
    public PlayerStats stats;
    
    // Movement direction vector
    private Vector2 move_dir;

    // Jump boolean
    private bool can_jump;

    void Update()
    {
        get_input();
    }

    void FixedUpdate()
    {
        apply_movement(move_dir);
        apply_gravity_modifier();
    }

    // Get Input Data, use for any input checks
    void get_input()
    {
        // Horizontal Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        move_dir = new Vector2(x,y);

        // Vertical Movement
        if(Input.GetButtonDown("Jump"))
        {
            can_jump = true;
        }
    }

    // Apply Movement
    void apply_movement(Vector2 direction)
    {
        physicsTarget.velocity = (new Vector2(move_dir.x * stats.moveSpeed, physicsTarget.velocity.y));

        if(can_jump)
        {
            physicsTarget.velocity = new Vector2(physicsTarget.velocity.x, 0);
            physicsTarget.AddForce(Vector2.up * stats.verticalForce, ForceMode2D.Impulse);
            can_jump = false;
        }
    }

    // Apply gravity mods
    void apply_gravity_modifier()
    {
        // Apply Gravity
        if (physicsTarget.velocity.y < 0)
        {
            physicsTarget.gravityScale = stats.fallModifier;
        }

        // Apply Jump based on input being pressed
        else if(physicsTarget.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            physicsTarget.gravityScale = stats.jumpModifier;
        }   
        
        // If nothing else applies, reset gravity to it's default
        else
        {
            physicsTarget.gravityScale = 1.0f;
        }
    }
}
