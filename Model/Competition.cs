using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants = new List<IParticipant>();
        public Queue<Track> Tracks = new Queue<Track>();

        public Track NextTrack()
        {
            // Opgave 2.5
            if (this.Tracks == null || this.Tracks.Count == 0)
            {
                return null;
            }

            return this.Tracks.Dequeue();
        }
    }
}
