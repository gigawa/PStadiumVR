using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackMenu : MonoBehaviour
{
    public delegate void AttackSelected(int index);
    public event AttackSelected onAttackSelected;

    public List<Text> AttackListText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttackText(string [] attackNames)
    {
        for(int i = 0; i < attackNames.Length; i++)
        {
            AttackListText[i].text = attackNames[i];
        }
    }

    public void SelectAttack(int index)
    {
        Debug.Log("Menu - Attack Selected: " + index);
        onAttackSelected(index);
    }
}
