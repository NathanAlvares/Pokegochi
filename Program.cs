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
            InvocarGet();
        }
        private static void InvocarGet()
        {

            var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/3");
            RestRequest request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

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