using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrialRoom
{
    GameObject Intialise(Vector3 position, ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits);
}
