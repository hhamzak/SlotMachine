using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButton : MonoBehaviour
{
    [SerializeField]
    private Text totalText;
    [SerializeField]
    private Text betText;

    private AudioSource coinInsertSound;

    private void Start()
    {
        coinInsertSound = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (GameControl.totalCoin >= 250)
        {
            GameControl.totalCoin -= 250;
            totalText.text = "Total: " + GameControl.totalCoin;
            coinInsertSound.PlayOneShot(coinInsertSound.clip);
            GameControl.betCount++;
        }
    }
}
