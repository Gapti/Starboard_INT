using UnityEngine;
using System.Collections;

[System.Serializable]
public class Generator : Item {

	public Generator() : base(){}

	protected Generator(Generator other) : base(other)
	{

	}

	public override Item Clone ()
	{
		return new Generator(this);
	}

	public override string GetItemDescription ()
	{
		string s = base.ItemName + "\n" + base.Description;
		return (s);
	}
}
