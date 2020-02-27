using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    public GameplayManager gameplayManager;
    public GameObject explosion;
    public GameObject model;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            explosion.SetActive(true);
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(-35.0f, 40.0f, 0.0f);
            playerController.hasSlammed = false;
            gameplayManager.points += 1000.0f;
            playerController.audioFall.SetActive(false);
            playerController.groundTimer = 0.0f;
            Destroy(model);
        }
    }
}
