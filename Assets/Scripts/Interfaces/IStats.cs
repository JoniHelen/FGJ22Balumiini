using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    int HP { get; set; }
    int maxHP { get; }
    int Strength { get; }
    int SpiritualProwess { get; }
    int PhysicalDefense { get; }
    int SpiritualDefense { get; }
    bool IsSpiritual { get; set; }
}
