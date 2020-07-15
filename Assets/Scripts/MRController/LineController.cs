using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [Header("Drawing Settings")]
    [SerializeField]
    private GameObject strokePrefab;
    [SerializeField]
    private float minColorDelta = 0.01f;
    [SerializeField]
    private float minPositionDelta = 0.01f;
    [SerializeField]
    private float maxTimeDelta = 0.25f;

    private float width = 0f;
    private Dictionary<string, LineRenderer> lineMap = new Dictionary<string, LineRenderer>();
    private Dictionary<string, KeyValuePair<Vector3, float>> infoMap = new Dictionary<string, KeyValuePair<Vector3, float>>();
    private Dictionary<string, Stroke> strokeMap = new Dictionary<string, Stroke>();
    private PhotonView PV;

    public static LineController Instance = null;

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
    }
    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void DrawLine(float sx, float sy, float sz, string color, float timeScale, string key)
    {
        if (PhotonNetwork.InRoom)
        {
            PV.RPC("RPCDrawLine", RpcTarget.AllBuffered, sx, sy, sz, "red", timeScale, key);
        }
        else
        {
            RPCDrawLine(sx, sy, sz, "red", timeScale, key);
        }
    }

    public void StopDraw(string key)
    {
        if (PhotonNetwork.InRoom)
        {
            PV.RPC("RPCStopDraw", RpcTarget.AllBuffered, key);
        }
        else
        {
            RPCStopDraw(key);
        }
    }

    [PunRPC]
    private void RPCDrawLine(float sx, float sy, float sz, string color, float timeScale, string key)
    {
        Vector3 position = new Vector3(sx, sy, sz);
        LineRenderer line;
        //Debug.Log(key + position);

        if (!lineMap.ContainsKey(key))
        {
            // Create a new brush stroke by instantiating stokePrefab
            GameObject newStroke = Instantiate(strokePrefab);
            line = newStroke.GetComponent<LineRenderer>();
            newStroke.transform.position = position;
            line.positionCount = 1;
            line.SetPosition(0, position);
            lineMap.Add(key, line);
            infoMap.Add(key, new KeyValuePair<Vector3, float>(position, timeScale));
            if (MRDataHolder.Instance.IsEdit)
            {
                //set parent
                newStroke.transform.SetParent(MREditContextManager.Instance.container.transform);
                //add obj basic info
                ObjBasicInfo info = ConvertContextUtils.AddComponent<ObjBasicInfo>(line.gameObject);
                info.Id = 1;
                info.DownloadName = "BrushThinStroke";
                info.FromServer = false;
                //add stroke com
                Stroke stroke = ConvertContextUtils.AddComponent<Stroke>(line.gameObject);
                strokeMap.Add(key, stroke);
            }
        }
        else
        {
            line = lineMap[key];
        }

        KeyValuePair<Vector3, float> pair = infoMap[key];
        float lastPointAddedTime = pair.Value;
        float initialWidth = line.widthMultiplier;
        line.material.color = Color.red;
        //brushRenderer.material.color = Color.blue;
        line.widthMultiplier = Mathf.Lerp(initialWidth, initialWidth * 2, width);
        if (Vector3.Distance(position, pair.Key) > minPositionDelta || Time.unscaledTime > lastPointAddedTime + maxTimeDelta)
        {
            line.positionCount += 1;
            line.SetPosition(line.positionCount - 1, position);
            if (MRDataHolder.Instance.IsEdit)
            {
                //update pos in stroke com
                Stroke stroke = strokeMap[key];
                stroke.AddPosition(position);
            }
        }
    }

    [PunRPC]
    private void RPCStopDraw(string key)
    {
        if (lineMap.ContainsKey(key))
        {
            lineMap.Remove(key);
            infoMap.Remove(key);
            if (MRDataHolder.Instance.IsEdit)
            {
                strokeMap.Remove(key);
            }
        }
    }
}
