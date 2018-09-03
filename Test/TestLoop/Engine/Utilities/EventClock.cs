using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class EventClock
    {
        private static GameTime CurrentGameTime { get; set; }
        private static TimeSpan LastGameTimeWithEvent { get; set; }
        private static List<GameEvent> evtToProcess { get; }  = new List<GameEvent>();
        private static List<TimeSpan> timeSpanToRemove { get; } = new List<TimeSpan>();
        private static SortedList<TimeSpan, List<GameEvent>> EventsToProcess { get; } = new SortedList<TimeSpan, List<GameEvent>>();

        public delegate void GameEvent(GameTime gameTime);

        public static void AddGameEvent(GameEvent eventToProcess, TimeSpan whenToProcess)
        {
            List<GameEvent> events;
            if (EventsToProcess.TryGetValue(whenToProcess, out events))
            {
                events.Add(eventToProcess);
            }
            else
            {
                events = new List<GameEvent>();
                events.Add(eventToProcess);
                EventsToProcess.Add(whenToProcess, events);
            }
        }

        public static void ProcessGameEvents(GameTime currentGameTime)
        {
            CurrentGameTime = currentGameTime;

            TimeSpan cycleDuration = TimeSpan.Zero;
            if (LastGameTimeWithEvent != null)
            {
                cycleDuration = CurrentGameTime.TotalGameTime.Subtract(LastGameTimeWithEvent);
            }
            
            // check for events
            if (EventsToProcess.Keys[0].Ticks <= cycleDuration.Ticks)
            {
                foreach (KeyValuePair<TimeSpan, List<GameEvent>> kvp in EventsToProcess)
                {
                    if (kvp.Key.Ticks <= cycleDuration.Ticks)
                    {
                        timeSpanToRemove.Add(kvp.Key);
                        evtToProcess.AddRange(kvp.Value);
                    }
                }

                if (timeSpanToRemove.Count > 0)
                {
                    LastGameTimeWithEvent = CurrentGameTime.TotalGameTime;

                    // cleanup 
                    foreach (TimeSpan key in timeSpanToRemove)
                    {
                        EventsToProcess.Remove(key);                        
                    }                    

                    // process events
                    foreach (GameEvent evt in evtToProcess)
                    {
                        evt.Invoke(CurrentGameTime);
                    }

                    // reset collections
                    evtToProcess.Clear();
                    timeSpanToRemove.Clear(); 
                }

            }
        }
    }
}