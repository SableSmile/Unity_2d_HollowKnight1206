
using UnityEngine;
using UnityEngine.UI;
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

    [Header("血量文字")]
    public Text text_HP;
    [Header("血條圖")]
    public Image img_HP;
    
    private Player player;
    private float Max_HP;
    private AudioSource au;
    private Rigidbody2D rig;
    private Animator an;

    // Start is called before the first frame update
    void Start()
    {
        au=GetComponent<AudioSource>();
        rig=GetComponent<Rigidbody2D>();
        an=GetComponent<Animator>();
        text_HP.text=enemy_HP.ToString();
        Max_HP=enemy_HP;
        player=FindObjectOfType<Player>();
    }

    public void hurt(float damage){
        enemy_HP-=damage;
        an.SetTrigger("hurt");
        text_HP.text=enemy_HP.ToString();
        img_HP.fillAmount=enemy_HP/Max_HP;

        if(enemy_HP<=0)
            death();
    }

    public void death(){
        
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
        //鋼體.移動座標(座標+前方*一幀*移動速度)
        rig.MovePosition(transform.position+transform.right*Time.deltaTime*enemy_MoveSpeed);
        an.SetBool("walk",rig.velocity.magnitude>0);

        float dis=Vector2.Distance(transform.position,player.transform.position);
    }
    public void attack(){
        rig.velocity=player.transform.position;
        an.SetTrigger("attack");
    }
}
