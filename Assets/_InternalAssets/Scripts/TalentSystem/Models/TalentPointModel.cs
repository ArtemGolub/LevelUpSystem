public class TalentPointModel
{
    private int currentTalentPoints = 0;
    private int maxTallentPoints = 0;
    
    public int GetTalentPoints()
    {
        return currentTalentPoints;
    }
    public void RemoveTalentPoint(int amount)
    {
        if (!RemoveTalentEnable(amount)) return;
        currentTalentPoints--;
    }
    public void AddTalentPoint(int amount)
    {
        currentTalentPoints++;
        maxTallentPoints++;
    }

    public void RemoveAllTalentPoints()
    {
        currentTalentPoints = maxTallentPoints;
    }
    
    public void ReciveTalentPoint(int amount)
    {
        if(currentTalentPoints > maxTallentPoints) return;
        currentTalentPoints += amount;
    }

    private bool RemoveTalentEnable(int amount)
    {
        if (GetTalentPoints() - amount < 0) return false;
        return true;
    }
}
