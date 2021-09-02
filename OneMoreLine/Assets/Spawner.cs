using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : CSpawnerBase
{
    protected override void OnInit(ref string strSpawnObject_ContainFolderPath_Default_Is_ResourcesPath)
    {
        strSpawnObject_ContainFolderPath_Default_Is_ResourcesPath = Application.dataPath + "/Resources/InGame";
    }
}
