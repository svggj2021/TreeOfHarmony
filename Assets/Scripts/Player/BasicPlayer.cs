using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BasicPlayer : MonoBehaviour
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

    // Reference to Player camera
    public Camera playerCamera;
    public InstrumentPlayer playersampler;
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
    public float heldtimecounter;
    #endregion

    /// Private Variables ///
    #region Private Variables
    // Movement direction vector
    private Vector2 move_dir;

    // Jump boolean
    private bool can_jump;

    // Store Current Projectile
    private GameObject current_vfx;

    // Attack boolean
    private bool is_attacking;

    private float startVerticalPosition;

    private float keyUpTime;
    private float keyDownTime;
    private float fixedEightthOffset;
    int index = 0;

    #endregion

    ///                                              ///
    ///     UNITY FUNCTIONS          ///
    ///                                             ///
    #region Unity Built-In Functions

    void Awake()
    {
        // Assign current vfx (Can be done in a VFX manager or something later)
        current_vfx = attack.attackVFX[0];

        //current_vfx = attack.attackVFX[ip.instrumentIndex];
    }

    void Update()
    {
        get_input(2);
    }

    void FixedUpdate()
    {
        apply_movement(move_dir);
        apply_gravity_modifier();
    }
    #endregion

    ///                                              ///
    ///     PHYSICS FUNCTIONS      ///
    ///                                             ///
    #region  Input Functions

    // Get Input Data, use for any input checks
    void get_input(int instrumentIndex)
    {

        if (!GridController.inFightSceneMode || (GridController.inFightSceneMode && GridController.readyToCount))
        // Horizontal Movement
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            move_dir = new Vector2(x, y);

            // Vertical Movement
            if (Input.GetButtonDown("Jump"))
            {
                can_jump = true;
            }
        }
        else
        {
            move_dir = Vector2.zero;
        }


        //Shooting  ---The fight scene needs to start with player on ground.
        if (GridController.inFightSceneMode)
        {
            if (GridController.readyToCount)
            {
                timecounter += Time.deltaTime;
                float fixedEightthOffset = 0.0f;
                

                if (Input.GetMouseButtonDown(0) && BeatTimer.MeasureTime >= 0)
                {

                    /*    Debug.Log("pressed down");*/

                    //1/8th of a measure hence 0.25f
                    //divide by 2 because for 2 units -->x so for the visual width of the measure MeasureController.widthOfMeasure --> (x/2)*MeasureController.widthofMeasure
                    //    | is the measure and --- is the wall, so offset is calculated from the wall and back:  <-.(15.7)-----|(14th one)

                    fixedEightthOffset = (Mathf.Round((timecounter - BeatTimer.MeasureTime) / 0.25f) * 0.25f);
                    float offset = MeasureController.widthOfMeasure * fixedEightthOffset / 2f;
                    float temp = BeatTimer.MeasureTime + fixedEightthOffset;

                    if (("" + temp) == ("" + timeindex))
                    {
                        return;

                    }



                    float vertspacing = MeasureController.LatestMeasure.GetComponent<MeasureController>().vertspacing;

                    index = (int)Mathf.Clamp(Mathf.Round((physicsTarget.position.y - startVerticalPosition) / vertspacing), 0, 12);
                    Vector3 vectortogoto = new Vector3(MeasureController.LatestMeasure.transform.position.x - offset, MeasureController.LatestMeasure.transform.position.y + (index) * vertspacing, MeasureController.LatestMeasure.transform.position.z);

                    GameObject gameobjecttemp = GameObject.Instantiate(Resources.Load<GameObject>("Note"));
                    gameobjecttemp.transform.position = transform.position;


                    StartCoroutine(FlyToYourPlace(gameobjecttemp, 0.05f, vectortogoto, () => {
                        gameobjecttemp.transform.SetParent(MeasureController.LatestMeasure.transform, true);
                    }));

                     timeindex = BeatTimer.MeasureTime + fixedEightthOffset;
                    playersampler.playSound(index);
                    Debug.Log("sound started");
                    //AuidoScript.play


                    //starttimer to determine how long note was played

                    //As the note is held,scale the notemiddlesprite

                    //AudioScript.stop

                    //add the caps for the sprite

                    //clean the duration to the nearest eighth

                    //store cleaned duration in the recorded data

                    //in playback mode, at the dictionary index value play the instrument assigned, the pitch clip for the amount of duration


                }
                if (Input.GetMouseButton(0) && BeatTimer.MeasureTime >= 0)
                {
                   
                    heldtimecounter += Time.deltaTime;
                    //Jill's scaling part
                }
                if (Input.GetMouseButtonUp(0) && BeatTimer.MeasureTime >= 0)
                {
                    Debug.Log("sound stopped");
                        playersampler.stopSound(index);
                    Debug.Log(heldtimecounter);
                  RecordShootingData.AddEntry(timeindex, new RecordedData(InstrumentPlayer.globalinstrumentindex, Mathf.Round(heldtimecounter / 0.25f) * 0.25f, index));
                    heldtimecounter = 0;
                    timeindex = -1;
                    spawn_projectile();
                }

            }
            else
            {
                startVerticalPosition = physicsTarget.position.y;
              
            }
        }
        else
        {
            index = 0;
        }
    }
    #endregion

    ///                                              ///
    ///     PHYSICS FUNCTIONS      ///
    ///                                             ///
    #region Physics Functions

    // Apply Movement
    void apply_movement(Vector2 direction)
    {
        physicsTarget.velocity = (new Vector2(move_dir.x * stats.moveSpeed, physicsTarget.velocity.y));

        if (can_jump)
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
        else if (physicsTarget.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            physicsTarget.gravityScale = stats.jumpModifier;
        }

        // If nothing else applies, reset gravity to it's default
        else
        {
            physicsTarget.gravityScale = 1.0f;
        }
    }
    #endregion

    ///                                                   ///
    ///     PROJECTILE FUNCTIONS      ///
    ///                                                  ///
    #region Projectile Functions
    // Spawns bullet projectile
    void spawn_projectile()
    {    
        // Internal VFX sdata storage
        GameObject vfx;

        // Check to make sure that a spawn point has been assigned and is valid
        if(projectileSpawnPoint != null)
        {
            // Instance projectile
            vfx = Instantiate(current_vfx, projectileSpawnPoint.transform.position, Quaternion.identity);
            
            // Reference rigid body in projectile and apply for to move it forward
            vfx.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3, ForceMode2D.Impulse);
            ignore_collision(vfx);
        }
        else
        {
            Debug.Log("Missing projectile spawn point");
        }
    }
    #endregion

    ///                                             ///
    ///     UTILITY FUNCTIONS       ///
    ///                                             ///
    #region Utility Functions

    /// This function checks the collisions to ignore projectiles ///
    /// In this instance it is used to prevent the projectile from hitting the player ///
    void ignore_collision(GameObject projectile)
    {
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), playerCollider);
    }

    IEnumerator FlyToYourPlace(GameObject gameObject, float duration, Vector3 togo, Action abc)
    {
        float localtime = 0;
        Vector3 startingposition = gameObject.transform.position;
        while (localtime < duration)
        {
            gameObject.transform.position = Vector3.Slerp(startingposition, togo, localtime / duration);

            localtime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = togo;
        abc.Invoke();
    }
    #endregion
}
