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
        DisplayForecast(attacker, defender);

        StartCoroutine(Turn(attacker, defender));

    }

    private void DisplayForecast(Creature attacker, Creature defender)
    {
        //display HP
        Attacker.text =
            $"HP: {attacker.HP}/{attacker.maxHP}\nATK: {attacker.Strength}\nDEF: {attacker.PhysicalDefense}";
        Defender.text =
        $"HP: {defender.HP}/{defender.maxHP}\nATK: {defender.Strength}\nDEF: {defender.PhysicalDefense}";

        Forecast.text = $"DMG\n{attacker.Strength - defender.PhysicalDefense} <=> {defender.Strength - attacker.PhysicalDefense}";
    }

    WaitForSeconds turn = new WaitForSeconds(0.5f);

    IEnumerator Turn(Creature attacker, Creature defender)
    {
        yield return new WaitForSeconds(1);
        defender.TakeDamage(Mathf.Max(attacker.Strength - defender.PhysicalDefense, 0));
        DisplayForecast(attacker, defender);
        yield return turn;
        attacker.TakeDamage(Mathf.Max(defender.Strength - attacker.PhysicalDefense, 0));
        DisplayForecast(attacker, defender);
    }

}
