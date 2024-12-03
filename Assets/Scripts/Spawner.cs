using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public int humanPopulation;
    public int maxPop = 10;
    public int healthyCounter;
    public int sickCounter;
    public int immuneCounter;
    public int deadCounter;
    public GameObject Human;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HumanSpawner());
        //StartCoroutine(Human.GetComponent<Human>().SicknessCounter()); 
        //spriteRenderer.color = new Color(1f, 1f, 1f, 1f);



    }

    // Update is called once per frame
    void Update()
    {
        healthyCounter = GameObject.FindGameObjectsWithTag("Healthy").Length;
        sickCounter = GameObject.FindGameObjectsWithTag("Sick").Length;
        immuneCounter = GameObject.FindGameObjectsWithTag("Immune").Length;
        deadCounter = GameObject.FindGameObjectsWithTag("Dead").Length;
        humanPopulation = healthyCounter + sickCounter + immuneCounter - deadCounter;


    }

    IEnumerator HumanSpawner()
    {
        while (true)
        {
            if (humanPopulation < maxPop)
            {
                GameObject humanInstance = Instantiate(Human, new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0), Quaternion.identity);
                humanInstance.tag = "Healthy";
                //VIT
                SpriteRenderer renderer = humanInstance.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f, 1f, 1f, 1f);
                

                /* 
                Infektionsrisk vid start -> Rull 0-99:
                1 - 10 Infekterad/sick
                11 - 90 Frisk/human/healthycounter
                90 - 99 Immun/immune
                */
                int Dice = Random.Range(0, 100);

                if (Dice < 11)
                {
                    humanInstance.tag = "Sick";
                    //RÖD
                    renderer.color = new Color(1f, 0f, 0f, 1f);
                    //bör starta sickness coroutine?
                    //ändra movementspeed?

                }
                if (Dice > 89)
                {
                    humanInstance.tag = "Immune";
                    //GUL
                    renderer.color = new Color(1f, 1f, 0f, 1f);

                }

                if (humanPopulation == maxPop)
                {
                    Debug.Log("nu stannar spawn");
                    break;

                }

                yield return new WaitForSeconds(0.1f);
            }
            
            yield return null;
        }
    }

    }


