public class ModelTalents
{
    private int currentTalentPoints = 0;
    private int maxTallentPoints = 0;

    public int GetTalentPoints()
    {
        return currentTalentPoints;
    }
    
    public void RemoveTalentPoint()
    {
        if (!RemoveTalentEnable()) return;
        currentTalentPoints--;
    }

    public void RemoveAllTalentPoints()
    {
        currentTalentPoints = maxTallentPoints;
    }
    
    public bool RemoveTalentEnable()
    {
        if (GetTalentPoints() <= 0) return false;
        return true;
    }
    public void AddTalentPoint()
    {
        currentTalentPoints++;
        maxTallentPoints++;
    }
}
