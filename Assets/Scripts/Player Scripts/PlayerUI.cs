using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    private bool passiveHealing = false;
    private float healTime = 3f;
    public float currentScore;
    public float highscore;
    public float maxHealth = 50f;
    public float chipSpeed = 2f;
    public Image frontHeathBar;
    public Image backHealthBar;

    [SerializeField]
    private TMP_Text scoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetFloat("highscore");
        currentScore = 0f;
        health = maxHealth;
    }

    public void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHeathUI();
        UpdateScore();
        if (currentScore > highscore)
        {
            highscore = currentScore;
            PlayerPrefs.SetFloat("highscore", currentScore);
        }
        if (passiveHealing == true)
        {
            healTime -= Time.deltaTime;
            if (healTime <= 0)
            {
                Debug.Log("Healed");
                PassiveHealing();
                healTime = 3f;
            }
        }
    }

    public void UpdateHeathUI()
    {
        float FillF = frontHeathBar.fillAmount;
        float FillB = backHealthBar.fillAmount;
        float HFraction = health / maxHealth;
        if(FillB > HFraction)
        {
            frontHeathBar.fillAmount = HFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(FillB, HFraction, percentComplete);
        }
        if (FillF < HFraction)
        {
            backHealthBar.color = Color.cyan;
            backHealthBar.fillAmount = HFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHeathBar.fillAmount = Mathf.Lerp(FillF, HFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        if (health <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void UpdateScore()
    {
        if (currentScore <= 0)
        {
            scoreCounter.text = "";
        }
        else if (currentScore > 0)
        {
            scoreCounter.text = "Current score: " + currentScore.ToString() + " // High score:" + highscore.ToString();
        }

    }
    public void PassiveHealing()
    {
        passiveHealing = true;
        health += Random.Range(2, 5);
        lerpTimer = 0f;
    }
}
