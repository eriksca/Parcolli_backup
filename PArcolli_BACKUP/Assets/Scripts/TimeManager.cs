using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimeManager : MonoBehaviour
{
    [SerializeField] static public float currCountdownValue = 90; // devo farne una che setto dall'inspector che mi va a modificare una variabile con get e set  che mi ritorna il suo valroe 

    //public Text timeText;

    private void Start()
    {

        StartCoroutine(StartCountdown(currCountdownValue));

    }

    private void LateUpdate() {
       /*
        timeText.text = currCountdownValue.ToString();

        if(currCountdownValue<=10){
            timeText.color = Color.red;
        }
       */
    }



    public IEnumerator StartCountdown(float countdownValue)
    {
        currCountdownValue = countdownValue;

        while (currCountdownValue > 0)

        {

            Debug.Log("Countdown: " + currCountdownValue);

            yield return new WaitForSeconds(1.0f);

            currCountdownValue--;

           

           

        }
        if (currCountdownValue <= 0)
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/Sprites/lastFrame.png");
            Debug.Log("GameOver!");
            PlayerPrefs.SetInt("points", GameManager.points);
            SceneManagerScript.OpenScene("GameOver");
            
            
        }

    }
}
