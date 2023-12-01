using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{    
    public string nextScene;
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "NextScene"){
            Next(nextScene);
        }
    }
    public void Next(string name)
    {
        SceneManager.LoadScene(sceneName:name);
    }
}
