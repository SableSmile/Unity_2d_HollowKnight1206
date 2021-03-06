﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    /// <summary>
    /// 子彈攻擊力
    /// </summary>
    public float attack;
    private float time;
    
    /// <summary>
    /// 碰撞事件:2個物件都沒有勾選is trigger才可觸發
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.GetComponent<Enemy>()){
            col.gameObject.GetComponent<Enemy>().hurt(attack);           
            Destroy(gameObject); 
        }
    }
    void Update() {
        time+=Time.deltaTime;
        if(time>=0.5){
            Destroy(gameObject); 
        }
    }
}
