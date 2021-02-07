using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;
    private void Awake() {
        player=FindObjectOfType<Player>();
    }
    public void PauseGame(){
        Time.timeScale=0;
        player.enabled=false;
    }
    public void ContinueGame(){
        Time.timeScale=1;
        player.enabled=true;
    }
}
