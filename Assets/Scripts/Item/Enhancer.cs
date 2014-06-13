using UnityEngine;
using System.Collections;

[System.Serializable]
public class Enhancer : Item {

	public Enhancer() : base(){}

	protected Enhancer(Enhancer other) : base(other)
	{

	}

	public override Item Clone ()
	{
		return new Enhancer(this);
	}

	public override string GetItemDescription ()
	{
		string s = base.ItemName + "\n" + base.Description;
		return (s);
	}
}
