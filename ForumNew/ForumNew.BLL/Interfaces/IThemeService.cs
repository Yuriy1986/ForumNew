using System;
using System.Collections.Generic;
using ForumNew.BLL.DTO;
using ForumNew.BLL.Infrastructure;


namespace ForumNew.BLL.Interfaces
{
    public interface IThemeService : IDisposable
    {
        IEnumerable<DTOThemeViewModel> GetAllThemes();

        void CreateTheme(DTOCreateThemeViewModel dtoCreateThemeViewModel);

        void DeleteTheme(int id);
    }
}
