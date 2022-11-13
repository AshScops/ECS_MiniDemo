using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text scoreText;
    public GameObject resultUI;

    private void OnEnable()
    {
        EventManager.instance.updateHealthEvent.AddListener(UpdateHealth);
        EventManager.instance.updateScoreEvent.AddListener(UpdateScore);
        EventManager.instance.showResultEvent.AddListener(ShowResult);
    }

    private void OnDisable()
    {
        EventManager.instance.updateHealthEvent.RemoveListener(UpdateHealth);
        EventManager.instance.updateScoreEvent.RemoveListener(UpdateScore);
        EventManager.instance.showResultEvent.RemoveListener(ShowResult);
    }

    private void UpdateHealth(int health)
    {
        Debug.Log("UpdateHealth");
        healthText.text = "Health : " + health;
    }

    private void UpdateScore(int score)
    {
        Debug.Log("UpdateScore");
        scoreText.text = "Score : " + score;
    }

    private void ShowResult(int score)
    {
        Debug.Log("ShowResult");
        resultUI.SetActive(true);
        resultUI.GetComponent<Text>().text = "Score : " + score;
    }
}
