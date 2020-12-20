
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("移動速度"),Range(0f,1000f)]
    public float MoveSpeed;
    
    [Header("跳躍高度"),Range(0,3000)]
    public int JumpHeight;

    [Header("是否在地板上")] //預設為否
    public bool OnGround;

    [Header("子彈")] //物件 子彈
    public GameObject bullet;

    [Header("子彈生成點")] //物件位置
    public Transform bullet_pos;

    [Header("子彈速度"),Range(0,5000)]
    public int bullet_speed;   

    [Header("開槍音效")] //槍聲
    public AudioClip shoot_sound;   

    [Header("血量"),Range(0,200)]
    public int HP;    

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
