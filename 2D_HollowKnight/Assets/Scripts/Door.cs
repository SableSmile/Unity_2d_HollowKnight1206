using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("鑰匙")]
    public GameObject key;
    private Animator ani;
    public AudioClip open_sound;
    public AudioSource door_open;
    void Start()
    {
        ani=GetComponent<Animator>();
        door_open=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col) {
        //玩家碰到鑰匙，且鑰匙已消失
        if(col.name=="Player" && key==null){
            ani.SetTrigger("open");
            door_open.PlayOneShot(open_sound,1);
        }
    }
}
