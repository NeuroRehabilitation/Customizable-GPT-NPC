using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public InputActionReference Y = null;
    public InputActionReference X = null;
    public InputActionReference A = null;
    public InputActionReference B = null;

    public GameObject[] spawnObjectManipulators;
    private Sprite last_screenshot_save;
    public GameObject path;
    public GameObject map;

    public GameObject polaroid;

    public GameObject StateController;
    public float speed = 10.0f;
    public float rotSpeed = 1f;
    public ActionBasedContinuousMoveProvider MoveScript;

    private GameObject leftHand;

    private void Awake() 
    {
        leftHand = GameObject.Find("LeftHand Controller");
        Y.action.started += YTrigger;
        X.action.started += XTrigger;
        A.action.started += ATrigger;
        B.action.started += BTrigger;
    }

    private void OnDestroy() 
    {
        Y.action.started -= YTrigger;
        X.action.started -= XTrigger;
        A.action.started -= ATrigger;
        B.action.started -= BTrigger;
    }

    private void YTrigger(InputAction.CallbackContext context)
    {
        if (StateController.GetComponent<StateController>().currScene == "Garden")
            print("screenshot()");
        else
            ShowMap();
    }
    private void XTrigger(InputAction.CallbackContext context)
    {
        if (StateController.GetComponent<StateController>().currScene == "Garden")
            print("edit()");
        else
            ShowMap();
        //ShowMap();
    }
    private void ATrigger(InputAction.CallbackContext context)
    {
        GameObject.Find("Narrator").GetComponent<NarratorController>().TapNarrator();

    }
    public void BTrigger(InputAction.CallbackContext context)
    {
        //ShowMap();
        //GameObject.Find("Narrator").GetComponent<NarratorController>().TapNarrator();
    }

    private IEnumerator CountdownCoroutine()
    {
        int countdownTime = 3;

        while (countdownTime > 0)
        {
            GameObject.Find("CountdownUI").GetComponent<TextMeshProUGUI>().text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        GameObject.Find("CountdownUI").GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1f);

    }

    public void countdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    public void StartScreenShot()
    {
        Invoke("screenShot", 4.0f);
        countdown();
    }

    public void screenShot()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Garden","user takes screenshot");
        GameObject narrator = GameObject.Find("Narrator");
        narrator.SetActive(false);
        SetSpawnObjectManipulatorsInactive();

        string filepath= Application.persistentDataPath+"/MyGameSaveFolder/Screenshots/";
        Directory.CreateDirectory(Path.GetDirectoryName(filepath));
        ScreenCapture.CaptureScreenshot(filepath+StateController.GetComponent<StateController>().Username+".png");

        StartCoroutine(screenShotDelay(narrator));
        
    }

    void SetSpawnObjectManipulatorsInactive()
    {
        // Find all GameObjects with the specified name
         spawnObjectManipulators = GameObject.FindGameObjectsWithTag("GardenUI");

        // Iterate through the found GameObjects and set them to inactive
        foreach (GameObject spawnObjectManipulator in spawnObjectManipulators)
        {
            spawnObjectManipulator.SetActive(false);
        }
    }

    void SetSpawnObjectManipulatorsActive()
    {
        // Iterate through the found GameObjects and set them to inactive
        foreach (GameObject spawnObjectManipulator in spawnObjectManipulators)
        {
            spawnObjectManipulator.SetActive(true);
        }
    }

    IEnumerator screenShotDelay(GameObject narrator)
    {
        yield return new WaitForSeconds(.5f);
        polaroid.SetActive(true);
        string path = Application.persistentDataPath+"/MyGameSaveFolder/Screenshots/"+StateController.GetComponent<StateController>().Username+".png";        
        last_screenshot_save = LoadSprite(path);
        GameObject.Find("screenshot").GetComponent<Image>().sprite = last_screenshot_save;
        StartCoroutine(polaroidC(narrator));
    }

    IEnumerator polaroidC(GameObject narrator)
    {
        yield return new WaitForSeconds(4f);
        polaroid.SetActive(false);
        narrator.SetActive(true);
        SetSpawnObjectManipulatorsActive();
    }

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

    public void Start()
    {
        StateController = GameObject.Find("StateController");
        try
        {
            polaroid = GameObject.Find("polaroid");
            polaroid.SetActive(false);
        }
        catch (System.Exception)
        {
             
        }
        
        if (StateController.GetComponent<StateController>().currScene == "Gallery")
        {
            if (StateController.GetComponent<StateController>().SpawnPos != (new Vector3(0.0f,0.0f,0.0f)))
            {
                print("spawn pos "+StateController.GetComponent<StateController>().SpawnPos);
                transform.position = StateController.GetComponent<StateController>().SpawnPos; 
                StateController.GetComponent<StateController>().SpawnPos = new Vector3(0.0f,0.0f,0.0f);
            }
        }
        else
        {
            if (StateController.GetComponent<StateController>().currScene == "Exploration" && StateController.GetComponent<StateController>().milestoneCurrentTopic <=4)
                StateController.GetComponent<StateController>().SpawnPos = new Vector3(-50f,12f,-35f);
            if (StateController.GetComponent<StateController>().currScene == "Garden" || (StateController.GetComponent<StateController>().currScene == "Exploration" && StateController.GetComponent<StateController>().milestoneCurrentTopic == 5))
                StateController.GetComponent<StateController>().SpawnPos = new Vector3(-84f,12f,-26f);
            if (StateController.GetComponent<StateController>().currScene == "Share" || (StateController.GetComponent<StateController>().currScene == "Exploration" && StateController.GetComponent<StateController>().milestoneCurrentTopic == 6))
                StateController.GetComponent<StateController>().SpawnPos = new Vector3(-80f,12f,24f);
            if (StateController.GetComponent<StateController>().currScene == "Adaptation" || (StateController.GetComponent<StateController>().currScene == "Exploration" && StateController.GetComponent<StateController>().milestoneCurrentTopic == 7))
                StateController.GetComponent<StateController>().SpawnPos = new Vector3(-50f,12f,38f);
        }
    }

    public void ShowMap()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Exploration","user toggles map");
        map.SetActive(!map.activeSelf);
        try
        {
            leftHand.SetActive(!map.activeSelf);
            GameObject.Find("ReticleTeleport(Clone)").SetActive(!map.activeSelf);
        }
        catch (System.Exception)
        {
            
        }
    }

    // all for debug only
    void FixedUpdate()
    {
        // rotation
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left")) 
        { 
            transform.Rotate(0, -rotSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
        {
            transform.Rotate(0, rotSpeed, 0);
        }

        // Go back to the gallery 
        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject.Find("StateController").GetComponent<StateController>().currScene="Gallery";
            SceneManager.LoadScene("Gallery");
        }

        if (Input.GetKey(KeyCode.E))
        {
            string filepath= Application.persistentDataPath+"/MyGameSaveFolder/Screenshots/";
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            ScreenCapture.CaptureScreenshot(filepath+StateController.GetComponent<StateController>().Username+".png");
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            ShowMap();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            //transform.position = StateController.GetComponent<StateController>().SpawnPos; 
            SceneManager.LoadScene("Chest");
        }
        
    }
}