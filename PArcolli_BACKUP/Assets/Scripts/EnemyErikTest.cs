using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyErikTest :MonoBehaviour
{
   // GameObject playerCollision;
   // IEnumerator trashTimer;
    [SerializeField]GameObject littleTrash;
    Vector3 fromPlayerOffset;
    [SerializeField] GameObject bigTrash;
    

    

    GameObject trashInstance;
    

    Coroutine myCoroutine = null;
    

    bool isEntered = false;
   [HideInInspector] static public bool hasHitTrash = false;

    private void Start()
    {
        
        
    }

    private void Update()
    {
        fromPlayerOffset = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 2);
       
    }
    private void OnTriggerEnter(Collider other)
    {
        

       if(other.gameObject.tag=="Player")
       {
            Debug.Log("I'm IN");
            // fai partire il timer


            // istanzia la spazzatura piccola
           // GameObject trashInstance;

            if (isEntered == false)
            {
                trashInstance = Instantiate(littleTrash, fromPlayerOffset, littleTrash.transform.rotation);

                StartCoroutine(TrashInstantiate());

                isEntered = true;
            } 

            if (myCoroutine !=null)
            {
                // stoppa la coroutine 
                // falla partire
                StopCoroutine(myCoroutine);
                myCoroutine = StartCoroutine(TrashInstantiate());
               
            }

            hasHitTrash = false;

           

        }
    }
    public IEnumerator TrashInstantiate() // deve partire quando entro nel trigger, deve fermarsi quando caccio il nemico
    {
        int maxTime = 10;


        for (int i = maxTime; i > 0; i--)
        {
            Debug.Log($"remaining time before big trash: {maxTime}");
            yield return new WaitForSeconds(1f);

            maxTime--;
        }

        if (maxTime == 0) // se non si è ancora fermato il timer allora istanzia la spazzatura grande
        {
            Destroy(trashInstance);
            Vector3 bigTrashOffset = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z + 3);
            Instantiate(bigTrash, bigTrashOffset, bigTrash.transform.rotation);
            Debug.Log("Ferma la coroutine e falla ripartire");

            GameManager.ModifyPoints(-10);
            Destroy(gameObject);


        }


    }
}
