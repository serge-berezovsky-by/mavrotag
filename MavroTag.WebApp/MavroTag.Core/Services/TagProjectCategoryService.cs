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
    public class TagProjectCategoryService : ITagProjectCategoryService
    {
        private readonly ITagProjectCategoryRepository _tagProjectCategoryRepository;

        public TagProjectCategoryService(ITagProjectCategoryRepository tagProjectCategoryRepository)
        {
            _tagProjectCategoryRepository = tagProjectCategoryRepository;
        }

        public IEnumerable<TagProjectCategory> GetAll()
        {
            return _tagProjectCategoryRepository.GetAll().AsEnumerable();
        }

        public TagProjectCategory GetById(long id)
        {
            var tagProject = _tagProjectCategoryRepository.GetAll().FirstOrDefault(c => c.Id == id);
            return tagProject;
        }

        public void Update(TagProjectCategory tagProjectCategory)
        {
            _tagProjectCategoryRepository.Update(tagProjectCategory);
        }

        public TagProjectCategory Add(TagProjectCategory tagProjectCategory)
        {
            return _tagProjectCategoryRepository.Insert(tagProjectCategory);
        }

        public void Delete(long id)
        {
            _tagProjectCategoryRepository.Delete(id);
        }
    }
}
