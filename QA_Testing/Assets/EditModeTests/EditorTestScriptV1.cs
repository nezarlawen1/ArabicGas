using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditorTestScriptV1
{
   // //A Test behaves as an ordinary method
   //[Test]
   // public void EditorTestScriptV1SimplePasses()
   // {
   //     FunctionsVault functionsVault = new FunctionsVault();
   //     Assert.IsTrue(functionsVault.MyFunc());
   // }


    [Test]
    // Should Fail - Meant To Test If(PlayerData == SaveData) After A Data Reset Occured.
    public void EditorTestScriptV1LoadOne()
    {
        FunctionsVault functionsVault = new FunctionsVault();
        functionsVault.InitSaveSystem();
        functionsVault.DataReset();
        Assert.AreEqual(functionsVault.PlayerHP, functionsVault.RetrieveSaveFileHP());
    }

    [Test]
    // Should Succeed - Meant To Test If(PlayerData == SaveData) After Proper Save & Load Occur.
    public void EditorTestScriptV1LoadTwo()
    {
        FunctionsVault functionsVault = new FunctionsVault();
        functionsVault.InitSaveSystem();
        functionsVault.SaveToJson();
        functionsVault.LoadFromJson();
        Assert.AreEqual(functionsVault.PlayerHP, functionsVault.RetrieveSaveFileHP());
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EditorTestScriptV1WithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
