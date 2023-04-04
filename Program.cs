using RestSharp;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokegochi
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Console.WriteLine(@"                                  ,'\
                                    _.----.        ____         ,'  _\   ___    ___     ____
                                _,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.
                                \      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |
                                 \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |
                                   \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |
                                    \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |
                                     \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |
                                      \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |
                                       \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |
                                        \_.-'       |__|    `-._ |              '-.|     '-.| |   |
                                                                `'                            '-._|");
            Console.Write("Escreva seu nome: ");
            string? name = Console.ReadLine();

            Console.WriteLine("-------------------- Menu --------------------");
            Console.WriteLine($"Treinador {name}, o que você deseja fazer?");

            string menuName;
            int menuOption;
            for (menuOption = 1; menuOption <= 3; menuOption++)
            {
                switch (menuOption)
                {
                    case 1:
                        menuName = "Adotar um mascote virtual";
                        Console.WriteLine($"{menuOption} - {menuName}");
                        break;
                    case 2:
                        menuName = "Ver seus mascotes";
                        Console.WriteLine($"{menuOption} - {menuName}");
                        break;
                    case 3:
                        menuName = "Sair";
                        Console.WriteLine($"{menuOption} - {menuName}");
                        break;
                }
            }
            Console.WriteLine($"-------------------- {menuName} --------------------");
            var response = GetPokemon("Charmander");
            ShowPokemon(response);
        }
        private static RestResponse GetPokemon(string pokemonName)
        {
            var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{pokemonName}");
            RestRequest request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            return response;
        }

        static void ShowPokemon(RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Pokemon pokemon = JsonSerializer.Deserialize<Pokemon>(response.Content);

                Console.WriteLine($"Nome:{pokemon.name}");
                Console.WriteLine($"Altura:{pokemon.height}");
                Console.WriteLine($"Peso:{pokemon.weight}");
                Console.WriteLine($"Habilidades:");
                List<AbilityPokemon> abilities = new();
                abilities.AddRange(pokemon.abilities);
                abilities.ForEach(a =>
                {
                    Console.WriteLine(a.ability.name);
                });
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
            }
            Console.ReadKey();
        }
    }

    

    public class Pokemon
    {
        public List<AbilityPokemon> abilities { get; set; }
        public int id { get; set; }
        public string? name { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
    }

    public class Ability
    {
        public string name { get; set; }
        public string url { get; set; }
    }
    public class AbilityPokemon
    {
        public bool is_hidden { get; set; }
        public int slot { get; set; }
        public Ability ability { get; set; }
    }
}