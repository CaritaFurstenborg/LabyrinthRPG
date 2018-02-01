using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerScript : MonoBehaviour {

    public static LevelManagerScript levelManager;

    [SerializeField]
    private Slider loadingBar;

    //bool sceneLoaded = false;
    
    private void Awake()
    {
        if(levelManager == null)
        {
            levelManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
        //currentScene = SceneManager.GetActiveScene();
        //if (currentScene != SceneManager.GetSceneByName("StartScreen")) 
        //{
        //    LoadLevel("Player");
        //}

    }

    public void LoadLevel(string sceneName)
    {
        if(!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            StartCoroutine(Load(sceneName));

            if (sceneName == "StartScreen")
            {
                StartCoroutine(Unload("PlayerScene"));                
            }
            else
            {
                StartCoroutine(Load("PlayerScene"));
            }     
        }
    }

    public void UnloadLevel(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            StartCoroutine(Unload(sceneName));
        }        
    }

    IEnumerator Unload(string scene)        //Coroutine on pakollinen koska jostakin syystä Unloadia ei voi ajaa ilman sitä
    {
        yield return new WaitForSeconds(.50f);

        SceneManager.UnloadSceneAsync(scene);
    }

    IEnumerator Load(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while(!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }
    }
}
