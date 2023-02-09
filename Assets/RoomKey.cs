using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomKey : MonoBehaviour
{
    public GameObject buttonPrompt;

	private bool inRange = false;
	private AudioSource pickupSound;
	// Start is called before the first frame update
	private void Start()
	{
		gameObject.transform.position = staticVariables.keyPos;
		pickupSound = gameObject.GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.F))
		{
			gameObject.SetActive(false);
			staticVariables.aquiredRoomKey = true;
			pickupSound.Play();
		}


    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && staticVariables.aquiredRoomKey == false) // if the player is in range and he hasnt already picked up the key
		{
            buttonPrompt.SetActive(true);
			inRange = true;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Player")) // if the player leaves the range
		{
			inRange = false;
			buttonPrompt.SetActive(false);
		}
	}
}
