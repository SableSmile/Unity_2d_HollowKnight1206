using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("目標物件")]
    public Transform target;
    [Header("追蹤速度"),Range(0,100)]
    public float speed=3.5f;
    [Header("晃動間隔"),Range(0,100)]
    public float shake_Interval=0.05f;
    [Header("晃動大小"),Range(0,100)]
    public float ShakeValue=0.5f;

    private void Track(){
        Vector3 posA =target.position; //取得玩家座標
        Vector3 posB=transform.position;
        posA.z=-10;

        posB=Vector3.Lerp(posB,posA,0.5f * speed * Time.deltaTime);    //插植
        transform.position=posB;    //更新camera座標
    }

    //延遲更新-在update執行後才執行，追蹤用
    private void LateUpdate() {
        Track();

    }
    public IEnumerator ShakeCamera(){

        for(int i=0;i<3;i++){
            transform.position+=Vector3.up*ShakeValue;
            yield return new WaitForSeconds(shake_Interval);
            transform.position-=Vector3.up*ShakeValue;
            yield return new WaitForSeconds(shake_Interval);
        }
    }
}
