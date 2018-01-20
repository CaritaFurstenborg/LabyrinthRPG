using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour {

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellText;

    [SerializeField]
    private Spell[] spells;

    private Coroutine spellRout;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Spell CastSpell(int index)
    {
        castingBar.color = Color.green;
        spellText.text = spells[index].MyName;

        castingBar.fillAmount = 0;

        spellRout = StartCoroutine(Progress(index));

        return spells[index];
    }

    private IEnumerator Progress(int index)
    {
        float timeLeft = Time.deltaTime;

        float rate = 1.0f / spells[index].MyCastTime;

        float progress = 0.0f;

        while(progress <= 1.0f)
        {
            Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }
}
