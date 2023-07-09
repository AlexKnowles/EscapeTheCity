

public static class WorldTileTypeComparitorsExtensions
{
    public static bool IsSatisfied(this WorldTileTypeComparitor comparitor, WorldTileType currentTileType, WorldTileType surroundTileType)
    {
        if(comparitor == WorldTileTypeComparitor.AnyType)
        {
            return true;
        }

        if (comparitor == WorldTileTypeComparitor.AnyTypeOtherThanCurrent)
        {
            return (currentTileType != surroundTileType);
        }

        if( comparitor == WorldTileTypeComparitor.Pavement)
        {
            return (surroundTileType == WorldTileType.Pavement);
        }

        if (comparitor == WorldTileTypeComparitor.Grass)
        {
            return (surroundTileType == WorldTileType.Grass);
        }

        if (comparitor == WorldTileTypeComparitor.Water)
        {
            return (surroundTileType == WorldTileType.Water);
        }

        if (comparitor == WorldTileTypeComparitor.Road)
        {
            return (surroundTileType == WorldTileType.Road);
        }

        return false;
    }
}