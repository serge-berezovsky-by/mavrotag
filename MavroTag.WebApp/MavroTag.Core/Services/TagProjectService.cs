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
    public class TagProjectService : ITagProjectService
    {
        private readonly ITagProjectRepository _tagProjectRepository;

        public TagProjectService(ITagProjectRepository tagProjectRepository)
        {
            _tagProjectRepository = tagProjectRepository;
        }

        public IEnumerable<TagProject> GetAll()
        {
            return _tagProjectRepository.GetAll().AsEnumerable();
        }

        public TagProject GetById(long id)
        {
            var tagProject = _tagProjectRepository.GetAll().FirstOrDefault(c => c.Id == id);
            return tagProject;
        }

        public void Update(TagProject tagProject)
        {
            _tagProjectRepository.Update(tagProject);
        }

        public TagProject Add(TagProject tagProject)
        {
            return _tagProjectRepository.Insert(tagProject);
        }

        public void Delete(long id)
        {
            _tagProjectRepository.Delete(id);
        }
    }
}
