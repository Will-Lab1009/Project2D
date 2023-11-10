using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ItemCollection : MonoBehaviour
{
    private int currentScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource diamondCollectAudio;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "00000";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = currentScore.ToString("D5");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            diamondCollectAudio.Play();
            Destroy(collision.gameObject);
            currentScore++;
        }
    }
}
