using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using VRTK;

public class MainManager : MonoBehaviour
{
    private string sceneName;
    // public OVRCameraRig _ovr;
    private Vector3 _originPosition;
    private float _originRotation;
    public Transform _playspace;
    public Transform _centerEyeAnchor;

    // Start is called before the first frame update
    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        _originPosition = new Vector3(_playspace.transform.position.x, 0, _playspace.transform.position.z);
        _originRotation = _playspace.rotation.eulerAngles.y;

        // Use my custom function provided here to manually reset the position and angle
        // Given the position and direction of where you want to move the playspace to.

        ResetCameraToPositionOrigin(_originPosition, _originRotation);
        
        /*
        _OVRCameraRig = _ovr.transform;
        _centerEyeAnchor = _ovr.centerEyeAnchor;

        _OVRCameraRig = VRTK_DeviceFinder.PlayAreaTransform();
        _centerEyeAnchor = VRTK_DeviceFinder.HeadsetCamera();
        _centerEyeAnchor = _OVRCameraRig;

        This only works with Rift, not Quest, etc. 
         UnityEngine.XR.InputTracking.Recenter();


        ResetCameraToPositionOrigin(_OVRCameraRig.position, _OVRCameraRig.rotation.eulerAngles.y);
        Debug.Log(sceneName + " Playspace: " + _playspace.transform.position);
        Debug.Log(sceneName + " HMD: " + _centerEyeAnchor.transform.position);
        */
    }

    private void Update()
    {
        // If you press the B button on the right Oculus Touch controller, for debugging.Re - enable later if you need to test it. Otherwise, use the scene door.
        //if (sceneName == "Kitchen" && (OVRInput.GetUp(OVRInput.Button.Two)))
        //{
        //    VocabManager.languageID = (VocabManager.languageID + 1) % VocabManager.num_languages;
        //    VocabManager.language = VocabManager.languages[VocabManager.languageID];
        //}

        /*
        if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            Debug.Log(sceneName + "B Playspace: " + _playspace.transform.position);
            Debug.Log(sceneName + "B HMD: " + _centerEyeAnchor.transform.position);
            ResetCameraToPositionOrigin(_originPosition, _originRotation);
            Debug.Log(sceneName + "B Playspace: " + _playspace.transform.position);
            Debug.Log(sceneName + "B HMD: " + _centerEyeAnchor.transform.position);
        }

        If you press the B button on the right Oculus Touch controller, for debugging.Re - enable later if you need to test it. Otherwise, use the scene door.
        if (sceneName == "Menu" && (OVRInput.GetUp(OVRInput.Button.Two)))
        {
            Debug.Log("Loading Kitchen scene");
            SceneManager.LoadScene("Kitchen");
        }

        // If you press the B button on the right Oculus Touch controller
        if (sceneName == "Kitchen" && (OVRInput.GetUp(OVRInput.Button.Two)))
        {
            Debug.Log("Loading Kitchen scene");
            SceneManager.LoadScene("Menu");
        }
        */
    }

    void ResetCameraToPositionOrigin(Vector3 originPosition, float originRotation)
    {
        StartCoroutine(ResetCamera(originPosition, originRotation));
    }

    //Resets the OVRCameraRig's position and Y-axis rotation to help align the player's starting position and view to the target parameters
    IEnumerator ResetCamera(Vector3 targetPosition, float targetYRotation)
    {
        yield return new WaitForEndOfFrame();

        float currentRotY = _centerEyeAnchor.eulerAngles.y;
        Debug.Log(sceneName + " currentRotY: " + currentRotY);
        float difference = targetYRotation - currentRotY;
        Debug.Log(sceneName + " targetYRotation: " + targetYRotation);
        _playspace.Rotate(0, difference, 0);

        Vector3 newPos = new Vector3(targetPosition.x - _centerEyeAnchor.position.x, 0, targetPosition.z - _centerEyeAnchor.position.z);
        Debug.Log(sceneName + " newPos: " + newPos);
        _playspace.transform.position += newPos;
    }
}
