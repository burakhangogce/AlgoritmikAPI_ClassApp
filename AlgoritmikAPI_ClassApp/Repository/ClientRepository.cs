﻿using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class ClientRepository : IClient
    {
        readonly DatabaseContext _dbContext = new();

        public ClientRepository(DatabaseContext dbContext)
        { 
            _dbContext = dbContext;
        }
       

        public ClientModel GetClient(int id)
        {
            try
            {
                ClientModel? clientModel = _dbContext.Client!.Find(id);
                if (clientModel != null)
                {
                    
                    return clientModel;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateClient(ClientModel clientModel)
        {
            try
            {
                _dbContext.Client!.Update(clientModel);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public bool CheckClient(int id)
        {
            return _dbContext.Client.Any(e => e.clientId == id);
        }

       
    }
}
