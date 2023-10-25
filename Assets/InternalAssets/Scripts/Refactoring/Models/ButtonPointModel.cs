public class ButtonPointModel
{
    public void RecivePoint()
    {
        TalentEvents.current.TalentPointAdded.Invoke(1);
    }

    public void RemoveAllPoints()
    {
        TalentEvents.current.AllPointsReset.Invoke();
    }
}
