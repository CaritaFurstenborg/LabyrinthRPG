using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour {

    private static SpellBook instance;    

    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }

            return instance;
        }
    }      

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellText;

    [SerializeField]
    private Text castTimeText;

    [SerializeField]
    private CanvasGroup canGroup;

    [SerializeField]
    private Spell[] spells;

    private Coroutine spellRout;

    private Coroutine fadeRout;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Spell CastSpell(string name)
    {
        Spell spell = Array.Find(spells, x => x.MyName == name);

        castingBar.fillAmount = 0f;

        castingBar.color = Color.green;
        spellText.text = spell.MyName;        

        spellRout = StartCoroutine(Progress(spell));

        fadeRout = StartCoroutine(FadeBar());

        return spell;
    }

    private IEnumerator Progress(Spell spell)
    {
        float timeLeft = Time.deltaTime;

        float rate = 1.0f / spell.MyCastTime;

        float progress = 0.0f;

        while(progress <= 1.0f)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timeLeft += Time.deltaTime;

            castTimeText.text = (spell.MyCastTime - timeLeft).ToString("F1");

            if(spell.MyCastTime - timeLeft < 0)
            {
                castTimeText.text = "0.0";
            }

            yield return null;
        }

        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1.0f / 0.50f;

        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            canGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public void StopCasting()
    {
        if(fadeRout != null) {
            StopCoroutine(fadeRout);
            canGroup.alpha = 0;
            fadeRout = null;
        }

        if(spellRout != null)
        {
            StopCoroutine(spellRout);
            spellRout = null;
        }
    }

    public Spell GetSpell(string spellName)      // get a spell for ui manager IUseable
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);

        return spell;
    }

    /*private IEnumerator Cooldown()
    {

    }*/
}
