using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

public class Program
{
    private static HttpClient? HttpClient;
    public static void Main()
    {

        var match1 = new Match("Paris Saint-Germain", 2013);

        Output(match1);

        var match2 = new Match("Chelsea", 2014);
        
        Output(match2);

        //**************************************************
        // OBSERVAÇÃO DO DESENVOLVEDOR
        // AS CLASSES FORAM GERADAS APENAS PARA FUNCIONAR O DESAFIO, EM UM AMBIENTE REAL ELAS SERIAM MELHOR ESCRITAS, OS MÉTODOS MELHORES E MAIS EFICAZES,
        // TERIAMOS TESTES UNITÁRIOS, CONSUMO DA API COM O POLLY, ETC -  Vários critérios importantes foram omitidos apenas pelo tempo para se desenvolver todas as etapas do desafio.

        // OS RESULTADOS DO 'SCORE' DO 'OUTPUT EXPECTEC' DIFEREM DO RESULTADO RETORNADO DA API
        // PARA O Paris Saint - Germain SAO ESPERADOS 109 GOLS PORÉM NA API EXISTEM APENAS 62
        // PARA O Chelsea SAO ESPERADOS 92 GOLS PORÉM NA API EXISTEM APENAS 47
        //**************************************************



        //string teamName = "Paris Saint-Germain";
        //int year = 2013;
        //int totalGoals = getTotalScoredGoals(teamName, year);

        //Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        //teamName = "Chelsea";
        //year = 2014;
        //totalGoals = getTotalScoredGoals(teamName, year);

        //Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);


        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    private static void Output(Match result)
    {
        Console.WriteLine($"Team: {result.TeamName} | scored: {result.totalGoals}  goals in {result.Year}");

    }

    private static HttpClient GetHttpClient()
    {

        return HttpClient ?? CriarHttpClient();
    }

    private static HttpClient CriarHttpClient()
    {

        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();

        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

        var client = httpClientFactory?.CreateClient();

        client.BaseAddress = new Uri("https://jsonmock.hackerrank.com/api/football_matches");

        return client;
    }




    public static async Task<int> GetTotalScoredGoals(string team, int year)
    {
        var httpClient = GetHttpClient();


        var scores = new List<Data>();
        var nextPage = 0;
        
        var hasMorePages = true;

        do
        {
            nextPage++;

            var baseUrl = $"?year={year}&team1={team}&page={nextPage}";

            await httpClient.GetAsync(baseUrl)
                .ContinueWith(async (jobSearchTask) =>
                {
                    var response = await jobSearchTask;
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Root>(jsonString);
                        if (result != null)
                        {

                            if (result.data.Any())
                            {
                                scores.AddRange(result.data.ToList());
                            }

                            
                            if(result.page == result.total_pages)
                            {
                                hasMorePages = false;
                            }

                        }
                    }
                    else
                    {
                        hasMorePages = false;
                    }
                });

        } while (hasMorePages);


        return scores.Sum(x => Convert.ToInt32(x.team1goals));

        
    }




    public class Match
    {
        public string TeamName { get; init; }
        public int Year { get; init; }
        public int totalGoals { get; set; }

        public Match(string teamName, int year)
        {
            TeamName = teamName;
            Year = year;
            totalGoals = Task.Run(async () => await GetTotalScoredGoals(TeamName, Year)).Result ;
        }

    }
    public class Root
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Data> data { get; set; }
    }

    public class Data
    {
        public string competition { get; set; }
        public int year { get; set; }
        public string round { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string team1goals { get; set; }
        public string team2goals { get; set; }
    }
}
