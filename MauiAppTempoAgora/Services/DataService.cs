using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
    
namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "73630de248be4aa05904a613bba4be3c";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient()) 
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if(resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    t = new()
                    {
                        lon = (double)rascunho["coord"]["lon"],
                        lat = (double)rascunho["coord"]["lat"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp = (double)rascunho["main"]["temp"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        feels_like = (double)rascunho["main"]["feels_like"],
                        visibility = (int?)rascunho["visibility"],
                        speed = (double)rascunho["wind"]["speed"]
                    }; //Fecha obj tempo.
                } //Fecha if se status do servidor foi de sucesso
            } //Fecha laço using
            
            return t;
        }
    }
}
