using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuestItem : Item {

	public QuestItem() : base() {}

	protected QuestItem(QuestItem other) : base(other)
	{

	}

	public override Item Clone ()
	{
		return new QuestItem(this);
	}

	public override string GetItemDescription ()
	{
		string s = base.ItemName + "\n" + base.Description + "\n";
		return (s);
	}
}
