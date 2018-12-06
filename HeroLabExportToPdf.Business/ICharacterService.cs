using HeroLabExportToPdf.Entities;

namespace HeroLabExportToPdf.Business
{
    public interface ICharacterService<out T> where T: BaseCharacter
    {
        T Character { get; }
    }
}
