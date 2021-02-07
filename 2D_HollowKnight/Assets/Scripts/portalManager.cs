using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalManager : MonoBehaviour
{
    [Header("目標傳送門")]
    public Transform portal;

    private bool playerIn;
    private Transform player;

    private void trans(){
        if(playerIn && Input.GetKeyDown(KeyCode.W)){
            player.position=portal.position+Vector3.up*1.5f;
        }
    }
    
    private void Awake() {
        player=GameObject.Find("Player").transform;
    }

    private void Update() {
       trans(); 
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name=="Player"){
            playerIn=true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.name=="Player"){
            playerIn=false;
        }
    }
}
