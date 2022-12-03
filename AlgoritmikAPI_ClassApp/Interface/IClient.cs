using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IClient
    {
        public ClientModel GetClient(int id);
        public void UpdateClient(ClientModel clientModel);
        public bool CheckClient(int id);
        public List<ClientModel> GetMyClients(int id);

    }
}
