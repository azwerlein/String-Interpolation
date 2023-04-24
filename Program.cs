string file = "data.txt";

Console.WriteLine("Welcome!");

Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // Create data file
    Console.WriteLine("How many weeks of data is needed");
    int weeks = int.Parse(Console.ReadLine());

    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new Random();
    // create file
    StreamWriter sw = new StreamWriter(file);

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
    // TODO: parse data file
    Console.WriteLine("Weekly Report:");
    StreamReader sr = new StreamReader(file);
    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        string[] dateWeek = line.Split(',');
        DateTime date = DateTime.Parse(dateWeek[0]);
        string[] days = dateWeek[1].Split('|');

        Console.WriteLine($"Week of {date.ToLongDateString()}");
        Console.WriteLine("Su Mo Tu We Th Fr Sa Tot Avg");
        Console.WriteLine("-- -- -- -- -- -- -- --- ---");
        int sum = 0;
        for (int i = 0; i < days.Length; i++)
        {
            string hours = days[i];
            hours = hours.PadLeft(2);
            Console.Write($"{hours} ");

            sum += int.Parse(hours);
        }
        double avg = Math.Round((double)sum / days.Length, 1);
        string total = sum.ToString().PadLeft(3);
        string average = avg.ToString().PadLeft(3);
        Console.WriteLine($"{total} {average}\n");

    }
    sr.Close();
}