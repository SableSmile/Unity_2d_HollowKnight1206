using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{

    #region  欄位
    [Header("移動速度"), Range(0f, 1000)]
    public float MoveSpeed;

    [Header("跳躍高度"), Range(0, 3000)]
    public int JumpHeight;

    [Header("是否在地板上")] //預設為否
    public bool OnGround;

    [Header("子彈")] //物件 子彈
    public GameObject bullet;

    [Header("子彈生成點")] //物件位置
    public Transform bullet_pos;

    [Header("子彈速度"), Range(0, 5000)]
    public int bullet_speed;

    [Header("子彈傷害"), Range(0, 5000)]
    public int bullet_atk;

    [Header("開槍音效")] //槍聲
    public AudioClip shoot_sound;

    [Header("血量"), Range(0, 200)]
    public float HP;
    [Header("吃鑰匙音效")] //吃鑰匙聲音
    public AudioClip key_sound;
    [Header("血量文字")]
    public Text text_HP;
    [Header("血條圖")]
    public Image img_HP;

    private float Max_HP;
    private AudioSource aud;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    
    #endregion

    public float h;

    [Header("地面判定位移")] //地面判定位移
    public Vector3 offset;

    [Header("地面判定半徑")] //地面判定半徑
    public float radius = 0.3f;
    [Header("結束畫面")]
    public GameObject gameover;

    // Start is called before the first frame update
    private void Start()
    {
        //GetComponent<泛型>()
        //泛型:泛指所有類型
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        sr=GetComponent<SpriteRenderer>();
        Max_HP=HP;
    }

    // Update is called once per frame
    private void Update()
    {
        GetHorizontal();
        Move();
        Jump();
        Fire();
    }

    //在Unity內繪製圖示
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.35f);
        Gizmos.DrawSphere(transform.position + offset, radius);
    }

    private void Move()
    {
        AnimatorStateInfo info=animator.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("attack")||info.IsName("hurt")){
            
        }
        //剛體.加速度=二維(水平*速度,原本加速度的Y)
        rb.velocity = new Vector2(h * MoveSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {

            /*FlipX控制面相*/
            //this.GetComponent<SpriteRenderer>().flipX=false;


            //trandsorm指的是與此腳本同一層的Transform元件
            //rotation角度在程式是localEulerAngles
            transform.localEulerAngles = Vector3.zero;

        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {

            /*FlipX控制面相*/
            //this.GetComponent<SpriteRenderer>().flipX=enabled;

            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        //玩家按下左或右鍵時，動畫設置 跑步 判斷基準為 水平移動h!=0
        animator.SetBool("Run", h != 0);
    }
    private void GetHorizontal()
    {
        //輸入.軸向("水平");
        h = Input.GetAxis("Horizontal");
    }
    private void Jump()
    {
        if (OnGround && Input.GetKeyDown(KeyCode.Space))
        {

            //剛體.添加推力(二為向量)
            rb.AddForce(new Vector2(0, JumpHeight));
            animator.SetTrigger("JumpTrigger");
        }

        //碰撞物件= 2D 物理.覆蓋圓形(中心點，半徑，圖層)
        //layermack 1<<8 1<<圖層 偵測第8層
        Collider2D hit = Physics2D.OverlapCircle(transform.position + offset, radius, 1 << 8);
        //animator.SetFloat("Jump",JumpHeight);

        //如碰撞物件存在，OnGround為"是"
        if (hit)
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
        animator.SetFloat("Jump", rb.velocity.y);
        animator.SetBool("OnGround",OnGround);


    }
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {    //Mouse0=左鍵
            aud.PlayOneShot(shoot_sound, Random.Range(0.8f, 1.2f));
            GameObject temp = Instantiate(bullet, bullet_pos.position, bullet_pos.rotation);
            temp.GetComponent<Rigidbody2D>().AddForce(bullet_pos.right * bullet_speed + bullet_pos.up * 50);
            temp.AddComponent<bullet>().attack=bullet_atk;
        }
    }
    // 碰到物件的資訊
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "key")
        {
            Destroy(col.gameObject);
            aud.PlayOneShot(key_sound, Random.Range(0.8f, 1.2f));
            
        }

    }
    public void hurt(float damage){
        HP-=damage;
        //animator.SetTrigger("hurt");
        text_HP.text=HP.ToString();
        img_HP.fillAmount=HP/Max_HP;
        //StartCoroutine(DamageEffect());

        if(HP<=0)
            death();
    }
    public void death(){
        HP=0;
        text_HP.text=0.ToString();
        animator.SetBool("Death",true);
        this.enabled=false;
        rb.Sleep();
        transform.Find("FNScar").gameObject.SetActive(false);
        gameover.SetActive(true);
    }
    private IEnumerator DamageEffect(){
        Color red=new Color(1,0.1f,0.1f);
        float interval=0.05f;
        for(int i=0;i<5;i++){
            sr.color=red;   //轉紅
            yield return new WaitForSeconds(interval);  //等待
            sr.color=Color.white;   //恢復
            yield return new WaitForSeconds(interval);  //等待
        }
    }
}