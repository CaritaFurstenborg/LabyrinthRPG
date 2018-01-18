using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    private Slider content;

    [SerializeField]
    private Text statValue;

    [SerializeField]
    private float lerpSpeed;

    private float currentFill;

    public float MyMaxValue { get; set; } //Property

    private float currentValue;

    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if(value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if(value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;
            statValue.text = currentValue + "/" + MyMaxValue;    
        }
    } // Property for current value

    // Use this for initialization
    void Start () {
        content = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        /*if(currentFill != content.value)
        {
            content.value = Mathf.Lerp(content.value, currentFill, Time.deltaTime * lerpSpeed);
        }*/
        content.value = currentValue;
	}

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
