using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class LeftController : MonoBehaviour
{
    public enum Hand
    {
        Right,
        Left
    }

    [Header("HAND CONTROLLER")]
    [SerializeField]
    private List<AbstractController> controllers = new List<AbstractController>();
    //[SerializeField]
    private AbstractController currController;
    private int currentIndex = 0;
    public static LeftController Instance = null;

    public AbstractController CurrController { get => currController; set => currController = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrController = controllers[currentIndex];
        CurrController.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            ShowController();
        }
        else if (Input.GetKey(KeyCode.F2))
        {
            ShowPlayerController();
        }
        else if (Input.GetKey(KeyCode.F3))
        {
            ShowPlanetSpeedController();
        }
        else if (Input.GetKey(KeyCode.F4))
        {
            ShowPlanetScaleController();
        }
    }

    private void OnEnable()
    {
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
    }

    private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Left)
        {
            if (obj.pressType == InteractionSourcePressType.Grasp)
            {
                ShowController();
            }
            else if (obj.pressType == InteractionSourcePressType.Menu)
            {
                //show action
                ShowPlayerController();
            }
        }
    }

    public void ShowController()
    {
        if (MRDataHolder.Instance.IsEdit)
        {
            if (SettingMenuPanel.Instance.IsOn)
            {
                SettingMenuPanel.Instance.OffController();
            }
            else
            {
                if (MRDataHolder.Instance.CurrentClickObject == null)
                {
                    SettingMenuPanel.Instance.DisableObjectController();
                }
                SettingMenuPanel.Instance.OnController();
            }
        }
        else
        {
            if (FeatureController.Instance.IsOn)
            {
                FeatureController.Instance.OffController();
            }
            else
            {
                if (MRDataHolder.Instance.CurrentClickObject == null)
                {
                    FeatureController.Instance.DisableObjectController();
                }
                FeatureController.Instance.OnController();
            }
        }
    }

    public void ShowPlayerController()
    {
        if (!MRDataHolder.Instance.IsEdit)
        {
            if (PlayerController.Instance.IsOn)
            {
                PlayerController.Instance.OffController();
            }
            else
            {
                PlayerController.Instance.OnController();
            }
        }
    }
    private void ChangeTypeController()
    {
        if (CurrController == null)
        {
            CurrController = controllers[currentIndex];
        }
        CurrController.gameObject.SetActive(false);

        currentIndex++;
        if (currentIndex >= controllers.Count)
        {
            currentIndex = 0;
        }
        CurrController = controllers[currentIndex];
        CurrController.gameObject.SetActive(true);
    }

    private void ShowPlanetSpeedController()
    {
        if (!MRDataHolder.Instance.IsEdit)
        {
            FeatureController.Instance.ShowSpeedController();
        }
    }
    private void ShowPlanetScaleController()
    {
        if (!MRDataHolder.Instance.IsEdit && MRDataHolder.Instance.CurrentClickObject)
        {
            FeatureController.Instance.ShowScaleController();
        }
    }
}
