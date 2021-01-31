using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBoss : MonoBehaviour
{

    public GameObject player;
    public List<GameObject> attackVFX;
    public GameObject projectileSpawnPoint;
    public Collider2D ownCollider;

    private GameObject current_vfx;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        get_player_position();
    }

    void get_player_position()
    {
        playerPosition = player.transform.position;
    }

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

    void ignore_collision(GameObject projectile)
    {
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), ownCollider);
    }

}
