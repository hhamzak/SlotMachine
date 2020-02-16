using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetMaxButton : MonoBehaviour
{
    private AudioSource coinInsertSound;
    // Start is called before the first frame update
    void Start()
    {
        coinInsertSound = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (GameControl.totalCoin >= 250)
        {
            coinInsertSound.PlayOneShot(coinInsertSound.clip);
            int count = GameControl.totalCoin / 250;
            GameControl.betCount = GameControl.betCount + count;
            GameControl.totalCoin -= count * 250;
        }
    }
}
