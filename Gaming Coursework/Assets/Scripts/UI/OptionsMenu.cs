using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Text resolution;
    public Resolution[] resolutions;
    Resolution wantedRes;

    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        resolution.SetText(resolutions[GlobalVars.currentResolution].ToString());
    }
    public void LeftArrow()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Resolution[] resolutions = Screen.resolutions;
        if(GlobalVars.currentResolution > 0){
            GlobalVars.currentResolution --;
            resolution.SetText(resolutions[GlobalVars.currentResolution].ToString());
        }
    }
    public void RightArrow()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Resolution[] resolutions = Screen.resolutions;
        if(GlobalVars.currentResolution < resolutions.Length - 1){
            GlobalVars.currentResolution ++;
            resolution.SetText(resolutions[GlobalVars.currentResolution].ToString());
        }
    }

    public void Apply(){
        FindObjectOfType<AudioManager>().Play("Click");
        Resolution[] resolutions = Screen.resolutions;
        wantedRes = resolutions[GlobalVars.currentResolution];
        Screen.SetResolution(wantedRes.width, wantedRes.height, FullScreenMode.ExclusiveFullScreen, new RefreshRate() { numerator = 60, denominator = 1 });
    }

}
