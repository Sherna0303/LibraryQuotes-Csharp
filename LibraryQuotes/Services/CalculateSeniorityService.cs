using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Services.Interfaces;

namespace LibraryQuotes.Services
{
    public class CalculateSeniorityService : ICalculateSeniorityService
    {
        private readonly IDatabase _database;

        public CalculateSeniorityService(IDatabase database)
        {
            _database = database;
        }

        public int GetSeniority(string idUser)
        {
            int id = 0;

            try
            {
                id = int.Parse(idUser);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error converting user id to int");
            }

            var user = _database.users.FindAsync(id).Result;
            return CalculateAntiquityYears(user.CreationDate);
        }

        private int CalculateAntiquityYears(DateOnly date)
        {
            var today = DateTime.Now;
            var JoinDate = new DateTime(date.Year, date.Month, date.Day);
            TimeSpan totalDays = today - JoinDate;

            return totalDays.Days / 365;
        }
    }
}
