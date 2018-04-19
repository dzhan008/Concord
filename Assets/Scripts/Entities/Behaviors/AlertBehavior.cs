using System;

public class AlertBehavior : AIBehavior
{

    public void Initialize(Trigger trigger, Action triggerAction)
    {
        trigger.Initialize(triggerAction);
    }
}
