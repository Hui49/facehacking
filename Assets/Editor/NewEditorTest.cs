using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class NewEditorTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
		var gameObject = GameObject.Find("shape 2").transform.Find("Anakin").gameObject;
		ModelFaceController2 controller = gameObject.AddComponent<ModelFaceController2> ();
        //We register the newly create GameObject so it will be automatically removed after the test run
		//controller.setI();

    }
}
