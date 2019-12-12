using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocabManager : MonoBehaviour
{
    public static int num_languages  = 3;
    public static string[] languages = new string[num_languages];

    [Tooltip("Current order of languages: EN, FR, JP")]
    public string[] vocab = new string[num_languages];
    public Dictionary<string, string> vocabMap = new Dictionary<string, string>();

    public AudioClip[] wordAudio = new AudioClip[num_languages];
    public Dictionary<string, AudioClip> audioMap = new Dictionary<string, AudioClip>();

    public static int languageID = 2; // Debugging
    public static string language = "EN";

    private AudioSource src;

    // Start is called before the first frame update
    void Awake()
    {
        src = GetComponent<AudioSource>();
        //src.playOnAwake = false;

        //if (this.name == "Sink")
        //{
        //    string word = "";
        //    foreach (AudioClip voc in wordAudio)
        //    {
        //        word += voc.name + ", ";
        //    }
        //    Debug.Log("src: " + src.name);
        //    Debug.Log("wordAudio: " + word);
        //}


        languages[0] = "EN";
        languages[1] = "FR";
        languages[2] = "JP";

        languageID = PlayerPrefs.GetInt("languageID");
        language = languages[languageID];

        //string word = "";
        //foreach (string voc in vocab) {
        //    word += voc + ", ";
        //}
        //Debug.Log("Word: " + word);
        LoadMaps();
    }

    private void Update()
    {

    }

    private void LoadMaps()
    {
        for (int i = 0; i < num_languages; ++i)
        {
            if (vocab[i] == null)
            {
                vocabMap.Add(languages[i], languages[i]);
            }
            else
            {
                vocabMap.Add(languages[i], vocab[i]);
            }
        }

        for (int i = 0; i < num_languages; ++i)
        {
            if (wordAudio[i] != null)
            {
                Debug.Log(this + "'s audio loaded for " + languages[i]);
                audioMap.Add(languages[i], wordAudio[i]);
            }
            else
            {
                Debug.Log(this + "'s audio did not exist for " + languages[i]);
                audioMap.Add(languages[i], null);
            }
        }

        string debug = "";
        foreach (string wod in audioMap.Keys)
        {
            debug += "(" + wod + ", " + audioMap[wod] + ") ";
        }
        Debug.Log("audioMap is " + debug);
    }

    public void PlayWord()
    {
        //Debug.Log("Playword() from " + this.name);
        //Debug.Log("audioMap in Playword is " + audioMap[language]);
            
        if (audioMap[language] != null)
        {
            //Debug.Log(this.name + "'s audioMap is" + audioMap["EN"].name);
            //Debug.Log("Playing audio now");
            src.PlayOneShot(audioMap[language]);
            StartCoroutine(PlayResetAudio());
        }
        else
        {
            Debug.Log(this.name + "'s audioMap was empty");
        }
    }

    IEnumerator PlayResetAudio()
    {
        yield return new WaitForSeconds(audioMap[language].length);
        MainManager.music.Play();
    }
}
