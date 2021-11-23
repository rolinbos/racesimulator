using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();

        public Race(Track track, List<IParticipant> participants)
        {
            // Set the participants and the current track
            this.Participants = participants;
            this.Track = track;

            this.RandomizeEquipment();
        }

        /**
         * Lijst van posities. Section is bijv. links rechts etc
         * Je haalt hem op aan de hand van de section en daarmee krijg je de informatie van de section door de sectionData.
         * Opgave 2.4
         */
        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions[section] = new SectionData();
            }

            return _positions[section];
        }

        // Opgave 2.4
        public void RandomizeEquipment()
        {
            for (int i = 0; i < this.Participants.Count; i++)
            {
                this.Participants[i].Equipment.Quantity = this._random.Next();
                this.Participants[i].Equipment.Performance = this._random.Next();
            }
        }
    }
}
