using System.Collections;
using System.Collections.Generic;
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

    public abstract void UpdateInformation(ContextObject co);

    protected void UpdateMesh(string srcName, GameObject dest)
    {
        GameObject src = Instantiate(Resources.Load(ResourceManager.MRPrefab + srcName) as GameObject);

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
            UpdateTransform(exGo.gameObject, go);
            //getAndUpdateComponent<MeshRenderer>(exGo.gameObject, go);
            UpdateMeshRenderer(exGo.gameObject, go);
            //UpdateSkinMeshRenderer(exGo.gameObject, go);
            UpdateMeshFilter(exGo.gameObject, go);
            //getAndUpdateComponent<MeshFilter>(exGo.gameObject, go);
            getAndUpdateComponent<MeshCollider>(exGo.gameObject, go);
            getAndUpdateComponent<BoxCollider>(exGo.gameObject, go);
            go.name = exGo.name;

            foreach (Transform child in exGo.transform)
            {
                queue.Enqueue(child.gameObject);
                parent.Enqueue(go);
            }
        }

        Destroy(src);
    }

    protected void UpdateSkinMeshRenderer(GameObject src, GameObject dest)
    {
        SkinnedMeshRenderer com = src.GetComponent<SkinnedMeshRenderer>();
        if (com != null)
        {
            MeshUtils.Combine(src, dest);
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

    protected void getAndUpdateComponent<T>(GameObject src, GameObject dest) where T : Component
    {
        T comSrc = src.GetComponent<T>();
        if (comSrc == null)
        {
            return;
        }
        T comDest = dest.GetComponent<T>();
        if (comDest == null)
        {
            comDest = ConvertContextUtils.addComponent<T>(dest);
        }
        ConvertTypeUtils.GetCopyOf(comDest, comSrc);
        //FieldInfo[] fields = type.GetFields();
        //foreach (FieldInfo field in fields)
        //{
        //    field.SetValue(comDest, field.GetValue(comSrc));
        //}
    }


    protected void UpdateTransform(GameObject src, GameObject dest)
    {
        dest.transform.localPosition = src.transform.localPosition;
        dest.transform.localRotation = src.transform.localRotation;
        dest.transform.localScale = src.transform.localScale;
    }

    protected void UpdateMeshRenderer(GameObject src, GameObject dest)
    {
        MeshRenderer com = src.GetComponent<MeshRenderer>();
        if (com != null)
        {
            MeshRenderer newCom = ConvertContextUtils.addComponent<MeshRenderer>(dest);
            newCom.materials = com.materials;

            newCom.shadowCastingMode = com.shadowCastingMode;
            newCom.receiveShadows = com.receiveShadows;

            newCom.lightProbeUsage = com.lightProbeUsage;
            newCom.reflectionProbeUsage = com.reflectionProbeUsage;
            newCom.probeAnchor = com.probeAnchor;
            newCom.motionVectorGenerationMode = com.motionVectorGenerationMode;
            newCom.allowOcclusionWhenDynamic = com.allowOcclusionWhenDynamic;
        }
    }

    protected void UpdateMeshFilter(GameObject src, GameObject dest)
    {
        MeshFilter com = src.GetComponent<MeshFilter>();
        if (com != null)
        {
            MeshFilter newCom = dest.AddComponent<MeshFilter>();
            newCom.mesh = com.mesh;
        }
    }
}
