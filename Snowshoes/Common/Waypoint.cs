using System;

namespace Snowshoes.Common
{
    public class Waypoint
    {
        public Waypoint(D3.Point point, D3.SNOLevelArea levelArea, D3.SNOQuestId questId, int questStep = 0)
        {
            _point = point;
            _levelArea = levelArea;
            _questId = questId;
            _questStep = questStep;
        }
        private D3.Point _point
        { get; set; }
        private D3.SNOLevelArea _levelArea
        { get; set; }
        private D3.SNOQuestId _questId
        { get; set; }
        private int _questStep
        { get; set; }
    }
}
