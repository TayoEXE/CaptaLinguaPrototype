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

    public static int languageID = 2; // Debugging
    public static string language = "EN";

    // Start is called before the first frame update
    void Awake()
    {
        languages[0] = "EN";
        languages[1] = "FR";
        languages[2] = "JP";

        languageID = PlayerPrefs.GetInt("languageID");
        language = languages[languageID];

        LoadMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadMap()
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
    }
}
