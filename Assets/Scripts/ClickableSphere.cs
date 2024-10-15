using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ClickableSphere : NetworkBehaviour
{
    [SerializeField] NetworkVariable<int> sphereNumber = new NetworkVariable<int>();
    [SerializeField] TMP_Text sphereText;

    bool randomized;

    public int SphereNumber { get => sphereNumber.Value; set => sphereNumber.Value = value; } 

    // Update is called once per frame
    void Update()
    {
        sphereText.text = sphereNumber.Value.ToString();
        if (IsServer && !randomized)
            randomizeSphereNumberServerRpc();
    }

    private void OnMouseDown()
    {
        AddToSphereNumberServerRpc();
    }

    [Rpc(SendTo.Server)]
    private void AddToSphereNumberServerRpc(int clicksToAdd = 1)
    {
        sphereNumber.Value += clicksToAdd;
        if(sphereNumber.Value == 10)
            sphereNumber.Value = 0;
    }


    [Rpc(SendTo.Server)]
    private void randomizeSphereNumberServerRpc()
    {
        sphereNumber.Value = Random.Range(0, 10);
        randomized = true;
    }

}
