using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerScript : MonoBehaviour {

    public static LevelManagerScript levelManager;

    //[SerializeField]
    //private Slider loadingBar;

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

    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //public void UnloadLevel(string sceneName)
    //{
    //    if (SceneManager.GetSceneByName(sceneName).isLoaded)
    //    {
    //        StartCoroutine(Unload(sceneName));
    //    }
    //}

    //IEnumerator Unload(string scene)        //Coroutine on pakollinen koska jostakin syystä Unloadia ei voi ajaa ilman sitä
    //{
    //    yield return new WaitForSeconds(.50f);
    //    AsyncOperation async2 = SceneManager.UnloadSceneAsync(scene);

    //    while(!async2.isDone)
    //    {
    //        yield return null;            
    //    }
    //}

    //IEnumerator Load(string sceneName)
    //{
    //    AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

    //    while (!async.isDone)
    //    {
    //        float progress = Mathf.Clamp01(async.progress / 0.9f);
    //        loadingBar.value = progress;
    //        yield return null;            
    //        async.allowSceneActivation = true;
    //    }
    //    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    //}
}
