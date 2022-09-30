using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOverController : MonoBehaviour
{
    private void Awake() 
    {
        Debug.Log("Level controller awake");
    }
   private void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("Level Trigger");
        //if(collision.gameObject.CompareTag("Player"))
        if(collision.gameObject.GetComponent<PlayerController>() != null)
 
        //Level is Over;
        Debug.Log("level Finished by The Player");
    }
    
}
