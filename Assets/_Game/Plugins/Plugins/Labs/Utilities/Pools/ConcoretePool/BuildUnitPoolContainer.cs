using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Common;

public class BuildUnitPoolContainer : PoolsContainer<BaseBuild, BuildUnitPoolContainer>
{
    public BuildUnitPoolContainer(Transform pContainer) : base(pContainer)
    {
    }
}
