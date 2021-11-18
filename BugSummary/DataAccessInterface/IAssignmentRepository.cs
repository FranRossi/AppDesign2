using System;
using System.Collections.Generic;
using Domain;

namespace DataAccessInterface
{
    public interface IAssignmentRepository
    {
        void Add( Assignment assignment);
        void Save();

    }
}