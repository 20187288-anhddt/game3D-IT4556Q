using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Common;

public class BuildPooling : CustomPool<BaseBuild>
{
    public BuildPooling(BaseBuild pPrefab, int pInitialCount, Transform pParent, bool pBuildinPrefab, string pName = "", bool pAutoRelocate = true) : base(pPrefab, pInitialCount, pParent, pBuildinPrefab, pName, pAutoRelocate) { }
}
