using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class deleteWaste : XRGrabInteractable
{
    GameObject _wasteSpawner;
    void Start(){
        _wasteSpawner =  GameObject.Find("WasteSpawner");
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        if (CompareTag("Waste"))
        {
            _wasteSpawner.GetComponent<spawnWaste>().cnt -= 1;
            Destroy(gameObject);
        }
    }
}
