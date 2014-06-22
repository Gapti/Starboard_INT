using UnityEngine;
using System.Collections;

public class CurrencyUpdater : MonoBehaviour {

	private UILabel MoneyLabel;

	// Use this for initialization
	void Awake () {
		MoneyLabel = GetComponent <UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		MoneyLabel.text =  "100000 Troquer" ;
	}
}
