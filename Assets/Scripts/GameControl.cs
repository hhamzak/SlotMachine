using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };
    public static event Action<int> CoinEarned = delegate { };
    public static event Action<int> PlaySound = delegate { };

    [SerializeField]
    private Text prizeText;
    [SerializeField]
    private Text totalText;
    [SerializeField]
    private Text betText; 

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;
    [SerializeField]
    private GameObject GameOverUI;
    [SerializeField]
    private GameObject Coin;

    public static int totalCoin;

    public static int betCount = 0;


    private int prizeValue;
    private bool resultsChecked = false;

    AudioSource pullHandleSound;

    private void Start()
    {
        Time.timeScale = 1;
        pullHandleSound = handle.GetComponent<AudioSource>();
        totalCoin = 1000;
        betCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Last Prize: " + prizeValue;
            betCount = 0;
            if (totalCoin <= 0)
            {
                Debug.Log(String.Format("Game Over"));
                Time.timeScale = 0;
                GameOverUI.SetActive(true);
                //GetComponent<GameObject>().SetActive(false);
            }
        }
        totalText.text = "Total: " + totalCoin;
        betText.text = "Bet: x" + betCount;
    }

    private void OnMouseDown()
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            if (Time.timeScale != 0)
            {
                StartCoroutine("PullHandle");
                pullHandleSound.PlayOneShot(pullHandleSound.clip);
            }
        }
    }

    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        if (betCount > 0)
        {
            HandlePulled();
        }


        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CheckResults()
    {
        if (rows[0].stoppedSlot >= 0 && rows[1].stoppedSlot >= 0 && rows[2].stoppedSlot >= 0)
        {
            if (rows[0].stoppedSlot == rows[1].stoppedSlot &&
            rows[1].stoppedSlot == rows[2].stoppedSlot &&
            rows[0].stoppedSlot == rows[2].stoppedSlot
            && rows[0].stoppedSlot != -1)
            {
                prizeValue = 1000 * betCount;
                StartCoroutine(PlayEffects(5));
                //StartCoroutine("CoinEarn");
            }
            else if (rows[0].stoppedSlot == rows[1].stoppedSlot ||
                rows[1].stoppedSlot == rows[2].stoppedSlot ||
                rows[0].stoppedSlot == rows[2].stoppedSlot)
            {
                StartCoroutine(PlayEffects(5));
                prizeValue = 500 * betCount;
            }
            else
            {
                PlaySound(1);
            }
            totalCoin += prizeValue;
        }
        
        resultsChecked = true;
    }

    private IEnumerator PlayEffects(int count)
    {
        PlaySound(0);
        yield return new WaitForSeconds(1f);
        CoinEarned(count);
    }
}
