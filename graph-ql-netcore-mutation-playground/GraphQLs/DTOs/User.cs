using System;

namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class User
    {
        [Obsolete("For serialization only", true)]
        public User() { }

        public User(
            int id,
            string name,
            string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}