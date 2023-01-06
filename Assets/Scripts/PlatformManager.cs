using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _platformPrefab;
    private float _spawnOnz = 15;
    private float platformLength = 50;

    private List<GameObject> activePlatform = new List<GameObject>();

    [SerializeField]
    private Transform PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < _platformPrefab.Length; i++)
        {
            if(i == 0)
            {
                SpwanPlatform(0);
            }
            SpwanPlatform(Random.Range(0, _platformPrefab.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform.position.z - 50 > _spawnOnz - (_platformPrefab.Length * platformLength))
        {
            SpwanPlatform(Random.Range(0, _platformPrefab.Length));
            DeletePlatform();
        }
    }

    private void SpwanPlatform(int PlatformIndex)
    {
       GameObject go = Instantiate(_platformPrefab[PlatformIndex],transform.forward * _spawnOnz,transform.rotation);
        activePlatform.Add(go); 
       _spawnOnz += platformLength;
    }

    private void DeletePlatform()
    {
        Destroy(activePlatform[0]);
        activePlatform.RemoveAt(0);
    }
}
