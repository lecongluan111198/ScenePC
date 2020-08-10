using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMeshTemplate : AbstractTemplate
{

    private void UpdateSkinMeshRenderer1(GameObject src, GameObject dest)
    {
        SkinnedMeshRenderer data = src.GetComponent<SkinnedMeshRenderer>();
        if (data != null)
        {
            SkinnedMeshRenderer newData = ConvertContextUtils.AddComponent<SkinnedMeshRenderer>(dest);
        }
    }
    protected override GameObject UpdateOtherComponents(GameObject src, GameObject dest, ContextObject co)
    {
        UpdateSkinMeshRenderer1(src, dest);
        return dest;
    }
}