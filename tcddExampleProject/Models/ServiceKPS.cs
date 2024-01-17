using tcddKPS;

namespace tcddExampleProject.Models
{
    public class ServiceKPS
    {
        public async Task<bool> OnGetService(Parameters parameters)
        {
            bool result = false;
            var client = new tcddKPS.KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var response = await client.TCKimlikNoDogrulaAsync(parameters.TCKimlikNo, parameters.Ad, parameters.Soyad, parameters.DogumYili);
            return result = response.Body.TCKimlikNoDogrulaResult;
        }
    }
}
