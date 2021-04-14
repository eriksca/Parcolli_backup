using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ShowManager : MonoBehaviour
{
    

    private static ShowManager instance;
    public static ShowManager Instance;

    [SerializeField] Transform panel;
    [SerializeField] Transform layoutGroup;

    private int maxButton ; 

    
    
    // prefab fa istanziare all'apertura del pannello tab --> su pc questa fuzione � collegata alla pressione del tasto tab o dalla pressione del bottone alla destra 
    [SerializeField] GameObject scrollsImage;
    [SerializeField] Sprite image;

    private bool isOpened = false; // determina se il pannello scroll principale � aperto o meno
    private bool backPressed = false; // determina se ho premuto il tasto back gi� una volta
    private bool scrollButtonPressed = false;

    // testi che devono essere attivati
    [SerializeField] Text scrollTitle;
    [SerializeField] Text scrollText;

    [SerializeField] Button backButton;

    //  contenutodei titoli e dei testi
    List<string> testi = new List<string>();
    List<string> titoli = new List<string>();

    public static bool isPanelScrollOpen = false;



    private void Awake()
    {
        // attiva i pannelli aggiunge i testi alle liste e setta i testi attivi per poi disattivare tutto in start
        panel.gameObject.SetActive(true);
        AddTolists();
        
        TextsState(true);
    }
    void Start()
    {
        // disattiva i pannelli
       DeactivateAll();
        
    }

    void DeactivateAll()
    {
        panel.gameObject.SetActive(false);
        
        TextsState(false);
    }

    void TextsState(bool view)
    {
        scrollTitle.gameObject.SetActive(view);
        scrollText.gameObject.SetActive(view);
    }
    

    void Update()
    {
        maxButton = GameManager.collectedScrolls;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
           
            OpenPanelButton();
         
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitButton();
        
        }



    }



    public void ButtonFunction(int index) // funzione collegata alla pressione di una pergamena nel panel
    {
        backButton.gameObject.SetActive(true);
        backPressed = false; // non ho ancora premuto back
        scrollButtonPressed = true; // diventa vero nel momento in cui ho aperto una pergamena  qieste variabili mi servono per evitare che il back button replichi le pergamene gi� instanziate dopo l'uso del open button
        
        // disattiva le pergamene a vista e  attiva i testi che prendono il contenuto delle liste.
        for (int i = 0; i < layoutGroup.childCount; i++)
        {
            Destroy(layoutGroup.GetChild(i).gameObject); // per evitare che mi lasci nella hierarchy le vecchie scroll sprite
        }
        
        TextsState(true);
        scrollTitle.text = titoli[index];
        scrollText.text = testi[index];


       
        //Debug.Log(testi[index]);
    }

    public void OpenPanelButton()
    {
        backButton.gameObject.SetActive(false);
        scrollTitle.gameObject.SetActive(true);
        scrollTitle.text = "Pergamene raccolte";
        isPanelScrollOpen = true;
        // fa la stessa funzione del tab --> quando premo il pulsante tab oppure il bottone in bassso mi apre il pannello on le pergamene.
        panel.gameObject.SetActive(true);

        if (!isOpened)
        {
            isOpened = true;
            for (int i = 0; i < maxButton; i++)
            {
                GameObject button = Instantiate(scrollsImage, layoutGroup);
                GameObject scrollsNumber = new GameObject();
                scrollsNumber.AddComponent<Text>();
                scrollsNumber.GetComponent<Text>().font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                scrollsNumber.GetComponent<Text>().fontSize = 24;
                scrollsNumber.GetComponent<Text>().color = Color.black;
                scrollsNumber.GetComponent<Text>().text = "" + (i + 1);
                Instantiate(scrollsNumber, layoutGroup);



                int buttonIndex = i;
                button.AddComponent<Image>().sprite = image;
                button.AddComponent<Button>().onClick.AddListener(() => ButtonFunction(buttonIndex)); // aggiungo all onclick della funzione una funzione  che deve essere associata ad ogni bottone

            }
            //GameManager.timerText.color = Color.red;
            Time.timeScale = 0;
            
        }
        else
        {
            ExitButton();
        }
    }

    public void BackButton() // collegato al bottone blu dentro il pannello scroll
    {
        // devo disattivare i testi e istanziare nuovamente le pergamene
        scrollText.gameObject.SetActive(false);
        //scrollTitle.gameObject.SetActive(false);
        scrollTitle.text = "Pergamene raccolte";
        backButton.gameObject.SetActive(false);

        if (!backPressed && scrollButtonPressed)// e ho gia aperto una pergamena
        {

            backPressed = true;
            for (int i = 0; i < maxButton; i++) // deve farlo solo se ho gia aperto una pergamena
            {
                GameObject button = Instantiate(scrollsImage, layoutGroup);
                GameObject scrollsNumber = new GameObject();
                scrollsNumber.AddComponent<Text>();
                scrollsNumber.GetComponent<Text>().font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                scrollsNumber.GetComponent<Text>().fontSize = 24;
                scrollsNumber.GetComponent<Text>().color = Color.black;
                scrollsNumber.GetComponent<Text>().text = "" + (i + 1);
                Instantiate(scrollsNumber, layoutGroup);


                int buttonIndex = i;
                button.AddComponent<Image>().sprite = image;
                button.AddComponent<Button>().onClick.AddListener(() => ButtonFunction(buttonIndex)); // aggiungo all onclick della funzione una funzione  che deve essere associata ad ogni bottone

            }
            scrollButtonPressed = false;
        }

    }

    public void ExitButton()
    {
        isOpened = false;
        for (int i = 0; i < layoutGroup.childCount; i++)
        {
            Destroy(layoutGroup.GetChild(i).gameObject);

        }
       
        DeactivateAll();
        Time.timeScale = 1;
        isPanelScrollOpen = false;

    }
   
  

    void AddTolists() // questo metodo deve aggiungere i testi alla lista
    {
        titoli.Add("Aguzza la vista!");
        testi.Add("In cima al parco del Monte Paderno, quando il cielo e' limpido, si possono scorgere le cime di vari monti tra cui il monte Cimone e il Corno alle scale. \n" +
                  "Aguzzando la vista si puo' addirittura vedere il mare Adriatico, una vista mozzafiato che solo da qui si puo' osservare.");
        
        titoli.Add("Il torrente Aposa");
        testi.Add("Nella parte est del Monte si trova un solco vallivo che separa il nostro monte dalla collina accanto. \n" +
                  "Le acque che vengono raccolte in questo punto con gli anni sono andate a formare il torrente Aposa, che da molti anni fa parte della rete idrica della citta' di Bologna.");
       
        titoli.Add("L'origine del Nome");
        testi.Add("Le prime notizie che abbiamo sulla localita' denominata 'Paderno' risalgono al 1700 , il nome deriva dal latino 'fundus paternus' che significa 'fondo ereditato dal padre'.");

        titoli.Add("Arte in Parcolli");
        testi.Add("Nella chiesa di Paderno si puo' trovare un affresco settecentesco raffigurante una crocifissione. \n" +
                   "Purtroppo altre strutture religiose sono andate perdute, come due oratori(di cui uno del quattordicesimo secolo).");

        titoli.Add("La flora");
        testi.Add("Dalla fine del '700 circa i terreni del Monte Paderno sono stati ricoperti di boschi cedui, sfruttati per ricavare legname, e campi coltivati a frutteti. \n"+
                  " In tempi piu' recenti proseguirono le coltivazioni di alberi da frutta, come testimoniano i vari ciliegi o mandorli presenti nel parco.");

        titoli.Add("Un passato artigiano");
        testi.Add("Nel '700 attorno alla parrocchia sorse un gruppo di case, che venne nominato 'la Vizzana'. \n" +
                  "I residenti erano bravissim artigiani, utilizzavano le argille della zona per creare vasi per fiori, recipienti in cotto e laterizi per la citta'. La fornace che usavano rimasein attiva fino alla prima guerra mondiale.");

        titoli.Add("La pietra di Bologna");
        testi.Add("Tra le varie argille di Paderno venne ritrovata per la prima volta in Italia la 'baritina' o pietra fosforica, in quanto le sue polveri hanno delle proprieta' luminescenti.");

        titoli.Add("Segni della natura");
        testi.Add("Dal punto di vista geologico, il monte Paderno e' costituito da rocce sedimentarie di natura marnosa. \n " +
                  "Le marne sono rocce derivanti dalla deposizione di argille e carbonato di calcio, provenienti da un ambiente marino che milioni di anni fa era presente anche se lontano dalle coste.");

        titoli.Add("La fauna del parco");
        testi.Add("La fauna del parco e' molto ricca e diversificata, anche se con il tempo e' sempre andata a diminuire per i lavori dell'uomo. \n"+
                  " Possiamo trovare gli scoiattoli, i ricci, le lepri e pettirossi, tra i tanti animali presenti");
         

    }



}

