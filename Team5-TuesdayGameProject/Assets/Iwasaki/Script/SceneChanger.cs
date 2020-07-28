using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : SingletonMonoBehaviour<SceneChanger>
{
    public enum SceneName
    {
        Title = 0,
        StageSelect,
        Stage1,
        Stage2,
        TestScene
    }
    //連打対策
    private bool doOnceSceneChange = true;

    public void LoadScene(SceneName sceneName)
    {
        if (doOnceSceneChange == false) return;
        
        if (doOnceSceneChange)
        {
            StartCoroutine(LoadSceneCor(sceneName));
        }
    }

    private IEnumerator LoadSceneCor(SceneName sceneName)
    {        
        doOnceSceneChange = false;        
        var async = SceneManager.LoadSceneAsync(sceneName.ToString());
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(1);
        doOnceSceneChange = true;
        async.allowSceneActivation = true;
    }
}
