using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ClickableSphere : NetworkBehaviour
{
    [SerializeField] NetworkVariable<int> sphereNumber = new NetworkVariable<int>();
    [SerializeField] TMP_Text sphereText;

    public int SphereNumber { get => sphereNumber.Value; set => sphereNumber.Value = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sphereText.text = sphereNumber.Value.ToString();
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

}
