using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazBar : MonoBehaviour {

    public Image Barre;
    public int MaxHydro;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float test = 0.066f;
        float value = (1f / ((float)MaxHydro)) * (float)PickMoney.Hydro;

        Barre.fillAmount = value;
        
		
	}
}
