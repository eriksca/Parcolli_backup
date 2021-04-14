using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagerScript : MonoBehaviour
{
  
    public static void OpenScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }

    public void PlayButton(){
        ResetScene();
        //TimeManager.currCountdownValue = 180;
        SceneManager.LoadScene("SceneGame");


    }

    /*public void CreditsButton(){
        // bisogna aprire pannelo 

        creditsPanel.SetActive(true);
    }
    */

    public void GiveUpButton()
    {
        ResetScene();
        SceneManager.LoadScene("StartScene");
        //TimeManager.currCountdownValue = 180;
    }
    public void ExitButton(){
        Application.Quit();
    }

    private void ResetScene(){
       GameManager.points = 0;
       TimeManager.currCountdownValue = 90;
       GameManager.collectedScrolls = 0;
    }
}
