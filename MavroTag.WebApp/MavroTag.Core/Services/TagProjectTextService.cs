using MavroTag.Core.Domain;
using MavroTag.Core.Interfaces;
using MavroTag.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Services
{
    public class TagProjectTextService : ITagProjectTextService
    {
        private readonly ITagProjectTextRepository _tagProjectTextRepository;

        public TagProjectTextService(ITagProjectTextRepository tagProjectTextRepository)
        {
            _tagProjectTextRepository = tagProjectTextRepository;
        }

        public IEnumerable<TagProjectText> GetAll()
        {
            return _tagProjectTextRepository.GetAll().AsEnumerable();
        }

        public IEnumerable<TagProjectText> GetByCategoryId(long tagProjectCategoryId)
        {
            return _tagProjectTextRepository.GetAll().Where(c => c.TagProjectCategoryId == tagProjectCategoryId).AsEnumerable();
        }

        public TagProjectText GetById(long id)
        {
            var tagProjectText = _tagProjectTextRepository.GetAll().FirstOrDefault(c => c.Id == id);
            return tagProjectText;
        }

        public void Update(TagProjectText tagProjectText)
        {
            _tagProjectTextRepository.Update(tagProjectText);
        }

        public TagProjectText Add(TagProjectText tagProjectText)
        {
            return _tagProjectTextRepository.Insert(tagProjectText);
        }

        public void Delete(long id)
        {
            _tagProjectTextRepository.Delete(id);
        }
    }
}
