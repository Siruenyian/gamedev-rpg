using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FireAltar : MonoBehaviour
{
    Animator animator;
    [SerializeField] private bool isOn;
    void Start()
    {
        animator = GetComponent<Animator>();
        Toggle(isOn);
    }

    public void Toggle(bool isOn)
    {
        if (isOn)
        {
            animator.Play("FireAltarIdle");
        }
        else
        {
            animator.Play("FireAltarOff");
        }
    }
}