using System.Collections.Generic;

namespace IsuExtra.Charater
{
    public class Stream
    {
        public string Name { get; set; }

        public OGNP OGNP { get; set; } // ОГНП потока

        public List<ExtraStudent> Students { get; } = new List<ExtraStudent>();

        public int MaxPeopleGroup { get; set; } = 20; // максимальное количество человек в группе. по умолчанию 20

        public List<Lesson> Lessons { get; } = new List<Lesson>(); // список пар
    }
}