using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public Player player;
    public Timer timer;

    // Update is called once per frame
    void Update()
    {
        if (timer.over)
        {
            Show();
        }
    }

    public void Show()
    {
        var result = player.TotalBlockDigged();
        foreach (var compText in GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            compText.enabled = true;
            if (compText.gameObject.name == "Score")
            {
                compText.text = player.score.ToString();
            }
            if (compText.gameObject.name == "Total Digged")
            {
                compText.text = player.TotalBlockDigged().ToString();
            }
        }

        foreach (var image in GetComponentsInChildren<Image>())
        {
            image.enabled = true;
        }
    }

    public void Hide()
    {
        foreach (var compText in GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            compText.enabled = false;
        }

        foreach (var image in GetComponentsInChildren<Image>())
        {
            image.enabled = false;
        }
    }
}
