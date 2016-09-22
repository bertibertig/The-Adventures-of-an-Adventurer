using UnityEngine;
using System.Collections;

public class DropLoot : MonoBehaviour {

    private GameObject enemy;
    private GameObject item;
    private GameObject goldCoin;
    private float amountOfCoinsToDrop;

	// Use this for initialization
	void Start () {
        amountOfCoinsToDrop = 0;
        //goldCoin = GameObject.FindGameObjectWithTag("GoldCoin");
        goldCoin = Resources.Load("Items/Other/GoldCoin", typeof(GameObject)) as GameObject;
	}

    public void EnemyDropItem(GameObject _enemy,GameObject item)
    {

    }

    public void EnemyDropGold(GameObject _enemy, float minCoins, float maxCoins)
    {
        enemy = _enemy;
        amountOfCoinsToDrop = Random.Range(minCoins, maxCoins);
        StartCoroutine("DropCoins");
    }

    public IEnumerator DropCoins()
    {
        Vector3 currentPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
        for (int i = 0; i < amountOfCoinsToDrop; i++)
        {
            GameObject tmpObj = GameObject.Instantiate(goldCoin, currentPosition, Quaternion.identity) as GameObject;
            currentPosition += new Vector3(Random.value * 0.1f, Mathf.Abs(1f * Random.value), 0f);
            yield return null;
        }
    }
}
