public class ModelTalents
{
    private int talentPoints = 0;

    public int GetTalentPoints()
    {
        return talentPoints;
    }
    
    public void RemoveTalentPoint()
    {
        if (!RemoveTalentEnable()) return;
        talentPoints--;
    }
    public bool RemoveTalentEnable()
    {
        if (GetTalentPoints() <= 0) return false;
        return true;
    }
    public void AddTalentPoint()
    {
        talentPoints++;
    }
}
