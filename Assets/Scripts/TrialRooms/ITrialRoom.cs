using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;

public abstract class ITrialRoom : MonoBehaviour
{
    public delegate void TrialCompletedDelegate(Transform nextTrialPosition);
    public event TrialCompletedDelegate OnTrialCompleted;
    abstract public void Intialise(Transform position);
    abstract public void ProvideSetupInstructions(ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits);
    public ITrialEventDocumentGenerator DocumentGeneratorForTrial;
    public Transform nextTrialPosition;
    public string TrialName;

    protected void TrialFinished()
    {
        //notify subscribers that the trial completed
        if (OnTrialCompleted != null)
        {
            OnTrialCompleted(this.nextTrialPosition);
        }
    }
}
