namespace Isu
{
    public class Student
    {
        // counter of ID must be static
        // because of the need to assign a new number to each student
        private static int _countId = 0;
        public Student() { }
        public Student(Group group, string name)
        {
            Name = name;
            GroupName = group.GroupName;
            CourseNumber = group.CourseNumber;
            Id = ++_countId;
        }

        public int Id { get; }
        public string Name { get; }
        public string GroupName { get; }
        public CourseNumber CourseNumber { get; }
    }
}