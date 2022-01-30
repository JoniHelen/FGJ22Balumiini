using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
public class Battle : MonoBehaviour
{
    public static Battle Initiate;
    [SerializeField] TextMeshProUGUI Attacker;
    [SerializeField] TextMeshProUGUI Defender;
    [SerializeField] TextMeshProUGUI Forecast;

    private void Awake()
    {
        if (Initiate == null)
        {
            Initiate = this;
        }
        else
            Destroy(gameObject);
    }

    public void CombatRound(Creature attacker, Creature defender)
    {
        //display HP
        Attacker.text = 
            $"HP: {attacker.HP}/{attacker.maxHP}\nATK: {attacker.Strength}\nDEF: {attacker.PhysicalDefense}";
        Defender.text = 
        $"HP: {defender.HP}/{defender.maxHP}\nATK: {defender.Strength}\nDEF: {defender.PhysicalDefense}";

        Forecast.text = $"DMG\n{attacker.Strength-defender.PhysicalDefense} VS {defender.Strength-attacker.PhysicalDefense}";
    }

}
