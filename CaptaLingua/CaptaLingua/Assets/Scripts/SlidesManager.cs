using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidesManager : MonoBehaviour
{
    private const int NUM_SLIDES= 6;
    private int currentSlide = 0;
    public float slideIntervalTime = 5.0f;
    public Texture[] slides = new Texture[NUM_SLIDES];
    private Renderer m_renderer;
    private float timeSinceLast = 1.0f;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_renderer.material.SetTexture("_MainTex", slides[currentSlide]);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLast > slideIntervalTime && currentSlide < slides.Length)
        {
            m_renderer.material.SetTexture("_MainTex", slides[currentSlide]);
            timeSinceLast = 0.0f;
            currentSlide = (currentSlide + 1) % slides.Length;
        }
        timeSinceLast += Time.deltaTime;
    }
}
