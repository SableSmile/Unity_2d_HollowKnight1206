using UnityEngine;

public class Second_ATK : MonoBehaviour
{
    [Header("落石攻擊")]
    public float attack=50;

    //粒子碰撞事件
    private void OnParticleCollision(GameObject other) {
        if(other.GetComponent<Player>()){
            other.GetComponent<Player>().hurt(attack);
        }
    }
}
