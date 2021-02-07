
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
//第一次套用腳本
[RequireComponent(typeof(AudioSource),typeof(Rigidbody2D),typeof(CapsuleCollider2D))]

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0f, 1000)]
    public float enemy_MoveSpeed=10;
    [Header("血量"), Range(0, 5000)]
    public float enemy_HP=2500;
    [Header("攻擊範圍"), Range(0, 100)]
    public float enemy_AttackRange=10;
    [Header("攻擊力"), Range(0, 1000)]
    public float enemy_Attack=10;
    [Header("攻擊冷卻"), Range(0, 10)]
    public float enemy_cd=3.5f;

    [Header("攻擊延遲傳送傷害給玩家時間"), Range(0, 10)]
    public float Atkdelay=0.7f;

    [Header("血量文字")]
    public Text text_HP;
    [Header("血條圖")]
    public Image img_HP;
    [Header("攻擊位移")]
    public Vector3 offsetAtk;
    [Header("攻擊範圍大小")]
    public Vector3 sizeAtk;

    [Header("死亡事件")]
    public UnityEvent OnDead;
    
    private Player player;
    private float Max_HP;
    private float timer;
    private AudioSource au;
    private Rigidbody2D rig;
    private Animator an;
    private CameraControl cam;
    private bool IsSecond;
    private ParticleSystem ps;
    

    // Start is called before the first frame update
    void Start()
    {
        au=GetComponent<AudioSource>();
        rig=GetComponent<Rigidbody2D>();
        an=GetComponent<Animator>();
        
        text_HP.text=enemy_HP.ToString();
        Max_HP=enemy_HP;
        player=FindObjectOfType<Player>();
        cam=FindObjectOfType<CameraControl>();
        ps=GameObject.Find("attack_effect").GetComponent<ParticleSystem>();
        
    }
    private void Update() {
        if(an.GetBool("death"))
            return;
        Move();
    }

    public void hurt(float damage){
        enemy_HP-=damage;

        AnimatorStateInfo info=an.GetCurrentAnimatorStateInfo(0);
        if(!info.IsName("attack")){
            an.SetTrigger("hurt");
        }            
        text_HP.text=enemy_HP.ToString();
        img_HP.fillAmount=enemy_HP/Max_HP;

        if(enemy_HP<=Max_HP*0.8f){
            IsSecond=true;
            enemy_Attack=25;
        }
        if(enemy_HP<=0)
            death();
    }

    public void death(){
        
        OnDead.Invoke();

        enemy_HP=0;
        text_HP.text=0.ToString();
        an.SetBool("death",true);

        GetComponent<CapsuleCollider2D>().enabled=false;
        rig.Sleep();
        rig.constraints=RigidbodyConstraints2D.FreezeAll;
           
    }
    public void Move(){       
        /*if(transform.position.x>player.transform.position.x){
           transform.eulerAngles=new Vector3(0,180,0);
        }
        else
        {
            transform.eulerAngles=new Vector3(0,0,0);
        }*/
        //三源運算子 boolean ? 結果1 :(否則) 結果2
        //y = x是否大於 玩家x ? 是->y為180 : 否->y為0 
        float  y =transform.position.x>player.transform.position.x?180:0;
        transform.eulerAngles=new Vector3(0,y,0);
        float dis=Vector2.Distance(transform.position,player.transform.position);

        if(dis>enemy_AttackRange){
            AnimatorStateInfo asi=an.GetCurrentAnimatorStateInfo(0);
            if(asi.IsName("hurt") || asi.IsName("attack")){
                return;
            }
            //鋼體.移動座標(座標+前方*一幀*移動速度)
            rig.MovePosition(transform.position+transform.right*Time.deltaTime*enemy_MoveSpeed);
        }
        else{
            attack();
        }
        rig.WakeUp();
        an.SetBool("walk",rig.velocity.magnitude>0);
    }
    public void attack(){

        rig.velocity=Vector3.zero;
        if(timer<enemy_cd){             //計時<攻擊CD
            timer+=Time.deltaTime;      //累加timer
        }
        else{                           //否則 計時器>=攻擊CD
            an.SetTrigger("attack");    //攻擊
            timer=0;
            StartCoroutine(DelaySendDamage());
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color=new Color(0,1,0,0.5f);
        Gizmos.DrawCube(transform.position+transform.right* offsetAtk.x+transform.up*offsetAtk.y,sizeAtk);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, enemy_AttackRange);
    }
    private IEnumerator DelaySendDamage(){
        yield return new WaitForSeconds(Atkdelay);
        Collider2D hit=Physics2D.OverlapBox(transform.position + transform.right * offsetAtk.x + transform.up * offsetAtk.y,sizeAtk,0,1<<9);
            if(hit){
                player.hurt(enemy_Attack);
            }

        StartCoroutine(cam.ShakeCamera());

        if(IsSecond){
            ps.Play();
        }
    }
}
