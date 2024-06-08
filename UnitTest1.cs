using lab14;
using ClassLibraryLab10;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using ClassLibraryMyCollection;
using HashTable;

namespace Test_Lab14
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ParticipantsConstrucrorWitchOneParam()
        {
            MusicalInstrument m = new MusicalInstrument();
            Participants p1 = new Participants(m);
            Assert.IsNotNull(p1.PerformanceNumber);
            Assert.IsNotNull(p1.Instrument);
            Assert.IsNotNull(p1.ParticipantsType);
        }

        [TestMethod]
        public void ParticipantsConstrucrorWitchManyParam()
        {
            MusicalInstrument m = new MusicalInstrument();
            string type = "Solo";
            int num = 2;
            Participants p1 = new Participants(type, num, m);
            Assert.IsNotNull(p1.PerformanceNumber);
            Assert.IsNotNull(p1.Instrument);
            Assert.IsNotNull(p1.ParticipantsType);
        }

        [TestMethod]
        public void ParticipantsToString()
        {
            MusicalInstrument m = new MusicalInstrument();
            string type = "Solo";
            int num = 2;
            Participants p1 = new Participants(type, num, m);
            Assert.AreEqual(p1.ToString(), "Solo  ����� �����������: 2  ����������: 1: ����������� ����������   ");
        }

        [TestMethod]
        public void ParticipantsEquals()
        {
            MusicalInstrument m = new MusicalInstrument();
            string type = "Solo";
            int num = 2;
            Participants p1 = new Participants(type, num, m);
            Participants p2 = new Participants(type, num, m);
            Assert.AreEqual(p1.Equals(p2), true);
        }

        [TestMethod]
        public void ParticipantsNotEquals()
        {
            MusicalInstrument m = new MusicalInstrument();
            string type = "Solo";
            int num = 2;
            Participants p1 = new Participants("type", 9, new MusicalInstrument("fff",55));
            Participants p2 = new Participants(type, num, m);
            Assert.AreEqual(p1.Equals(p2), false);
        }

        [TestMethod]
        public void ParticipantsNotEqualsType()
        {
            MusicalInstrument m = new MusicalInstrument();
            string type = "Solo";
            int num = 2;
            Participants p1 = new Participants(type, num, m);
            string p2 = "dd";
            Assert.AreEqual(p1.Equals(p2), false);
        }

        [TestMethod]
        public void ParticipantsGetHashCode()
        {
            Participants p1 = new Participants("type", 9, new MusicalInstrument("fff", 55));
            int code = p1.GetHashCode();
            Assert.AreEqual(code, p1.GetHashCode());
        }

        [TestMethod]
        public void MusicGroupConstrucrorWitchOneParam()
        {
            MusicalInstrument m = new MusicalInstrument();
            MusicGroup p1 = new MusicGroup(m);
            Assert.IsNotNull(p1.PerformanceNumber);
            Assert.IsNotNull(p1.Instrument);
            Assert.IsNotNull(p1.ParticipantsType);
            Assert.IsNotNull(p1.GroupName);
        }

        [TestMethod]
        public void MusicGroupConstrucrorWitchManyParam()
        {
            MusicGroup p1 = new MusicGroup("group", "type", 2, new MusicalInstrument());
            Assert.IsNotNull(p1.PerformanceNumber);
            Assert.IsNotNull(p1.Instrument);
            Assert.IsNotNull(p1.ParticipantsType);
            Assert.IsNotNull(p1.GroupName);
        }

        [TestMethod]
        public void MusicGroupToString()
        {
            MusicGroup p1 = new MusicGroup("type", "group", 2, new MusicalInstrument());
            Assert.AreEqual(p1.ToString(), "���.������  ����� �����������: 2  ����������: 1: ����������� ����������   ������: group");
        }

        [TestMethod]
        public void MusicGroupEquals()
        {
            MusicGroup p1 = new MusicGroup("type", "group", 2, new MusicalInstrument());
            MusicGroup p2 = new MusicGroup("type", "group", 2, new MusicalInstrument());
            Assert.AreEqual(p1.Equals(p2), true);
        }

        [TestMethod]
        public void MusicGroupNotEquals()
        {
            MusicGroup p1 = new MusicGroup("type", "group", 2, new MusicalInstrument());
            MusicGroup p2 = new MusicGroup("type", "group", 9, new MusicalInstrument());
            Assert.AreEqual(p1.Equals(p2), false);
        }

        [TestMethod]
        public void MusicGroupNotEqualsType()
        {
            MusicGroup p1 = new MusicGroup("type", "group", 2, new MusicalInstrument());
            string p2 = "dd";
            Assert.AreEqual(p1.Equals(p2), false);
        }

        [TestMethod]
        public void MusicGroupGetHashCode()
        {
            MusicGroup p1 = new MusicGroup("type", "group", 2, new MusicalInstrument());
            int code = p1.GetHashCode();
            Assert.AreEqual(code, p1.GetHashCode());
        }


        [TestMethod]
        public void GroupInfoConstructor()
        {
            GroupInfo p1 = new GroupInfo();
            Assert.IsNotNull(p1);
        }

        [TestMethod]
        public void GroupInfoToString()
        {
            GroupInfo p1 = new GroupInfo();
            Assert.AreEqual(p1.ToString(), $"�������� ������: {p1.GroupName}  ��� ���������: {p1.FoundationYear}  ���������� �����:{p1.HitsNumber} ");
        }

        [TestMethod]
        public void ChooseGuitarLINQ()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            concert = Program.MakeCollection(concert, 3, 2);

            int count = 0;
            foreach (var item in concert)
            {
                foreach (var value in item.Value)
                {
                    if (value.Instrument is Guitar)
                        count++;
                }
            }
            IEnumerable<Participants> res = Program.ChooseGuitarLINQ(concert);
            Assert.AreEqual(count, res.Count());
        }

        [TestMethod]
        public void ChooseGuitarLINQEmpty()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            try
            {
                IEnumerable<Participants> res = Program.ChooseGuitarLINQ(concert);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� �����", ex.Message);
            }
        }

        [TestMethod]
        public void ChooseGuitarExp()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            concert = Program.MakeCollection(concert, 3, 2);

            int count = 0;
            foreach (var item in concert)
            {
                foreach (var value in item.Value)
                {
                    if (value.Instrument is Guitar)
                        count++;
                }
            }
            IEnumerable<Participants> res = Program.ChooseGuitarExp(concert);
            Assert.AreEqual(count, res.Count());
        }

        [TestMethod]
        public void ChooseGuitarExpEmpty()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            try
            {
                IEnumerable<Participants> res = Program.ChooseGuitarExp(concert);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� �����", ex.Message);
            }
        }

        [TestMethod]
        public void AvgPianoKeyNumberExp()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            concert = Program.MakeCollection(concert, 3, 2);

            double count = 0;
            double sum = 0;
            foreach (var item in concert)
            {
                foreach (var value in item.Value)
                {
                    if (value.Instrument is Piano)
                    {
                        count++;
                        sum += ((Piano)value.Instrument).KeysNumber;
                    }
                }
            }
            double avg = sum / count;
            double res = Program.AvgPianoKeyNumberExp(concert);
            Assert.AreEqual(avg, res);
        }

        [TestMethod]
        public void AvgPianoKeyNumberExpEmptyCollection()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            try
            {
                double res = Program.AvgPianoKeyNumberExp(concert);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void AvgPianoKeyNumberNoResultExp()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            List<Participants> participantList = new List<Participants>();
            MusicalInstrument m = new MusicalInstrument();
            m.IRandomInit();
            participantList.Add(new MusicGroup(m));
            Guitar g = new Guitar();
            g.IRandomInit();
            participantList.Add(new MusicGroup(g));
            ElectricGuitar e = new ElectricGuitar();
            e.IRandomInit();
            participantList.Add(new MusicGroup(e));

            concert.Add(1,participantList);

            try
            {
                double res = Program.AvgPianoKeyNumberExp(concert);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Sequence contains no elements", ex.Message);
            }
        }

        [TestMethod]
        public void AvgPianoKeyNumberLINQ()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            concert = Program.MakeCollection(concert, 3, 2);

            double count = 0;
            double sum = 0;
            foreach (var item in concert)
            {
                foreach (var value in item.Value)
                {
                    if (value.Instrument is Piano)
                    {
                        count++;
                        sum += ((Piano)value.Instrument).KeysNumber;
                    }
                }
            }
            double avg = sum / count;
            double res = Program.AvgPianoKeyNumberLINQ(concert);
            Assert.AreEqual(avg, res);
        }

        [TestMethod]
        public void AvgPianoKeyNumberLINQEmptyCollection()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            try
            {
                double res = Program.AvgPianoKeyNumberLINQ(concert);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void AvgPianoKeyNumberNoResultLINQ()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            List<Participants> participantList = new List<Participants>();
            MusicalInstrument m = new MusicalInstrument();
            m.IRandomInit();
            participantList.Add(new MusicGroup(m));
            Guitar g = new Guitar();
            g.IRandomInit();
            participantList.Add(new MusicGroup(g));
            ElectricGuitar e = new ElectricGuitar();
            e.IRandomInit();
            participantList.Add(new MusicGroup(e));

            concert.Add(1, participantList);

            try
            {
                double res = Program.AvgPianoKeyNumberLINQ(concert);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Sequence contains no elements", ex.Message);
            }
        }

        [TestMethod]
        public void GroupByGroupNameExp()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            concert = Program.MakeCollection(concert, 3, 2);
            var res = Program.GroupExp(concert);
            int countitem = 0;
            int countColl = 0;
            foreach(var name in res)
            {
                foreach (var item in name)
                {
                    countitem++;
                }
            }
            foreach (var item in concert)
            {
                foreach (var value in item.Value)
                {
                    countColl++;
                }
            }
            Assert.AreEqual(countitem, countColl);
        }

        [TestMethod]
        public void GroupByGroupNameExpNoResult()
        {
            var coll = new SortedDictionary<int, List<Participants>>();

            var participants = new List<Participants> { new Participants("type", 3, new MusicalInstrument()) };
            coll.Add(1, participants);

            Assert.ThrowsException<Exception>(() => Program.GroupExp(coll), "�� ������� ��������� �����������");
        }

        [TestMethod]
        public void GroupByGroupNameExpEmptyColl()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            try
            {
                Program.GroupExp(concert);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void GroupByGroupNameLINQ()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            concert = Program.MakeCollection(concert, 3, 2);
            var res = Program.GroupLINQ(concert);
            int countitem = 0;
            int countColl = 0;
            foreach (var name in res)
            {
                foreach (var item in name)
                {
                    countitem++;
                }
            }
            foreach (var item in concert)
            {
                foreach (var value in item.Value)
                {
                    countColl++;
                }
            }
            Assert.AreEqual(countitem, countColl);
        }

        [TestMethod]
        public void GroupByGroupNameLINQEmptyColl()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            try
            {
                var res = Program.GroupLINQ(concert);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void IntersectGroupsLINQEmptyColl()
        {
            List <Participants> list1 = new List<Participants>();
            List<Participants> list2 = new List<Participants>();
            try
            {
                var res = Program.IntersectGroupsLINQ(list1,list2);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void JoinGruopExpEmptyColl()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            List<GroupInfo> inform = new List<GroupInfo>();
            try
            {
                Program.JoinGruopExp(concert, inform);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void JoinGruopLINQEmptyColl()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            List<GroupInfo> inform = new List<GroupInfo>();
            try
            {
                Program.JoinGruopLINQ(concert, inform);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void JoinGruopExpNoResult()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            concert = Program.MakeCollection(concert, 3, 2);
            List<GroupInfo> inform = new List<GroupInfo>();
            try
            {
                Program.JoinGruopExp(concert, inform);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("���������� ���", ex.Message);
            }
        }

        [TestMethod]
        public void JoinGruopLINQNoResultl()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            concert = Program.MakeCollection(concert, 3, 2);
            List<GroupInfo> inform = new List<GroupInfo>();
            try
            {
                Program.JoinGruopLINQ(concert, inform);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("���������� ���", ex.Message);
            }
        }

        [TestMethod]
        public void IntersectGroupsLINQ()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            List<Participants> list1 = new List<Participants>();
            list1.Add(new MusicGroup("���.������", "A", 1, new MusicalInstrument()));
            List<Participants> list2 = new List<Participants>();
            list2.Add(new MusicGroup("���.������", "A", 1, new MusicalInstrument()));
            concert.Add(1,list1);
            concert.Add(2, list2);

            var res = Program.IntersectGroupsLINQ(list1, list2);

            foreach (var item in res)
            {
                Assert.AreEqual(item.ToString(), "���.������  ����� �����������: 1  ����������: 1: ����������� ����������   ������: A");
            }
        }

        [TestMethod]
        public void IntersectGroupsExpEmptyColl()
        {
            List<Participants> list1 = new List<Participants>();
            List<Participants> list2 = new List<Participants>();
            try
            {
                var res = Program.IntersectGroupsExp(list1, list2);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }


        [TestMethod]
        public void IntersectGroupsExp()
        {
            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();
            List<Participants> list1 = new List<Participants>();
            list1.Add(new MusicGroup("���.������", "A", 1, new MusicalInstrument()));
            List<Participants> list2 = new List<Participants>();
            list2.Add(new MusicGroup("���.������", "A", 1, new MusicalInstrument()));
            concert.Add(1, list1);
            concert.Add(2, list2);

            var res = Program.IntersectGroupsExp(list1, list2);

            foreach (var item in res)
            {
                Assert.AreEqual(item.ToString(), "���.������  ����� �����������: 1  ����������: 1: ����������� ����������   ������: A");
            }
        }

        [TestMethod]
        public void ChooseHashCollIDExp()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            tableColle�t = Program.MakeHashCollection(tableColle�t);
            var res = Program.ChooseDataExp(tableColle�t);
            int count = 0;
            foreach (var item in tableColle�t)
            {
                if (item.id.Number <= 30)
                    { count++; }
            }
            Assert.AreEqual(count, res.Count());
        }

        [TestMethod]
        public void ChooseHashCollIDExpEmpy()
        {
            try
            {
                MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
                var res = Program.ChooseDataExp(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� �����", ex.Message);
            }
        }

        [TestMethod]
        public void ChooseHashCollIDExpNoResult()
        {
            try
            {
                MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
                MusicalInstrument m1 = new MusicalInstrument("cc",55);
                Guitar g1 = new Guitar("ll", 8, 99);
                ElectricGuitar e1 = new ElectricGuitar("ss", 7, "dd", 77);
                Piano p1 = new Piano("ee", "kk", 99, 78);
                tableColle�t = new MyCollection<MusicalInstrument>(m1, g1, e1, p1);
                var res = Program.ChooseDataExp(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("������� ��������� ���", ex.Message);
            }
        }

        [TestMethod]
        public void ChooseHashCollIDLINQ()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            tableColle�t = Program.MakeHashCollection(tableColle�t);
            var res = Program.ChooseDataLINQ(tableColle�t);
            int count = 0;
            foreach (var item in tableColle�t)
            {
                if (item.id.Number <= 30)
                { count++; }
            }
            Assert.AreEqual(count, res.Count());
        }

        [TestMethod]
        public void ChooseHashCollIDLINQEmpy()
        {
            try
            {
                MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
                var res = Program.ChooseDataLINQ(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� �����", ex.Message);
            }
        }

        [TestMethod]
        public void ChooseHashCollIDLINQNoResult()
        {
            try
            {
                MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
                MusicalInstrument m1 = new MusicalInstrument("cc", 55);
                Guitar g1 = new Guitar("ll", 8, 99);
                ElectricGuitar e1 = new ElectricGuitar("ss", 7, "dd", 77);
                Piano p1 = new Piano("ee", "kk", 99, 78);
                tableColle�t = new MyCollection<MusicalInstrument>(m1, g1, e1, p1);
                var res = Program.ChooseDataLINQ(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("����� ��������� ���", ex.Message);
            }
        }

        [TestMethod]
        public void SumStringsNumberExp()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            tableColle�t = Program.MakeHashCollection(tableColle�t);

            int sum = 0;
            
            foreach(var item in tableColle�t)
            {
                if (item is Guitar)
                    sum += ((Guitar)item).StringsNumber;
            }

            int res = Program.SumStringsNumberExp(tableColle�t);
            Assert.AreEqual(sum, res);
        }

        [TestMethod]
        public void SumStringsNumberExpEmptyCollection()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>(); 
            try
            {
                int res = Program.SumStringsNumberExp(tableColle�t);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void SumStringsNumberExpNoResult()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            MusicalInstrument m1 = new MusicalInstrument("cc", 55);
            MusicalInstrument m2 = new MusicalInstrument("lll", 55);
            Piano p1 = new Piano("ee", "kk", 99, 78);
            tableColle�t = new MyCollection<MusicalInstrument>(m1,m2, p1);

            try
            {
                int res = Program.SumStringsNumberExp(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("����� ��������� ���", ex.Message);
            }
        }

        [TestMethod]
        public void SumStringsNumberLINQ()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            tableColle�t = Program.MakeHashCollection(tableColle�t);

            int sum = 0;

            foreach (var item in tableColle�t)
            {
                if (item is Guitar)
                    sum += ((Guitar)item).StringsNumber;
            }

            int res = Program.SumStringsNumberLINQ(tableColle�t);
            Assert.AreEqual(sum, res);
        }

        [TestMethod]
        public void SumStringsNumberLINQEmptyCollection()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            try
            {
                int res = Program.SumStringsNumberLINQ(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void SumStringsNumberLINQNoResult()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            MusicalInstrument m1 = new MusicalInstrument("cc", 55);
            MusicalInstrument m2 = new MusicalInstrument("lll", 55);
            Piano p1 = new Piano("ee", "kk", 99, 78);
            tableColle�t = new MyCollection<MusicalInstrument>(m1, m2, p1);

            try
            {
                int res = Program.SumStringsNumberLINQ(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("����� ��������� ���", ex.Message);
            }
        }

        [TestMethod]
        public void GroupInsrtumentsLINQ()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            tableColle�t = Program.MakeHashCollection(tableColle�t);
            var res = Program.GroupInsrtumentsLINQ(tableColle�t);
            int countitem = 0;
            foreach (var name in res)
            {
                foreach (var item in name)
                {
                    countitem++;
                }
            }
            
            Assert.AreEqual(countitem, tableColle�t.Count);
        }

        [TestMethod]
        public void GroupInsrtumentsExp()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            tableColle�t = Program.MakeHashCollection(tableColle�t);
            var res = Program.GroupInsrtumentsExp(tableColle�t);
            int countitem = 0;
            foreach (var name in res)
            {
                foreach (var item in name)
                {
                    countitem++;
                }
            }

            Assert.AreEqual(countitem, tableColle�t.Count);
        }

        [TestMethod]
        public void GroupInsrtumentsExpEmpty()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            try
            {
               Program.GroupInsrtumentsExp(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }

        [TestMethod]
        public void GroupInsrtumentsLINQEmpty()
        {
            MyCollection<MusicalInstrument> tableColle�t = new MyCollection<MusicalInstrument>();
            try
            {
                Program.GroupInsrtumentsLINQ(tableColle�t);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("��������� ������", ex.Message);
            }
        }
    }
}