using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace lab14
{
    public class MusicGroup : Participants
    {
        string groupName;

        public string GroupName { get; set; } //название группы

        string[] groupNames = { "Nirvana", "The Beatles", "The Rolling Stones", "Queen", "ABBA", "One Direction", "Linkin Park", "Red Hot Chill Pappers"};

        static Random random = new Random();

        public MusicGroup(MusicalInstrument m):base(m)
        {
            ParticipantsType = "Муз.группа";
            GroupName = groupNames[random.Next(groupNames.Length)];
            Instrument = m;
        }

        public MusicGroup(string type, string name, int number, MusicalInstrument instr) : base (type, number, instr)
        {
            ParticipantsType = "Муз.группа";
            GroupName = name;
            Instrument = instr;

        }

        public override string ToString()
        {
            return base.ToString() + "Группа: " + GroupName.ToString();
        }


        public override bool Equals(object? obj)
        {
            if (GetType() != obj.GetType())
            {
                return false;
            }
            MusicGroup p = (MusicGroup)obj;
            return ParticipantsType == p.ParticipantsType && (PerformanceNumber == p.PerformanceNumber) && (GroupName == p.GroupName); 
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + GroupName.GetHashCode();
                hash = hash * 23 + GroupName.GetHashCode();
                hash = hash * 23 + PerformanceNumber.GetHashCode();
                hash = hash * 23 + Instrument.GetHashCode();
                return hash;
            }
        }


    }
}
