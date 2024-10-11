using Unity.Netcode;
using UnityEngine;

public class SphereManager : NetworkBehaviour
{
    [SerializeField] ClickableSphere[] clickableSpheres;

    [SerializeField] GameObject winCube;

    private bool winning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Randomize sphere numbers
        if(IsServer)
        {
            foreach(ClickableSphere cs in clickableSpheres)
            {
                cs.SphereNumber = Random.Range(0, 10);
            }
        }
        //clickableSpheres = GetComponentsInChildren<ClickableSphere>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsServer) ServerUpdate();
        if(IsClient) ClientUpdate();
    }
    
    void ServerUpdate()
    {
        winning = true;
        int sphereValue = clickableSpheres[0].SphereNumber;
        foreach(ClickableSphere cs in clickableSpheres)
        {
            if(cs.SphereNumber != sphereValue)
            {
                winning = false;
                break;
            }
        }
        SetWinningStateClientRpc(winning);
    }
    void ClientUpdate()
    {
        //nada
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SetWinningStateClientRpc(bool state)
    {
        winCube.SetActive(state);
    }
}
