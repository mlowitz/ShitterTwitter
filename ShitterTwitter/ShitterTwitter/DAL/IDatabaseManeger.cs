﻿using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using ShitterTwitter.Common.Objects;

namespace ShitterTwitter.DAL
{
    public interface IDatabaseManeger
    {
        void AddMessage(IShitterTwitterMessage document);
        List<IShitterTwitterMessage> GetAllShitterMessages();


    }
}