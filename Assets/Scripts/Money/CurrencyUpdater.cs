using UnityEngine;
using System.Collections;

public class CurrencyUpdater : MonoBehaviour {

	private UILabel MoneyLabel;

	void OnEnable()
	{
		Currency.OnMoneyChange += HandleOnMoneyChange;
	}

	void HandleOnMoneyChange (int obj)
	{
		MoneyLabel.text = obj + " Troquer" ;
	}

	void OnDisable()
	{
		Currency.OnMoneyChange -= HandleOnMoneyChange;
	}


	// Use this for initialization
	void Awake () 
	{
		MoneyLabel = GetComponent <UILabel> ();
	}

	void Start()
	{
		HandleOnMoneyChange (Currency.Money);
	}

}
