using ForumNew.DAL.Entities;
using System;
using System.Collections.Generic;

namespace ForumNew.DAL.Interfaces
{
    public interface IRepositoryThemes
    {
        IEnumerable<Theme> GetAllThemes();

        void CreateTheme(Theme theme);

        void DeleteTheme(int id);
    }
}
