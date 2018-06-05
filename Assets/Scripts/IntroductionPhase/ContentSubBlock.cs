using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSubBlock : MonoBehaviour {

	public bool hasFinished = false;
	public bool isInMainRoute = true;
    public bool isActive = true;

    private void OnEnable()
    {
        isActive = true;
    }

    protected virtual void Start() {
        
    }

    protected virtual void SwitchDynamicObjectStatus()
    {
        
    }

    public virtual void Pause() {
        isActive = !isActive;
    }
}
