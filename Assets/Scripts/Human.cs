using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Human : MonoBehaviour
{
    
    public int movementSpeed;
    private Vector3 target;
    GameObject humanInstance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TargetSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Debug.Log("rör sig mot" + target);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //50/50 risk att smittas
        if (gameObject.tag == "Human" && humanInstance.tag == "Sick")
        {
            int Dice = Random.Range(0, 100);
            if (Dice < 49)
            {
                gameObject.tag = "Sick";
            }

        }
    }

    //rörelse mot random position, byt pos efter x antal sek
    IEnumerator TargetSpawner()
    {
         while (true)
         {
             if (gameObject != null)
             {
                 target = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
                 yield return new WaitForSeconds(5);
             }
             yield return null;
         }
    }
}
