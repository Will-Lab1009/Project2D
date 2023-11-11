using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLostLeanTween : MonoBehaviour
{
    [SerializeField] private GameObject mainText;
    [SerializeField] private GameObject mainTextShadow;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(mainText, new Vector3(1, 1, 1), 1).setEase(LeanTweenType.easeInExpo).setDelay(1);
        LeanTween.scale(mainTextShadow, new Vector3(1, 1, 1), 1).setEase(LeanTweenType.easeInExpo).setDelay(1);
    }
}
