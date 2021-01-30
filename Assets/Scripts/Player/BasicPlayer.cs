using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayer : MonoBehaviour
{
    // Struct containing player_stats
    public struct PlayerStats
    {
        public float movement_speed;
        private Vector2 move_dir;
    }


    // Reference RigidBody
    public Rigidbody2D physicsTarget;    


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Get Input Data
    void get_input()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Inpt.GetAxis("Vertical");
    }

    void get_movement(Vector2 direction)
    {

    }
}
