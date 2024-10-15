using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class PlayerMovement : NetworkBehaviour
{
    Rigidbody rb;
    Vector3 player_rotation;
    GameObject cam;

    bool canFire = true;

    [SerializeField] float rotation_speed;
    [SerializeField] float fire_cooldown;
    [SerializeField] float knockback_strength;
    [SerializeField] GameObject bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_rotation = new Vector3(0, rotation_speed, 0);
        if (IsClient && HasAuthority) { 
            cam = GameObject.Find("Main Camera");
            cam.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (IsClient)
        {
            ClientUpdate();
        }
    }
    void ClientUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + player_rotation);
        //Debug.Log(transform.rotation.eulerAngles.ToString());
        if (Input.GetKey(KeyCode.Space) && HasAuthority)
        {
            AttemptFireServerRpc();
        }
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
    }

    [Rpc(SendTo.Server)]
    void AttemptFireServerRpc()
    {
        Debug.Log("Attempted Fire");
        if (!canFire) return;
        StartCoroutine(FireProjectile()); //no clue how to make this only fire for the client that pressed space
    }

    IEnumerator FireProjectile()
    {
        canFire = false;
        //fire
        GameObject this_bullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
        NetworkObject bulletNetwork = this_bullet.GetComponent<NetworkObject>();
        bulletNetwork.Spawn();
        //player movement
        player_rotation *= -1;
        rb.AddRelativeForce(Vector3.back * knockback_strength);
        //fire cooldown
        yield return new WaitForSeconds(fire_cooldown);
        canFire = true;
    }
}
