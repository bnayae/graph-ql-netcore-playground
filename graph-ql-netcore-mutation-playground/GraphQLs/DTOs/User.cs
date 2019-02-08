namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class User
    {
        public User(
            int id,
            string name,
            string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public readonly int Id;
        public readonly string Name;
        public readonly string Email;
    }
}