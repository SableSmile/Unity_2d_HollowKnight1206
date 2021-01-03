using UnityEngine;
using UnityEngine.SceneManagement; //引用 場景管理 API

public class SceneControl : MonoBehaviour
{
    [Header("音效來源")]
    public AudioSource au;
    [Header("按鈕音效")]
    public AudioClip soundClick;
    public void StartGame()
    {
        //音效來源.播放一次(音效，音量)
        au.PlayOneShot(soundClick);
        //計時器("function name",延遲秒數)
        Invoke("DelayStartGame",1.5f);        
        
    }

    public void BackToMenu(){
        SceneManager.LoadScene("menu");
    }

    //<summery>
    //離開遊戲
    //</summery>
    public void QuitGame(){

        Application.Quit();
    }

    public void DelayStartGame(){
        //載入場景
        SceneManager.LoadScene("map1");
    }
}
