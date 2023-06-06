namespace CalendarApp.Helpers
{
    public class IdGenerator
    {
        //Generates a unique ID
        public static string CreateLetterId(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}