using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUI : MonoBehaviour
{
    public GameObject information;

    public void CloseInfo()
    {
        information.SetActive(false);
    }
}
