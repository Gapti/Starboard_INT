using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon : Item {

	public float MinDamage;
	public float MaxDamage;
	public float MinRange;
	public float MaxRange;
	public int CriticalStrike;

	protected Weapon(): base(){}

	protected Weapon(Weapon other) : base(other)
	{
		this.MinDamage = other.MinDamage;
		this.MaxDamage = other.MaxDamage;
		this.MinRange = other.MinRange;
		this.MaxRange = other.MaxRange;
		this.CriticalStrike = other.CriticalStrike;
	}
	
	public override Item Clone()
	{
		return new Weapon(this);
	}

	public override string GetItemDescription ()
	{
		string s = base.ItemName + "\n" + "[0088FF]" + base.Description + "\n" + "Damage: " + MinDamage + " / " + MaxDamage + "\n" + "Range: " + MinRange + " / " + MaxRange + "\n" + "CriticalStrike: " + CriticalStrike;
		return (s);
	}
}
