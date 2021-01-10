
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("目標物件")]
    public Transform target;
    [Header("追蹤速度"),Range(0,100)]
    public float speed=3.5f;
    // Start is called before the first frame update
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
}
