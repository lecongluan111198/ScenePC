using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance = null;
    private PhotonView PV;
    public string currentScene = "";
    public string mainBoardScene;
    public string waitingRoomScene;
    public string gamePlayScene;
    public string mrEditScene;

    public string CurrentScene { get => currentScene; set => currentScene = value; }

    public enum SceneType
    {
        MAINBOARD,
        WAITING,
        GAMEPLAY,
        MREDIT
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        PV = GetComponent<PhotonView>();
        LoadScene(SceneType.MAINBOARD, false);
    }

    public void LoadScene(SceneType type, bool isPhoton)
    {
        switch (type)
        {
            case SceneType.MAINBOARD:
                MRDataHolder.Instance.IsEdit = false;
                LoadScene(mainBoardScene, isPhoton);
                break;
            case SceneType.WAITING:
                MRDataHolder.Instance.IsEdit = false;
                LoadScene(waitingRoomScene, isPhoton);
                break;
            case SceneType.GAMEPLAY:
                MRDataHolder.Instance.IsEdit = false;
                LoadScene(gamePlayScene, isPhoton);
                break;
            case SceneType.MREDIT:
                MRDataHolder.Instance.IsEdit = true;
                LoadScene(mrEditScene, isPhoton);
                break;
        }
    }

    public void LoadScene(string name, bool isPhoton)
    {
        UnloadCurrentScene();
        if (isPhoton)
        {
            PhotonNetwork.LoadLevel(name);
        }
        else
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        }
        CurrentScene = name;

        //PV.RPC("UpdateCurrentScene", RpcTarget.AllBuffered, name);
    }

    public void UnloadCurrentScene()
    {
        if (CurrentScene != null && CurrentScene != "")
        {
            SceneManager.UnloadSceneAsync(CurrentScene);
        }
    }

    public void UpdateCurrentScene(string name)
    {
        CurrentScene = name;
    }

    public void UpdateCurrentScene(SceneType type)
    {
        switch (type)
        {
            case SceneType.MAINBOARD:
                CurrentScene = mainBoardScene;
                break;
            case SceneType.WAITING:
                CurrentScene = waitingRoomScene;
                break;
            case SceneType.GAMEPLAY:
                CurrentScene = gamePlayScene;
                break;
            case SceneType.MREDIT:
                CurrentScene = mrEditScene;
                break;
        }
    }
}