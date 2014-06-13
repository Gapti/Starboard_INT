
public abstract class INTAttributeModifier
{
    public abstract void ApplyModifiers(INTAttribute attribute, INTAttributeTypes type);
}

public class BaseHealthModifier : INTAttributeModifier
{
    public override void ApplyModifiers(INTAttribute attribute, INTAttributeTypes type)
    {
        if (type == INTAttributeTypes.Health)
            attribute.BaseModifier += 100;
    }
}