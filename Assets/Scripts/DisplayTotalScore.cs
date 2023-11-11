using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTotalScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        int collectedDiamonds = PlayerPrefs.GetInt("CollectedDiamonds", 0);
        int totalDiamonds = PlayerPrefs.GetInt("TotalDiamonds", 0);

        totalScoreText.text = collectedDiamonds.ToString("D2") + " / " + totalDiamonds.ToString("D2");
    }
}
