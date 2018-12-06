using System;
using HeroLabExportToPdf.Entities;

namespace HeroLabExportToPdf.Business
{
    public class BaseCharacterService<T> :ICharacterService<T> where T : BaseCharacter, new()
    {
        public T Character { get; private set; }

        protected BaseCharacterService(string filePath)
        {
            throw new NotImplementedException();
        }

        protected BaseCharacterService()
        {
            Character = new T();
        }

        
    }
}