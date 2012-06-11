namespace Snowshoes.Common
{
    public class Waypoint
    {
        public Waypoint(D3.Point point, D3.SNOLevelArea levelArea, D3.SNOQuestId questId, int questStep = 0)
        {
            Point = point;
            LevelArea = levelArea;
            QuestId = questId;
            QuestStep = questStep;
        }
        private D3.Point Point
        { get; set; }
        private D3.SNOLevelArea LevelArea
        { get; set; }
        private D3.SNOQuestId QuestId
        { get; set; }
        private int QuestStep
        { get; set; }
    }
}
