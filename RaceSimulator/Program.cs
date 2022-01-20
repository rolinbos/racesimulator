using System;
using System.Collections.Generic;
using System.Threading;
using Controller;
using Model;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Competition competition = new Competition();
            Data.Initialize(competition);
            Data.NextRace();
            Visualisation.Initialize();
            Visualisation.DrawTrack(Data.CurrentRace.Track);

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
