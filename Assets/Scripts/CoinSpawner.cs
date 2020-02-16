using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Coin;

    // Start is called before the first frame update
    void Start()
    {
        GameControl.CoinEarned += StartCoinDrop;
        Debug.Log("Coin spawner tetiklendi");
    }

    private void StartCoinDrop(int count)
    {
        StartCoroutine(CoinDrop(count));
    }

    private IEnumerator CoinDrop(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(Coin, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }
    
    private void OnDestroy()
    {
        GameControl.CoinEarned -= StartCoinDrop;
    }
}
