using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public int stoppedSlot;

    AudioSource slotTurnSound;
    AudioSource failSound;

    // Start is called before the first frame update
    void Start()
    {
        stoppedSlot = -1;
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
        slotTurnSound = GetComponent<AudioSource>();
        failSound = GameObject.Find("SlotMachine").GetComponent<AudioSource>();
    }

    private void StartRotating()
    {
        stoppedSlot = -1;
        StartCoroutine("Rotate");
    }

    private float start = -2.65f;
    private float end = 4.35f;

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;

        for (int i = 0; i < 24; i++)
        {
            if (transform.position.y <= start)
            {
                transform.position = new Vector2(transform.position.x, end);
            }
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
            yield return new WaitForSeconds(timeInterval);
            if (i % 4 == 0)
            {
                slotTurnSound.PlayOneShot(slotTurnSound.clip);
            }
        }
        
        randomValue = UnityEngine.Random.Range(60, 100);
        
        switch (randomValue % 4)
        {
            case 1:
                randomValue += 3; ;
                break;
            case 2:
                randomValue += 2;
                break;
            case 3:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {
            if (i % 4 == 0)
            {
                slotTurnSound.PlayOneShot(slotTurnSound.clip);
            }
            if (transform.position.y <= start)
            {
                transform.position = new Vector2(transform.position.x, end);
            }
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

            if (i > Mathf.RoundToInt(randomValue * 0.95f))
            {
                timeInterval = 0.2f;
            }
            else if (i > Mathf.RoundToInt(randomValue * 0.75f))
            {
                timeInterval = 0.15f;
            }
            else if (i > Mathf.RoundToInt(randomValue * 0.5f))
            {
                timeInterval = 0.1f;
            }
            else if (i > Mathf.RoundToInt(randomValue * 0.25f))
            {
                timeInterval = 0.05f;
            }

            yield return new WaitForSeconds(timeInterval);
        }

        for (int k = 0; k < 8; k++)
        {
            if (k == 1)
            {
                if (transform.position.y == -1.65f)
                {
                    stoppedSlot = k;
                }
            }
            if (transform.position.y == (start + (k * 1f)))
            {
                stoppedSlot = k;
                if (k == 7)
                { stoppedSlot = 0; }
            }
        }

        rowStopped = true;
    }

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }

}
