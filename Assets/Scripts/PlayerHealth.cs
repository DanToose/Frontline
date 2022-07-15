using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public Text healthText;
    public float playerMaxHealth = 100;
    public KeyCode healthKey;
    private GameObject player;
    public float healingPackAmount;

    private bool keyWasPressed;

    public Text deathText;
    public Text respawnText;
    public Image deathPanel;

    // THIS SCRIPT ALSO CONTAINS RESPAWN INFO FOR THE PLAYER
    //public GameObject currentCheckpoint;
    public float respawnDelay = 3.0f;
    public Respawner respawn;

    public bool playerIsAlive;
    private bool readyToRespawn;

    // Start is called before the first frame update
    void Start()
    {
        deathText = GameObject.Find("DeathText").GetComponent<Text>();
        respawnText = GameObject.Find("RespawnText").GetComponent<Text>();
        deathPanel = GameObject.Find("DeathPanel").GetComponent<Image>();

        playerIsAlive = true;
        respawnText.text = "";
        deathText.text = "";
        deathPanel.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerMaxHealth;
        healthText.text = "Health: " + playerHealth;
        //currentCheckpoint = GameObject.FindGameObjectWithTag("StartPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + playerHealth;

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                playerDeath();
            }
        }

        keyWasPressed = false;
        keyWasPressed = Input.GetKeyDown(healthKey);

        if (player.GetComponent<PlayerInventory>().healingPackCount > 0 && keyWasPressed)
        {
            playerHealth += healingPackAmount;
            player.GetComponent<PlayerInventory>().LostHealing();
            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
        }

        if (readyToRespawn && Input.anyKeyDown)
        {
            readyToRespawn = false;
            respawnText.text = "";
            deathText.text = "";
            deathPanel.gameObject.SetActive(false);
            ActualRespawn();
        }
    }

    public void playerDeath()
    {
        // death stuff
        playerIsAlive = false;
        deathPanel.gameObject.SetActive(true);
        deathText.text = "You Died!";
        Invoke("RespawnFromDeath", respawnDelay);
    }

    void RespawnFromDeath()
    {
        respawnText.text = "Press any key to respawn";
        readyToRespawn = true;
    }

    void ActualRespawn()
    {
        playerHealth = playerMaxHealth;
        playerIsAlive = true;
        respawn.RespawnPlayer();
    }
}
