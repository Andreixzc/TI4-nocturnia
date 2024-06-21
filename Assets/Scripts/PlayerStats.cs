using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    //Current stats
   [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;

    #region Current Stats Properties
    public float CurrentHealth
    {
        get {return currentHealth;}
        set
        {
            if(currentHealth != value)
            {
                currentHealth = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health " + currentHealth; 
                }
            }
        }
    }
   
    public float CurrentRecovery
    {
        get {return currentRecovery;}
        set
        {
            if(currentRecovery != value)
            {
                currentRecovery = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery " + currentRecovery; 
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get {return currentMoveSpeed;}
        set
        {
            if(currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed " + currentMoveSpeed; 
                }
            }
        }
    }

    public float CurrentMight
    {
        get {return currentMight;}
        set
        {
            if(currentMight != value)
            {
                currentMight = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might " + currentMight; 
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get {return currentProjectileSpeed;}
        set
        {
            if(currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileDisplay.text = "Projectile Speed " + currentProjectileSpeed; 
                }
            }
        }
    }
    #endregion

    [Header("I-Frames")]
    public float invincibilityDuration;
    public float invincibilityTimer;
    public bool isInvincible;

    [Header("UI")]

    public Image healthBar;
    public ParticleSystem damageEffect;
   
    
    void Awake()
    {
        //Assign the variables
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
    }

    void Start()
    {
        GameManager.instance.currentHealthDisplay.text = "Health " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery " + currentRecovery; 
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed " + currentMoveSpeed; 
        GameManager.instance.currentMightDisplay.text = "Might " + currentMight; 
        GameManager.instance.currentProjectileDisplay.text = "Projectile Speed " + currentProjectileSpeed; 

    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;
        }
    }


    public void Kill()
    {
        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.GameOver();
        }
    }

    void UpdateHealthBar(){
        healthBar.fillAmount = CurrentHealth / characterData.MaxHealth;
    }

    public void TakeDamage(float dmg){

    if (!isInvincible){
    currentHealth -= dmg;

    if (damageEffect) Instantiate(damageEffect, transform.position, Quaternion.identity);
    invincibilityTimer = invincibilityDuration;
    isInvincible = true;

    if (currentHealth <= 0){
    Kill();
    }

    UpdateHealthBar();
    }
    }


}
