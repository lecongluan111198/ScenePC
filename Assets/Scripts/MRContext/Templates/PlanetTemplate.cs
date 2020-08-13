using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlanetTemplate : AbstractTemplate
{
    public PhotonView PV;
    public Material[] materials;

    private void Start()
    {
        //PV = GetComponent<PhotonView>();
    }

    protected override void DoUpdate(GameObject src, GameObject dest, ContextObject co)
    {
        //GameObject src = Instantiate(Resources.Load(ResourceManager.MRPrefab + srcName) as GameObject);

        GameObject go = dest;
        Queue<GameObject> parent = new Queue<GameObject>();
        Queue<GameObject> queue = new Queue<GameObject>();
        parent.Enqueue(null);
        queue.Enqueue(src);

        while (queue.Count != 0)
        {
            GameObject exGo = queue.Dequeue();
            GameObject parentGo = parent.Dequeue();

            if (parentGo != null)
            {
                if(exGo.GetComponent<FollowOrbit>() || exGo.GetComponent<SphereCollider>())
                {
                    go = PhotonNetwork.Instantiate(Path.Combine(ResourceManager.MRPrefab, "Templates/Template"), Vector3.zero, Quaternion.identity, 0);
                }
                else
                {
                    go = new GameObject();
                }

                if(exGo.GetComponent<SphereCollider>())
                {
                    ConvertContextUtils.AddComponent<ObjectSetting>(go);
                }
                go.transform.SetParent(parentGo.transform);
            }
            else
            {
                go = dest;
            }
            go.name = exGo.name;

            UpdateTransform(exGo.gameObject, go);
            UpdateMeshRenderer(exGo.gameObject, go);
            UpdateSkinMeshRenderer(exGo.gameObject, go);
            UpdateMeshFilter(exGo.gameObject, go);
            UpdateBillBoard(exGo.gameObject, go);
            UpdateText(exGo.gameObject, go);
            UpdateRectTransform(exGo.gameObject, go);
            UpdateAnimator(exGo.gameObject, go);
            UpdateMeshCollider(exGo.gameObject, go);
            UpdateLight(exGo.gameObject, go);
            UpdateTerrain(exGo.gameObject, go);
            UpdateParticle(exGo.gameObject, go);

            UpdateOtherComponents(exGo.gameObject, go, co);

            foreach (Transform child in exGo.transform)
            {
                queue.Enqueue(child.gameObject);
                parent.Enqueue(go);
            }
            if (!exGo.gameObject.activeInHierarchy)
            {
                go.SetActive(false);
            }
        }

        //Destroy(src);
    }

    private void UpdateIndividualPlanetData(GameObject src, GameObject go)
    {
        IndividualPlanetData data = src.GetComponent<IndividualPlanetData>();
        if (data != null)
        {
            IndividualPlanetData newData = ConvertContextUtils.AddComponent<IndividualPlanetData>(go);
            newData.earthRadiusRatio = data.earthRadiusRatio;
            newData.overwievCameraRatioSize = data.overwievCameraRatioSize;
        }
    }

    private void UpdateFollowOrbit(GameObject src, GameObject go)
    {
        FollowOrbit data = src.GetComponent<FollowOrbit>();
        if (data != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(data))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(go);
            }
            //FollowOrbit newData = ConvertContextUtils.AddComponent<FollowOrbit>(go);
            //newData.orbitSpeed = data.orbitSpeed;
            //newData.earthOffset = data.earthOffset;
        }
    }

    private void UpdateSphereCollider(GameObject src, GameObject go)
    {
        SphereCollider data = src.GetComponent<SphereCollider>();
        if (data != null)
        {
            Collider collider = go.GetComponent<Collider>();
            if (collider != null)
                Destroy(collider);
            SphereCollider newData = ConvertContextUtils.AddComponent<SphereCollider>(go);
            newData.isTrigger = data.isTrigger;
            newData.center = new Vector3(data.center.x, data.center.y, data.center.z);
            newData.radius = data.radius;
        }
    }

    private void UpdatEllipseLineRenderer(GameObject src, GameObject go)
    {
        EllipseLineRenderer data = src.GetComponent<EllipseLineRenderer>();
        Debug.Log(src.name + " " + data);
        if (data != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(data))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(go);
            }
            //EllipseLineRenderer newData = ConvertContextUtils.AddComponent<EllipseLineRenderer>(go);
            //newData.segments = data.segments;
            //newData.xRadius = data.xRadius;
            //newData.yRadius = data.yRadius;
        }
    }

    private GameObject UpdateLineRenderer(GameObject src, GameObject go, ContextObject co)
    {
        LineRenderer data = src.GetComponent<LineRenderer>();
        if (data != null)
        {
            string orbitName = go.transform.parent.name.Replace("(Clone)", "") + "Orbit";
            GameObject newGo = new GameObject(orbitName);
            newGo.transform.SetParent(GameObject.Find("ListObject").transform);

            UpdateTransform(go, newGo);
            //Vector3 pos = new Vector3();
            //pos.x = src.transform.position.x + src.transform.parent.position.x;
            //pos.y = src.transform.localPosition.y + src.transform.parent.localPosition.y;
            //pos.z = src.transform.localPosition.z + src.transform.parent.localPosition.z;
            newGo.transform.position = src.transform.position + ConvertTypeUtils.ListToVector3(co.position);

            Debug.Log(src.transform.localPosition.x + " " + src.transform.parent.localPosition.x + " " + newGo.transform.position.x);
            if (UnityEditorInternal.ComponentUtility.CopyComponent(data))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(newGo);
            }
            UpdatEllipseLineRenderer(src, newGo);

            //LineRenderer newData = ConvertContextUtils.AddComponent<LineRenderer>(go);
            //Vector3[] pos = new Vector3[data.positionCount];
            ////data.GetPositions(pos);
            ////newData.SetPositions(pos);
            //newData.startWidth = data.startWidth;
            //newData.endWidth = data.endWidth;
            //newData.widthCurve = data.widthCurve;

            //newData.colorGradient = data.colorGradient;
            //newData.startColor = data.startColor;
            //newData.endColor = data.endColor;
            //newData.numCornerVertices = data.numCornerVertices;
            //newData.numCapVertices = data.numCapVertices;
            //newData.alignment = data.alignment;
            //newData.textureMode = data.textureMode;
            //newData.shadowBias = data.shadowBias;
            //newData.generateLightingData = data.generateLightingData;
            //newData.useWorldSpace = data.useWorldSpace;

            //newData.materials = materials.Clone() as Material[];

            //newData.shadowCastingMode = data.shadowCastingMode;
            //newData.receiveShadows = data.receiveShadows;

            //newData.lightProbeUsage = data.lightProbeUsage;
            //newData.reflectionProbeUsage = data.reflectionProbeUsage;

            //newData.motionVectorGenerationMode = data.motionVectorGenerationMode;
            //newData.allowOcclusionWhenDynamic = data.allowOcclusionWhenDynamic;
            //newData.sortingLayerID = data.sortingLayerID;
            //newData.sortingOrder = data.sortingOrder;
            //newData.renderingLayerMask = data.renderingLayerMask;

        }
        return go;
    }

    private void UpdateObjectRotation(GameObject src, GameObject go)
    {
        ObjectRotation data = src.GetComponent<ObjectRotation>();
        if (data != null)
        {
            ObjectRotation newData = ConvertContextUtils.AddComponent<ObjectRotation>(go);
            newData.planetSpeedRotation = data.planetSpeedRotation;
        }
    }

    protected override GameObject UpdateOtherComponents(GameObject src, GameObject dest, ContextObject co)
    {
        UpdateLineRenderer(src, dest, co);
        if (PV.IsMine)
        {
            UpdateFollowOrbit(src, dest);
            UpdateObjectRotation(src, dest);
        }
        UpdateIndividualPlanetData(src, dest);
        UpdateSphereCollider(src, dest);
        return dest;
    }
}
