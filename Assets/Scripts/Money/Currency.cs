using UnityEngine;
using System.Collections;


/// <summary>
/// Handles currency/money.
/// </summary>
[System.Serializable]
public class Currency : MonoBehaviour {
	// Current money total
	private static int money = 0;
	public static int Money {
		get {
			return money;
		}
		set	{
			money = value;
				}
	}
	

	/// <summary>
	/// Adds an amount of money to the total
	/// </summary>
	/// <returns><c>true</c>, if money was added, <c>false</c> otherwise.</returns>
	/// <param name="amount">Amount of money.</param>
	public static bool AddMoney(int amount) {
		bool gotEnoughMoney = GotEnoughMoney(-amount);

		if (gotEnoughMoney)
		{
			money += amount;
		}
		return gotEnoughMoney;
	}


	/// <summary>
	/// Removes an amount of money from the total
	/// </summary>
	/// <returns><c>true</c>, if money was removed, <c>false</c> otherwise.</returns>
	/// <param name="amount">Amount of money.</param>
	public static bool RemoveMoney(int amount) {
		return AddMoney(-amount);
	}


	/// <summary>
	/// Checks if there is enough money to subtract given amount.
	/// </summary>
	/// <returns><c>true</c>, if enough, <c>false</c> otherwise.</returns>
	/// <param name="amount">Amount of money.</param>
	public static bool GotEnoughMoney(int amount) {
		return (money - amount) >= 0;
	}
}
