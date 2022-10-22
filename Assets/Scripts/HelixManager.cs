
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] helixRings;
    public float ySpawn = 0;
    public float ringsDistance = 5;
    public int numberOfRings = 7;
  
    // Start is called before the first frame update
    void Start()
    {
        //SpawnRing helix rings
        for( int i = 0; i < numberOfRings; i++)
        {
            if (i == 0)
                SpawnRing(0);
            else
            SpawnRing(Random.Range(1, helixRings.Length - 1));
        }
        //spawn last ring
        SpawnRing(helixRings.Length - 1);
    }

   public void SpawnRing(int ringIndex)
    {
       GameObject go =  Instantiate(helixRings[ringIndex],transform.up*ySpawn,Quaternion.identity);
        go.transform.parent = transform;
        ySpawn -= ringsDistance;
    }
}
