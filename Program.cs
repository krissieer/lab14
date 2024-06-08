using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using ClassLibraryMyCollection;

namespace lab14
{
    public class Program
    {
        public static SortedDictionary<int, List<Participants>> MakeCollection(SortedDictionary<int, List<Participants>> concert, int dictionaryLength, int listLength)
        {
            for (int i = 0; i < dictionaryLength; i++)
            {
                List<Participants> participantList = new List<Participants>();
                participantList.Add(new MusicGroup("Муз.Группа", "No Name Group", 100, new MusicalInstrument()));
                participantList = MakeList(listLength, participantList);
                concert.Add(i+1, participantList);
            }
            return concert;
        }

        public static void PrintColl(SortedDictionary<int, List<Participants>> coll)
        {
            if (coll.Count == 0)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }
            foreach (var item in coll)
            {
                Console.WriteLine($"Ключ - номер концерного зала: {item.Key}");
                foreach (var value in item.Value)
                {
                    Console.WriteLine("    "+value);
                }
            }
        }

        public static List<Participants> MakeList(int length, List<Participants> list)
        {
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int instrType = rnd.Next(1,5);
                if (instrType == 1)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.IRandomInit();
                    list.Add(new MusicGroup(m));
                }
                else if (instrType == 2)
                {
                    Guitar g = new Guitar();
                    g.IRandomInit();
                    list.Add(new MusicGroup(g));
                }
                else if (instrType == 3)
                {
                    ElectricGuitar e = new ElectricGuitar();
                    e.IRandomInit();
                    list.Add(new MusicGroup(e));
                }
                else if (instrType == 4)
                {
                    Piano p = new Piano();
                    p.IRandomInit();
                    list.Add(new MusicGroup(p));
                }
            }
            return list;
        }

        public static void PrintResult(IEnumerable<Participants> res)
        {
            if (res.Count() == 0)
                throw new Exception("В коллекции нет искомых элементов");

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Выборка всех гитар с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<Participants> ChooseGuitarLINQ(SortedDictionary<int, List<Participants>> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пуста");

            var res = from item in coll.Values
                      from item2 in item
                      where item2.Instrument is Guitar
                      select item2;
            return res;
        }

        /// <summary>
        /// Выборка всех гитар с помощью методов расширения
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<Participants> ChooseGuitarExp(SortedDictionary<int, List<Participants>> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пуста");

            var res = coll.Values
                    .SelectMany(item => item)
                    .Where(item2 => item2.Instrument is Guitar)
                    .Select(item2 => item2);

            return res;
        }

        /// <summary>
        /// Нахождение среднего количества клавиш на фортепиано с помощью методов расширений
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double AvgPianoKeyNumberExp(SortedDictionary<int, List<Participants>> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = coll.Values.SelectMany(item => item)
                .Where(item2 => item2.Instrument is Piano)
                .Select(item2 => ((Piano)item2.Instrument).KeysNumber).Average();

            return res;
        }

        /// <summary>
        ///  Нахождение среднего количества клавиш на фортепиано с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double AvgPianoKeyNumberLINQ(SortedDictionary<int, List<Participants>> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = (from item in coll.Values
                      from item2 in item
                      where item2.Instrument is Piano
                      select ((Piano)item2.Instrument).KeysNumber).Average(); //Неотложенное выполнение;

            return res;
        }

        /// <summary>
        /// Группировака элеметов по названию музыкальной группы с помощью методов расширений
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<IGrouping<string,Participants>> GroupExp(SortedDictionary<int, List<Participants>> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = coll.Values.SelectMany(item => item)
                   .Where(item => item is MusicGroup)
                   .GroupBy(item2 => ((MusicGroup)item2).GroupName);

            if (res.Count() == 0)
                throw new Exception("Не удалось выполнить группировку");

            return res;
        }

        /// <summary>
        /// Группировака элеметов по названию музыкальной группы с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
         public static IEnumerable<IGrouping<string, Participants>> GroupLINQ(SortedDictionary<int, List<Participants>> coll)
         {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = from item in coll.Values
                      from item2 in item
                      group item2 by ((MusicGroup)item2).GroupName;

            return res;
         }

        /// <summary>
        /// Получнеие количества элементов в каждой группе с помощью методов расширений
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static void GetCountInGroupExp(SortedDictionary<int, List<Participants>> coll)
        {
            var res = coll.Values.SelectMany(item => item)
            .GroupBy(item2 => ((MusicGroup)item2).GroupName)
            .Select(grouped => new { Name = grouped.Key, Count = grouped.Count() });  //Неотложенное выполнение

            foreach (var item in res)
                Console.WriteLine($"{item.Name} - {item.Count}");
        }

        /// <summary>
        ///  Получнеие количества элементов в каждой группе с помощью  LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static void GetCountInGroupLINQ(SortedDictionary<int, List<Participants>> coll)
        {
            var res = from item in coll.Values
                      from item2 in item
                      group item2 by ((MusicGroup)item2).GroupName
                      into grouped
                      select new { Name = grouped.Key, Count = grouped.Count() };  //Неотложенное выполнение

            foreach (var item in res)
                Console.WriteLine($"{item.Name} - {item.Count}");
        }

        /// <summary>
        /// Объединение элементов двух коллекций с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="inform"></param>
        /// <exception cref="Exception"></exception>
        public static void JoinGruopLINQ(SortedDictionary<int, List<Participants>> coll, List<GroupInfo> inform)
        {
            if (coll.Count == 0)
            {
                throw new Exception("Коллекция пустая");
            }

            var res = from item in coll.Values
                      from item2 in item
                      where item2 is MusicGroup
                      join g in inform on ((MusicGroup)item2).GroupName equals g.GroupName
                      select new 
                      { 
                          PerformanceNumber = item2.PerformanceNumber, 
                          GroupName = g.GroupName, FoundationYear = g.FoundationYear, NumberOfHits = g.HitsNumber 
                      };

            if ( res.Count() == 0)
                throw new Exception("Совпадений нет");

            foreach (var item in res)
                Console.WriteLine(item);
        }

        /// <summary>
        /// Объединение элементов двух коллекций с помощью методов расширения
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="inform"></param>
        /// <exception cref="Exception"></exception>
        public static void JoinGruopExp(SortedDictionary<int, List<Participants>> coll, List<GroupInfo> inform)
        {
            if (coll.Count == 0)
            {
                throw new Exception("Коллекция пустая");
            }

            var res = coll.Values.SelectMany(item => item)
                  .Where(item => item is MusicGroup)
                  .Join(inform,
                  item2 => ((MusicGroup)item2).GroupName,
                  g => g.GroupName,
                  (item2, g) => new
                  {
                      PerformanceNumber = item2.PerformanceNumber,
                      GroupName = g.GroupName,
                      FoundationYear = g.FoundationYear,
                      NumberOfHits = g.HitsNumber
                  });

            if (res.Count() == 0)
                throw new Exception("Совпадений нет");

            foreach (var item in res)
                Console.WriteLine(item);
        }

        /// <summary>
        /// Нахождение общего элемента в списках словаря с помощью LINQ запросов
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<Participants> IntersectGroupsLINQ(List<Participants> p1, List<Participants> p2)
        {
            if (p1.Count == 0 || p2.Count == 0) throw new Exception("Коллекция пустая");

            var res = (from item in p1 select item)
                .Intersect(from item in p2 select item);

            return res;
        }

        /// <summary>
        /// Нахождение общего элемента в списках словаря с помощью методов расширения
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<Participants> IntersectGroupsExp(List<Participants> p1, List<Participants> p2)
        {
            if (p1.Count == 0 || p2.Count == 0) throw new Exception("Коллекция пустая");

            var res = p1.Intersect(p2); 
            return res;
        }

        public static MyCollection<MusicalInstrument> MakeHashCollection(MyCollection<MusicalInstrument> coll)
        {
            MusicalInstrument m1 = new MusicalInstrument();
            m1.IRandomInit();
            MusicalInstrument m2 = new MusicalInstrument();
            m2.IRandomInit();
            Guitar g1 = new Guitar();
            g1.IRandomInit();
            Guitar g2 = new Guitar();
            g2.IRandomInit();
            Guitar g3 = new Guitar();
            g3.IRandomInit();
            ElectricGuitar e1 = new ElectricGuitar();
            e1.IRandomInit();
            ElectricGuitar e2 = new ElectricGuitar();
            e2.IRandomInit();
            Piano p1 = new Piano();
            p1.IRandomInit();
            Piano p2 = new Piano();
            p2.IRandomInit();
            Piano p3 = new Piano();
            p3.IRandomInit();
            coll = new MyCollection<MusicalInstrument>(m1, m2, g1, g2, g3, e1, e2, p1, p2, p3);
            return coll;
        }

        /// <summary>
        /// Выборка элементов с определенным id номером с помощью методов расширений
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<MusicalInstrument> ChooseDataExp(MyCollection<MusicalInstrument> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пуста");

            var res = coll.Where(item => item.id.Number <= 30);

            if (res.Count() == 0)
                throw new Exception("Искомых элементов нет");

            return res;
        }

        /// <summary>
        /// Выборка элементов с определенным id номером с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<MusicalInstrument> ChooseDataLINQ(MyCollection<MusicalInstrument> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пуста");

            var res = from item in coll
                      where item.id.Number <= 30
                      select item;

            if (res.Count() == 0)
                throw new Exception("Таких элементов нет");

            return res;
        }

        /// <summary>
        /// Получение суммы струн гитар с помощью методов расширения
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int SumStringsNumberExp(MyCollection<MusicalInstrument> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = coll.Where(item => item is Guitar).Sum(item => ((Guitar)item).StringsNumber);

            if (res == 0)
                throw new Exception("Таких элементов нет");

            return res;
        }

        /// <summary>
        /// Получение суммы струн гитар с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int SumStringsNumberLINQ(MyCollection<MusicalInstrument> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = (from item in coll
                       where item is Guitar
                       select ((Guitar)item).StringsNumber).Sum();

            if (res == 0)
                throw new Exception("Таких элементов нет");

            return res;
        }

        /// <summary>
        /// Группировака элементов коллекции по названию инструмента с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<IGrouping<string, MusicalInstrument>> GroupInsrtumentsLINQ(MyCollection<MusicalInstrument> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = from item in coll
                      group item by item.Name;

            return res;
        }

        /// <summary>
        /// Группировака элементов коллекции по названию инструмента с помощью методов расширения
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<IGrouping<string, MusicalInstrument>> GroupInsrtumentsExp(MyCollection<MusicalInstrument> coll)
        {
            if (coll.Count == 0) throw new Exception("Коллекция пустая");

            var res = coll.GroupBy(item => item.Name);

            return res;
        }

        /// <summary>
        /// Получение количества элементов в каждой группе с помощью методов расширения
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static void GetCountInGroupInstrumentsExp(MyCollection<MusicalInstrument> coll)
        {
            var res = coll.GroupBy(item => item.Name)
              .Select(grouped => new { Name = grouped.Key, Count = grouped.Count() });

            if (res.Count() == 0)
                throw new Exception("Таких элементов нет");

            foreach (var item in res)
                Console.WriteLine($"{item.Name} - {item.Count}");
        }

        /// <summary>
        /// Получение количества элементов в каждой группе с помощью LINQ запросов
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public static void GetCountInGroupInstrumentsLINQ(MyCollection<MusicalInstrument> coll)
        {
            var res = from item in coll
                      group item by item.Name
                      into grouped
                      select new { Name = grouped.Key, Count = grouped.Count() };

            if (res.Count() == 0)
                throw new Exception("Таких элементов нет");

            foreach (var item in res)
                Console.WriteLine($"{item.Name} - {item.Count}");
        }

        public static int ChooseOption(string msg)
        {
            int number;
            bool isConvert;
            do
            {
                Console.Write(msg);
                isConvert = int.TryParse(Console.ReadLine(), out number);
                if (!isConvert || number <= 0) Console.WriteLine("Неверный ввод. Попробуйте еще раз");
            } while (!isConvert || number <= 0);

            return number;
        }

        static void Main(string[] args)
        {
            int choose;
            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Стандартные коллекции");
                Console.WriteLine("2. Коллекция 12 ЛР");
                Console.WriteLine("3. Звершить работу");
                Console.WriteLine();

                choose = ChooseOption("Выберете пункт меню: ");

                switch(choose)
                {
                    case 1:
                        {

                            SortedDictionary<int, List<Participants>> concert = new SortedDictionary<int, List<Participants>>();

                            int ans;
                            
                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("1. Создание коллекции");
                                Console.WriteLine("2. Печать коллекции");
                                Console.WriteLine("3. Выборка данных: Выбрать элементы коллекци, у которых музыкальный инструмент - гитара");
                                Console.WriteLine("4. Агрегирование: среднее количество клавиш на фортепиано ");
                                Console.WriteLine("5. Группироавка по названию музыкальной группы ");
                                Console.WriteLine("6. Соединение GroupInfo и MusicGroup ");
                                Console.WriteLine("7. Пересечение в списках");
                                Console.WriteLine("8. Назад");
                                Console.WriteLine();

                                ans = ChooseOption("Выберете пункт меню: ");

                                switch (ans)
                                {
                                    case 1: //Создание коллекции
                                        {
                                            int dictLength = ChooseOption("Введите длину отсортированного словаря: ");
                                            int listLength = ChooseOption("Введите длину списка : ");

                                            concert = MakeCollection(concert, dictLength, listLength);

                                            Console.WriteLine("Коллекция сформирована");
                                            break;
                                        }
                                    case 2: //Печать коллекции
                                        {
                                            Console.WriteLine(" === КОЛЛЕКЦИЯ === ");
                                            PrintColl(concert);
                                            break;
                                        }
                                    case 3: // Выборка
                                        {
                                            Console.WriteLine(" === Элементы, у которых музыкальный инструмент - гитары ===");
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                IEnumerable<Participants> res = ChooseGuitarExp(concert);
                                                PrintResult(res);
                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                IEnumerable<Participants> res2 = ChooseGuitarLINQ(concert);
                                                PrintResult(res2);
                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }

                                            break;
                                        }
                                    case 4: //Нахождение среднего 
                                        {
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                Console.WriteLine($" === Среднее количество клавиш на фортепиано: {(AvgPianoKeyNumberExp(concert)).ToString("0.00")} === ");
                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                Console.WriteLine($" === Среднее количество клавиш на фортепиано: {(AvgPianoKeyNumberLINQ(concert)).ToString("0.00")} === ");
                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: "+ex.Message); }
                                            break;
                                        }
                                    case 5: //Группировака
                                        {
                                            Console.WriteLine("Группирвоака по названию группы");
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                var res = GroupExp(concert);
                                                foreach (var name in res)
                                                {
                                                    Console.WriteLine(name.Key);
                                                    foreach (var item in name)
                                                    {
                                                        Console.WriteLine("  " + item);
                                                    }
                                                }

                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                var res2 = GroupLINQ(concert);
                                                foreach (var name in res2)
                                                {
                                                    Console.WriteLine(name.Key);
                                                    foreach (var item in name)
                                                    {
                                                        Console.WriteLine("  " + item);
                                                    }
                                                }

                                                Console.WriteLine($"\nКоличество элементов в группе: ");
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ:  "); 
                                                GetCountInGroupExp(concert);
                                                Console.WriteLine("\nLINQ ЗАПРОСЫ : ");
                                                GetCountInGroupLINQ(concert);
                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }

                                            break;
                                        }
                                    case 6: //Объединение
                                        {
                                            Console.WriteLine(" === Соединение GroupInfo и MusicGroup === ");
                                            try
                                            {
                                                List<GroupInfo> inform = new List<GroupInfo>();
                                                GroupInfo g1 = new GroupInfo();
                                                GroupInfo g2 = new GroupInfo();
                                                GroupInfo g3 = new GroupInfo();
                                                inform.Add(g1);
                                                inform.Add(g2);
                                                inform.Add(g3);
                                                Console.WriteLine(g1);
                                                Console.WriteLine(g2);
                                                Console.WriteLine(g3);

                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                JoinGruopExp(concert, inform);
                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                JoinGruopLINQ(concert, inform);
                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }


                                            break;
                                        }
                                    case 7: //Пересечение в списках
                                        {
                                            Console.WriteLine(" === Пересечения списков === ");
                                            if (concert.Count != 0)
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                IEnumerable<Participants> res1 = IntersectGroupsExp(concert[1], concert[2]);
                                                PrintResult(res1);
                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                IEnumerable<Participants> res2 = IntersectGroupsLINQ(concert[1], concert[2]);
                                                PrintResult(res2);
                                            }
                                            else Console.WriteLine(" Коллекция пуста ");
                                            break;
                                        }

                                }

                            } while (ans != 8);
                            break;
                        }
                    case 2:
                        {
                            MyCollection<MusicalInstrument> tableColleсt = new MyCollection<MusicalInstrument>();

                            int ans;
                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("1. Создание коллекции");
                                Console.WriteLine("2. Печать коллекции");
                                Console.WriteLine("3. Выборка данных: Выбрать элементы, у которых id меньше или равно 30");
                                Console.WriteLine("4. Агрегирование:  Сумма струн на всех гитарах ");
                                Console.WriteLine("5. Группироавка по названию инструмента и получение счетчика ");
                                Console.WriteLine("6. Назад");
                                Console.WriteLine();

                                ans = ChooseOption("Выберете пункт меню: ");
                                switch (ans)
                                {
                                    case 1: //Создание коллекции
                                        {
                                            tableColleсt = MakeHashCollection(tableColleсt);
                                            Console.WriteLine("Коллекция сформирована");
                                            break;
                                        }
                                    case 2: //Печать коллекции
                                        {
                                            Console.WriteLine(" === КОЛЛЕКЦИЯ === ");
                                            try
                                            {
                                                tableColleсt.PrintTable();
                                            }
                                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                                            
                                            break;
                                        }
                                    case 3: // Выборка
                                        {
                                            Console.WriteLine(" === Элементы, у которых id меньше или равно 30 ===");
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                var res =  ChooseDataExp(tableColleсt);
                                                foreach (var item in res)
                                                {
                                                    Console.WriteLine(item);
                                                }

                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                var res2 = ChooseDataLINQ(tableColleсt);
                                                foreach (var item in res2)
                                                {
                                                    Console.WriteLine(item);
                                                }
                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }

                                            break;
                                        }
                                    case 4: //Нахождение суммы 
                                        {
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                Console.WriteLine($"=== Сумма струн на всех гитарах: {SumStringsNumberExp(tableColleсt)} ===");
                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                Console.WriteLine($"=== Сумма струн на всех гитарах: {SumStringsNumberLINQ(tableColleсt)} ===");
                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }
                                            break;
                                        }
                                    case 5: //Группировака и счетчик
                                        {
                                            Console.WriteLine("Группирвоака по названию инструмента");
                                            try
                                            {
                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                var res = GroupInsrtumentsExp(tableColleсt);
                                                foreach (var name in res)
                                                {
                                                    Console.WriteLine(name.Key);
                                                    foreach (var item in name)
                                                    {
                                                        Console.WriteLine("  " + item);
                                                    }
                                                }

                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                var res2 = GroupInsrtumentsLINQ(tableColleсt);
                                                foreach (var name in res2)
                                                {
                                                    Console.WriteLine(name.Key);
                                                    foreach (var item in name)
                                                    {
                                                        Console.WriteLine("  " + item);
                                                    }
                                                }

                                                Console.WriteLine($"\nКоличество элементов в группе: ");

                                                Console.WriteLine(" МЕТОДЫ РАСШИРЕНИЯ: ");
                                                GetCountInGroupInstrumentsExp(tableColleсt);
                                                Console.WriteLine("\n LINQ ЗАПРОСЫ : ");
                                                GetCountInGroupInstrumentsLINQ(tableColleсt);

                                            }
                                            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }

                                            break;
                                        }
                                }

                            } while (ans != 6);
                            break;
                        }
                }

            } while (choose != 3);
        }
    }
}



