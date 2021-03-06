using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MyUnitTester
{
    private string turn;

    // A Test behaves as an ordinary method
    [Test]
    public void MyUnitTesterSimplePasses()
    {
        // Use the Assert class to test conditions

        //Generate tile
        
    }


    [Test]
    public void EnemyTurnBegins()
    {
        // Assert that it is enemy's turn
        Assert.That(turn == "enemy", "It is not enemy turn");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MyUnitTesterWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
