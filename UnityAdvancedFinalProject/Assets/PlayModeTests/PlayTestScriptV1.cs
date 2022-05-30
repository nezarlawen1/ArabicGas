using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayTestScriptV1
{
    //// A Test behaves as an ordinary method
    //[Test]
    //public void PlayTestScriptV1SimplePasses()
    //{
    //    // Use the Assert class to test conditions
    //}

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayTestScriptV1WithEnumeratorPasses()
    {
        // Loading Scene 1
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1);


        // Moving Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.name);
        Vector3 pos = player.transform.position;
        for (int i = 0; i < 25; i++)
        {
            Vector3 tempDirection = Vector3.forward;
            Vector3 moveTarget = player.transform.position + tempDirection;
            player.transform.position = Vector3.MoveTowards(player.transform.position, moveTarget, 10 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        for (int i = 0; i < 35; i++)
        {
            Vector3 tempDirection = Vector3.right;
            Vector3 moveTarget = player.transform.position - tempDirection;
            player.transform.position = Vector3.MoveTowards(player.transform.position, moveTarget, 10 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Assert.AreNotEqual(pos, player.transform.position);
        yield return new WaitForSeconds(1);


        // Checking If Reached Healing Station & Healing Player
        TerminalOpen healingStation = GameObject.Find("Healing Station (1)").GetComponentInChildren<TerminalOpen>();
        Assert.IsTrue(healingStation.IsColliding == true);
        if (healingStation.IsColliding == true)
        {
            healingStation.Interact.onClick.Invoke();
        }
        yield return new WaitForSeconds(1);


        // Finding The First Enemy & Checking If PLayerInSight
        EnemyAI firstEnemy = GameObject.Find("thc6").GetComponent<EnemyAI>();
        Debug.Log(firstEnemy.name);
        Assert.IsTrue(firstEnemy.playerInsightRange);
        yield return new WaitForSeconds(1);

        // Finding The Second Enemy & Checking If PLayerInSight NOT
        EnemyAI secondEnemy = GameObject.Find("thc6 (3)").GetComponent<EnemyAI>();
        Debug.Log(secondEnemy.name);
        Assert.IsFalse(secondEnemy.playerInsightRange);
        yield return new WaitForSeconds(1);
    }
}
