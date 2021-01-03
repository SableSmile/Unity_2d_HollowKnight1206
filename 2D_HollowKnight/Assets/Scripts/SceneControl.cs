using UnityEngine;
using UnityEngine.SceneManagement; //引用 場景管理 API

public class SceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        //載入場景
        SceneManager.LoadScene("map1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BackToMenu(){
        SceneManager.LoadScene("menu");
    }

    //<summery>
    //離開遊戲
    //</summery>
    public void QuitGame(){

        Application.Quit();
    }
}
