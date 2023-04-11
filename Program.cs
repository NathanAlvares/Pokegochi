using RestSharp;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokegochi
{
    public class Program
    {

        static void Main(string[] args)
        {
            MainMenu();
            int mainMenuOption = int.Parse(Console.ReadLine());

            switch (mainMenuOption)
            {
                case 1:
                    AdoptMenu();
                    break;
                case 2:

                    break;
                case 3:
                    Console.WriteLine("Até a próxima!");
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Não há opção válida, tente novamente.");
                    MainMenu();
                    break;
            }

        }
        private static RestResponse GetPokemon(string pokemonName)
        {
            var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}");
            RestRequest request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            return response;
        }

        static void ShowPokemon(RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Pokemon pokemon = JsonSerializer.Deserialize<Pokemon>(response.Content);

                Console.WriteLine($"Nome:{pokemon.name.ToUpper()}");
                Console.WriteLine($"Altura:{pokemon.height}");
                Console.WriteLine($"Peso:{pokemon.weight}");
                Console.WriteLine($"Habilidades:");
                List<AbilityPokemon> abilities = new();
                abilities.AddRange(pokemon.abilities);
                abilities.ForEach(a =>
                {
                    Console.WriteLine(a.ability.name.ToUpper());
                });
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
            }
        }

        public static void MainMenu()
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
            Console.WriteLine($"1 - Adotar um mascote");
            Console.WriteLine($"2 - Ver seus mascotes");
            Console.WriteLine($"3 - Sair");
        }
        public static void AdoptMenu()
        {
            Console.WriteLine("-------------------- ADOTAR UM MASCOTE --------------------");
            Console.Write($"Escolha, pelo nome, um pokémon para adotar e ver suas informações: ");
            string pokemon = Console.ReadLine();
            var response = GetPokemon(pokemon);
            ShowPokemon(response);

            Console.WriteLine("Deseja adotar este Pokémon? (s/n)");
            char repeatOption = char.Parse(Console.ReadLine().ToUpper());
            while( repeatOption == 'N')
            {
                AdoptMenu();
            }
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