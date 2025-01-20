using ApiAuth.Models;

namespace ApiAuth.Repository
{
    public class UserRepository
    {

        public static User Get(string username, string password){

            var users = new List<User>
            {
                new() {Id = 1, Username = "wagner",Password = "1234", Role = "Estudante"!},
                new() {Id = 2, Username = "daniel",Password = "1234", Role = "Pedreiro"!}
            };
            return users
                .FirstOrDefault(x => x.Username == username && x.Password == password)!;
            
        }
    }
}