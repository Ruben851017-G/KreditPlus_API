using KreditPlus_API.Entity;

namespace KreditPlus_API.Helper
{
    public class GenerateOrderNumber
    {
        readonly DatabaseContext db = new();

        public GenerateOrderNumber(DatabaseContext dbContext)
        {
            db = dbContext;
        }
        public static string GenerateNumber(string input)
        {
            string result = null;
            return result;
        }
    }
}
