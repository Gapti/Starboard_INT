public class INTAttribute
{
    public float Base;
    public float BaseModifier;
    public float Multiplier = 1f;
    public float Total;

    public float Damage;

    /// <summary>
    /// takes the base, modifier and multiplier to compute the total attribute value. 
    /// </summary>
    public void CalculateTotal()
    {
        Total = (Base + BaseModifier)*Multiplier;
    }
}