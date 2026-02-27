using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject m_startPlatform;
    [SerializeField] private GameObject m_emptyPlatform;
    [SerializeField] private GameObject[] easyPlatforms, mediumPlatforms, hardPlatforms;

    const float m_platformLength = 50;
    const float m_platformOffset = 0;
    //Lower = more spawns; 1 = 100% spawn chance
    public int m_obsticalSpawnChance = 10;

    private List<GameObject> m_platforms = new List<GameObject>();
    private GameObject m_currentPlatform;

    private int _platCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_currentPlatform = m_startPlatform;
        foreach (GameObject plat in easyPlatforms) {m_platforms.Add(plat);}
        
    }

    // Update is called once per frame
    void Update()
    {
        print(m_platforms.Count);
        if (m_currentPlatform.transform.position.z <= m_platformOffset) { SpawnPlatform(); }
    }

    private void SpawnPlatform()
    {
        Vector3 position = new Vector3(0, 0, m_platformLength + m_platformOffset);
        Transform parent = GameObject.Find("Environment").transform.GetChild(0).transform;
        GameObject platformToSpawn;

        if(_platCounter == 5)
        {
            platformToSpawn = m_emptyPlatform;            
            _platCounter = 0;
        }
        else   // random platform from the platforms list
            platformToSpawn = m_platforms[Random.Range(0, m_platforms.Count - 1)];


        m_currentPlatform = Instantiate(platformToSpawn, position, Quaternion.identity, parent);
        _platCounter++;
    }
}
