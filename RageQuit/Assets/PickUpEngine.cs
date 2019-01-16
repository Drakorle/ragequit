using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpEngine : MonoBehaviour {

    // Use this for initialization
    // Update is called once per frame
    public float i = 0;
	void Update () {
        i += 0.01f;
        transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);
        transform.Translate(new Vector3(0, Mathf.Cos(i)/150, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("PickUpEngine");
            EndOfLevel.DisplayMenu();


        }
    }
}
