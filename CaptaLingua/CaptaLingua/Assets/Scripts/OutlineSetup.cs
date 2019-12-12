using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class OutlineSetup : MonoBehaviour
{
    [Tooltip("Drag in the VRTK_Pointer component which is either on this object or another. Sets up highlighting with the pointer.")]
    public VRTK_Pointer pointer;
    public GameObject rightHandAnchor;
    public GameObject fruitSaladTask;

    private RaycastHit hit;

    private GameObject currentHighlight = null;

    private const float TIME_LEFT = 1f; // With Time.deltaTime, this is one second
    private float audioTimer = 0f;
    private bool resettingTimer = false;
    private string currentWord = "";
    private VocabManager curVocManager = null;

    private void Awake()
    {

        fruitSaladTask.SetActive(true);
        var highlightableObjects = GameObject.FindGameObjectsWithTag("CanHighlight");
        fruitSaladTask.SetActive(false);

        // Set up the Outline component for all objects in the scene that can be highlighted, then disable the outline for now
        foreach (GameObject obj in highlightableObjects)
        {
            obj.AddComponent<Outline>();
            //Debug.Log(obj.name);
            Outline outline = obj.GetComponent<Outline>();
            outline.enabled = true;

            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.cyan;
            outline.OutlineWidth = 3f;
            outline.precomputeOutline = true;
            //Debug.Log("Precomputed Outlines");

            outline.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer is still resetting or not.
        if (resettingTimer)
        {
            audioTimer -= Time.deltaTime;
            
            if (audioTimer < 0f)
            {
                resettingTimer = false;
            }
        }

        // Check if the pointer is pointing at highlightable object and check if it's pointing at nothing
        hit = pointer.pointerRenderer.GetDestinationHit();

        //Debug.Log("hit = " + hit.transform.name);
        if (hit.transform != null)
        {
            if (hit.transform.tag == "CanHighlight")
            {
                PlayHighlightedWord();

                //Debug.Log("hit CanHighlight = " + hit.transform.name);
                GameObject nextHighlight = hit.transform.gameObject;
                if (currentHighlight != null && currentHighlight != nextHighlight)
                {
                    // Unhighlight last object
                    Outline oldOutline = currentHighlight.GetComponent<Outline>();
                    oldOutline.enabled = false;

                   //// Get the tooltip canvas and make the word disappear of the last object
                   //GameObject canvas = currentHighlight.transform.Find("ObjectTooltip/TooltipCanvas").gameObject;

                   // if (canvas != null)
                   // {
                   //     canvas.SetActive(false);
                   // }

                    DeactivateCanvas();
                }
                currentHighlight = nextHighlight;
                Outline outline = currentHighlight.GetComponent<Outline>();
                if (!outline.enabled)
                {
                    outline.enabled = true;
                    //GameObject canvas = currentHighlight.transform.Find("ObjectTooltip/TooltipCanvas").gameObject;
                    

                    //if (canvas != null)
                    //{
                    //    canvas.SetActive(true);
                    //}

                    SetObjectWord(currentHighlight, "FR");
                }

            }
            else
            {
                if (currentHighlight.GetComponent<Outline>())
                {
                    Outline oldOutline = currentHighlight.GetComponent<Outline>();
                    oldOutline.enabled = false;
                }

                //GameObject canvas = currentHighlight.transform.Find("ObjectTooltip/TooltipCanvas").gameObject;

                //if (canvas != null)
                //{
                //    canvas.SetActive(false);
                //}

                DeactivateCanvas();

                currentHighlight = null;
            }
        }
    }

    private void DeactivateCanvas()
    {
        // Deactivate the right hand anchor's ObjectTooltip canvas
        GameObject canvas = rightHandAnchor.transform.Find("ObjectTooltip/TooltipCanvas").gameObject;

        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        else
        {
            Debug.Log("DeactiveCanvas, canvas was null");
        }
    }

    private void SetObjectWord(GameObject wordObject, string language)
    {
        // Get word from the object that was hit based on the language
        curVocManager = wordObject.GetComponent<VocabManager>();

        currentWord = curVocManager.vocabMap[VocabManager.language];
        Debug.Log("vocabManager.vocabMap" + curVocManager.vocabMap);

        // Set word to the right hand anchor's ObjectTooltip canvas UIs
        GameObject canvas = rightHandAnchor.transform.Find("ObjectTooltip/TooltipCanvas").gameObject;

        if (canvas != null)
        {
            canvas.transform.Find("UITextFront").gameObject.GetComponent<Text>().text = currentWord;
            canvas.transform.Find("UITextReverse").gameObject.GetComponent<Text>().text = currentWord;
        }
        else
        {
            canvas.transform.Find("UITextFront").gameObject.GetComponent<Text>().text = "Error";
            canvas.transform.Find("UITextReverse").gameObject.GetComponent<Text>().text = "Error";
        }

        // Then display the canvas
        if (canvas != null)
        {
            canvas.SetActive(true);

            // For now
            //vocabManager.PlayWord();
        }
    }

    private void PlayHighlightedWord()
    {
        // Check for the B button being pressed, if the timer is still being reset, ignore
        if (OVRInput.GetUp(OVRInput.Button.Two) && !resettingTimer)
        {
            // Pause the music, play the word, and restart the music 
            MainManager.music.Pause();
            curVocManager.PlayWord();
            resettingTimer = true;
            audioTimer = TIME_LEFT;
        }
    }
}






/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class OutlineSetup : MonoBehaviour
{
    [Tooltip("Drag in the VRTK_Pointer component which is either on this object or another. Sets up highlighting with the pointer.")]
    public VRTK_Pointer pointer;
    private RaycastHit hit;

    private GameObject currentHighlight = null;

    private void Awake()
    {

        var highlightableObjects = GameObject.FindGameObjectsWithTag("CanHighlight");

        // Set up the Outline component for all objects in the scene that can be highlighted, then disable the outline for now
        foreach (GameObject obj in highlightableObjects)
        {
            obj.AddComponent<Outline>();
            Outline outline = obj.GetComponent<Outline>();
            outline.enabled = false;

            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.cyan;
            outline.OutlineWidth = 3f;
            outline.precomputeOutline = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the pointer is pointing at highlightable object and check if it's pointing at nothing
        hit = pointer.pointerRenderer.GetDestinationHit();
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "CanHighlight")
            {
                GameObject nextHighlight = hit.collider.gameObject;
                if (currentHighlight != null && currentHighlight != nextHighlight)
                {
                    Outline oldOutline = currentHighlight.GetComponent<Outline>();
                    oldOutline.enabled = false;
                    GameObject canvas = currentHighlight.transform.Find("ObjectTooltip/TooltipCanvas").gameObject;

                    if (canvas != null)
                    {
                        canvas.SetActive(false);
                    }
                }
                currentHighlight = nextHighlight;
                Outline outline = currentHighlight.GetComponent<Outline>();
                if (!outline.enabled)
                {
                    outline.enabled = true;
                    GameObject canvas = currentHighlight.transform.Find("ObjectTooltip/TooltipCanvas").gameObject;

                    if (canvas != null)
                    {
                        canvas.SetActive(true);
                    }

                }

            }
            else
            {
                Outline oldOutline = currentHighlight.GetComponent<Outline>();
                oldOutline.enabled = false;
                GameObject canvas = currentHighlight.transform.Find("ObjectTooltip/TooltipCanvas").gameObject;

                if (canvas != null)
                {
                    canvas.SetActive(false);
                }

                currentHighlight = null;
            }
        }
    }
}

*/
