using UnityEngine;
using System.Collections;

[System.Serializable]
public class Misc : Item {

	public Misc() : base(){}

	protected Misc(Misc other) : base(other)
	{

	}

	public override Item Clone ()
	{
		return new Misc(this);
	}

	public override string GetItemDescription ()
	{
		string s = base.ItemName + "\n" + base.Description;
		return (s);
	}
}
