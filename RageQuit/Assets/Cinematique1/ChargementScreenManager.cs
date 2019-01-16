using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargementScreenManager : MonoBehaviour {

    public GameObject ChargementImages;
    public GameObject ChargementTexts;
    public GameObject[] ObjectsToDisable;

	public void DisplayChargementScreen()
    {
        int pickedImage = Random.Range(0, ChargementImages.transform.childCount);
        int pickedText = Random.Range(0, ChargementTexts.transform.childCount);

        GameObject img = ChargementImages.transform.Find(pickedImage.ToString()).gameObject;
        GameObject txt = ChargementTexts.transform.Find(pickedText.ToString()).gameObject;

        img.SetActive(true);
        txt.SetActive(true);

        foreach(GameObject obj in ObjectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
