using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportTrigger : MonoBehaviour
{
    public static TeleportTrigger instance {get; private set;}
    private bool canFinish = false;



    private void Awake() {
        if(instance != null)
        {
            Debug.LogError("There is more than one instance of TeleportTrigger in game");
        }
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player in");

            if(canFinish)
            {
                SceneManager.LoadSceneAsync("GameOver");
            }
            
            
        }
        
    }

    public void SetCanFinish(bool status)
    {
        canFinish = status;
    }
}
