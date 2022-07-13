using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePlank : MonoBehaviour
{
    public GameObject player;
    public GameObject plankObject;
    public GameObject blockObject;
    private Collider blockMesh;
    private MeshRenderer plankMesh;
    private bool isPlankPlaced;
    private bool plankIsPlacable;
    private bool keyWasPressed;
    public KeyCode keyToPlacePlank;


    // Start is called before the first frame update
    void Start()
    {
        blockMesh = blockObject.GetComponent<Collider>();
        plankMesh = plankObject.GetComponent<MeshRenderer>();
        player = GameObject.FindWithTag("Player");
        plankMesh.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        keyWasPressed = false;
        keyWasPressed = Input.GetKeyDown(keyToPlacePlank);

        if (plankIsPlacable == true && keyWasPressed)
        {
            //Debug.Log("plank placement fired");
            plankIsPlacable = false;
            blockMesh.GetComponent<Collider>().enabled = false;
            plankMesh.enabled = true;
            player.GetComponent<PlayerInventory>().LostPlank();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isPlankPlaced == false)
        {
            if (player.GetComponent<PlayerInventory>().plankCount > 0)
            {
                plankIsPlacable = true;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isPlankPlaced == true)
        {
            plankIsPlacable = false;
        }
    }
}
