using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class AbstractTemplate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateInformation(ContextObject co)
    {
        try
        {
            GameObject src = Instantiate(Resources.Load(ResourceManager.MRPrefab + co.nameDownload) as GameObject);
            DoUpdate(src, gameObject, co);
            gameObject.name = co.nameObj;
            co.ToGameObject(gameObject);
            Destroy(src);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

    protected abstract GameObject UpdateOtherComponents(GameObject src, GameObject dest, ContextObject co);

    protected void DoUpdate(GameObject src, GameObject dest, ContextObject co)
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
                go = new GameObject();
                go.transform.SetParent(parentGo.transform);
            }
            else
            {
                go = dest;
            }
            go.name = exGo.name;

            UpdateTransform(exGo.gameObject, go);
            //getAndUpdateComponent<MeshRenderer>(exGo.gameObject, go);
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
    public void UpdateBillBoard(GameObject src, GameObject dest)
    {
        Billboard com = src.GetComponent<Billboard>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
        }
    }

    public void UpdateRectTransform(GameObject src, GameObject dest)
    {
        RectTransform com = src.GetComponent<RectTransform>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                RectTransform newCom = ConvertContextUtils.AddComponent<RectTransform>(dest);
                UnityEditorInternal.ComponentUtility.PasteComponentValues(newCom);
                newCom.sizeDelta = com.sizeDelta;
            }
        }
    }
    public void UpdateText(GameObject src, GameObject dest)
    {
        TextMeshPro com = src.GetComponent<TextMeshPro>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
        }
    }

    public void UpdateAnimator(GameObject src, GameObject dest)
    {
        Animator com = src.GetComponent<Animator>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
        }
    }

    public void UpdateSkinMeshRenderer(GameObject src, GameObject dest)
    {
        SkinnedMeshRenderer com = src.GetComponent<SkinnedMeshRenderer>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
            //MeshUtils.Combine(src, dest);
            //SkinnedMeshRenderer newCom = addComponent<SkinnedMeshRenderer>(dest);
            //newCom.sharedMesh = com.sharedMesh;
            ////newCom.localBounds.center = com.localBounds.center;
            //newCom.localBounds = com.localBounds;
            //newCom.rootBone = com.rootBone;
            //newCom.bones = com.bones;

            //newCom.forceMatrixRecalculationPerRender = com.forceMatrixRecalculationPerRender;
            //newCom.materials = com.materials;
            //newCom.shadowCastingMode = com.shadowCastingMode;
            //newCom.receiveShadows = com.receiveShadows;

            //newCom.lightProbeUsage = com.lightProbeUsage;
            //newCom.reflectionProbeUsage = com.reflectionProbeUsage;
            //newCom.probeAnchor = com.probeAnchor;
            //newCom.motionVectorGenerationMode = com.motionVectorGenerationMode;
            //newCom.allowOcclusionWhenDynamic = com.allowOcclusionWhenDynamic;
        }
    }

    public void getAndUpdateComponent<T>(GameObject src, GameObject dest) where T : Component
    {
        T comSrc = src.GetComponent<T>();
        if (comSrc == null)
        {
            return;
        }
        T comDest = dest.GetComponent<T>();
        if (comDest == null)
        {
            comDest = ConvertContextUtils.AddComponent<T>(dest);
        }
        ConvertTypeUtils.GetCopyOf(comDest, comSrc);
        //FieldInfo[] fields = type.GetFields();
        //foreach (FieldInfo field in fields)
        //{
        //    field.SetValue(comDest, field.GetValue(comSrc));
        //}
    }


    public GameObject UpdateTransform(GameObject src, GameObject dest)
    {
        dest.transform.localPosition = src.transform.localPosition;
        dest.transform.localRotation = src.transform.localRotation;
        dest.transform.localScale = src.transform.localScale;
        return dest;
    }

    public GameObject UpdateMeshRenderer(GameObject src, GameObject dest)
    {
        MeshRenderer com = src.GetComponent<MeshRenderer>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
            //MeshRenderer newCom = ConvertContextUtils.AddComponent<MeshRenderer>(dest);
            //newCom.materials = com.materials;

            //newCom.shadowCastingMode = com.shadowCastingMode;
            //newCom.receiveShadows = com.receiveShadows;

            //newCom.lightProbeUsage = com.lightProbeUsage;
            //newCom.reflectionProbeUsage = com.reflectionProbeUsage;
            //newCom.probeAnchor = com.probeAnchor;
            //newCom.motionVectorGenerationMode = com.motionVectorGenerationMode;
            //newCom.allowOcclusionWhenDynamic = com.allowOcclusionWhenDynamic;
        }
        return dest;
    }

    public GameObject UpdateMeshFilter(GameObject src, GameObject dest)
    {
        MeshFilter com = src.GetComponent<MeshFilter>();
        if (com != null)
        {
            //MeshFilter newCom = dest.AddComponent<MeshFilter>();
            //newCom.mesh = com.mesh;
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
        }
        return dest;
    }

    public void UpdateMeshCollider(GameObject src, GameObject dest)
    {
        MeshCollider com = src.GetComponent<MeshCollider>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
        }
    }

    public void UpdateLight(GameObject src, GameObject dest)
    {
        Light com = src.GetComponent<Light>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
        }
    }

    public void UpdateTerrain(GameObject src, GameObject dest)
    {
        Terrain com = src.GetComponent<Terrain>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
        }

        TerrainCollider com1 = src.GetComponent<TerrainCollider>();
        if (com != null)
        {
            if (UnityEditorInternal.ComponentUtility.CopyComponent(com1))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(dest);
            }
        }
    }
}
