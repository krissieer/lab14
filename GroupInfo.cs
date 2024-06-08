using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    public class GroupInfo
    {
        public int FoundationYear { get; set; }
        public int HitsNumber { get; set; }
        public string GroupName { get; set; }

        static string[] groupNames = { "Nirvana", "The Beatles", "The Rolling Stones", "Queen", "ABBA", "One Direction", "Linkin Park", "Red Hot Chill Pappers" };
        Random random = new Random();
        public GroupInfo()
        {
            FoundationYear = random.Next(1950,2010);
            HitsNumber = random.Next(5,50);
            GroupName = groupNames[random.Next(groupNames.Length)];
        }

        public override string ToString()
        {
            return $"Название группы: {GroupName}  Год основания: {FoundationYear}  Количестов хитов:{HitsNumber} ";
        }
    }
}
