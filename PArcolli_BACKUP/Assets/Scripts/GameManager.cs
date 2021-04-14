using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    static public  int points; // incrementa ogni volta in ui elimino un sacco di immondizia
    static public int collectedScrolls; // pu� avere valore massimo di 99 mettere in get e set.

    
    [SerializeField] Text scoreText;
    [SerializeField] Text scrollText;
    [SerializeField] Text timerText;
    //[SerializeField] Text secondsText;
    [SerializeField] Image grayPanel;




    // ++ lista stringhe

    private List<Text> ScrollList = new List<Text>();


   
    void LateUpdate()
    {

        UpdateTexts();

        timerText.text = TimeManager.currCountdownValue.ToString();

        if(TimeManager.currCountdownValue<=10){
            timerText.color = Color.red;
        }
    }

    public static void ModifyPoints(int value)
    {
        points += value;
        Debug.Log($"Points : {points}");
    }

    public static void ModifyCollectedScrolls()
    {
        if(collectedScrolls < 9){
            collectedScrolls++;
        }
        
    }

    private void UpdateTexts()
    {
        scoreText.text = points.ToString();
        scrollText.text = collectedScrolls.ToString() ;
        timerText.text = TimeManager.currCountdownValue.ToString();

        if (ShowManager.isPanelScrollOpen == false){
            //timerText.color = Color.white;
            //secondsText.color = Color.white;
            grayPanel.enabled = false;
        }

        if(ShowManager.isPanelScrollOpen == true){
            //timerText.color = Color.cyan;
            //secondsText.color = Color.cyan;
            grayPanel.enabled = true;
        }

        if(PlayerCollision.isFiore == true){
            points = points + 20;
            PlayerCollision.isFiore = false;
        }
        

    }



    
}
