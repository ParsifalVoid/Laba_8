using System.Net;
using Лаба_8;
internal class Program
{
    private static void Main(string[] args)
    {
        int[] arr = new int[100000];

        for (int i = 0; i < arr.Length; i++)
        {
            Random rand = new Random();
            arr[i] = rand.Next(0, 1000);
        }

        int search = 2;
        
        int first = 0;
        int last = 20;

        SearchService searchService = new SearchService();

        var a = searchService.LinearSearch(arr, search);

        Console.WriteLine("Линейный поиск");
        Console.WriteLine("data=>" + a.data);
        Console.WriteLine("position=>" + a.position);
        Console.WriteLine("itterCount=>" + a.itterCount);
        Console.WriteLine("time=>" + (a.workTimeCount / TimeSpan.TicksPerMillisecond));

        var b = searchService.BinarySearch(arr, search, first, last);

        Console.WriteLine("Бинарный поиск");
        Console.WriteLine("data=>" + b.data);
        Console.WriteLine("position=>" + b.positon);
        Console.WriteLine("itterCount=>" + b.itterCount);
        Console.WriteLine("time=>" + (b.workTimeCount / TimeSpan.TicksPerMillisecond));

        var c = searchService.interpolationSearch(search, arr);

        Console.WriteLine("Интерполяционный поиск");
        Console.WriteLine("data=>" + c.data);
        Console.WriteLine("position=>" + c.position);
        Console.WriteLine("itterCount=>" + c.itterCount);
        Console.WriteLine("time=>" + (c.workTimeCount / TimeSpan.TicksPerMillisecond));

        string result = "";
        Console.WriteLine("Making API Call...");
        using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
        {
            client.BaseAddress = new Uri("http://donnu.ru/");
            HttpResponseMessage response = client.GetAsync("").Result;
            response.EnsureSuccessStatusCode();

            result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Result: " + result);
        }
        Console.ReadLine();


        var d = searchService.FindSubstring("body", result);


        Console.WriteLine("position=>" + d.position);
        Console.WriteLine("itterCount=>" + d.itterCount);
        Console.WriteLine("time=>" + (d.workTimeCount / TimeSpan.TicksPerMillisecond));



        var f = searchService.boyerMooreHorsepool("body", result);

        f.arr.ToList().ForEach(item =>
        {
            Console.WriteLine("position=>" + item);
        });

        Console.WriteLine("itterCount=>" + f.itterCount);
        Console.WriteLine("time=>" + (f.workTimeCount / TimeSpan.TicksPerMillisecond));

    }
}