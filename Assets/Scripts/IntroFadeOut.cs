using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroFadeOut : MonoBehaviour
{
    float time;
    public float _fadeTIme = 1f;
    public Image image;
    public GameManager gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IntroEndCheckNum == 1)
        {
            if (time < _fadeTIme)
            {
                image.color = new Color(1, 1, 1, 1f - time / _fadeTIme);
            }
            else
            {
                time = 0;
                image.gameObject.SetActive(false);
            }
            time += Time.deltaTime;
        }
    }
}
