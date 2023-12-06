using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{    
    public string nextSceneName;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NextScene"){
            FindObjectOfType<AudioManager>().Play("Click");
            Next(nextSceneName);
        }
    }
    public void Next(string name)
    {
        SceneManager.LoadScene(sceneName:name);
    }
}
