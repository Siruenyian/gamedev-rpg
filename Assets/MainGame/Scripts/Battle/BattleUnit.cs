using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleUnit : MonoBehaviour
{
    public CharacterData data { get; private set; }
    private Animator animator;
    private SpriteRenderer sprite;
    private Color originalColor;
    private Coroutine flashRoutine;
    private int currentHP;
    public int CurrentHP => currentHP;

    [SerializeField] private HPBar healthBar;
    private int defense;
    private int originalDefense;
    void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }
    public void Initialize(CharacterData characterData)
    {
        data = characterData;
        sprite.sprite = data.sprite;
        currentHP = data.maxHP;
        defense = data.defense;
        originalDefense = defense;
        animator.runtimeAnimatorController = data.animatorController;
        healthBar.SetHealth(currentHP, data.maxHP);
        healthBar.SetName(data.characterName);
    }
    public int TakeDamage(int damage)
    {
        // int trueDamage = Mathf.Max(1, damage - defense);

        int trueDamage = Mathf.Max(1, damage - defense);
        defense = originalDefense;
        currentHP = Mathf.Max(0, currentHP - trueDamage);
        healthBar.SetHealth(currentHP, data.maxHP);
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(DamageEffect(Color.red));
        return trueDamage;
    }
    public int Defend()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(DamageEffect(Color.blue, 0, 0.6f));
        defense *= 2;
        return defense;
    }
    public bool Run()
    {
        return false;
    }
    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public IEnumerator DamageEffect(Color color, float shakeAmount = 0.08f, float duration = 0.2f)
    {
        Vector3 originalPos = transform.localPosition;

        float timer = 0f;

        sprite.color = color;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            transform.localPosition = originalPos +
                (Vector3)UnityEngine.Random.insideUnitCircle * shakeAmount;

            yield return null;
        }

        transform.localPosition = originalPos;
        sprite.color = originalColor;
    }

}