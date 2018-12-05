using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListManager : MonoBehaviour {

    string previousLevelName = "";

    public void ManagePreview(string levelName)
    {
        if (previousLevelName == levelName || levelName == "HidePreview")
        {
            HidePreview(previousLevelName);
            previousLevelName = "";
        }
        else
        {
            if (previousLevelName != "")
                HidePreview(previousLevelName);
            DisplayPreview(levelName);
        }
    }

    private void DisplayPreview (string levelName)
    {
        Transform currentPos = transform.Find("MenuZone");
        VerticalLayoutGroup list = currentPos.Find("GridWithElements").GetComponent<VerticalLayoutGroup>();
        currentPos = currentPos.Find("GridWithElements");
        RectTransform level = (RectTransform) currentPos.Find(levelName);
        GameObject preview = level.Find("MapButton").Find("Image").gameObject;
        GameObject playButton = level.Find("MapButton").Find("PlayButton").gameObject;

        previousLevelName = levelName;

        preview.SetActive(true);
        playButton.SetActive(true);

        level.sizeDelta = new Vector2(level.sizeDelta.x, level.sizeDelta.y + 200);
        list.spacing += 0.1f;
        list.spacing -= 0.1f;
    }

    private void HidePreview(string levelName)
    {
        Transform currentPos = transform.Find("MenuZone");
        VerticalLayoutGroup list = currentPos.Find("GridWithElements").GetComponent<VerticalLayoutGroup>();
        currentPos = currentPos.Find("GridWithElements");
        RectTransform level = (RectTransform)currentPos.Find(levelName);
        GameObject preview = level.Find("MapButton").Find("Image").gameObject;
        GameObject playButton = level.Find("MapButton").Find("PlayButton").gameObject;

        preview.SetActive(false);
        playButton.SetActive(false);

        level.sizeDelta = new Vector2(level.sizeDelta.x, level.sizeDelta.y - 200);
        list.spacing += 0.1f;
        list.spacing -= 0.1f;
    }
}
