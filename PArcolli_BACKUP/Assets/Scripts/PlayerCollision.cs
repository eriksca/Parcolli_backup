using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //CharacterController playerController;
    [SerializeField] GameObject scrollObject;
    [SerializeField] GameObject powerupText;
    bool hasCollided = false;
    bool hittedNpc;
    public static bool isFiore = false;
   
    private void Awake()
    {
        gameObject.GetComponent<CharacterController>();

    }

    private void FixedUpdate() {
        hittedNpc = true;
    }
   

    private void OnControllerColliderHit(ControllerColliderHit hit) // aggiungere collisioni con oggetti power up che richiamano un metodo che aumenta il tempo a disposizione.
    {
        if (hit.gameObject.tag == "Trash" && !EnemyErikTest.hasHitTrash || hit.gameObject.tag=="Trash")
        {
            TimeManager.currCountdownValue += 5f;
            // distrugge la spazzatura
            Destroy(hit.gameObject);
            // evento oppure richiama un metodo di game managwer che incrementa la variabile punteggio
            GameManager.ModifyPoints(+15);
            EnemyErikTest.hasHitTrash = true;

        }

        if (hit.gameObject.tag == "Enemy")
        {
            // spingilo via 

            var magnitude = 5000;
            var force = transform.position - hit.transform.position;

            force.Normalize();

            hit.gameObject.GetComponent<Rigidbody>().AddForce(-force * magnitude);
            hit.collider.enabled = false;

            Destroy(hit.gameObject,0.5f); // distruggi l'enemy dopo averlo sparato via --> se  distruggo la corooutine si ferma
            
            // istanzia la pergamena
            if(GameManager.collectedScrolls<9) // se ho già 9 pergamene e continuo a prendere
            {
                StartCoroutine(ScrollsInstance());
                StopCoroutine(ScrollsInstance());
            }
           
            hasCollided = false; // to fix collision detection
            
            // ferma il timer che era partito nell on trigger enter dell'enemy holder --> dal trigger enter avvia un metodo del time manager 
        }
    
        if (hit.gameObject.tag == "Scroll" && !hasCollided ) // se io elimino più di un enemy, has collided non ritorna false e quindi posso prendere la pergamena solo una volta
        {
            
            //distruggila e incrementa la variabile pergamene raccolte che determina quanti bottoni/sprite mostrare quando apro il panel

            Destroy(hit.gameObject);
            GameManager.ModifyCollectedScrolls();
            hasCollided = true;
           
        }
        if(hasCollided && hit.gameObject.tag=="Scroll")
        {
            hasCollided = false;
        }

        if(hit.gameObject.tag=="Npc")
        {
            if(hittedNpc == true){
                GameManager.ModifyPoints(-5);
                var magnitude = 700;
                var force = transform.position - hit.transform.position;

                force.Normalize();


                hit.gameObject.GetComponent<Rigidbody>().AddForce(-force * magnitude);

                hittedNpc = false;
            }
            
        }
       
        // -------------------------------------------------------------------------------------------------------------------------
        if(hit.gameObject.tag=="PowerUp1")
        {
            // power up che aumenta curr countdown value (es + 10 sec o 5 s) 
            Destroy(hit.gameObject);
            TimeManager.currCountdownValue += 10f;
            isFiore = true;
            PowerupText(hit.transform, "+ 10 SECONDI");
        }

        if(hit.gameObject.tag=="PowerUp2")
        {
            // power up che da doppi punti --> cambia il colore del testo points per tutto il periodo in cui ho i doppi punti
        }

       //-----------------------------------------------------------------------------------------------------------------------------------
    }

    IEnumerator ScrollsInstance() // puo anche essere convertito come metodo generale per spawnare oggetti dopo un tot di secondi --> mettere come parametro un game objecte un float
    {
        
        Vector3 scrollOffset = new Vector3(transform.position.x + 2, transform.position.y+2, transform.position.z + 2);
        yield return new WaitForSeconds(1f);
        GameObject scrollInstance = Instantiate(scrollObject, scrollOffset, scrollObject.transform.rotation);
    }

    private void PowerupText(Transform p, string text)
    {
        GameObject pwt = Instantiate(powerupText, new Vector3(p.position.x, p.position.y+1, p.position.z), new Quaternion(0, 0, 0, 1));
        pwt.GetComponentInChildren<TextMesh>().text = text;
        Destroy(pwt, 4);
    }


}