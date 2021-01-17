
using UnityEngine;
//第一次套用腳本
[RequireComponent(typeof(AudioSource),typeof(Rigidbody2D),typeof(CapsuleCollider2D))]

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0f, 1000)]
    public float enemy_MoveSpeed;
    [Header("血量"), Range(0, 5000)]
    public int enemy_HP;
    [Header("攻擊範圍"), Range(0, 100)]
    public int enemy_AttackRange;
    [Header("攻擊力"), Range(0, 1000)]
    public int enemy_Attack;
    
    private AudioSource au;
    private Rigidbody2D rig;
    private Animator an;

    // Start is called before the first frame update
    void Start()
    {
        au=GetComponent<AudioSource>();
        rig=GetComponent<Rigidbody2D>();
        an=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hurt(int damage){
        enemy_HP-=damage;
        an.SetTrigger("hurt");
    }
}
