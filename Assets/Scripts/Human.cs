using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.UIElements.ToolbarMenu;
using static UnityEngine.GraphicsBuffer;

public class Human : MonoBehaviour
{
    
    public float movementSpeed;
    private Vector3 target;
    GameObject humanInstance;
    public GameObject Manager;
    bool isIdle = false;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine(Idle());
        StartCoroutine(Movement());
        Manager = GameObject.Find("Manager");
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
    }

    //rörelse mot random position, byt pos efter X sek.
    IEnumerator Movement()
    {
        while (gameObject != null)
        {
            if (isIdle == false)
            {
                target = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
                movementSpeed = 1f;
 
                //efter tiden gått ut, bli idle
                yield return new WaitForSeconds(5);
                //target = transform.position;
                isIdle = true;
            }
    
            else
            {
                movementSpeed = 0f;
                yield return new WaitForSeconds(2);

                isIdle = false;
            }
            yield return null;
        }

    }
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        Infektionsrisk vid kollision med sjuk -> Rull 0 - 99:
        0 - 40 Sjuk
        Annars: Inget händer
        */
        if (gameObject.tag == "Healthy")
        {
            //VIT
           spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            int Dice = Random.Range(0, 100);
            if (Dice < 41)
            {
                gameObject.tag = "Sick";
                movementSpeed = 0.5f;
                //RÖD
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
                StartCoroutine(SicknessCounter());

            }

        }

        /*
        Timer:
        När man varit sjuk en stund - konsekvenser -> Rull 0-99:
        0-10 Död/dead
        11-50 Frisk/healthy
        51-99 Immun/immune
        */
        IEnumerator SicknessCounter()
    {
         //räkna inte counter, ändra bara tag

            yield return new WaitForSeconds(2);
            int Dice = Random.Range(0, 100);
            //0 - 10 Död / dead
            
            if (Dice < 11)
            {
                gameObject.tag = "Dead";
                movementSpeed = 0f;
                //SVART
                spriteRenderer.color = new Color(0f, 0f, 0f, 1f);

            }

            //11 - 50 Frisk / healthy
            if (Dice > 10 && Dice < 51)
            {
                gameObject.tag = "Healthy";
                //VIT
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            }
            //51 - 99 Immun / immune
            if (Dice > 50)
            {
                gameObject.tag = "Immune";
                //GUL
                spriteRenderer.color = new Color(1f, 1f, 0f, 1f);

            }

        }
     

    }

}


