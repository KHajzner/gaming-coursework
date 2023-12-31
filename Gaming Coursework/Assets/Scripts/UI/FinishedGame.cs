using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishedGame : MonoBehaviour
{
    public TMP_Text congrats;
    public GameObject chest, finishButton;

    void Start()
    {
        finishButton.SetActive(false);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            
            Invoke("OpenChest", 1.5f);
            FindObjectOfType<AudioManager>().Play("Click");
        }
    }
    
    void OpenChest()
    {
        FindObjectOfType<AudioManager>().Play("Completed");
        chest.GetComponent<Animator>().SetBool("Open", true);
        Invoke("FriendsAlongTheWay", 1f);
        Invoke("ShowFinishButton", 2f);
    }

    void FriendsAlongTheWay()
    {
        if(GlobalVars.crewScore > 14){
            congrats.SetText("The treasure was... the friends we made along the way??\n" + GlobalVars.crewScore + " of your crew survived this journey. \n You should be proud of yourself.");
        }
        else if(GlobalVars.crewScore > 8){
            congrats.fontSize = 30f;
            congrats.SetText("The treasure was... the friends we made along the way??\n But oh..., seems like some of your friends may have been lost during this journey, be it to scurvy or bandits. You should treasure the  " + GlobalVars.crewScore + " of your crew that you've got left.");
        }
        else if(GlobalVars.crewScore > 1){
            congrats.fontSize = 30;
            congrats.SetText("Hmm, the treasure was supposed to be the friends we made along the way, but you don't seem to have many left. You need to treasure your friendships more and take better care of them. Make sure you do that with your " + GlobalVars.crewScore + " crew left.");
        }
        else{
            congrats.SetText("You've got the treasure, but at what cost? Was it really worth it? I think you should do some thinking about your personal values. Hopefully till we never meet again, Capitain.");
        }

    }

    void ShowFinishButton(){
        finishButton.SetActive(true);
    }
}
