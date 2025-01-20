using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{

    void Save(ref SaveData.Save save);

    void Load(SaveData.Save save);
}
