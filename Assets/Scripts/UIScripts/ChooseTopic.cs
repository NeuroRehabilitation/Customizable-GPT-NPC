using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseTopic : MonoBehaviour
{
    public GameObject loading;
    public int level;
    public string chosenScene;
    public int milestoneTopic;
    private StateController stateController;

    private void Awake()
    {
        stateController = GameObject.Find("StateController").GetComponent<StateController>();
    }

    // when user pressed the button of the exploration module, these are populated by the value set on the button in the inspector
    public void getScene(string sceneName)
    {
        chosenScene = sceneName;
    }

    public void getMilestone(int milestone)
    {
        milestoneTopic = milestone;
    }

    // Depending on the previously set values, a scene is loaded
    public void loadScene()
    {
        stateController.currScene = chosenScene;
        stateController.mapSeed = 10;
        stateController.milestoneCurrentTopic = milestoneTopic;
        //The map seed generations are pre-defined, to ensure that the same map is generated for the same topic
        if (chosenScene == "Exploration")
            stateController.mapSeed = level == 0 ? 123 * (milestoneTopic + 1) : 123;
        else
            stateController.mapSeed = 10;
        StartCoroutine(LoadSceneWithDelay( chosenScene));
    }
    
    // Delay for the tip to be read on the loading screen
    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        loading.SetActive(true);
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Gallery","Joined "+sceneName+" scene");
        yield return new WaitForSeconds(20f);

        

        SceneManager.LoadScene(sceneName);
    }

}
