using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace lab14
{
    public class Participants: MusicalInstrument
    {
        string participantType;
        MusicalInstrument instrument;
        string[] types = { "Певец", "Певица", "Ансамбль", "Музыкальная группа" };
        static Random random = new Random();

        public string ParticipantsType { get; set; }
        public MusicalInstrument Instrument { get; set; }
        public int PerformanceNumber { get; set; }
      


        public Participants(MusicalInstrument m)
        {
            ParticipantsType = types[random.Next(types.Length)];
            Instrument = m;
            PerformanceNumber = random.Next(1,90);
        }

        public Participants(string type, int number, MusicalInstrument m)
        {
            ParticipantsType = type;
            Instrument = m;
            PerformanceNumber = number;
        }

        public override string ToString()
        {
            return ParticipantsType.ToString() + "  " + "Номер выступления: " + PerformanceNumber.ToString()+ "  " + "Инструмент: " + Instrument.ToString()+ " ";
        }

        public override bool Equals(object? obj)
        {
            if (GetType() != obj.GetType())
            {
                return false;
            }
            Participants p = (Participants)obj;
            return ParticipantsType == p.ParticipantsType && (PerformanceNumber == p.PerformanceNumber); //&& (Instrument == p.Instrument)
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ParticipantsType.GetHashCode();
                hash = hash * 23 + PerformanceNumber.GetHashCode();
                hash = hash * 23 + Instrument.GetHashCode();
                return hash;
            }
        }
    }
}
