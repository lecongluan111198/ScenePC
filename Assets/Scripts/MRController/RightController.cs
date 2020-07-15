using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class RightController : MonoBehaviour
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
    public static RightController Instance = null;

    public AbstractController CurrController { get => currController; set => currController = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != this)
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
        if (Input.GetMouseButtonDown(2))
        {
            ChangeTypeController();
        }
    }

    private void OnEnable()
    {
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
    }

    private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Right && obj.pressType == InteractionSourcePressType.Grasp)
        {
            ChangeTypeController();
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
}
