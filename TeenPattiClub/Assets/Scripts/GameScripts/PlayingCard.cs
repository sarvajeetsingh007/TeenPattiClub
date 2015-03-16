using UnityEngine;
using System.Collections;

public class PlayingCard : MonoBehaviour {

	public int value;
	public int suit;

	public PlayingCard(int val, int suit)
	{
		this.value = val;
		this.suit = suit;
	}
}
