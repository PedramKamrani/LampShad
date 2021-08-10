using _0_FrameWork.BaseClass;
using Comment.Managment.Cantract.Comment;
using Commentmanagment.Domain.CommentAgg;
using System.Collections.Generic;

namespace CommentManagment.Application
{
    public class CommentApplication : ICommentApplication
    {
        OperationResult opration = new OperationResult();
        private readonly ICommentRepository _repository;
        public CommentApplication(ICommentRepository repository)
        {
            _repository = repository;
        }
        public OperationResult Add(AddComment command)
        {
           
            var comment = new Commentmanagment.Domain.CommentAgg.Comment(command.Name, command.Email, command.Website, command.Message,
                command.OwnerRecordId, command.Type, command.ParentId);
            
            _repository.Create(comment);
            _repository.SaveChanges();
            return opration.Succedded();
        }

        public OperationResult Cancel(long id)
        {
            var comment = _repository.Get(id);
            if (comment == null)
                return opration.Failed(ApplicationMessages.RecordNotFound);
             comment.Cancel();
            _repository.SaveChanges();
            return opration.Succedded();

        }

        public OperationResult Confirm(long id)
        {
            var comment = _repository.Get(id);
            if (comment == null)
                return opration.Failed(ApplicationMessages.RecordNotFound);
            comment.Confirm();
            _repository.SaveChanges();
            return opration.Succedded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
