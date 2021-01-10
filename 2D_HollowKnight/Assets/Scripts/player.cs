
using UnityEngine;

public class player : MonoBehaviour
{
    #region  欄位
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
    #endregion

    public float h;
    [Header("地面判定位移")] //地面判定位移
    public Vector3 offset;
    [Header("地面判定半徑")] //地面判定半徑
    public float radius=0.3f;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<泛型>()
        //泛型:泛指所有類型
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetHorizontal();
        Move();
        Jump();
    }

    //在Unity內繪製圖示
    private void OnDrawGizmos() {
        Gizmos.color=new Color(1,0,0,0.35f);
        Gizmos.DrawSphere(transform.position+offset,radius);
    }

    private void Move(){
        //剛體.加速度=二維(水平*速度,原本加速度的Y)
        rb.velocity=new Vector2(h*MoveSpeed,rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){

            /*FlipX控制面相*/
            //this.GetComponent<SpriteRenderer>().flipX=false;


            //trandsorm指的是與此腳本同一層的Transform元件
            //rotation角度在程式是localEulerAngles
            transform.localEulerAngles=Vector3.zero; 
            
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            
            /*FlipX控制面相*/
            //this.GetComponent<SpriteRenderer>().flipX=enabled;

            transform.localEulerAngles=new Vector3(0,180,0);
        }
        //玩家按下左或右鍵時，動畫設置 跑步 判斷基準為 水平移動h!=0
        animator.SetBool("Run",h!=0);
    }
    private void GetHorizontal(){
        //輸入.軸向("水平");
        h=Input.GetAxis("Horizontal");
        this.Move();
    }
    private void Jump(){
        if(OnGround && Input.GetKeyDown(KeyCode.Space)){

            //剛體.添加推力(二為向量)
            rb.AddForce(new Vector2(0,JumpHeight));
            OnGround=false;
        }

        //碰撞物件= 2D 物理.覆蓋圓形(中心點，半徑，圖層)
        //layermack 1<<8 1<<圖層 偵測第8層
        Collider2D hit= Physics2D.OverlapCircle(transform.position+offset,radius,1<<8);
        //animator.SetFloat("Jump",JumpHeight);

        //如碰撞物件存在，OnGround為"是"
        if(hit){
            OnGround=true;
        }
        else{
            OnGround=false;
        }

    }
}
