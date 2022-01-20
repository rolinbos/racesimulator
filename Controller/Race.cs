using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        public int Laps = 1;

        private Random _random = new Random();
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private Timer _timer;

        public delegate void onDriversChanged(Object sender, DriversChangedEventArgs args);
        public event onDriversChanged DriverChanged;

        public delegate void onNextRace(object Sender, RaceStartEventArgs nextRaceEventArgs);
        public event onNextRace NextRace;

        public int participantFinished = 0;

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
            
            this.RandomizeEquipment();

            for (int i = 0; i < this.Track.Sections.Count; i++)
            {
                var sectionData = this.GetSectionData(sectionNode.Value);

                if (sectionData.Left != null && !sectionData.Left.Equipment.IsBroken)
                {
                    sectionData.DistanceLeft += (sectionData.Left.Equipment.Performance * sectionData.Left.Equipment.Speed);

                    if (sectionData.DistanceLeft > 50)
                    {
                        var sectionNodeNext = sectionNode.Next == null ? this.Track.Sections.First.Value : sectionNode.Next.Value;
                        var nextSectionData = this.GetSectionData(sectionNodeNext);
                        if (sectionNodeNext.SectionType == SectionTypes.Finish)
                        {
                            sectionData.Left.Laps += 1;
                        }
                        if (sectionData.Left.Laps >= this.Laps)
                        {
                            GivePoints(sectionData.Left);
                            sectionData.Left = null;
                            participantFinished += 1;
                        } else 
                        {
                            if (nextSectionData.Left == null)
                            {
                                nextSectionData.Left = sectionData.Left;
                                nextSectionData.DistanceLeft = sectionData.DistanceLeft - 50;
                                sectionData.Left = null;
                            }
                        }
                    }
                }

                if (sectionData.Right != null && !sectionData.Right.Equipment.IsBroken)
                {
                    sectionData.DistanceRight += (sectionData.Right.Equipment.Performance * sectionData.Right.Equipment.Speed);

                    if (sectionData.DistanceRight > 50)
                    {
                        var sectionNodeNext = sectionNode.Next == null ? this.Track.Sections.First.Value : sectionNode.Next.Value;
                        var nextSectionData = this.GetSectionData(sectionNodeNext);
                        if (sectionNodeNext.SectionType == SectionTypes.Finish)
                        {
                            sectionData.Right.Laps += 1;
                        }
                        if (sectionData.Right.Laps >= this.Laps)
                        {
                            GivePoints(sectionData.Right);
                            sectionData.Right = null;
                            participantFinished += 1;
                        }
                        else
                        {
                            if (nextSectionData.Right == null)
                            {
                                nextSectionData.Right = sectionData.Right;
                                nextSectionData.DistanceRight = sectionData.DistanceRight - 50;
                                sectionData.Right = null;
                            }
                        }
                    }
                }

                sectionNode = sectionNode.Next;

                this.DriverChanged?.Invoke(this, new DriversChangedEventArgs(this.Track));

                if (this.participantFinished == this.Participants.Count)
                {
                    this.StartNextRace();
                }
            }
        }

        private void GivePoints(IParticipant participant)
        {
            if (participant != null)
            {
                participant.Points = Data.Competition.Participants.Count - participantFinished;
            }
        }

        public void StartNextRace()
        {
            _timer.Enabled = false;
            _timer.Stop();
            DriverChanged = null;
            this.participantFinished = 0;
            Data.NextRace();
            this.NextRace(this, new RaceStartEventArgs(Data.CurrentRace));
            //NextRace?.Invoke(this, new RaceStartEventArgs(Data.CurrentRace));
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
                this.Participants[i].Equipment.IsBroken = this._random.Next(1, 100) > 90 ? true : false;
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
                    sectionData.Left.Laps = 0;
                    NumberOfParticipant += 1;
                }

                if (NumberOfParticipant < this.Participants.Count)
                {
                    sectionData.Right = this.Participants[NumberOfParticipant];
                    sectionData.Right.Laps = 0;
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

    public class RaceStartEventArgs : EventArgs
    {
        public Race Race { get; set; }

        public RaceStartEventArgs(Race race)
        {
            Race = race;
        }
    }
}
