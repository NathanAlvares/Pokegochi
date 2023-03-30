using RestSharp;
using System.ComponentModel;
using System.Text.Json;


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

            var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/");
            RestRequest request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Pokemon pokemon = new()
                {
                    Id = JsonSerializer.Deserialize<Pokemon>(response.Content).Id,
                    Name = JsonSerializer.Deserialize<Pokemon>(response.Content).Name,
                    Height = JsonSerializer.Deserialize<Pokemon>(response.Content).Height,
                    Weight = JsonSerializer.Deserialize<Pokemon>(response.Content).Weight,
                };

                Console.WriteLine($"Nome:{pokemon.Name}");
                Console.WriteLine($"Altura:{pokemon.Height}");
                Console.WriteLine($"Peso:{pokemon.Weight}");
                Console.WriteLine($"Habilidades:");
                //List<Ability> abilities = new();
                //abilities.AddRange(JsonSerializer.Deserialize<Pokemon>(response.Content).Abilities);
                //abilities.ForEach(a =>
                //{
                //    Console.WriteLine(a.Name);
                //});
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
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<Ability> Abilities { get; set; }
    }

    public class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AbilityPokemon> Pokemons { get; set; }
    }
    public class AbilityPokemon
    {
        public bool Hidden { get; set; }
        public int Slot { get; set; }
        public Pokemon Pokemons { get; set; }
    }
}