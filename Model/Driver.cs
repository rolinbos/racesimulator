using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
        public int Laps { get; set; }

        public Driver(string name, int points, IEquipment equipment, TeamColors teamColor, int laps = 0)
        {
            this.Name = name;
            this.Points = points;
            this.Equipment = equipment;
            this.TeamColor = teamColor;
            this.Laps = laps;
        }
    }
}
