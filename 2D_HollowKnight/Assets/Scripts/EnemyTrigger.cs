using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [Header("敵人血條")]
    public GameObject tarHP;
    [Header("敵人")]
    public GameObject target;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name=="Player"){
            tarHP.SetActive(true);
            target.SetActive(true);
        }
    }
}
