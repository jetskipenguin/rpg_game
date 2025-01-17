using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomKey : MonoBehaviour
{
    public GameObject buttonPrompt;

	private bool inRange = false;
	private discoveryTracker mapTracker;

	// Start is called before the first frame update
	private void Start()
	{
		if(staticVariables.aquiredRoomKey)
		{
			gameObject.SetActive(false);
		}
		gameObject.transform.position = staticVariables.keyPos;
		mapTracker = GameObject.FindGameObjectWithTag("Discovery Tracker").GetComponent<discoveryTracker>();

	}
	// Update is called once per frame
	void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.F))
		{
			mapTracker.track("Interactable", gameObject);


			SoundManager.PlaySound(SoundManager.Sound.PickupKey);
			gameObject.SetActive(false);
			staticVariables.aquiredRoomKey = true;
			
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
