using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Model;
using System.Diagnostics;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random = new Random();
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private Timer _timer;

        public delegate void DriversChanged(Object sender, DriversChangedEventArgs args);
        public event DriversChanged DriverChangedEvent;

        public Race(Track track, List<IParticipant> participants)
        {
            // Set the participants and the current track
            this.Participants = participants;
            this.Track = track;

            this.RandomizeEquipment();
            this.GiveParticipantsStartPosition();

            this._timer = new Timer(500);
            this._timer.Elapsed += OnTimedEvent;
            this._timer.Start();
            Debug.WriteLine("RACE");
        }

        private void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
#if DEBUG
            this._timer.Stop();
#endif
            updateDrivers();
#if DEBUG
            this._timer.Start();
#endif
        }

        private void updateDrivers()
        {
            var sectionNode = this.Track.Sections.First;

            for (int i = 0; i < this.Track.Sections.Count; i++)
            {
                var sectionData = this.GetSectionData(sectionNode.Value);

                if (sectionData.Left != null)
                {
                    sectionData.DistanceLeft += (sectionData.Left.Equipment.Performance * sectionData.Left.Equipment.Speed);

                    if (sectionData.DistanceLeft > 50)
                    {
                        var nextSectionData = this.GetSectionData(sectionNode.Next == null ? this.Track.Sections.First.Value : sectionNode.Next.Value);
                        if (nextSectionData.Left == null)
                        {
                            nextSectionData.Left = sectionData.Left;
                            nextSectionData.DistanceLeft = sectionData.DistanceLeft - 50;
                            sectionData.Left = null;

                            this.RandomizeEquipment();

                            this.DriverChangedEvent?.Invoke(this, new DriversChangedEventArgs(this.Track));
                        }
                    }
                }

                if (sectionData.Right != null)
                {
                    sectionData.DistanceRight += (sectionData.Right.Equipment.Performance * sectionData.Right.Equipment.Speed);

                    if (sectionData.DistanceRight > 50)
                    {
                        var nextSectionData = this.GetSectionData(sectionNode.Next == null ? this.Track.Sections.First.Value : sectionNode.Next.Value);
                        if (nextSectionData.Right == null)
                        {
                            nextSectionData.Right = sectionData.Right;
                            nextSectionData.DistanceRight = sectionData.DistanceRight - 50;
                            sectionData.Right = null;

                            this.RandomizeEquipment();

                            this.DriverChangedEvent?.Invoke(this, new DriversChangedEventArgs(this.Track));
                        }
                    }
                }

                sectionNode = sectionNode.Next;
            }
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
                this.Participants[i].Equipment.Speed = this._random.Next(5, 10);
                this.Participants[i].Equipment.Performance = this._random.Next(1, 3);
            }
        }

        // 4-3
        public void GiveParticipantsStartPosition()
        {
            int NumberOfParticipant = 0;
            var sectionNode = this.Track.Sections.First;
            for (int i = 0; i < this.Track.Sections.Count; i++)
            {
                SectionData sectionData = this.GetSectionData(sectionNode.Value);
                if (NumberOfParticipant < this.Participants.Count)
                {
                    sectionData.Left = this.Participants[NumberOfParticipant];
                    NumberOfParticipant += 1;
                }

                if (NumberOfParticipant < this.Participants.Count)
                {
                    sectionData.Right = this.Participants[NumberOfParticipant];
                    NumberOfParticipant += 1;
                }

                sectionNode = sectionNode.Previous;

                if (sectionNode == null)
                {
                    sectionNode = this.Track.Sections.Last;
                }
            }
        }
    }
}
