using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Struct containing player stats
    #region Structs
    [Serializable]
    public struct PlayerStats
    {
        [Header("Movement Settings")]
        public float moveSpeed;

        [Header("Jump Settings")]
        [Range(1, 10)]
        public float verticalForce;
        public float fallModifier;
        public float jumpModifier;
    }

    [Serializable]
    public struct PlayerAttacks
    {
        //[Header("Attack VFX")]
        public List<GameObject> attackVFX;

        [Header("Attack Data")]
        [Range(1, 10)]
        public int basicAttackDamage;
    }
    #endregion

    /// Public Variables ///
    #region  Public Variables
    [Header("Player References")]
    // Reference RigidBody

    public Rigidbody2D physicsTarget;
    float timeindex = 0;


    //Reference and Assignment of Projectile Spawn Point
    public GameObject projectileSpawnPoint;

    // Reference Player Collider
    public Collider2D playerCollider;

    [Header("Player Data")]

    // Expose player stats
    public PlayerStats stats;

    [Header("Player Attacks")]
    // Expose player attacks
    public PlayerAttacks attack;

    public static float timecounter;
    public InstrumentPlayer enemysampler;
    #endregion

    /// Private Variables ///
    #region Private Variables
    // Movement direction vector
    private Vector2 move_dir;

    // Jump boolean
    private bool can_jump;

    public float heldtimecounter;
    private float startVerticalPosition;


    // Store Current Projectile
    private GameObject current_vfx;

    // Attack boolean
    private bool is_attacking;

    // Instrument Player Reference
    private InstrumentPlayer ip;


    void Awake()
    {
     /*   // Assign current vfx (Can be done in a VFX manager or something later)
        current_vfx = attack.attackVFX[0];

        //current_vfx = attack.attackVFX[ip.instrumentIndex];*/
    }

    void Update()
    {
        work();
    }

    void FixedUpdate()
    {

    }
    #endregion


    // Get Input Data, use for any input checks
    void work()
    {




        //Shooting  ---The fight scene needs to start with player on ground.
        if (GridController.inFightSceneMode)
        {
            if (GridController.readyToCount)
            {
                timecounter += Time.deltaTime;


                if (BeatTimer.MeasureTime >= 0 && RecordShootingData.allRecordedData.Count!=0)
                {
                    float fixedEightthOffset = (Mathf.Round((timecounter - BeatTimer.MeasureTime) / 0.25f) * 0.25f);
                    float timeindex = BeatTimer.MeasureTime + fixedEightthOffset;
                    Debug.Log("xcvxcvxcv"+BeatTimer.beatTime);
                    if (RecordShootingData.allRecordedData.ContainsKey(timeindex))

                    {
                        spawn_projectile();

                        for(int i=0;i< RecordShootingData.allRecordedData[timeindex].Count;i++)
                        {
                            enemysampler.playSound(RecordShootingData.allRecordedData[timeindex][i].instrument, true, RecordShootingData.allRecordedData[timeindex][i].duration, RecordShootingData.allRecordedData[timeindex][i].pitchindex);
                        }
                        
                    }
                }
            }


        }

        else
        {
            timecounter = 0;
        }
    }


    // Spawns bullet projectile
    void spawn_projectile()
    {
        // Internal VFX sdata storage
        GameObject vfx;

        // Check to make sure that a spawn point has been assigned and is valid
        if (projectileSpawnPoint != null)
        {
            // Instance projectile
            vfx = Instantiate(current_vfx, projectileSpawnPoint.transform.position, Quaternion.identity);

            // Reference rigid body in projectile and apply for to move it forward
            vfx.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3, ForceMode2D.Impulse);
          /*  ignore_collision(vfx);*/
        }
        else
        {
            Debug.Log("Missing projectile spawn point");
        }
    }
}