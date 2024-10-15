using UnityEngine;
using Unity.Netcode;

public class MenuManager : NetworkBehaviour
{
    //Canvas elements
    [SerializeField] GameObject joinSessionField;
    [SerializeField] GameObject quickJoinButton;
    [SerializeField] GameObject sessionCode;
    [SerializeField] GameObject leaveButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joinSessionField.SetActive(true);
        quickJoinButton.SetActive(true);
        sessionCode.SetActive(false);
        leaveButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SessionJoined()
    {
        joinSessionField.SetActive(false);
        quickJoinButton.SetActive(false);
        sessionCode.SetActive(true);
        leaveButton.SetActive(true);
    }
    public void SessionLeft()
    {
        joinSessionField.SetActive(true);
        quickJoinButton.SetActive(true);
        sessionCode.SetActive(false);
        leaveButton.SetActive(false);
    }

}
