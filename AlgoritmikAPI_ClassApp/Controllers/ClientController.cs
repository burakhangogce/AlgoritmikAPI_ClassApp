using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _IClient;

        public ClientController(IClient IClient)
        {
            _IClient = IClient;
        }

      

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientModel>> Get(int id)
        {
            ClientModel diet = await Task.FromResult(_IClient.GetClient(id));

            if (diet == null)
            {
                return NotFound();
            }
            return diet;
        }


        [HttpGet("myclients/{id}")]
        public async Task<ActionResult<IEnumerable<ClientModel>>> GetMyClients(int id)
        {
            List<ClientModel> myClients = await Task.FromResult(_IClient.GetMyClients(id));

            if (myClients == null)
            {
                return NotFound();
            }
            return myClients;
        }




        [HttpPut("update/{id}")]
        public async Task<ActionResult<ClientModel>> Put(int id, ClientModel clientModel)
        {
            if (id != clientModel.clientId)
            {
                return BadRequest();
            }
            try
            {
                _IClient.UpdateClient(clientModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(clientModel);
        }

        private bool ClientExists(int id)
        {
            return _IClient.CheckClient(id);
        }
    }
}
