using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageSelection : MonoBehaviour
{
    public static int NUM_SLIDES = 3;
    public static int currentSlide = 0;
    public Transform flag;
    public Transform language;
    //public Transform leftButton;
    public Texture[] slides = new Texture[NUM_SLIDES];
    public string[] languages = new string[NUM_SLIDES];
    private Renderer m_renderer;
    private TMP_Text m_text;
    public static bool changeFlag = false;

    void Start()
    {
        m_renderer = flag.GetComponent<Renderer>();
        m_text = language.GetComponent<TMP_Text>();
        ChangeFlag();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeFlag)
        {
            ChangeFlag();
        }
    }

    void ChangeFlag()
    {
        m_renderer.material.SetTexture("_MainTex", slides[currentSlide]);
        m_text.text = languages[currentSlide];
        changeFlag = false;
    }
}
