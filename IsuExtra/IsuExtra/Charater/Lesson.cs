using System;

namespace IsuExtra.Charater
{
    public class Lesson
    {
        public DateTime Start { get; set; } // начало пары
        public DateTime End { get; set; } // конец пары

        public Teacher Teacher { get; set; } // преподаватель

        public ExtraGroup Group { get; set; } // группа

        public Stream Stream { get; set; } // поток

        public Classroom Classroom { get; set; } // аудитория
    }
}