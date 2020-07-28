using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LessonCreate : MonoBehaviour
{
    [Header("INFORMATION")]
    public TMP_InputField title;
    public TMP_InputField description;
    public TextMeshProUGUI backgound;
    public Image avatar;

    [Header("RESOURCE")]
    public TMP_Text errorMessage;
    public TMP_Text successMessage;
    //public GameObject AddButton;
    //public GameObject EditButton;

    private Context context;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearInformation()
    {
        title.text = "";
        description.text = "";
        backgound.text = "Spaceship";
        errorMessage.text = "";
        successMessage.text = "";
    }

    public void EditContext()
    {

    }

    public void CreateLesson()
    {
        context = new Context();
        context.Author = AccountInfo.Instance.Username;
        context.AvatarId = ResourceManager.GetContextAvatarId(avatar.sprite.name);
        context.Name = title.text;
        context.Description = description.text;
        context.TeacherId = AccountInfo.Instance.UID;
        if (backgound.text.Equals("Spaceship"))
        {
            context.Content = StringCompressor.CompressString(File.ReadAllText(@"Assets\DefaultData\Scri-fi.json"));
        }
        else if (backgound.text.Equals("Clinic"))
        {
            context.Content = StringCompressor.CompressString(File.ReadAllText(@"Assets\DefaultData\Clinic.json"));
        }
        ContextModel.Instance.createContext(context, (data) =>
        {
            Debug.Log(data);
            if (data != null)
            {

                successMessage.text = "Create lesson success";
            }
            else
            {
                errorMessage.text = "Create lesson fail";
            }
        });
    }
}
