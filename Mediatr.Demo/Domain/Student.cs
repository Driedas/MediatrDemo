using System;

namespace MediatrDemo.Domain
{
    public class Student : IAuditable
    {
        protected Student()
        {
        }

        public Student(Guid id, string firstName, string middleName, string lastName, DateTimeOffset birthDate, Gender gender)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;

            CreatedDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public string IdentityNumber { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public DateTimeOffset UpdatedDate { get; private set; }
        public Guid CreatedBy { get; private set; }
        public Guid UpdatedBy { get; private set; }
    }
}
