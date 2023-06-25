using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenScreenshot : MonoBehaviour
{
    public GameObject screenshot_backround;
    private Sprite last_screenshot_save;
    
    // This function is called at the start of the script. It constructs the path for the screenshot and loads it into the game.
    private void Start()
    {
        string path = Application.persistentDataPath+"/MyGameSaveFolder/Screenshots/"+GameObject.Find("StateController").GetComponent<StateController>().Username+".png";        
        last_screenshot_save = LoadSprite(path);
        screenshot_backround.GetComponent<Image>().sprite = last_screenshot_save;
    }
    
    // This function takes a file path, checks if a file exists there, and if so, creates a sprite from the image file at that location and returns it.
    public Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }
}
