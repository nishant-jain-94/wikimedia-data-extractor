using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WikimediaDataExtractor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            string SPARQL_QUERY = Uri.EscapeDataString(@"
            SELECT ?s ?sLabel ?BirthPlace ?PlaceLabel ?sfamilynameLabel ?cordinate
            WHERE{
            ?s wdt:P106 wd:Q12299841 .
            ?s wdt:P569 ?BirthPlace .
            ?s wdt:P19 ?Place .
            ?s wdt:P734 ?sfamilyname .
            ?Place wdt:P625 ?cordinate
                SERVICE wikibase:label{
                    bd:serviceParam wikibase:language 'en'
                }
            }");
            HttpResponseMessage responseMessage = 
            await httpClient.GetAsync($"https://query.wikidata.org/sparql?query={SPARQL_QUERY}");
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            Console.ReadLine();
        }

    }
}
