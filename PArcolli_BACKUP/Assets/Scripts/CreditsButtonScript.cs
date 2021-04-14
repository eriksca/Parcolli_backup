using UnityEngine;
using UnityEngine.UI;

public class CreditsButtonScript : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject textPanel;

    [SerializeField] Image creditsImage;
    [SerializeField] Image instructionImage;

    
    bool isOpened = false;
    bool creditsClicked = false;
    bool istruzioniText = false;

    private void Awake()
    {
        creditsPanel.SetActive(true);
        textPanel.SetActive(true);
    }
        
   
    private void Start()
    {
        creditsPanel.SetActive(false);
        textPanel.SetActive(false);
    }



    public void CreditsButton()
    {
        if(!isOpened)
        {
            creditsPanel.SetActive(true);
            isOpened = true;
        }
        else
        {
            creditsPanel.SetActive(false);
            textPanel.SetActive(false);
            isOpened = false;
        }
        
    }

    public void IstructionButton()
    {
        // apre un nuovo pannello  con dentro un testo per la descrizione del gioco
        istruzioniText = true;
        creditsClicked = false;
        OpenTextPanel();
        
    }

    public void CreditsText()
    {
        creditsClicked = true;
        istruzioniText = false;
        OpenTextPanel();

    }


    private void OpenTextPanel()
    {
        if(istruzioniText == true){
            textPanel.SetActive(true);
            instructionImage.enabled = true;
            //sprite credits setActive = false
            
        }

        if(creditsClicked == true){
            textPanel.SetActive(true);
            creditsImage.enabled = true;
            //sprite istruzione setActive = false
            instructionImage.enabled = false;
        }

    }

    private void CloseTextPanel(){

    }

    
}
