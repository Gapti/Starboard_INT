using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(INTCharacter))]
public class CharacterDamageTester : Editor
{

    public override void OnInspectorGUI()
    {
        if (NGUIEditorTools.DrawPrefixButton("Take Damage"))
        {
            var character = (INTCharacter) target;
            character[INTAttributeTypes.Health].Damage += 10;
            Debug.Log(character.GetCurrentPercentageOf(INTAttributeTypes.Health));
        }
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
