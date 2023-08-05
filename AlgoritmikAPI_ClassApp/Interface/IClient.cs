using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IClient
    {
        public ClientModel GetClient(int id);
        public ClientModel GetClientWithUserId(int id);
        public void UpdateClient(ClientModel clientModel);
        public bool CheckClient(int id);
        public void AddClient(ClientModel clientModel);
        public List<ClientModel> GetMyClients(int id);

    }
}
