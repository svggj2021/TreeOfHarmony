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

    [Serializable]
    public struct PlayerAttacks
    {
        //[Header("Attack VFX")]
        public List<GameObject> attackVFX;

        [Header("Attack Data")]
        [Range(1,10)]
        public int basicAttackDamage;
    }

    [Header("Player References")]
    // Reference RigidBody
    public Rigidbody2D physicsTarget;    

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

    // Movement direction vector
    private Vector2 move_dir;

    // Jump boolean
    private bool can_jump;
    
    // Store Current Projectile
    private GameObject current_vfx;

    // Attack boolean
    private bool is_attacking;

    public static float timecounter;
    private float startVerticalPosition;

    void Awake()
    {
        // Assign current vfx (Can be done in a VFX manager or something later)
        current_vfx = attack.attackVFX[0];
    }

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
        
        //Shooting  ---The fight scene needs to start with player on ground.
        if(GridController.inFightSceneMode)
        {
            if (GridController.readyToCount)
            {
                timecounter += Time.deltaTime;
                if (Input.GetMouseButtonDown(0) && BeatTimer.MeasureTime >= 0)
                {
                    /// Spawns Notes And attaches to grid ///

                    timecounter += Time.deltaTime;

                    //1/8th of a measure hence 0.25f
                    //divide by 2 because for 2 units -->x so for the visual width of the measure MeasureController.widthOfMeasure --> (x/2)*MeasureController.widthofMeasure
                    //    | is the measure and --- is the wall, so offset is calculated from the wall and back:  <-.(15.7)-----|(14th one)

                    float fixedEightthOffset = (Mathf.Round((timecounter - BeatTimer.MeasureTime) / 0.25f) * 0.25f);
                    float offset = MeasureController.widthOfMeasure *  fixedEightthOffset/ 2f;



                    float vertspacing = MeasureController.LatestMeasure.GetComponent<MeasureController>().vertspacing;

                    int index = (int)Mathf.Clamp(Mathf.Round((physicsTarget.position.y-startVerticalPosition) / vertspacing), 0, 12 );
                    Vector3 vectortogoto = new Vector3(MeasureController.LatestMeasure.transform.position.x - offset, MeasureController.LatestMeasure.transform.position.y + (index) * vertspacing, MeasureController.LatestMeasure.transform.position.z);
                    GameObject gameobjecttemp = GameObject.Instantiate(Resources.Load<GameObject>("Note"));
                    gameobjecttemp.transform.position = transform.position;
    

                    StartCoroutine(FlyToYourPlace(gameobjecttemp,0.25f, vectortogoto,()=> {
                        gameobjecttemp.transform.SetParent(MeasureController.LatestMeasure.transform, true);
                    }));
                        

                    RecordShootingData.AddEntry(BeatTimer.MeasureTime + fixedEightthOffset, new RecordedData("Guitar",0));

                    /// Spawns Attack Projectile ///
                    spawn_projectile();
                }
            }
            else
            {
                startVerticalPosition = physicsTarget.position.y;
            }
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

    /// This function checks the collisions to ignore projectiles ///
    void ignore_collision(GameObject projectile)
    {
        Physics.IgnoreCollision(projectile.GetComponent<Collider2D>(), playerCollider);
    }

    IEnumerator FlyToYourPlace(GameObject gameObject,float duration,Vector3 togo, Action abc)
    {
        float localtime = 0;
        Vector3 startingposition = gameObject.transform.position;
        while(localtime<duration)
        {
            gameObject.transform.position = Vector3.Slerp(startingposition, togo, localtime / duration);

            localtime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = togo;
        abc.Invoke();
    }
}
