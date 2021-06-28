using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pollonPrefab;
    [SerializeField] private float pollonSpawnRate = 1.0f;
    [SerializeField] private float pollonTTL = 10.0f;
    [SerializeField] private GameObject troutPrefab;
    [SerializeField] private float troutSpawnRate = 1.0f;
    [SerializeField] private float troutTTL = 10.0f;
    [SerializeField] private GameObject salmonPrefab;
    [SerializeField] private float salmonSpawnRate = 1.0f;
    [SerializeField] private float salmonTTL = 10.0f;
    [SerializeField] private Transform fishParent;

    private List<GameObject> fish = new List<GameObject>();


    public void SpawnPollon()
    {
        StartCoroutine(Spawn(pollonPrefab, pollonSpawnRate, pollonTTL));
    }

    public void SpawnTrout()
    {
        StartCoroutine(Spawn(troutPrefab, troutSpawnRate, troutTTL));
    }
    public void SpawnSalmon()
    {
        StartCoroutine(Spawn(salmonPrefab, salmonSpawnRate, salmonTTL));
    }

    private IEnumerator Spawn(GameObject prefab, float spawnRate, float TTL)
    {
        Debug.Log("Coroutine Started");
        while (true)
        {
            fish.RemoveAll(item => item == null);
           // Debug.Log("Loop Started");
            if (fish.Count < 7)
            {
                //Debug.Log("Fish Started");
                float mod = Random.Range(0.8f, 1.2f);
                transform.position = GenerateCoords();
                fish.Add(Instantiate(prefab,transform));
                fish[fish.Count - 1].transform.parent = fishParent;
                fish[fish.Count - 1].GetComponent<Fish>().DestroyFish(TTL * mod);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private Vector2 GenerateCoords()
    {
        return new Vector2(Random.Range(-1.0f, 5.5f), Random.Range(0.0f, 2.4f));
    }
}
