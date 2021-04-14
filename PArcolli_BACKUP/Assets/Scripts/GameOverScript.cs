using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameOverScript : MonoBehaviour
{
    /*
    [SerializeField] GameObject panel;
    Texture2D myTexture2D;
    Sprite mySprite;
    */
    Text scoreText;

    private void Start()
    {
        /*
        byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Sprites/lastFrame.png");
        myTexture2D = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        myTexture2D.filterMode = FilterMode.Trilinear;
        myTexture2D.LoadImage(bytes);
        mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        panel.GetComponent<Image>().sprite = mySprite;
        */
        scoreText = this.gameObject.GetComponent<Text>();
        scoreText.text = $"{PlayerPrefs.GetInt("points")}";
    }

}
