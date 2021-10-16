using Isu.Charater;

namespace IsuExtra.Charater
{
    public class ExtraStudent : Student
    {
        public OGNP[] OGNPs { get; } = new OGNP[2];

        public bool JoinOGNP(OGNP ognp)
        {
            if (!CheckSchedule(ognp)) // проверяем расписание пар
                return false;

            if (OGNPs[0] == null && OGNPs[0] != ognp)
            {
                OGNPs[0] = ognp;
                ognp.AddStudent(this);
                return true;
            }

            if (OGNPs[1] == null && OGNPs[1] != ognp)
            {
                OGNPs[1] = ognp;
                ognp.AddStudent(this);
                return true;
            }

            return false;
        }

        public void ExitOGNP(OGNP ognp)
        {
            if (OGNPs[0] == ognp)
            {
                OGNPs[0] = null;
                ognp.RemoveStudent(this);
            }

            if (OGNPs[1] == ognp)
            {
                OGNPs[1] = null;
                ognp.RemoveStudent(this);
            }
        }

        private bool CheckSchedule(OGNP ognp)
        {
            var extraGroup = Group as ExtraGroup;

            if (ognp == extraGroup.Base) // если одна специальность то нельзя
                return false;

            if (!ognp.Streams.TrueForAll(s => s.Students.Count < s.MaxPeopleGroup)) // если нет свободных мест
                return false;

            // проверяем, что пары не пересекаются
            foreach (Stream stream in ognp.Streams)
            {
                bool req = false;
                foreach (Lesson l in extraGroup.Lessons)
                {
                    foreach (Lesson l2 in stream.Lessons)
                    {
                        if (l.End < l2.Start || l.Start > l2.End)
                            req = true;
                    }
                }

                if (!req)
                    return false;
            }

            return true;
        }
    }
}