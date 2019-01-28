using ForumNew.BLL.DTO;
using ForumNew.DAL.Entities;
using ForumNew.BLL.Interfaces;
using ForumNew.DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace ForumNew.BLL.Services
{
    public class ThemeService : IThemeService
    {
        IUnitOfWork Database { get; set; }

        public ThemeService(IUnitOfWork uow)
        {
            Database = uow;
        }
        

        public IEnumerable<DTOThemeViewModel> GetAllThemes()
        {
            return Mapper.Map<IEnumerable<Theme>, IEnumerable<DTOThemeViewModel>>(Database.Themes.GetAllThemes());
        }

        public void CreateTheme(DTOCreateThemeViewModel dtoCreateThemeViewModel)
        {
            var theme = Mapper.Map<Theme>(dtoCreateThemeViewModel);
            Database.Themes.CreateTheme(theme);
        }

        public void DeleteTheme(int id)
        {
            Database.Themes.DeleteTheme(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
