using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class BulletBehavior : NetworkBehaviour
{

    [SerializeField] float bulletSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    [Rpc(SendTo.ClientsAndHost)]
    void updateBulletPositionClientRpc()
    {

    }
}
