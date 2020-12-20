using UnityEngine;

public class Tip : MonoBehaviour
{
    [Header("速度"),Range(1,20)]
    public int speed;
    public GameObject character;

    [Header("距離")]
    [Range(5.5f,20.5f)]
    public float distancef;    
    public string log_text="提示文字";
    public bool active=true;
    public Color rgb=new Color(0.3f,0.5f,0.8f);
    public Color color2=Color.gray;
    public Color rgba=new Color(0.8f,0.3f,0.8f,0.4f);

    public Vector2 pos2=new Vector2(12,-15);
    public Vector3 pos3=new Vector3(12,15,16);
    public Sprite pic;
    public AudioClip sound;
    public Camera came;
    public Transform tran;



 
}
