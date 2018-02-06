using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour {

    [SerializeField]
    private GameObject slotPrefab;      //The slot prefab to be instantiated in the bag

    private CanvasGroup canvasGroup;

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

	public void AddSlots(int slotCount)     // Sets the bag to the right size
    {
        for(int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, transform);
        }
    }

    public void OpenCloseBag()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;      //Toggles between shown and invisible
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;     //toggles between block raycast
    }
}
