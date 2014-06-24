using UnityEngine;
using System.Collections;

public class SplitBoxControl : MonoBehaviour {

	public UILabel SplitNumberLabel;
	private int _MaxSplitAmount;
	private int _SplitAmount = 0;


	public int MaxSplitAmount
	{
		set
		{
			_MaxSplitAmount = value;
			UpdateLabel();
		}
	}

	public void Increase()
	{
		if (_SplitAmount + 1 < _MaxSplitAmount) 
		{
			_SplitAmount += 1;

			UpdateLabel ();
		}

	}

	public void Decrease()
	{
		if (_SplitAmount - 1 > -1) 
		{
			_SplitAmount -= 1;
			
			UpdateLabel ();
		}
	}

	void UpdateLabel()
	{
		SplitNumberLabel.text = _SplitAmount.ToString();

	}
}
