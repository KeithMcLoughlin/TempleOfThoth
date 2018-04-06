using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrialRoom
{
    GameObject Intialise(Transform position, ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits);
}
