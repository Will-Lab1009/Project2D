using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLostLeanTween : MonoBehaviour
{
    [SerializeField] private GameObject mainText;
    [SerializeField] private GameObject mainTextShadow;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject diamond;
    [SerializeField] private GameObject playText;
    [SerializeField] private GameObject playImg;
    [SerializeField] private GameObject returnText;
    [SerializeField] private GameObject gargoyleLeft;
    [SerializeField] private GameObject gargoyleRight;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(mainText, new Vector3(4.85f, 4.85f, 4.85f), 2f).setEase(LeanTweenType.easeOutElastic).setDelay(0.5f);
        LeanTween.scale(mainTextShadow, new Vector3(4.85f, 4.85f, 4.85f), 2f).setEase(LeanTweenType.easeOutElastic).setDelay(0.5f);

        LeanTween.scale(scoreText, new Vector3(2.12f, 2.12f, 2.12f), 1f).setEase(LeanTweenType.easeInExpo).setDelay(0.5f);
        LeanTween.scale(diamond, new Vector3(3.15f, 3.15f, 3.15f), 1f).setEase(LeanTweenType.easeInExpo).setDelay(0.5f);

        LeanTween.moveLocal(playText, new Vector3(-47, -218, 0), 1f).setEase(LeanTweenType.easeInOutCubic).setDelay(0.5f);
        LeanTween.moveLocal(playImg, new Vector3(256, -228, 0), 1f).setEase(LeanTweenType.easeInOutCubic).setDelay(0.5f);

        LeanTween.moveLocal(returnText, new Vector3(0, -398, 0), 1.5f).setEase(LeanTweenType.easeInOutCubic).setDelay(0.5f);

        LeanTween.moveLocal(gargoyleLeft, new Vector3(-598, -292, -9720), 2f).setEase(LeanTweenType.easeInOutCubic).setDelay(0.5f);
        LeanTween.moveLocal(gargoyleRight, new Vector3(598, -292, -9720), 2f).setEase(LeanTweenType.easeInOutCubic).setDelay(0.5f);
    }
}
