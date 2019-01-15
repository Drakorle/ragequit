using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddText : MonoBehaviour
{
    public string FollowingText;

    public void AddFollowingText()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        if (text != null)
            text.text += FollowingText;
        else
        {
            Text txt = GetComponent<Text>();
            txt.text += FollowingText;
        }
    }
}
