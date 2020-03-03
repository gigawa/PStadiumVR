using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMenu : MonoBehaviour
{
    public delegate void AttackSelected(int index);
    public event AttackSelected onAttackSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectAttack(int index)
    {
        Debug.Log("Menu - Attack Selected: " + index);
        onAttackSelected(index);
    }
}
