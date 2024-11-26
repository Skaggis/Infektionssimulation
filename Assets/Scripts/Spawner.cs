using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int humanPopulation;
    public int sickCounter;
    public int immuneCounter;
    public int deadCounter;
    public GameObject Human;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HumanSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HumanSpawner()
    {
        while (true)
        {
            if (humanPopulation < 50)
            {
                GameObject humanInstance = Instantiate(Human, new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0), Quaternion.identity);
                humanPopulation++;
                
                int Dice = Random.Range(0, 100);
                if (Dice  < 49)
                {
                    humanInstance.tag = "Sick";
                    sickCounter++;
                } 
                Debug.Log(humanPopulation);
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }
    /*
    public void HumanDeathTracker(int humanPopulation)
    {
        humanCounter--;

        public int humanCounter;
        public int sickCounter;
        public int immuneCounter;
        public int deadCounter;
       
    }
    */
}
