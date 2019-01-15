using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandDestroy : MonoBehaviour {

    public int step = 0;
    public float current_time = 0;
    public static bool fired;
    public static float time;
    public GameObject island;
    public GameObject islandDamaged;
    public GameObject islandRealyDamaged;
    public bool firstcall = true;

    // Update is called once per frame
    void Update () {

        IslandDestruction(time);
        if (fired)
        {
            if(firstcall)
            {
                step += 1;
                firstcall = false;
            }
            
            current_time += 0.08f;
        }
        
    }

    void IslandDestruction(float time)
    {
        if (current_time >= time)
        {
            if (step == 1)
            {
                island.SetActive(false);
                islandDamaged.SetActive(true);
            }
            else if (step == 2)
            {
                islandDamaged.SetActive(false);
                islandRealyDamaged.SetActive(true);
            }
            else if (step == 3)
            {
                //fin de scène
                //Load scène Boss Final Rochers
            }
            fired = false;
            firstcall = true;
            current_time = 0f;
        }
            
        

    }
}
