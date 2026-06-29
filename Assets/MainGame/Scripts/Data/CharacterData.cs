using UnityEngine;

[CreateAssetMenu(menuName = "Character/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public RuntimeAnimatorController animatorController;
    public Sprite sprite;
    public int maxHP;
    public int maxMana;
    public int attack;
    public int magic;
    public int defense;
    public int speed;

}
