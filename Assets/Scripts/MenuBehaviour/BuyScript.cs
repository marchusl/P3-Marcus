using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject buyMenu;
    PlayerInteractions instance;
    Gun gunInfluence;
    PlayerUI plyrUI;
    public GameObject buyAuto;
    public GameObject healButton;

    // Start is called before the first frame update
    // Update is called once per frame

    private void Start()
    {
        buyMenu.SetActive(false);
        instance = GameObject.Find("PlayerHolder").GetComponent<PlayerInteractions>();
        gunInfluence = GameObject.Find("PlayerHolder").GetComponent<Gun>();
        plyrUI = GameObject.Find("PlayerHolder").GetComponent<PlayerUI>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        buyMenu.SetActive(true);
        Time.timeScale = 0.1f;
        isPaused = true;
    }

    public void Resume ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        buyMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BuyAutomatic()
    {
        instance.AutomaticFire();
        Resume();
        buyAuto.SetActive(false);
    }

    public void FasterShooting()
    {
        gunInfluence.ShootFaster();
        Resume();
    }

    public void HealPassively()
    {
        plyrUI.PassiveHealing();
        healButton.SetActive(false);
        Resume();
    }
}
