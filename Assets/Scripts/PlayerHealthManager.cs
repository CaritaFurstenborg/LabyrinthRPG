using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public int playerMaxHealth;
    public int playerCurrentHealth;

    public float flashLength;

    private float flashCounter;
    private bool flashActive;

    private SpriteRenderer pSprite;

    // Use this for initialization
    void Start () {
        playerCurrentHealth = playerMaxHealth;
        pSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        if(flashActive)
        {
            if(flashCounter > flashLength * 0.66f)
            {
                pSprite.color = new Color(pSprite.color.r, pSprite.color.g, pSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                pSprite.color = new Color(pSprite.color.r, pSprite.color.g, pSprite.color.b, 1f);
            } 
            else if (flashCounter > 0)
            {
                pSprite.color = new Color(pSprite.color.r, pSprite.color.g, pSprite.color.b, 0f);
            }
            else
            {
                pSprite.color = new Color(pSprite.color.r, pSprite.color.g, pSprite.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
	}

    public void HurtPlayer(int damageToTake)
    {
        playerCurrentHealth -= damageToTake;

        flashActive = true;
        flashCounter = flashLength;
    }

    public void HealPlayer(int damageToHeal)
    {
        playerCurrentHealth += damageToHeal;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
