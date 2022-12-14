using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public double remaingTime, freezingTimer, slowRate;
    public double baseTime = 5;
    public bool over = false;
    public bool freezed = false;
    public bool slowed = false;
    public Player player;
    public Image timeBar;

    void Start()
    {
        Reset();
    }

    void Update()
    {

        if (remaingTime < 0)
        {
            Debug.Log(remaingTime);
            this.GameOver();
            Debug.Log("GAME OVER");
        }

        if (!freezed && !over)
        {
            remaingTime -= Time.deltaTime / slowRate;
        }

        UpdateFreeze();

        timeBar.fillAmount = (float)(remaingTime / baseTime);
    }

    public void Reset()
    {
        remaingTime = baseTime;
        over = freezed = slowed = false;
        slowRate = 1.0;

        timeBar = GetComponent<Image>();
    }

    public void GameOver()
    {

        remaingTime = 0;
        over = true;

        player.Stop(true);
    }

    public void Freeze(double freezingTimer)
    {
        this.freezingTimer = freezingTimer;
        freezed = true;
    }

    public void SetSlowRate(double slowRate)
    {
        this.slowRate = slowRate;
        this.slowed = slowRate > 1.0 ? true : false;
    }

    public void UpdateFreeze()
    {
        if (freezed == false) return;

        freezingTimer -= Time.deltaTime;
        if (freezingTimer < 0)
        {

            freezingTimer = 0;
            freezed = false;
        }

        // BUG INTERAZIONE FREZZE + SLOW TIME CON GTA BLOCK-
    }
}
