using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRTK;


public class DoorManager : MonoBehaviour
{

    public float angleThreshold = 25.0f;
    private string sceneName;
    VRTK_HeadsetFade fade;
    private bool switchScene = false;

    void Start()
    {
        fade = new VRTK_HeadsetFade();
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current scene is " + sceneName);

        angleThreshold *= .707f;
        angleThreshold /= 90.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(gameObject.transform.rotation.y) > angleThreshold && switchScene == false)
        {
            switchScene = true;
            if (sceneName == "Menu")
            {
                Debug.Log("Loading Kitchen scene");

                SwitchScene("Kitchen");
                //SceneManager.LoadSceneAsync("Kitchen");
            }

            if (sceneName == "Kitchen")
            {
                Debug.Log("Loading Menu scene");

                SwitchScene("Menu");
                //SceneManager.LoadSceneAsync("Menu");
            }
        }
    }

    void SwitchScene(string sceneName)
    {
        Debug.Log("Fading out");
        //fade.Fade(Color.white, 3);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
