using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class SceneAutoLoad : MonoBehaviour
{
    public float minLoadingTime = 6;
    public bool skipMinLoadingTime = false;
    public float timeDelayLoadMain = 3;
    public bool loadOnStart = true;
    public bool allowReload = false;
    public bool setActiveAfterLoad = false;
    public bool autoUnload = true;
    public bool isLoaded = false;

    public bool disableLoaderAfterLoadScene = false;
    [SerializeField] private Text txtLoading;
    public UnityEvent<float> onLoadingProgress = new UnityEvent<float>();
    public UnityEvent onLoadSuccess = new UnityEvent();

    public void Start()
    {
        onLoadingProgress.Invoke(0.1f);
        LoadTextLoading(0.1f);
        StartCoroutine(IE_DelayAction(() => {  //verify if the scene is already open to avoid opening a scene twice
            if (SceneManager.sceneCount > 0)
            {
                for (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == gameObject.name)
                    {
                        isLoaded = true;
                    }
                }
            }
            if (loadOnStart)
            {
                 LoadScene();
               // SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);

            }
            SDK.AdsManager.Instance.UpdateAdsMediation();
            AppOpenAdLauncher.Instance.ShowAOA();
        }, timeDelayLoadMain));
        //onLoadSuccess.Invoke();
    }


    public void LoadScene()
    {
        if (allowReload && isLoaded && autoUnload)
            SceneManager.UnloadSceneAsync(gameObject.name);

        if (!isLoaded || allowReload)
        {
            //Loading the scene, using the gameobject name as it's the same as the name of the scene to load
            AsyncOperation async = SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            //We set it to true to avoid loading the scene twice
            isLoaded = true;

            if (setActiveAfterLoad)
            {
                StartCoroutine(ActiveAfterLoad(async));
            }
        }

        Scene sceneToLoad = SceneManager.GetSceneByName(gameObject.name);
        if (sceneToLoad.isLoaded && setActiveAfterLoad)
            SceneManager.SetActiveScene(sceneToLoad);
    }

    IEnumerator ActiveAfterLoad(AsyncOperation async)
    {
        float loadTime = 0;
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            // Check if the load has finished
            if (async.progress >= 0.9f && (loadTime >= minLoadingTime || skipMinLoadingTime))
            {
                async.allowSceneActivation = true;
                //onLoadSuccess.Invoke();
            }
            loadTime += Time.deltaTime;
            float ValueLoad = Mathf.Clamp(loadTime / minLoadingTime, 0.1f, 1.1f);
            onLoadingProgress.Invoke(ValueLoad);
            LoadTextLoading(ValueLoad);
             yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(gameObject.name));
       // SDK.AdsManager.Instance._ShowBannerAds();
        if (disableLoaderAfterLoadScene)
            gameObject.SetActive(false);
    }
    public void LoadTextLoading(float value)
    {
        txtLoading.text = "Loading " + Mathf.Clamp((int)(value * 100), 0, 100) + "%";
    }
    IEnumerator IE_DelayAction(Action action, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        action?.Invoke();
    }

}
